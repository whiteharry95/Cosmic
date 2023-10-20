namespace Cosmic.Utilities {
    using Microsoft.Xna.Framework;
    using System;

    public static class MathUtilities {
        public enum Direction {
            Right,
            Up,
            Left,
            Down
        }

        public static Vector2 GetNormalisedVector2(Vector2 vector2) {
            vector2 = Vector2.Normalize(vector2);

            if (float.IsNaN(vector2.X) || float.IsNaN(vector2.Y)) {
                return Vector2.Zero;
            }

            return vector2;
        }

        public static float GetDirectionBetweenPositions(Vector2 positionA, Vector2 positionB) {
            return MathF.Atan2(positionB.Y - positionA.Y, positionB.X - positionA.X);
        }
    }
}