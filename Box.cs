namespace Cosmic {
    using Microsoft.Xna.Framework;
    using System;

    public class Box {
        public Vector2 Position {
            get => new Vector2(x, y);

            set {
                x = value.X;
                y = value.Y;
            }
        }

        public Vector2 Size {
            get => new Vector2(width, height);

            set {
                width = value.X;
                height = value.Y;
            }
        }

        public Vector2 Centre => Position + (Size / 2f);

        public float Left => x;
        public float Right => x + width;

        public float Top => y;
        public float Bottom => y + height;

        public float x;
        public float y;

        public float width;
        public float height;

        public Box(float x, float y, float width, float height) {
            this.x = x;
            this.y = y;

            this.width = width;
            this.height = height;
        }

        public Box(Vector2 position, Vector2 size) {
            Position = position;
            Size = size;
        }

        public bool GetContains(Vector2 position, float offset = 0f) {
            return position.X >= Left + offset && position.Y >= Top + offset && position.X < Right - offset && position.Y < Bottom - offset;
        }

        public bool GetIntersects(Box box) {
            return Left < box.Right && Top < box.Bottom && Right > box.Left && Bottom > box.Top;
        }

        public bool GetIntersectsCircle(Vector2 position, float radius) {
            return Vector2.Distance(Position, new Vector2(Math.Clamp(position.X, Left, Right), Math.Clamp(position.Y, Top, Bottom))) <= radius;
        }
    }
}