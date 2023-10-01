namespace Cosmic.Utilities {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class DrawUtilities {
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

        public static void DrawText(SpriteFont font, string text, Vector2 position, Color colour, AlignmentHor alignmentHor, AlignmentVer alignmentVer) {
            Vector2 size = font.MeasureString(text);
            string[] lines = text.Split('\n');

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