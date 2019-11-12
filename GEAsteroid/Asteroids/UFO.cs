using GEAsteroid.Engine.Extensions;
using GEAsteroid.GameFraemwork.ActorComponents;
using GEAsteroid.GameFraemwork.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Asteroids
{
    public class UFO : DrawableActor
    {
        private PolygonCollider polygonCollider;
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

        private MainAsteroidsScene scene;
        private SpaceShip spaceShip;
        private Gun gun;
        private PhysicCore physic;
        private float fireInterval = 0.5f;
        private float _fireInterval = -1;
        private float pathCorrectInterval = 0.3f;
        private float _pathCorrectInterval = -1;
        private float floatSpeed = 150;
        public UFO(MainAsteroidsScene scene, SpaceShip spaceShip) : base(scene)
        {
            this.scene = scene;
            this.spaceShip = spaceShip;
            Name = "UFO";
            physic = new PhysicCore(this);
            gun = new Gun(scene, this, 0, physic, 1);

            gun.BulletSpeed = 450;
            gun.BulletLifeTime = 1.5f;
            gun.FireRate = 1.5f;

            var plgn = Core.Resources.Resources.Polygones.UFO;
            polygonCollider = new PolygonCollider(this, plgn, scene.collidersWorld, 1);
            polygonCollider.OnCollide += PolygonCollider_OnCollide;
            polygonCollider.Tag = Name;
            PolygonRender = new PolygonRender(this, plgn);
            SpriteRender = new SpriteRender(this, Core.Resources.Resources.Sprites.UFO);
            SpriteRender.SetActive(false);

        }

        private void PolygonCollider_OnCollide(GameFraemwork.Collisions.PolygonColliderObject collider)
        {
            if(collider.Tag == "Bullet")
            {
                scene.Score += 10;
                SetActive(false);
            }
        }
        public override void SetActive(bool active)
        {
            base.SetActive(active);
            if (active)
            {
                _fireInterval = fireInterval;
                _pathCorrectInterval = pathCorrectInterval;
            }
        }

        private void ShootToTarget()
        {
            if (spaceShip.IsActive())
            {
                var dir = (spaceShip.Transform.Position - Transform.Position).Normalized();
                gun.SpawnBullet(dir);
            }
        }
        private void ForceToTarget()
        {
            if (spaceShip.IsActive())
            {
                var dir = (spaceShip.Transform.Position - Transform.Position).Normalized();
                physic.Velocity = dir * floatSpeed;
            }
        }
        public override void Update()
        {
            base.Update();
            _fireInterval -= Engine.EngineCore.DeltaTime;
            _pathCorrectInterval -= Engine.EngineCore.DeltaTime;
            if (_fireInterval < 0)
            {
                _fireInterval = fireInterval;
                ShootToTarget();
            }
            if(_pathCorrectInterval < 0)
            {
                _pathCorrectInterval = pathCorrectInterval;
                ForceToTarget();
            }
        }
    }
}
