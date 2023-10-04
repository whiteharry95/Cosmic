namespace Cosmic {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Sprite {
        public enum OriginPreset {
            TopLeft,
            TopCentre,
            TopRight,
            MiddleLeft,
            MiddleCentre,
            MiddleRight,
            BottomLeft,
            BottomCentre,
            BottomRight
        }

        public Vector2 Size => new Vector2(texture.Width, texture.Height);

        public Texture2D texture;
        public Vector2 origin;

        public Sprite(Texture2D texture, Vector2 origin) {
            this.texture = texture;
            this.origin = origin;
        }

        public Sprite(Texture2D texture, OriginPreset originPreset) {
            this.texture = texture;
            origin = GetOriginFromPreset(originPreset);
        }

        public void Draw(Vector2 position, float rotation = 0f, float scale = 1f, Color? colour = null, SpriteEffects flip = SpriteEffects.None, Vector2? origin = null) {
            Game1.spriteBatch.Draw(texture, position, null, colour ?? Color.White, rotation, origin ?? this.origin, scale, flip, 0f);
        }

        public Vector2 GetOriginFromPreset(OriginPreset originPreset) {
            Vector2 origin;

            switch (originPreset) {
                case OriginPreset.TopCentre: origin = new Vector2(texture.Width / 2f, 0f); break;
                case OriginPreset.TopRight: origin = new Vector2(texture.Width, 0f); break;
                case OriginPreset.MiddleLeft: origin = new Vector2(0f, texture.Height / 2f); break;
                case OriginPreset.MiddleCentre: origin = new Vector2(texture.Width / 2f, texture.Height / 2f); break;
                case OriginPreset.MiddleRight: origin = new Vector2(texture.Width, texture.Height / 2f); break;
                case OriginPreset.BottomLeft: origin = new Vector2(0f, texture.Height); break;
                case OriginPreset.BottomCentre: origin = new Vector2(texture.Width / 2f, texture.Height); break;
                case OriginPreset.BottomRight: origin = new Vector2(texture.Width, texture.Height); break;
                default: origin = new Vector2(0f, 0f); break;
            }

            return origin;
        }
    }
}