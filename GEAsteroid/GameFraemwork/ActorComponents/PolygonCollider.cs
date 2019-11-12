using GEAsteroid.Engine.Data;
using GEAsteroid.GameFraemwork.ActorComponents.Base;
using GEAsteroid.GameFraemwork.Actors;
using GEAsteroid.GameFraemwork.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.GameFraemwork.ActorComponents
{
    public class PolygonCollider : ActorComponent
    {
        public PolygonColliderObject colliderObject;
        public event PolygonColliderObject.CollisionNotyfy OnCollide = delegate { };
        public string Tag
        {
            set
            {
                colliderObject.Tag = value;
            }
            get
            {
                return colliderObject.Tag;
            }
        }
        private CollisionWorld collisionWorld;
        private int layer;
        private int indexInWorld;
        public PolygonCollider(SceneActor actor, Polygon polygon, CollisionWorld collisionWorld, int layer) : base(actor)
        {
            
            colliderObject = new PolygonColliderObject(polygon, actor.Transform);
            AddSubObject(colliderObject);
            indexInWorld = collisionWorld.AddCollider(colliderObject, layer);
            this.collisionWorld = collisionWorld;
            this.layer = layer;
            colliderObject.OnCollide += ColliderObject_OnCollide;
        }
        private void ColliderObject_OnCollide(PolygonColliderObject collider)
        {
            OnCollide(collider);
        }
        ~PolygonCollider()
        {
            collisionWorld.RemoveCollider(indexInWorld, layer);
        }
        
    }
}
