namespace Cosmic {
    using Microsoft.Xna.Framework;
    using System;

    public class Polygon {
        public Func<Vector2[]> getVertices;
        public Func<Vector2> getPosition;
        public Func<float> getRotation;
        public Func<Vector2> getScale;
        public Func<Vector2> getOrigin;

        public Polygon(Func<Vector2[]> getVertices, Func<Vector2> getPosition, Func<float> getRotation = null, Func<Vector2> getScale = null, Func<Vector2> getOrigin = null) {
            this.getVertices = getVertices;
            this.getPosition = getPosition;
            this.getRotation = getRotation ?? (() => 0f);
            this.getScale = getScale ?? (() => Vector2.One);
            this.getOrigin = getOrigin ?? (() => Vector2.Zero);
        }

        public float GetRight(Vector2[] verticesReal = null) {
            float right = float.MinValue;

            if (verticesReal == null) {
                verticesReal = GetVerticesReal();
            }

            for (int i = 0; i < verticesReal.Length; i++) {
                right = Math.Max((float)right, verticesReal[i].X);
            }

            return right;
        }

        public float GetLeft(Vector2[] verticesReal = null) {
            float left = float.MaxValue;

            if (verticesReal == null) {
                verticesReal = GetVerticesReal();
            }

            for (int i = 0; i < verticesReal.Length; i++) {
                left = Math.Min((float)left, verticesReal[i].X);
            }

            return left;
        }

        public float GetBottom(Vector2[] verticesReal = null) {
            float down = float.MinValue;

            if (verticesReal == null) {
                verticesReal = GetVerticesReal();
            }

            for (int i = 0; i < verticesReal.Length; i++) {
                down = Math.Max((float)down, verticesReal[i].Y);
            }

            return down;
        }

        public float GetTop(Vector2[] verticesReal = null) {
            float up = float.MaxValue;

            if (verticesReal == null) {
                verticesReal = GetVerticesReal();
            }

            for (int i = 0; i < verticesReal.Length; i++) {
                up = Math.Min((float)up, verticesReal[i].Y);
            }

            return up;
        }

        public Vector2 GetTopLeft() {
            Vector2[] verticesReal = GetVerticesReal();
            return new Vector2(GetLeft(verticesReal), GetTop(verticesReal));
        }

        public Vector2 GetBottomRight() {
            Vector2[] verticesReal = GetVerticesReal();
            return new Vector2(GetRight(verticesReal), GetBottom(verticesReal));
        }

        public static Vector2[] GetRectangleVertices(Vector2 size) {
            return new Vector2[4] {
                Vector2.Zero,
                new Vector2(size.X, 0f),
                size,
                new Vector2(0f, size.Y)
            };
        }

        public Vector2[] GetVerticesReal(Vector2? offset = null) {
            Vector2[] verticesReal = getVertices();

            Vector2 directionRight = new Vector2(MathF.Cos(getRotation()), MathF.Sin(getRotation()));
            Vector2 directionUp = new Vector2(MathF.Cos(getRotation() + (MathF.PI / 2f)), MathF.Sin(getRotation() + (MathF.PI / 2f)));

            if (Math.Abs(directionRight.X) < 10e-5f) {
                directionRight.X = 0f;
            }

            if (Math.Abs(directionRight.Y) < 10e-5f) {
                directionRight.Y = 0f;
            }

            if (Math.Abs(directionUp.X) < 10e-5f) {
                directionUp.X = 0f;
            }

            if (Math.Abs(directionUp.Y) < 10e-5f) {
                directionUp.Y = 0f;
            }

            Vector2 topLeft = getPosition() - (getOrigin().X * getScale().X * directionRight) - (getOrigin().Y * getScale().Y * directionUp);

            for (int i = 0; i < verticesReal.Length; i++) {
                Vector2 horizontalShift = getVertices()[i].X * getScale().X * directionRight;
                Vector2 verticalShift = getVertices()[i].Y * getScale().Y * directionUp;

                verticesReal[i] = topLeft + horizontalShift + verticalShift + (offset ?? Vector2.Zero);
            }

            return verticesReal;
        }

        public bool GetCollisionWithPolygon(Polygon polygon, Vector2? offset = null) {
            Vector2[] vertices = GetVerticesReal(offset);
            Vector2[] polygonVertices = polygon.GetVerticesReal();

            for (int i = 0; i < vertices.Length; i++) {
                Vector2 edge = vertices[(i + 1) % vertices.Length] - vertices[i];
                edge = new Vector2(-edge.Y, edge.X);

                ProjectVerticesOntoEdge(vertices, edge, out float projectionMin, out float projectionMax);
                ProjectVerticesOntoEdge(polygonVertices, edge, out float polygonProjectionMin, out float polygonProjectionMax);

                if (projectionMin >= polygonProjectionMax || polygonProjectionMin >= projectionMax) {
                    return false;
                }
            }

            for (int i = 0; i < polygonVertices.Length; i++) {
                Vector2 edge = polygonVertices[(i + 1) % polygonVertices.Length] - polygonVertices[i];
                edge = new Vector2(-edge.Y, edge.X);

                ProjectVerticesOntoEdge(vertices, edge, out float projectionMin, out float projectionMax);
                ProjectVerticesOntoEdge(polygonVertices, edge, out float polygonProjectionMin, out float polygonProjectionMax);

                if (projectionMin >= polygonProjectionMax || polygonProjectionMin >= projectionMax) {
                    return false;
                }
            }

            return true;
        }

        private void ProjectVerticesOntoEdge(Vector2[] vertices, Vector2 edge, out float projectionMin, out float projectionMax) {
            projectionMin = float.MaxValue;
            projectionMax = float.MinValue;

            foreach (Vector2 vertex in vertices) {
                float projection = Vector2.Dot(vertex, edge);

                projectionMin = MathF.Min(projection, projectionMin);
                projectionMax = MathF.Max(projection, projectionMax);
            }
        }
    }
}