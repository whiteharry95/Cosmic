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

        public override void Update(GameTime gameTime) {
            text = "";

            if (UIManager.playerInventory.open && UIManager.playerInventory.slotSelectedIndex != -1) {
                if (Game1.server.netPlayers[0].player.inventory.slots[UIManager.playerInventory.slotSelectedIndex] != null) {
                    text = $"{Game1.server.netPlayers[0].player.inventory.slots[UIManager.playerInventory.slotSelectedIndex].item.name}";

                    if (Game1.server.netPlayers[0].player.inventory.slots[UIManager.playerInventory.slotSelectedIndex].quantity > 1) {
                        text += $" ({Game1.server.netPlayers[0].player.inventory.slots[UIManager.playerInventory.slotSelectedIndex].quantity})";
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime) {
            Vector2 position = InputManager.mouseState.Position.ToVector2();

            if (inventorySlot != null) {
                inventorySlot.item.sprite.Draw(position, inventorySlot.item.displayRotation, origin: inventorySlot.item.sprite.Size / 2f);
            }

            DrawUtilities.DrawText(FontManager.ArialMedium, text, position + new Vector2(8f), Color.White, DrawUtilities.HorizontalAlignment.Left, DrawUtilities.VerticalAlignment.Top);

            sprite.Draw(position);
        }
    }
}