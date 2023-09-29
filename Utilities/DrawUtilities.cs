namespace Cosmic.Utilities {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class DrawUtilities {
        public class Text {
            public string text;

            public Text(string text) {
                this.text = text;
            }
        }

        public enum AlignmentHor {
            Left,
            Middle,
            Right
        }

        public enum AlignmentVer {
            Top,
            Middle,
            Bottom
        }

        public static Vector2 GetStringSize(SpriteFont font, string text) {
            return font.MeasureString(text);
        }

        public static void DrawString(SpriteFont font, Text text, Vector2 position, Color colour, AlignmentHor alignmentHor, AlignmentVer alignmentVer) {
            Vector2 size = font.MeasureString(text.text);
            string[] lines = text.text.Split('\n');

            foreach (string line in lines) {
                Vector2 lineSize = font.MeasureString(line);
                Vector2 origin = Vector2.Zero;

                switch (alignmentHor) {
                    case AlignmentHor.Middle:
                        origin.X += lineSize.X / 2f;
                        break;

                    case AlignmentHor.Right:
                        origin.X += lineSize.X;
                        break;
                }

                switch (alignmentVer) {
                    case AlignmentVer.Middle:
                        origin.Y += size.Y / 2f;
                        break;

                    case AlignmentVer.Bottom:
                        origin.Y += size.Y;
                        break;
                }

                Game1.spriteBatch.DrawString(font, line, position - origin, colour);

                position.Y += lineSize.Y;
            }
        }
    }
}