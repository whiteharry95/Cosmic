namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Tiles;

    public class StoneBlock : BlockItem {
        public override void Load(ContentManager contentManager) {
            name = "Stone Block";

            sprite = new Sprite(TileManager.stone.texture, new Vector2(TileManager.stone.texture.Width, TileManager.stone.texture.Height) / 2f);

            tile = TileManager.stone;

            base.Load(contentManager);
        }
    }
}