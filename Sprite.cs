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

        public Rectangle mask;

        public Sprite(Texture2D texture, Vector2 origin) {
            this.texture = texture;
            this.origin = origin;

            mask = GetMask();
        }

        public Sprite(Texture2D texture, OriginPreset originPreset) {
            this.texture = texture;
            origin = GetOriginFromPreset(originPreset);

            mask = GetMask();
        }

        public void Draw(Vector2 position, float rotation = 0f, Vector2? scale = null, Color? colour = null, SpriteEffects flip = SpriteEffects.None, Vector2? origin = null) {
            Game1.spriteBatch.Draw(texture, position, null, colour ?? Color.White, rotation, origin ?? this.origin, scale ?? Vector2.One, flip, 0f);
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
                default: origin = Vector2.Zero; break;
            }

            return origin;
        }

        private Rectangle GetMask() {
            Color[] textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);

            int x1 = int.MaxValue;
            int y1 = int.MaxValue;

            int x2 = int.MinValue;
            int y2 = int.MinValue;

            for (int i = 0; i < textureData.Length; i++) {
                int x = i % texture.Width;
                int y = i / texture.Width;

                if (textureData[i].A > 0) {
                    x1 = MathHelper.Min(x, x1);
                    y1 = MathHelper.Min(y, y1);

                    x2 = MathHelper.Max(x, x2);
                    y2 = MathHelper.Max(y, y2);
                }
            }

            return new Rectangle(x1, y1, x2 - x1 + 1, y2 - y1 + 1);
        }
    }
}