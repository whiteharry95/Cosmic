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
                if (Game1.server.netPlayers[0]?.player?.ItemCurrent?.showTileSelection ?? false) {
                    foreach (Point tile in Game1.server.netPlayers[0].player.tileSelection) {
                        Game1.spriteBatch.Draw(TextureManager.UI_TileSelection, (tile.ToVector2() * new Vector2(Tile.Size)) - Camera.position, Color.White * 0.5f);
                    }
                }
            }
        }
    }
}