namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Tiles;
    using Cosmic.Assets;
    using Microsoft.Xna.Framework.Graphics;

    public class TileSelection : UIElement {
        public override void Update() {
        }

        public override void Draw() {
            if (!UIManager.playerInventory.open) {
                if (EntityManager.player?.ItemCurrent?.showTileSelection ?? false) {
                    foreach (Point tile in EntityManager.player.tileSelection) {
                        Game1.spriteBatch.Draw(TextureManager.Pixel, ((tile.ToVector2() * new Vector2(Tile.Size)) - Camera.position) * Camera.Scale, null, Color.White * 0.5f, 0f, Vector2.Zero, Tile.Size * Camera.Scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }
    }
}