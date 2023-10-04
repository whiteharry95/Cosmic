namespace Cosmic.Entities.Characters {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Cosmic;
    using Cosmic.Entities;
    using Cosmic.Items;
    using Cosmic.Items.Blocks;
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;
    using Cosmic.UI;
    using Cosmic.Inventory;
    using Cosmic.TileMap;
    using Cosmic.UI.UIElements;
    using Cosmic.Assets;
    using Cosmic.Utilities;
    using Microsoft.Xna.Framework.Graphics;

    public class Player : Character {
        public Item ItemCurrent => inventory.slots[UIManager.playerInventory.hotBarSlotSelectedIndex]?.item;

        public int itemUseTime;
        public float itemRotationOffset;
        public float itemRotationOffsetAxis = 1f;
        private float itemRotationOffsetSpeed = 0.5f;

        public float itemDropThrowSpeed = 8f;

        public float moveSpeedChange = 0.6f;
        public float moveSpeedMax = 6f;

        private float jumpSpeed = 6f;
        private int jumpTime;
        private int jumpTimeMax = 12;

        private float flySpeedChange = 0.4f;
        private float flySpeedMax = 12f;

        private float dashSpeed = 12f;
        private int dashSpeedAxis;
        private int dashTime;
        private int dashTimeMax = 12;
        private int dashBreakTime;
        private int dashBreakTimeMax = 45;

        public Inventory inventory;

        public List<Point> tileSelection = new List<Point>();
        public int tileSelectionSize = 4;
        public int tileSelectionRange = 16;
        public bool tileSelectionWalls;

        public override void Init() {
            drawLayer = DrawLayer.Player;

            healthMax = 100;
            health = healthMax;

            sprite = new Sprite(TextureManager.Characters_Player, Sprite.OriginPreset.MiddleCentre);
            collider = new EntityCollider(this, new Box(-sprite.origin, sprite.Size));

            inventory = new Inventory(PlayerInventory.RowSize * 5);
            inventory.AddItem(ItemManager.stoneBlock, 950);
            inventory.AddItem(ItemManager.stoneBlock, 51, 10);
            inventory.AddItem(ItemManager.sword, 1);
            inventory.AddItem(ItemManager.gun, 1);
            inventory.AddItem(ItemManager.miner, 1);
        }

        public override void Update(GameTime gameTime) {
            bool canFly = true;
            bool keyHeldSpace = InputManager.GetKeyHeld(Keys.Space);

            int moveSpeedAxis = Convert.ToInt32(InputManager.GetKeyHeld(Keys.D)) - Convert.ToInt32(InputManager.GetKeyHeld(Keys.A));
            float horizontalSpeedDest = moveSpeedMax * moveSpeedAxis;

            velocity.X += Math.Min(moveSpeedChange, Math.Abs(horizontalSpeedDest - velocity.X)) * Math.Sign(horizontalSpeedDest - velocity.X);

            bool fly = canFly && keyHeldSpace;
            float verticalSpeedDest = fly ? -flySpeedMax : world.fallSpeedMax;

            velocity.Y += Math.Min(fly ? flySpeedChange : world.fallSpeed, Math.Abs(verticalSpeedDest - velocity.Y)) * Math.Sign(verticalSpeedDest - velocity.Y);

            if (!canFly) {
                if (InputManager.GetKeyPressed(Keys.Space)) {
                    if (jumpTime == 0) {
                        if (collider.GetCollisionWithTiles(new Vector2(0f, 0.5f))) {
                            jumpTime = jumpTimeMax;
                        }
                    }
                }

                if (!keyHeldSpace) {
                    jumpTime = 0;
                }

                if (jumpTime > 0) {
                    velocity.Y = -jumpSpeed;
                    jumpTime--;
                }
            } else {
                jumpTime = 0;
            }

            if (moveSpeedAxis != 0f && dashTime <= 0 && dashBreakTime <= 0) {
                if (InputManager.GetKeyPressed(Keys.LeftShift)) {
                    dashTime = dashTimeMax;
                    dashBreakTime = dashBreakTimeMax;

                    dashSpeedAxis = moveSpeedAxis;
                }
            }

            if (!InputManager.GetKeyHeld(Keys.LeftShift)) {
                dashTime = 0;
            }

            if (dashTime > 0) {
                dashTime--;

                velocity.X = dashSpeed * dashSpeedAxis;
                velocity.Y = 0f;
            }

            if (dashBreakTime > 0) {
                dashBreakTime--;
            }

            if (velocity.X != 0f) {
                if (collider.GetCollisionWithTiles(new Vector2(velocity.X, 0f))) {
                    collider.MakeContactWithTiles(Math.Abs(velocity.X), velocity.X >= 0f ? 0 : 2);
                    velocity.X = 0f;
                }
            }

            if (collider.GetCollisionWithTiles(new Vector2(0f, velocity.Y))) {
                collider.MakeContactWithTiles(Math.Abs(velocity.Y), velocity.Y >= 0f ? 3 : 1);
                velocity.Y = 0f;
            }

            if (collider.GetCollisionWithTiles(velocity)) {
                velocity.X = 0f;
            }

            base.Update(gameTime);

            if (collider.GetCollisionWithEntities(out List<ItemDrop> itemDrops)) {
                foreach (ItemDrop itemDrop in itemDrops) {
                    if (itemDrop.pickupTime <= 0) {
                        int quantity = inventory.AddItem(itemDrop.item, itemDrop.quantity);

                        if (quantity > 0) {
                            itemDrop.quantity = quantity;
                        } else {
                            itemDrop.Destroy();
                        }
                    }
                }
            }

            if (inventory.slots[UIManager.playerInventory.hotBarSlotSelectedIndex] != null) {
                if (InputManager.GetKeyPressed(Keys.Q)) {
                    EntityManager.AddEntity<ItemDrop>(position, world, itemDrop => {
                        itemDrop.item = ItemCurrent;
                        itemDrop.quantity = inventory.slots[UIManager.playerInventory.hotBarSlotSelectedIndex].quantity;

                        itemDrop.velocity = itemDropThrowSpeed * MathUtilities.NormaliseVector2(InputManager.GetMousePosition() - position);

                        itemDrop.pickupTime = itemDrop.pickupTimeMax;

                        itemDrop.flip = InputManager.GetMousePosition().X < position.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                    });

                    inventory.RemoveItem(ItemCurrent, inventory.slots[UIManager.playerInventory.hotBarSlotSelectedIndex].quantity, UIManager.playerInventory.hotBarSlotSelectedIndex, true);
                }
            }

            tileSelection.Clear();

            tileSelectionWalls = InputManager.GetKeyHeld(Keys.LeftAlt);

            int tileSelectionSizeCapped = tileSelectionSize;

            if (ItemCurrent is BlockItem) {
                while (tileSelectionSizeCapped * tileSelectionSizeCapped > inventory.slots[UIManager.playerInventory.hotBarSlotSelectedIndex].quantity) {
                    tileSelectionSizeCapped--;
                }
            }

            Point tilePosition = TileMap.GetWorldToTilePosition(position + new Vector2(tileSelectionRange % 2f == 0f ? (Tile.Size / 2f) : 0f));
            Point mouseTilePosition = TileMap.GetWorldToTilePosition(InputManager.GetMousePosition() + new Vector2(tileSelectionSizeCapped % 2f == 0f ? (Tile.Size / 2f) : 0f));

            for (int y = mouseTilePosition.Y - (int)Math.Floor(tileSelectionSizeCapped / 2f); y < mouseTilePosition.Y + Math.Ceiling(tileSelectionSizeCapped / 2f); y++) {
                for (int x = mouseTilePosition.X - (int)Math.Floor(tileSelectionSizeCapped / 2f); x < mouseTilePosition.X + Math.Ceiling(tileSelectionSizeCapped / 2f); x++) {
                    if (x >= tilePosition.X - (int)Math.Floor(tileSelectionRange / 2f) && y >= tilePosition.Y - (int)Math.Floor(tileSelectionRange / 2f) && x < tilePosition.X + Math.Ceiling(tileSelectionRange / 2f) && y < tilePosition.Y + Math.Ceiling(tileSelectionRange / 2f)) {
                        tileSelection.Add(new Point(x, y));
                    }
                }
            }

            if (itemUseTime > 0) {
                itemUseTime--;
            } else {
                if (ItemCurrent != null && !UIManager.playerInventory.open) {
                    if (ItemCurrent.useHold ? InputManager.GetMouseLeftHeld() : InputManager.GetMouseLeftPressed()) {
                        itemUseTime = ItemCurrent.useTime;
                        ItemCurrent.OnUse();
                    }
                }
            }

            itemRotationOffset = ((ItemCurrent?.holdRotationOffset ?? 0f) == 0f) ? 0f : itemRotationOffset + (((ItemCurrent.holdRotationOffset * itemRotationOffsetAxis) - itemRotationOffset) * itemRotationOffsetSpeed);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            Vector2 mousePosition = InputManager.GetMousePosition();
            float mouseDirection = (float)Math.Atan2(mousePosition.Y - position.Y, mousePosition.X - position.X);

            ItemCurrent?.sprite?.Draw(position - Camera.position + (ItemCurrent.holdLengthOffset * new Vector2((float)Math.Cos(mouseDirection + itemRotationOffset), (float)Math.Sin(mouseDirection + itemRotationOffset))), mouseDirection + itemRotationOffset);
        }

        public override bool Hurt(int damage, Vector2? force = null, Vector2? position = null) {
            bool hurt = base.Hurt(damage, force, position);

            if (hurt) {
                if (force != null) {
                    jumpTime = 0;
                    dashTime = 0;
                }
            }

            return hurt;
        }
    }
}