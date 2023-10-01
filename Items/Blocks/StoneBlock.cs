namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;

    public class StoneBlock : BlockItem {
        public override void Generate() {
            name = "Stone Block";

            sprite = new Sprite(TileManager.stone.texture, new Vector2(TileManager.stone.texture.Width, TileManager.stone.texture.Height) / 2f);

            tile = TileManager.stone;

            base.Generate();
        }
    }
}