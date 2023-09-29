namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;

    public class TileSelection : UIElement {
        public override void Update(GameTime gameTime) {
        }

        public override void Draw(GameTime gameTime) {
            if (EntityManager.player?.ItemCurrent?.showTileSelection ?? false) {
                foreach (Point tile in EntityManager.player.tileSelection) {
                    Game1.spriteBatch.Draw(AssetManager.tileSelection, (tile.ToVector2() * new Vector2(Game1.tileSize)) - Camera.position, Color.White * 0.5f);
                }
            }
        }
    }
}