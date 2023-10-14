namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework.Graphics;
    using Cosmic.Items;

    public class Tile {
        public const int Size = 16;

        public ushort id;

        public Texture2D texture;

        public ushort life;

        public Item item;

        public virtual void Load() {
        }
    }
}