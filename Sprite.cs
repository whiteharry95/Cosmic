namespace Cosmic {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class Sprite {
        public Texture2D Texture => textures[0];

        public int Width => Texture.Width;
        public int Height => Texture.Height;

        public Point Size => new Point(Width, Height);

        public Texture2D[] textures;
        public Vector2 origin;

        public Sprite(Texture2D texture, Vector2 origin) {
            textures = new Texture2D[1] {
                texture
            };

            this.origin = origin;
        }

        public Sprite(params Texture2D[] textures) {
            this.textures = textures;
        }

        public void Draw(Vector2 position, float rotation, float scale, float alpha) {
            Game1.spriteBatch.Draw(Texture, position, null, Color.White * alpha, rotation, origin, scale, SpriteEffects.None, 0f);
        }

        public void DrawIndex(int index, Vector2 position, float rotation, float scale, float alpha) {
            Game1.spriteBatch.Draw(textures[index], position, null, Color.White * alpha, rotation, origin, scale, SpriteEffects.None, 0f);
        }
    }
}