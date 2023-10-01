namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework;
    using Cosmic.Items;

    public class WoodenPlatform : Tile {
        public override void Generate() {
            texture = TileManager.CreateTileTexture(Color.Purple);
            
            life = 20;

            item = ItemManager.woodenPlatform;

            platform = true;
        }
    }
}