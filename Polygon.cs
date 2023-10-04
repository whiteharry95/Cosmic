namespace Cosmic {
    using Cosmic.Utilities;
    using Microsoft.Xna.Framework;

    public class Polygon {
        public Vector2[] vertices;

        public Polygon(params Vector2[] vertices) {
            this.vertices = vertices;
        }

        public bool GetCollisionWithPolygon(Polygon polygon) {
            for (int i = 0; i < vertices.Length; i++) {
                Vector2 edge = MathUtilities.NormaliseVector2(vertices[(i + 1) % vertices.Length] - vertices[i]);

                ProjectVerticesOntoEdge(vertices, edge, out float projectionMin, out float projectionMax);
                ProjectVerticesOntoEdge(polygon.vertices, edge, out float polygonProjectionMin, out float polygonProjectionMax);

                if (projectionMax < polygonProjectionMin || polygonProjectionMax < projectionMin) {
                    return false;
                }
            }

            for (int i = 0; i < polygon.vertices.Length; i++) {
                Vector2 edge = MathUtilities.NormaliseVector2(polygon.vertices[(i + 1) % polygon.vertices.Length] - polygon.vertices[i]);

                ProjectVerticesOntoEdge(vertices, edge, out float projectionMin, out float projectionMax);
                ProjectVerticesOntoEdge(polygon.vertices, edge, out float polygonProjectionMin, out float polygonProjectionMax);

                if (projectionMax < polygonProjectionMin || polygonProjectionMax < projectionMin) {
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