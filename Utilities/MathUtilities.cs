namespace Cosmic.Utilities {
    using Microsoft.Xna.Framework;

    public static class MathUtilities {
        public enum Direction {
            Right,
            Up,
            Left,
            Down
        }

        public static Vector2 NormaliseVector2(Vector2 vector2) {
            vector2 = Vector2.Normalize(vector2);

            if (float.IsNaN(vector2.X) || float.IsNaN(vector2.Y)) {
                return Vector2.Zero;
            }

            return vector2;
        }
    }
}