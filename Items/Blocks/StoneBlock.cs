namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;

    public class StoneBlock : BlockItem {
        public override void Load() {
            name = "Stone Block";

            sprite = new Sprite(TileManager.stone.texture, Sprite.OriginPreset.MiddleCentre);

            tile = TileManager.stone;

            base.Load();
        }
    }
}