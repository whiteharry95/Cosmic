namespace Cosmic {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

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

            mask = GetMask();

            origin = GetOriginFromPreset(originPreset);
        }

        public void Draw(Vector2 position, float rotation = 0f, Vector2? scale = null, Color? colour = null, SpriteEffects flip = SpriteEffects.None, Vector2? origin = null) {
            Vector2 drawOrigin = origin ?? this.origin;

            if (flip.HasFlag(SpriteEffects.FlipHorizontally)) {
                drawOrigin.X = texture.Width - drawOrigin.X;
            }

            if (flip.HasFlag(SpriteEffects.FlipVertically)) {
                drawOrigin.Y = texture.Height - drawOrigin.Y;
            }

            Game1.SpriteBatch.Draw(texture, position, null, colour ?? Color.White, rotation, drawOrigin, scale ?? Vector2.One, flip, 0f);
        }

        public Vector2 GetOriginFromPreset(OriginPreset originPreset) {
            Vector2 origin;

            switch (originPreset) {
                case OriginPreset.TopCentre: origin = mask.Location.ToVector2() + new Vector2(mask.Width / 2f, 0f); break;
                case OriginPreset.TopRight: origin = mask.Location.ToVector2() + new Vector2(mask.Width, 0f); break;
                case OriginPreset.MiddleLeft: origin = mask.Location.ToVector2() + new Vector2(0f, mask.Height / 2f); break;
                case OriginPreset.MiddleCentre: origin = mask.Location.ToVector2() + (mask.Size.ToVector2() / 2f); break;
                case OriginPreset.MiddleRight: origin = mask.Location.ToVector2() + new Vector2(mask.Width, mask.Height / 2f); break;
                case OriginPreset.BottomLeft: origin = mask.Location.ToVector2() + new Vector2(0f, mask.Height); break;
                case OriginPreset.BottomCentre: origin = mask.Location.ToVector2() + new Vector2(mask.Width / 2f, mask.Height); break;
                case OriginPreset.BottomRight: origin = mask.Location.ToVector2() + mask.Size.ToVector2(); break;
                default: origin = mask.Location.ToVector2(); break;
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
                    x1 = Math.Min(x, x1);
                    y1 = Math.Min(y, y1);

                    x2 = Math.Max(x, x2);
                    y2 = Math.Max(y, y2);
                }
            }

            return new Rectangle(x1, y1, x2 - x1 + 1, y2 - y1 + 1);
        }
    }
}