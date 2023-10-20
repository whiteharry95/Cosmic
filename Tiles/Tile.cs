namespace Cosmic.Tiles {
    using Cosmic.Items;
    using Cosmic.Assets;

    public class Tile {
        public const int Size = 8;

        public int id;

        public TextureSheet textureSheet;

        public int life;

        public Item item;

        public virtual void Load() {
        }
    }
}