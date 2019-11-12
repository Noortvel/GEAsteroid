using System;
using System.Numerics;
using GEAsteroid.Core.Extensions;
using GEAsteroid.Core.Render;
using GEAsteroid.Engine;
using GEAsteroid.Engine.Extensions;
using GEAsteroid.Core.Resources;
using GEAsteroid.GameFraemwork.ActorComponents;
using GEAsteroid.GameFraemwork.Actors;

namespace GEAsteroid.Asteroids
{
    public class SpaceShip : DrawableActor
    {
        public enum RotateDirection
        {
            Left, Right
        }
        public float MaxForce
        {
            set;
            get;
        }
        public float AccelerationScale
        {
            set;
            get;
        }
        public float BrakingScale
        {
            set;
            get;
        }
        public bool IsForwarding
        {
            private set;
            get;
        }
        public bool IsMoving
        {
            private set;
            get;
        }
        public bool IsRotating
        {
            private set;
            get;
        }
        public RotateDirection RotateDir
        {
            private set;
            get;
        }
        public float RotationSpeed
        {
            private set;
            get;
        }
        private int leftRotateScale = 0;
        private int rightRotateScale = 0;

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
        public PhysicCore Physic {
            private set;
            get;
        }
        private SpaceShipPlayerController controller;
        private PolygonCollider collider2D;
        public Gun Gun
        {
            private set;
            get;
        }
        public GunLaser GunLaser
        {
            private set;
            get;
        }
        private MainAsteroidsScene mainScene;
        public SpaceShip(MainAsteroidsScene scene) : base(scene)
        {
            this.mainScene = scene;
            MaxForce = 300;
            AccelerationScale = 320;
            BrakingScale = AccelerationScale * 1.5f;
            RotationSpeed = 200;
            RotateDir = 0;

            var polygon = Resources.Polygones.SpaceShip;
            SpriteRender = new SpriteRender(this, Resources.Sprites.SpaceShip);
            SpriteRender.SetActive(false);

            PolygonRender = new PolygonRender(this, polygon);
            Physic = new PhysicCore(this);
            controller = new SpaceShipPlayerController(this);
            collider2D = new PolygonCollider(this, polygon, scene.collidersWorld, 0);
            collider2D.OnCollide += Collider2D_OnCollide;
            Gun = new Gun(scene, this, 20, Physic, 0);

            Gun.BulletSpeed = 450;
            Gun.BulletLifeTime = 1.5f;
            Gun.FireRate = 0.1f;

            GunLaser = new GunLaser(scene, this, 20f, 1f);

            SetActive(false);
        }

        private void Collider2D_OnCollide(GameFraemwork.Collisions.PolygonColliderObject collider)
        {
            if(collider.Tag == "Asteroid" || collider.Tag == "Bullet" || collider.Tag == "UFO")
            {
                GunLaser.SetActive(false);
                mainScene.Stop();
            }
        }
        public void MoveForwardStart()
        {
            IsForwarding = true;
            IsMoving = true;
        }
        public void MoveForwardStop()
        {
            IsForwarding = false;
        }
        public void RotateStart(RotateDirection dir)
        {
            if(dir == RotateDirection.Left)
            {
                leftRotateScale = -1;
            }
            if(dir == RotateDirection.Right)
            {
                rightRotateScale = 1;
            }
            IsRotating = true;
        }
        public void RotateStop(RotateDirection dir)
        {
            if (dir == RotateDirection.Left)
            {
                leftRotateScale = 0;
            }
            if (dir == RotateDirection.Right)
            {
                rightRotateScale = 0;
            }
            if(rightRotateScale == 0 && leftRotateScale == 0)
            {
                IsRotating = false;
            }
        }
        private void ForwardForceStep()
        {
            float ratiof = Physic.Speed / MaxForce;
            float scale = ratiof > 1 ? 1 : ratiof;

            Vector2 fwd = Transform.CaclForward();
            Vector2 vel = (Physic.Velocity).Normalized();
            Vector2 mid = (fwd - vel * scale).Normalized();

            Physic.AddForce(mid * AccelerationScale * EngineCore.DeltaTime);
            //Fix float errors
            float error = ratiof - 1;
            if (error > 0)
            {
                Physic.AddForce(-Physic.Velocity * error);
            }
        }
        private void BrakingForceStep()
        {
            if (Physic.Speed > Engine.Mathn.Constants.Epsilon_1)
            {
                Vector2 vec = Vector2.Normalize(Physic.Velocity);
                Physic.AddForce(vec * EngineCore.DeltaTime * (-BrakingScale));
            }
            else
            {
                Physic.Velocity = new Vector2(0, 0);
                IsMoving = false;
            }
        }
        private void MoveUpdate()
        {
            if (IsMoving)
            {
                if (IsForwarding)
                {
                    ForwardForceStep();
                }
                else
                {
                    BrakingForceStep();
                }
            }
        }
        private void RotateUpdate()
        {
            if (IsRotating) 
            {
                Transform.Rotation += 
                    (rightRotateScale + leftRotateScale) * RotationSpeed * EngineCore.DeltaTime;
            }
        }
        public override void Update()
        {
            base.Update();
            MoveUpdate();
            RotateUpdate();
        }
    }

}
