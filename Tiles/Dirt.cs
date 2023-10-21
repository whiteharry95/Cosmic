namespace Cosmic.Tiles {
    using Cosmic.Assets;
    using Cosmic.Items;

    public class Dirt : Tile {
        public override void Load() {
            textureSheet = TextureManager.Tiles_Dirt;

            life = 15;

            item = ItemManager.DirtBlock;
        }
    }
}