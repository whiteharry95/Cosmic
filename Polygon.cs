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

        public static Vector2[] GetRectangleVertices(Vector2 size, Vector2? offset = null) {
            return new Vector2[4] {
                Vector2.Zero + (offset ?? Vector2.Zero),
                new Vector2(size.X, 0f) + (offset ?? Vector2.Zero),
                size + (offset ?? Vector2.Zero),
                new Vector2(0f, size.Y) + (offset ?? Vector2.Zero)
            };
        }

        public Vector2[] GetRealVertices(Vector2? offset = null) {
            Vector2[] realVertices = getVertices();

            for (int i = 0; i < realVertices.Length; i++) {
                Vector2 topLeft = getPosition() - getOrigin().X * getScale().X * new Vector2(MathF.Cos(getRotation()), MathF.Sin(getRotation())) - getOrigin().Y * getScale().Y * new Vector2(MathF.Cos(getRotation() + MathF.PI / 2f), MathF.Sin(getRotation() + MathF.PI / 2f));

                Vector2 horizontalShift = getVertices()[i].X * getScale().X * new Vector2(MathF.Cos(getRotation()), MathF.Sin(getRotation()));
                Vector2 verticalShift = getVertices()[i].Y * getScale().Y * new Vector2(MathF.Cos(getRotation() + MathF.PI / 2f), MathF.Sin(getRotation() + MathF.PI / 2f));

                realVertices[i] = topLeft + horizontalShift + verticalShift + (offset ?? Vector2.Zero);
            }

            return realVertices;
        }

        public bool GetCollisionWithPolygon(Polygon polygon, Vector2? offset = null) {
            Vector2[] vertices = GetRealVertices(offset);
            Vector2[] polygonVertices = polygon.GetRealVertices();

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

                projectionMin = MathHelper.Min(projection, projectionMin);
                projectionMax = MathHelper.Max(projection, projectionMax);
            }
        }
    }
}