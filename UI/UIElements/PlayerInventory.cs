namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Cosmic.Entities;
    using Cosmic.Utilities;
    using System;
    using Cosmic.Assets;
    using Cosmic;
    using Microsoft.Xna.Framework.Graphics;
    using Cosmic.Inventory;

    public class PlayerInventory : UIElement {
        public const int RowSize = 8;

        public bool open;

        private Sprite slotSprite = new Sprite(TextureManager.UI_InventorySlot, Sprite.OriginPreset.MiddleCentre);
        private float slotBackgroundBrightness = 0.4f;
        public int slotSelectedIndex;
        public int hotBarSlotSelectedIndex;

        public override void EarlyUpdate(GameTime gameTime) {
            if (!(Game1.server.netPlayers[0]?.player?.GetExists() ?? false)) {
                return;
            }

            if (InputManager.GetKeyPressed(Keys.Tab)) {
                open = !open;
            }

            if (open) {
                slotSelectedIndex = -1;

                for (int i = 0; i < Game1.server.netPlayers[0].player.inventory.slots.Length; i++) {
                    /*if (new Box(GetSlotPosition(i) - slotSprite.origin, slotSprite.Size).GetContains(InputManager.mouseState.Position.ToVector2())) {
                        slotSelectedIndex = i;
                    }*/
                }

                if (slotSelectedIndex != -1) {
                    if (InputManager.GetMouseLeftPressed()) {
                        if (UIManager.cursor.inventorySlot != null && Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex] == null) {
                            Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex] = UIManager.cursor.inventorySlot;
                            UIManager.cursor.inventorySlot = null;
                        } else {
                            if (UIManager.cursor.inventorySlot == null && Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex] != null) {
                                UIManager.cursor.inventorySlot = Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex];
                                Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex] = null;
                            } else {
                                if (UIManager.cursor.inventorySlot != null && Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex] != null) {
                                    if (UIManager.cursor.inventorySlot.item != Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex].item || UIManager.cursor.inventorySlot.quantity == UIManager.cursor.inventorySlot.item.stack || Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex].quantity == Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex].item.stack) {
                                        InventorySlot slotSelected = Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex];

                                        Game1.server.netPlayers[0].player.inventory.slots[slotSelectedIndex] = UIManager.cursor.inventorySlot;
                                        UIManager.cursor.inventorySlot = slotSelected;
                                    } else {
                                        int quantity = Game1.server.netPlayers[0].player.inventory.AddItem(UIManager.cursor.inventorySlot.item, UIManager.cursor.inventorySlot.quantity, slotSelectedIndex, true);

                                        if (quantity > 0) {
                                            UIManager.cursor.inventorySlot.quantity = quantity;
                                        } else {
                                            UIManager.cursor.inventorySlot = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } else {
                hotBarSlotSelectedIndex += Convert.ToInt32(InputManager.GetMouseScrollDown()) - Convert.ToInt32(InputManager.GetMouseScrollUp());
                hotBarSlotSelectedIndex %= RowSize;

                if (hotBarSlotSelectedIndex < 0) {
                    hotBarSlotSelectedIndex += RowSize;
                }
            }
        }

        public override void Update(GameTime gameTime) {
        }

        public override void Draw(GameTime gameTime) {
            if (!(Game1.server.netPlayers[0]?.player?.GetExists() ?? false)) {
                return;
            }

            for (int i = 0; i < Game1.server.netPlayers[0].player.inventory.slots.Length; i++) {
                if (i >= RowSize && !open) {
                    break;
                }

                Vector2 slotPosition = GetSlotPosition(i);
                Color slotColour = (open ? slotSelectedIndex == i : hotBarSlotSelectedIndex == i) ? Color.Yellow : Color.White;

                Game1.spriteBatch.Draw(TextureManager.Pixel, slotPosition - slotSprite.origin, null, Color.Black * slotBackgroundBrightness, 0f, Vector2.Zero, slotSprite.Size, SpriteEffects.None, 0f);

                slotSprite.Draw(slotPosition, colour: slotColour);

                Game1.server.netPlayers[0].player.inventory.slots[i]?.item.sprite.Draw(slotPosition, Game1.server.netPlayers[0].player.inventory.slots[i].item.displayRotation, origin: Game1.server.netPlayers[0].player.inventory.slots[i].item.sprite.Size / 2f);
            }

            if (!open) {
                if (Game1.server.netPlayers[0].player.inventory.slots[hotBarSlotSelectedIndex] != null) {
                    Vector2 itemTextPosition = GetHotBarPosition() - new Vector2(0f, 72f);
                    string itemText = Game1.server.netPlayers[0].player.inventory.slots[hotBarSlotSelectedIndex].item.name;

                    if (Game1.server.netPlayers[0].player.inventory.slots[hotBarSlotSelectedIndex].quantity > 1) {
                        itemText += $" ({Game1.server.netPlayers[0].player.inventory.slots[hotBarSlotSelectedIndex].quantity})";
                    }

                    DrawUtilities.DrawText(FontManager.ArialMedium, itemText, itemTextPosition, Color.White, DrawUtilities.HorizontalAlignment.Centre, DrawUtilities.VerticalAlignment.Middle);
                }
            }
        }

        private Vector2 GetSlotPosition(int i) {
            int x = i % RowSize;
            int y = i / RowSize;

            float slotsGap = 80f;
            Vector2 slotPosition = GetHotBarPosition() + new Vector2(slotsGap * (x - ((RowSize - 1f) / 2f)), 0f);

            if (y > 0) {
                slotPosition = (new Vector2(Game1.graphicsDeviceManager.PreferredBackBufferWidth, Game1.graphicsDeviceManager.PreferredBackBufferHeight) / 2f) + (new Vector2(slotsGap) * new Vector2(x - ((RowSize - 1f) / 2f), y - ((Game1.server.netPlayers[0].player.inventory.slots.Length / RowSize) / 2f)));
            }

            return slotPosition;
        }

        private Vector2 GetHotBarPosition() {
            return new Vector2(Game1.graphicsDeviceManager.PreferredBackBufferWidth / 2f, (Game1.graphicsDeviceManager.PreferredBackBufferHeight / 12f) * 11f);
        }
    }
}