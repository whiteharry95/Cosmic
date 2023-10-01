namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;

    public class WoodenPlatform : BlockItem {
        public override void Generate() {
            name = "Wooden Platform";

            sprite = new Sprite(TileManager.woodenPlatform.texture, new Vector2(TileManager.woodenPlatform.texture.Width, TileManager.woodenPlatform.texture.Height) / 2f);

            tile = TileManager.woodenPlatform;

            base.Generate();
        }
    }
}