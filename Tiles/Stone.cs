namespace Cosmic.Tiles {
    using Cosmic.Items;
    using Cosmic.Assets;

    public class Stone : Tile {
        public override void Load() {
            texture = TextureManager.Tiles_Stone;

            life = 30;

            item = ItemManager.stoneBlock;
        }
    }
}