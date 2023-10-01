namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;

    public class DirtBlock : BlockItem {
        public override void Generate() {
            name = "Dirt Block";

            sprite = new Sprite(TileManager.dirt.texture, new Vector2(TileManager.dirt.texture.Width, TileManager.dirt.texture.Height) / 2f);

            tile = TileManager.dirt;

            base.Generate();
        }
    }
}