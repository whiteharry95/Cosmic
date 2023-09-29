namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Cosmic.Entities;
    using Cosmic.Utilities;
    using System;

    public class PlayerInventory : UIElement {
        public bool open;

        public Point slotSelected;
        public int hotbarSlotSelected;

        private Sprite inventorySlotSprite = new Sprite(AssetManager.inventorySlot, new Vector2(AssetManager.inventorySlot.Width, AssetManager.inventorySlot.Height) / 2f);

        public override void Update(GameTime gameTime) {
            if (!(EntityManager.player?.GetExists() ?? false)) {
                return;
            }

            hotbarSlotSelected += Convert.ToInt32(InputManager.GetMouseScrollDown()) - Convert.ToInt32(InputManager.GetMouseScrollUp());
            hotbarSlotSelected %= EntityManager.player.inventory.Width;

            if (hotbarSlotSelected < 0) {
                hotbarSlotSelected += EntityManager.player.inventory.Width;
            }

            if (InputManager.GetKeyPressed(Keys.Tab)) {
                open = !open;
            }

            slotSelected = new Point(-1, -1);

            if (open) {
                for (int y = 0; y < EntityManager.player.inventory.Height; y++) {
                    for (int x = 0; x < EntityManager.player.inventory.Width; x++) {
                        if (new Box(GetSlotPosition(x, y) - inventorySlotSprite.origin, inventorySlotSprite.Size.ToVector2()).GetContains(InputManager.mouseState.Position.ToVector2())) {
                            slotSelected = new Point(x, y);
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime) {
            if (!(EntityManager.player?.GetExists() ?? false)) {
                return;
            }

            for (int y = 0; y < EntityManager.player.inventory.Height; y++) {
                if (!open && y > 0) {
                    break;
                }

                for (int x = 0; x < EntityManager.player.inventory.Width; x++) {
                    Vector2 slotPosition = GetSlotPosition(x, y);

                    inventorySlotSprite.Draw(slotPosition, 0f, 1f, 1f);
                    DrawUtilities.DrawString(AssetManager.arial, new DrawUtilities.Text((EntityManager.player.inventory.slots[x, y]?.item.id ?? -1).ToString()), slotPosition, (slotSelected.X == x && slotSelected.Y == y) ? Color.Yellow : ((x == EntityManager.player.inventoryHotbarSlotSelected && y == 0) ? Color.Blue : Color.White), DrawUtilities.AlignmentHor.Middle, DrawUtilities.AlignmentVer.Middle);
                }
            }

            if (EntityManager.player.inventory.slots[EntityManager.player.inventoryHotbarSlotSelected, 0] != null) {
                Vector2 itemTextPosition = GetHotbarPosition() - new Vector2(0f, 72f);
                string itemText = EntityManager.player.inventory.slots[EntityManager.player.inventoryHotbarSlotSelected, 0].item.name;

                if (EntityManager.player.inventory.slots[EntityManager.player.inventoryHotbarSlotSelected, 0].quantity > 1) {
                    itemText += $", {EntityManager.player.inventory.slots[EntityManager.player.inventoryHotbarSlotSelected, 0].quantity}x";
                }

                DrawUtilities.DrawString(AssetManager.arial, new DrawUtilities.Text(itemText), itemTextPosition, Color.White, DrawUtilities.AlignmentHor.Middle, DrawUtilities.AlignmentVer.Middle);
            }
        }

        private Vector2 GetHotbarPosition() {
            return new Vector2(Game1.graphicsDeviceManager.PreferredBackBufferWidth / 2f, (Game1.graphicsDeviceManager.PreferredBackBufferHeight / 12f) * 11f);
        }

        private Vector2 GetSlotPosition(int x, int y) {
            float slotsGap = 80f;
            Vector2 slotPosition = GetHotbarPosition() + new Vector2(slotsGap * (x - ((EntityManager.player.inventory.Width - 1f) / 2f)), 0f);

            if (y > 0) {
                slotPosition = (new Vector2(Game1.graphicsDeviceManager.PreferredBackBufferWidth, Game1.graphicsDeviceManager.PreferredBackBufferHeight) / 2f) + (new Vector2(slotsGap) * new Vector2(x - ((EntityManager.player.inventory.Width - 1f) / 2f), y - (EntityManager.player.inventory.Height / 2f)));
            }

            return slotPosition;
        }
    }
}