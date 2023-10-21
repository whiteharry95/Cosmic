namespace Cosmic.Utilities {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class DrawUtilities {
        public enum HorizontalAlignment {
            Left,
            Centre,
            Right
        }

        public enum VerticalAlignment {
            Top,
            Middle,
            Bottom
        }

        public static void DrawText(SpriteFont font, string text, Vector2 position, Color colour, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment) {
            Vector2 size = font.MeasureString(text);
            string[] textLines = text.Split('\n');

            foreach (string line in textLines) {
                Vector2 lineSize = font.MeasureString(line);
                Vector2 origin = Vector2.Zero;

                switch (horizontalAlignment) {
                    case HorizontalAlignment.Centre:
                        origin.X += lineSize.X / 2f;
                        break;

                    case HorizontalAlignment.Right:
                        origin.X += lineSize.X;
                        break;
                }

                switch (verticalAlignment) {
                    case VerticalAlignment.Middle:
                        origin.Y += size.Y / 2f;
                        break;

                    case VerticalAlignment.Bottom:
                        origin.Y += size.Y;
                        break;
                }

                Game1.SpriteBatch.DrawString(font, line, position - origin, colour);

                position.Y += lineSize.Y;
            }
        }
    }
}