namespace Cosmic.Items.Blocks {
    using Cosmic.Tiles;

    public class DirtBlock : BlockItem {
        public override void Load() {
            name = "Dirt Block";

            sprite = new Sprite(TileManager.dirt.textureSheet.Textures[0], Sprite.OriginPreset.MiddleCentre);

            tile = TileManager.dirt;

            base.Load();
        }
    }
}