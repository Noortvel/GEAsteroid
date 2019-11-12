using GEAsteroid.Core.Resources;
using GEAsteroid.Engine.Data;
using GEAsteroid.GameFraemwork.ActorComponents;
using GEAsteroid.GameFraemwork.Actors;
using GEAsteroid.GameFraemwork.Collisions;

using System;

namespace GEAsteroid.Asteroids
{
    public class Asteroid : DrawableActor
    {
        public SpriteRender SpriteRender
        {
            private set;
            get;
        }
        public PolygonRender PolygonRender
        {
            private set;
            get;
        }
        private PolygonCollider collider2D;
        
        public event Action OnCollideBullet = delegate { };
        public PhysicCore Physic
        {
            private set;
            get;
        }
        private MainAsteroidsScene scene;
        public Asteroid(MainAsteroidsScene scene, SpriteInfo spriteInfo, Polygon polygon) : base(scene)
        {
            this.scene = scene;
            SpriteRender = new SpriteRender(this, spriteInfo);
            SpriteRender.SetActive(false);

            PolygonRender = new PolygonRender(this, polygon);

            collider2D = new PolygonCollider(this, polygon, scene.collidersWorld, 1);
            collider2D.OnCollide += Collider2D_OnCollide;
            collider2D.Tag = "Asteroid";
            Physic = new PhysicCore(this);
        }

        private void Collider2D_OnCollide(PolygonColliderObject collider)
        {
            if(collider.Tag == "Bullet")
            {
                OnCollideBullet();
            }
        }
        public override void Update()
        {
            base.Update();
        }
    }
}