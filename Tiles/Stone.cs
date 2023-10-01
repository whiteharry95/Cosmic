namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework;
    using Cosmic.Items;

    public class Stone : Tile {
        public override void Generate() {
            texture = TileManager.CreateTileTexture(Color.Gray);

            life = 30;

            item = ItemManager.stoneBlock;
        }
    }
}