namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Cosmic.Items;

    public class Tile {
        public int id;

        public Texture2D texture;

        public byte life;

        public byte timeMax = 60;

        public Item item;

        public bool platform;

        public bool wall = true;

        public virtual void Generate() {
        }

        public virtual void Update(GameTime gameTime) {
        }
    }
}