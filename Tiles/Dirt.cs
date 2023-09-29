namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Items;

    public class Dirt : Tile {
        public override void Load(ContentManager contentManager) {
            texture = TileManager.CreateTileTexture(Color.Brown);

            life = 15;

            item = ItemManager.dirtBlock;
        }
    }
}