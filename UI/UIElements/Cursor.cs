namespace Cosmic.UI.UIElements {
    using Cosmic;
    using Cosmic.Assets;
    using Cosmic.Entities;
    using Cosmic.Inventory;
    using Cosmic.Utilities;
    using Microsoft.Xna.Framework;

    public class Cursor : UIElement {
        private Sprite sprite = new Sprite(TextureManager.UI_Cursor, Sprite.OriginPreset.MiddleCentre);

        public InventorySlot inventorySlot;

        public string text;

        public Polygon polygon;

        public Cursor() {
            polygon = new Polygon(() => Polygon.GetRectangleVertices(sprite.Size), () => InputManager.mouseState.Position.ToVector2(), getScale: () => new Vector2(Camera.Scale), getOrigin: () => sprite.origin);
        }

        public override void Update() {
            text = "";

            if (UIManager.playerInventory.open && UIManager.playerInventory.slotSelectedIndex != -1) {
                if (EntityManager.player.inventory.slots[UIManager.playerInventory.slotSelectedIndex] != null) {
                    text = $"{EntityManager.player.inventory.slots[UIManager.playerInventory.slotSelectedIndex].item.name}";

                    if (EntityManager.player.inventory.slots[UIManager.playerInventory.slotSelectedIndex].quantity > 1) {
                        text += $" ({EntityManager.player.inventory.slots[UIManager.playerInventory.slotSelectedIndex].quantity})";
                    }
                }
            }
        }

        public override void Draw() {
            Vector2 position = InputManager.mouseState.Position.ToVector2();

            if (inventorySlot != null) {
                inventorySlot.item.sprite.Draw(position, inventorySlot.item.displayRotation, scale: new Vector2(Camera.Scale), origin: inventorySlot.item.sprite.mask.Location.ToVector2() + (inventorySlot.item.sprite.mask.Size.ToVector2() / 2f));
            }

            DrawUtilities.DrawText(FontManager.ArialMedium, text, position + new Vector2(5f), Color.White, DrawUtilities.HorizontalAlignment.Left, DrawUtilities.VerticalAlignment.Top);

            sprite.Draw(position, scale: new Vector2(Camera.Scale));
        }
    }
}