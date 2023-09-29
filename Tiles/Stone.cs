namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Items;

    public class Stone : Tile {
        public override void Load(ContentManager contentManager) {
            texture = TileManager.CreateTileTexture(Color.Gray);

            life = 30;

            item = ItemManager.stoneBlock;
        }
    }
}