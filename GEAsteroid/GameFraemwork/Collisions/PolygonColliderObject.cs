using GEAsteroid.Core;
using GEAsteroid.Core.Extensions;
using GEAsteroid.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.GameFraemwork.Collisions
{
    public class PolygonColliderObject : AObject
    {
        public string Tag
        {
            set;
            get;
        }
        private PolygonT polygonT;

        private Transform2 target;
        public PolygonColliderObject(Polygon polygon, Transform2 trans)
        {
            target = trans;
            this.polygonT = new PolygonT(polygon, trans);
        }
        public delegate void CollisionNotyfy(PolygonColliderObject collider);
        public event CollisionNotyfy OnCollide = delegate { };
        private void ToTransform()
        {

            polygonT.SetPosition(target.Position);
            polygonT.SetRotation(target.Rotation);
        }
        public bool PolygonCollision(PolygonColliderObject collider)
        {
            ToTransform();
            collider.ToTransform();
            PolygonT polygonA = polygonT;
            PolygonT polygonB = collider.polygonT;

            var posA = polygonA.GetPosition() - polygonA.GetOrigin();
            var posB = polygonB.GetPosition() - polygonB.GetOrigin();

            //Debug.DrawPolygon(polygonA);
            //Debug.DrawPolygon(polygonB);

            int edgeCountA = polygonA.GetEdges().Length;
            int edgeCountB = polygonB.GetEdges().Length;
            Vector2 edge;

            for (int edgeIndex = 0; edgeIndex < edgeCountA + edgeCountB; edgeIndex++)
            {
                if (edgeIndex < edgeCountA)
                {
                    edge = polygonA.GetEdges()[edgeIndex];
                }
                else
                {
                    edge = polygonB.GetEdges()[edgeIndex - edgeCountA];
                }
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                axis = Vector2.Normalize(axis);
                float minA = 0; float minB = 0; float maxA = 0; float maxB = 0;
                ProjectPolygon(axis, polygonA, ref minA, ref maxA);
                ProjectPolygon(axis, polygonB, ref minB, ref maxB);
                if (IntervalDistance(minA, maxA, minB, maxB) > 0) return false;
            }
            OnCollide(collider);
            collider.OnCollide(this);
            return true;
        }
        private void ProjectPolygon(Vector2 axis, PolygonT polygon, ref float min, ref float max)
        {
            float d = Vector2.Dot(axis, polygon.GetVertices()[0] + polygon.GetPosition() - polygon.GetOrigin());// axis.DotProduct(polygon.Points[0]);
            min = d;
            max = d;
            for (int i = 0; i < polygon.GetVertices().Length; i++)
            {
                d = Vector2.Dot(polygon.GetVertices()[i] + polygon.GetPosition() - polygon.GetOrigin(), axis);
                if (d < min)
                {
                    min = d;
                }
                else
                {
                    if (d > max)
                    {
                        max = d;
                    }
                }
            }
        }
        private float IntervalDistance(float minA, float maxA, float minB, float maxB)
        {
            if (minA < minB)
            {
                return minB - maxA;
            }
            else
            {
                return minA - maxB;
            }
        } 

    }
}
