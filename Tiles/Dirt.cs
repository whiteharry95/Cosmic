namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework;
    using Cosmic.Items;

    public class Dirt : Tile {
        public override void Generate() {
            texture = TileManager.CreateTileTexture(Color.Brown);

            life = 15;

            item = ItemManager.dirtBlock;
        }
    }
}