using GEAsteroid.Engine.Mathn;
using GEAsteroid.Engine.Data;
using System;
using System.Numerics;

namespace GEAsteroid.GameFraemwork.Collisions
{
    public class PolygonT
    {
        private Transform2 transform;
        private Polygon polygon;
        private Vector2[] vertices;
        private Vector2[] edges;
        private float lastRotation = 0;

        public PolygonT(Polygon polygon, Transform2 trans)
        {
            transform = new Transform2();
            this.polygon = polygon;
            transform.Origin = polygon.Center;
            transform.Position = trans.Position;
            transform.Rotation = trans.Rotation;
            transform.Scale = trans.Scale;
            lastRotation = transform.Rotation;

            var pvert = polygon.GetVertices();
            vertices = new Vector2[pvert.Length];
            Array.Copy(pvert, vertices, vertices.Length);


            edges = new Vector2[vertices.Length - 1];
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                edges[i] = vertices[i + 1] - vertices[i];
            }
            edges[edges.Length - 1] = vertices[0] - vertices[vertices.Length - 1];
        }
        public void SetRotation(float rotation)
        {
            float angle = rotation * Constants.Deg2Rad;
            if (Math.Abs(lastRotation - angle) < Constants.Epsilon_01)
            {
                return;
            }
            Vector2[] verts = polygon.GetVertices();
            float cx = transform.Origin.X;
            float cy = transform.Origin.Y;
            for (int i = 0; i < vertices.Length; i++)
            {
                float newX = cx + (verts[i].X - cx) * (float)Math.Cos(angle) - (verts[i].Y - cy) * (float)Math.Sin(angle);
                float newY = cy + (verts[i].X - cx) * (float)Math.Sin(angle) + (verts[i].Y - cy) * (float)Math.Cos(angle);
                vertices[i] = new Vector2(newX, newY);
            }
            transform.Rotation = rotation;
        }
        public void SetPosition(Vector2 position)
        {
            transform.Position = position;
        }
        public Vector2 GetPosition()
        {
            return transform.Position;
        }
        public Vector2 GetOrigin()
        {
            return transform.Origin;
        }
        public Vector2[] GetVertices()
        {
            return vertices;
        }
        public Vector2[] GetEdges()
        {
            return edges;
        }
        //public void SetScale(Vector2 scale)
        //{
        //    var verts = polygon.GetVertices();
        //    for(int i = 0; i < vertices.Length; i++)
        //    {
        //        float y = scale.Y * (verts[i].Y - polygon.center.Y) + polygon.center.Y;
        //        float x = scale.X * (verts[i].X - polygon.center.X) + polygon.center.X;
        //        vertices[i] = new Vector2(x, y);
        //    }
        //}
    }
}
