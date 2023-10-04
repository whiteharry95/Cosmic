namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Tiles;
    using Cosmic.Assets;

    public class TileSelection : UIElement {
        public override void Update(GameTime gameTime) {
        }

        public override void Draw(GameTime gameTime) {
            if (!UIManager.playerInventory.open) {
                if (EntityManager.player?.ItemCurrent?.showTileSelection ?? false) {
                    foreach (Point tile in EntityManager.player.tileSelection) {
                        Game1.spriteBatch.Draw(TextureManager.UI_TileSelection, (tile.ToVector2() * new Vector2(Tile.Size)) - Camera.position, Color.White * 0.5f);
                    }
                }
            }
        }
    }
}