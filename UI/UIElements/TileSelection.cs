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
                if (EntityManager.Player?.ItemCurrent?.showTileSelection ?? false) {
                    for(int y = 0; y < EntityManager.Player.TileSelection.GetLength(1); y++) {
                        for (int x = 0; x < EntityManager.Player.TileSelection.GetLength(0); x++) {
                            if (EntityManager.Player.TileSelection[x, y]) {
                                Game1.SpriteBatch.Draw(TextureManager.Pixel, (((EntityManager.Player.TileSelectionPosition.ToVector2() + new Vector2(x, y)) * new Vector2(Tile.Size)) - Camera.Position) * Camera.Scale, null, Color.White * 0.5f, 0f, Vector2.Zero, Tile.Size * Camera.Scale, SpriteEffects.None, 0f);
                            }
                        }
                    }
                }
            }
        }
    }
}