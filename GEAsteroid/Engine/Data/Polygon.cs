using System;
using System.Numerics;

namespace GEAsteroid.Engine.Data
{
    /// <summary>
    /// Need clockwise defenition
    /// </summary>
    public class Polygon
    {
        public Vector2 Center
        {
            set;
            get;
        }
        private readonly Vector2[] vertices;
        private readonly Vector2[] edges;
        public Vector2[] GetVertices()
        {
            return vertices;
        }
        public Vector2[] GetEdges()
        {
            return edges;
        }
        public Polygon(Vector2[] vertices, Vector2 center)
        {
            int len = vertices.Length;
            this.vertices = new Vector2[len];
            Array.Copy(vertices, this.vertices, len);
            this.Center = center;

            edges = new Vector2[vertices.Length - 1];
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                edges[i] = vertices[i + 1] - vertices[i];
            }
            edges[edges.Length - 1] = vertices[0] - vertices[vertices.Length - 1];
        }
        public Polygon(Vector2[] vertices) : this(vertices, CentreOfMass(vertices))
        {

        }
        public Polygon(Polygon polygon) : this(polygon.vertices, polygon.Center)
        {

        }
        public static Vector2 CentreOfMass(Vector2[] vertices)
        {
            Vector2 centre = new Vector2(0, 0);
            foreach (var x in vertices)
            {
                centre += x;
            }
            centre /= vertices.Length;
            return centre;
        }
        public void Scale(Vector2 scale)
        {
            var verts = GetVertices();
            for (int i = 0; i < vertices.Length; i++)
            {
                float y = scale.Y * (verts[i].Y - Center.Y) + Center.Y;
                float x = scale.X * (verts[i].X - Center.X) + Center.X;
                vertices[i] = new Vector2(x, y);
            }
        }

    }
}
