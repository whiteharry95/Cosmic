namespace Cosmic.Tiles {
    using Cosmic.Items;
    using Cosmic.Assets;

    public class Stone : Tile {
        public override void Load() {
            textureSheet = TextureManager.Tiles_Stone;

            life = 30;

            item = ItemManager.StoneBlock;
        }
    }
}