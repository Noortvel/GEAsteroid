using GEAsteroid.GameFraemwork;
using GEAsteroid.GameFraemwork.ActorComponents;
using GEAsteroid.GameFraemwork.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Asteroids
{
    public class Bullet : DrawableActor
    {
        private PolygonCollider polygonCollider;
        private PolygonRender polygonRender;
        private SpriteRender spriteRender;
        private PhysicCore physic;
        public Vector2 Direction
        {
            set;
            get;
        }
        public float Speed
        {
            set;
            get;
        }
        public float LiveTime
        {
            set;
            get;
        }
        private float _liveTime = 0;

        public Bullet(MainAsteroidsScene scene, int collideLayer) : base(scene)
        {
            polygonCollider = new PolygonCollider(this, Core.Resources.Resources.Polygones.Bullet, scene.collidersWorld, collideLayer);
            polygonCollider.Tag = "Bullet";
            polygonCollider.OnCollide += PolygonCollider_OnCollide;

            physic = new PhysicCore(this);

            polygonRender = new PolygonRender(this, Core.Resources.Resources.Polygones.Bullet);
            spriteRender = new SpriteRender(this, Core.Resources.Resources.Sprites.Bullet);
            spriteRender.SetActive(false);    
        }

        private void PolygonCollider_OnCollide(GameFraemwork.Collisions.PolygonColliderObject actor)
        {
            SetActive(false);
        }
        public override void SetActive(bool active)
        {
            base.SetActive(active);
            if (active)
            {
                _liveTime = LiveTime;
                physic.Velocity = Direction * Speed;
            }
        }
        public override void Update()
        {
            base.Update();
            _liveTime -= Engine.EngineCore.DeltaTime;
            if (_liveTime < 0)
            {
                SetActive(false);
            }
        }
    }
}
