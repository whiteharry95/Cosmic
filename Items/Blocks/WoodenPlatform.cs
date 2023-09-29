namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Tiles;

    public class WoodenPlatform : BlockItem {
        public override void Load(ContentManager contentManager) {
            name = "Wooden Platform";

            sprite = new Sprite(TileManager.woodenPlatform.texture, new Vector2(TileManager.woodenPlatform.texture.Width, TileManager.woodenPlatform.texture.Height) / 2f);

            tile = TileManager.woodenPlatform;

            base.Load(contentManager);
        }
    }
}