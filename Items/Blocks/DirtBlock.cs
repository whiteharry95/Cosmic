namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Tiles;

    public class DirtBlock : BlockItem {
        public override void Load(ContentManager contentManager) {
            name = "Dirt Block";

            sprite = new Sprite(TileManager.dirt.texture, new Vector2(TileManager.dirt.texture.Width, TileManager.dirt.texture.Height) / 2f);

            tile = TileManager.dirt;

            base.Load(contentManager);
        }
    }
}