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

        public override void EarlyUpdate() {
            if (!(EntityManager.player?.GetExists() ?? false)) {
                return;
            }

            if (InputManager.GetKeyPressed(Keys.Tab)) {
                open = !open;

                if (!open) {
                    if (UIManager.cursor.inventorySlot != null) {
                        EntityManager.player.TossItemDrop(UIManager.cursor.inventorySlot.item, UIManager.cursor.inventorySlot.quantity);
                        UIManager.cursor.inventorySlot = null;
                    }
                }
            }

            if (open) {
                slotSelectedIndex = -1;

                for (int i = 0; i < EntityManager.player.inventory.slots.Length; i++) {
                    Polygon slotPolygon = new Polygon(() => Polygon.GetRectangleVertices(slotSprite.Size), () => GetSlotPosition(i), getScale: () => new Vector2(Camera.Scale), getOrigin: () => slotSprite.origin);

                    if (slotPolygon.GetCollisionWithPolygon(UIManager.cursor.polygon)) {
                        slotSelectedIndex = i;
                    }
                }

                if (slotSelectedIndex != -1) {
                    if (InputManager.GetMouseLeftPressed()) {
                        if (UIManager.cursor.inventorySlot != null && EntityManager.player.inventory.slots[slotSelectedIndex] == null) {
                            EntityManager.player.inventory.slots[slotSelectedIndex] = UIManager.cursor.inventorySlot;
                            UIManager.cursor.inventorySlot = null;
                        } else {
                            if (UIManager.cursor.inventorySlot == null && EntityManager.player.inventory.slots[slotSelectedIndex] != null) {
                                UIManager.cursor.inventorySlot = EntityManager.player.inventory.slots[slotSelectedIndex];
                                EntityManager.player.inventory.slots[slotSelectedIndex] = null;
                            } else {
                                if (UIManager.cursor.inventorySlot != null && EntityManager.player.inventory.slots[slotSelectedIndex] != null) {
                                    if (UIManager.cursor.inventorySlot.item != EntityManager.player.inventory.slots[slotSelectedIndex].item || UIManager.cursor.inventorySlot.quantity == UIManager.cursor.inventorySlot.item.stack || EntityManager.player.inventory.slots[slotSelectedIndex].quantity == EntityManager.player.inventory.slots[slotSelectedIndex].item.stack) {
                                        InventorySlot slotSelected = EntityManager.player.inventory.slots[slotSelectedIndex];

                                        EntityManager.player.inventory.slots[slotSelectedIndex] = UIManager.cursor.inventorySlot;
                                        UIManager.cursor.inventorySlot = slotSelected;
                                    } else {
                                        int quantity = EntityManager.player.inventory.AddItem(UIManager.cursor.inventorySlot.item, UIManager.cursor.inventorySlot.quantity, slotSelectedIndex, true);

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
                if (!InputManager.GetKeyHeld(Keys.LeftAlt) || !(EntityManager.player.inventory.slots[hotBarSlotSelectedIndex].item?.showTileSelection ?? false)) {
                    hotBarSlotSelectedIndex += Convert.ToInt32(InputManager.GetMouseScrollUp()) - Convert.ToInt32(InputManager.GetMouseScrollDown());
                    hotBarSlotSelectedIndex %= RowSize;

                    if (hotBarSlotSelectedIndex < 0) {
                        hotBarSlotSelectedIndex += RowSize;
                    }
                }
            }
        }

        public override void Update() {
        }

        public override void Draw() {
            if (!(EntityManager.player?.GetExists() ?? false)) {
                return;
            }

            for (int i = 0; i < EntityManager.player.inventory.slots.Length; i++) {
                if (i >= RowSize && !open) {
                    break;
                }

                Vector2 slotPosition = GetSlotPosition(i);
                Color slotColour = (open ? slotSelectedIndex == i : hotBarSlotSelectedIndex == i) ? Color.Yellow : Color.White;

                Game1.spriteBatch.Draw(TextureManager.Pixel, slotPosition - (slotSprite.origin * Camera.Scale), null, Color.Black * slotBackgroundBrightness, 0f, Vector2.Zero, slotSprite.Size * Camera.Scale, SpriteEffects.None, 0f);

                slotSprite.Draw(slotPosition, scale: new Vector2(Camera.Scale), colour: slotColour);

                EntityManager.player.inventory.slots[i]?.item.sprite.Draw(slotPosition, EntityManager.player.inventory.slots[i].item.displayRotation, new Vector2(Camera.Scale), origin: EntityManager.player.inventory.slots[i].item.sprite.mask.Location.ToVector2() + (EntityManager.player.inventory.slots[i].item.sprite.mask.Size.ToVector2() / 2f));
            }

            if (!open) {
                if (EntityManager.player.inventory.slots[hotBarSlotSelectedIndex] != null) {
                    Vector2 itemTextPosition = GetHotBarPosition() - new Vector2(0f, 72f);
                    string itemText = EntityManager.player.inventory.slots[hotBarSlotSelectedIndex].item.name;

                    if (EntityManager.player.inventory.slots[hotBarSlotSelectedIndex].quantity > 1) {
                        itemText += $" ({EntityManager.player.inventory.slots[hotBarSlotSelectedIndex].quantity})";
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
                slotPosition = (new Vector2(Game1.graphicsDeviceManager.PreferredBackBufferWidth, Game1.graphicsDeviceManager.PreferredBackBufferHeight) / 2f) + (new Vector2(slotsGap) * new Vector2(x - ((RowSize - 1f) / 2f), y - ((EntityManager.player.inventory.slots.Length / RowSize) / 2f)));
            }

            return slotPosition;
        }

        private Vector2 GetHotBarPosition() {
            return new Vector2(Game1.graphicsDeviceManager.PreferredBackBufferWidth / 2f, (Game1.graphicsDeviceManager.PreferredBackBufferHeight / 12f) * 11f);
        }
    }
}