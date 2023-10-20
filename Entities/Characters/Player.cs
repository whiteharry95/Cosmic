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
    using Cosmic.Items.WorldObjects;
    using System.Diagnostics;

    public class Player : Character {
        public Item ItemCurrent => inventory.slots[UIManager.playerInventory.hotBarSlotSelectedIndex]?.item;

        public int itemUseTime;
        public float itemRotationOffset;
        public float itemRotationOffsetAxis = 1f;
        private float itemRotationOffsetSpeed = 0.5f;

        public Vector2 itemDropTossVelocity = new Vector2(4.5f, -1f);

        public float moveSpeedChange = 0.3f;
        public float moveSpeedMax = 3f;

        private float jumpSpeed = 3f;
        private int jumpTime;
        private int jumpTimeMax = 6;

        private float flySpeedChange = 0.2f;
        private float flySpeedMax = 6f;

        private float dashSpeed = 6f;
        private int dashSpeedAxis;
        private int dashTime;
        private int dashTimeMax = 12;
        private int dashBreakTime;
        private int dashBreakTimeMax = 45;

        public Inventory inventory;

        public Tiles.TileSelection[,] tileSelection;
        public Point tileSelectionSize;
        public Point tileSelectionSizeMax = new Point(5);
        public int tileSelectionRange = 16;

        public override void Init() {
            drawLayer = DrawLayer.Player;

            healthMax = 250;
            health = healthMax;

            sprite = new Sprite(TextureManager.Characters_Player, Sprite.OriginPreset.MiddleCentre);
            collider = new EntityCollider(this);

            inventory = new Inventory(PlayerInventory.RowSize * 5);
            inventory.AddItem(ItemManager.stoneBlock, 950);
            inventory.AddItem(ItemManager.stoneBlock, 51, 10);
            inventory.AddItem(ItemManager.rock, 100);
            inventory.AddItem(ItemManager.chandelier, 100);
            inventory.AddItem(ItemManager.sword, 1);
            inventory.AddItem(ItemManager.gun, 1);
            inventory.AddItem(ItemManager.copperDrill, 1);
            inventory.AddItem(ItemManager.worldTeleporter, 1);
            inventory.AddItem(ItemManager.worldBuilder, 1);
        }

        public override void Update() {
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

            if (moveSpeedAxis != 0f && dashTime == 0 && dashBreakTime == 0) {
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
                bool makeContact = false;

                while (collider.GetCollisionWithTiles(new Vector2(velocity.X, 0f), tileMapTile => (tileMapTile.textureIndex == 3 || tileMapTile.textureIndex == 4) && (position.Y - sprite.origin.Y + sprite.Size.Y - Tile.Size <= tileMapTile.y * Tile.Size))) {
                    position.Y--;
                    makeContact = true;
                }

                if (collider.GetCollisionWithTiles(new Vector2(velocity.X, 0f))) {
                    collider.MakeContactWithTiles(Math.Abs(velocity.X), velocity.X >= 0f ? MathUtilities.Direction.Right : MathUtilities.Direction.Left);
                    velocity.X = 0f;
                }

                if (makeContact) {
                    collider.MakeContactWithTiles(1f, MathUtilities.Direction.Down);
                }
            }

            if (velocity.Y != 0f) {
                if (collider.GetCollisionWithTiles(new Vector2(0f, velocity.Y))) {
                    collider.MakeContactWithTiles(Math.Abs(velocity.Y), velocity.Y >= 0f ? MathUtilities.Direction.Down : MathUtilities.Direction.Up);
                    velocity.Y = 0f;
                }
            }

            base.Update();

            if (collider.GetCollisionWithEntities(out List<ItemDrop> itemDrops)) {
                foreach (ItemDrop itemDrop in itemDrops) {
                    if (itemDrop.pickupTime == 0) {
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
                    TossItemDrop(ItemCurrent, inventory.slots[UIManager.playerInventory.hotBarSlotSelectedIndex].quantity);
                    inventory.RemoveItem(ItemCurrent, inventory.slots[UIManager.playerInventory.hotBarSlotSelectedIndex].quantity, UIManager.playerInventory.hotBarSlotSelectedIndex, true);
                }
            }

            if (ItemCurrent?.showTileSelection ?? false) {
                if (InputManager.GetKeyHeld(Keys.LeftAlt)) {
                    tileSelectionSize += new Point(Convert.ToInt32(InputManager.GetMouseScrollUp()) - Convert.ToInt32(InputManager.GetMouseScrollDown()));
                }
            }

            tileSelectionSize.X = Math.Clamp(tileSelectionSize.X, 1, tileSelectionSizeMax.X);
            tileSelectionSize.Y = Math.Clamp(tileSelectionSize.Y, 1, tileSelectionSizeMax.Y);

            Point tileSelectionSizeReal = tileSelectionSize;

            if (ItemCurrent is BlockItem) {
                while (tileSelectionSizeReal.X * tileSelectionSizeReal.Y > inventory.slots[UIManager.playerInventory.hotBarSlotSelectedIndex].quantity) {
                    tileSelectionSizeReal.X = Math.Max(tileSelectionSizeReal.X - 1, 1);
                    tileSelectionSizeReal.Y = Math.Max(tileSelectionSizeReal.Y - 1, 1);
                }
            } else if (ItemCurrent is WorldObjectItem) {
                tileSelectionSizeReal.X = (int)Math.Ceiling((float)((WorldObjectItem)ItemCurrent).worldObject.sprite.texture.Width / Tile.Size);
                tileSelectionSizeReal.Y = (int)Math.Ceiling((float)((WorldObjectItem)ItemCurrent).worldObject.sprite.texture.Height / Tile.Size);
            }

            tileSelection = new Tiles.TileSelection[tileSelectionSizeReal.X, tileSelectionSizeReal.Y];

            Point tilePosition = TileMap.GetWorldToTilePosition(position + new Vector2(tileSelectionRange % 2f == 0f ? (Tile.Size / 2f) : 0f));
            Point mouseTilePosition = TileMap.GetWorldToTilePosition(InputManager.GetMouseWorldPosition() + new Vector2(tileSelectionSizeReal.X % 2f == 0f ? (Tile.Size / 2f) : 0f, tileSelectionSizeReal.Y % 2f == 0f ? (Tile.Size / 2f) : 0f));

            for(int y = 0; y < tileSelection.GetLength(1); y++) {
                for (int x = 0; x < tileSelection.GetLength(0); x++) {
                    int rx = mouseTilePosition.X - (int)Math.Floor(tileSelectionSizeReal.X / 2f) + x;
                    int ry = mouseTilePosition.Y - (int)Math.Floor(tileSelectionSizeReal.Y / 2f) + y;

                    if (rx >= tilePosition.X - (int)Math.Floor(tileSelectionRange / 2f) && ry >= tilePosition.Y - (int)Math.Floor(tileSelectionRange / 2f) && rx <= tilePosition.X + Math.Floor(tileSelectionRange / 2f) && ry <= tilePosition.Y + Math.Floor(tileSelectionRange / 2f)) {
                        tileSelection[x, y] = new Tiles.TileSelection();
                    }
                }
            }

            /*if (ItemCurrent is WorldObjectItem) {
                if (tileSelection.GetLength(0) * tileSelection.GetLength(1) != tileSelectionSizeReal.X * tileSelectionSizeReal.Y) {
                    tileSelection = null;
                }
            } else {
                foreach(Tiles.TileSelection tileSelection in tileSelection) {
                    
                }
            }*/

            if (itemUseTime > 0) {
                itemUseTime--;
            } else {
                if (ItemCurrent != null && !UIManager.playerInventory.open) {
                    if (ItemCurrent.useHold ? InputManager.GetMouseLeftHeld() : InputManager.GetMouseLeftPressed()) {
                        itemUseTime = ItemCurrent.useTime;
                        ItemCurrent.OnPrimaryUse();
                    } else if (ItemCurrent.useHold ? InputManager.GetMouseRightHeld() : InputManager.GetMouseRightPressed()) {
                        itemUseTime = ItemCurrent.useTime;
                        ItemCurrent.OnSecondaryUse();
                    }
                }
            }

            itemRotationOffset = ((ItemCurrent?.holdRotationOffset ?? 0f) == 0f) ? 0f : itemRotationOffset + (((ItemCurrent.holdRotationOffset * itemRotationOffsetAxis) - itemRotationOffset) * itemRotationOffsetSpeed);
        }

        public override void Draw() {
            base.Draw();

            Vector2 mousePosition = InputManager.GetMouseWorldPosition();
            float mouseDirection = MathF.Atan2(mousePosition.Y - position.Y, mousePosition.X - position.X);

            ItemCurrent?.sprite?.Draw(position + (ItemCurrent.holdLengthOffset * new Vector2(MathF.Cos(mouseDirection + itemRotationOffset), MathF.Sin(mouseDirection + itemRotationOffset))), mouseDirection + itemRotationOffset, flip: mousePosition.X < position.X ? SpriteEffects.FlipVertically : SpriteEffects.None, origin: ItemCurrent.sprite.origin);
        }

        public override bool Hurt(int damage, Vector2? force = null) {
            bool hurt = base.Hurt(damage, force);

            if (hurt) {
                if (force != null) {
                    jumpTime = 0;
                    dashTime = 0;
                }
            }

            return hurt;
        }

        public void TossItemDrop(Item item, int quantity) {
            EntityManager.AddEntity<ItemDrop>(position, world, itemDrop => {
                itemDrop.flip = InputManager.GetMouseWorldPosition().X < position.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                itemDrop.item = item;
                itemDrop.quantity = quantity;

                itemDrop.velocity = itemDropTossVelocity * new Vector2(itemDrop.flip.HasFlag(SpriteEffects.FlipHorizontally) ? -1f : 1f, 1f);

                itemDrop.pickupTime = itemDrop.pickupTimeMax;
            });
        }
    }
}