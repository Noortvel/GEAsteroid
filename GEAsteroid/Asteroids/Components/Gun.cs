using GEAsteroid.Engine.Mathn;
using GEAsteroid.GameFraemwork.ActorComponents.Base;
using GEAsteroid.GameFraemwork.Actors;
using System;
using System.Numerics;


namespace GEAsteroid.Asteroids
{
    public class Gun : UpdatableActorComponent
    {
        private DrawableActor drawableActor;
        private Bullet[] bulletsPull;
        private PhysicCore physic;
        private float fwdBulletSpawnScale;

        public float BulletLifeTime
        {
            set;
            get;
        }
        public float FireRate
        {
            set;
            get;
        }
        public float BulletSpeed
        {
            set;
            get;
        }
        
        private float _fireRate = -1;

        public Gun(MainAsteroidsScene scene, DrawableActor actor, float fwdSpawnBulletScale, PhysicCore physic, int bulletCollideLayer) : base(actor)
        {
          
            drawableActor = actor;
            this.physic = physic;
            this.fwdBulletSpawnScale = fwdSpawnBulletScale;
            BulletLifeTime = 1.5f;
            FireRate = 0.1f;
            bulletsPull = new Bullet[(int)(BulletLifeTime / FireRate)];
            for(int i = 0; i < bulletsPull.Length; i++)
            {
                bulletsPull[i] = new Bullet(scene, bulletCollideLayer);
                bulletsPull[i].LiveTime = BulletLifeTime;
                bulletsPull[i].SetActive(false);
            }
        }
        public void SpawnBullet()
        {
            SpawnBullet(drawableActor.Transform.CaclForward());
        }
        public void SpawnBullet(Vector2 direction)
        {
            if (_fireRate < 0)
            {
                if (IsActive())
                {
                    var b = FindFree();
                    if (b != null)
                    {
                        b.Transform.Position = drawableActor.Transform.Position 
                            + drawableActor.Transform.CaclForward() * fwdBulletSpawnScale;
                        b.Transform.Rotation = (float)Math.Atan2(direction.Y, direction.X) * Constants.Rad2Deg + 90;
                        b.Direction = (direction);
                        b.Speed = BulletSpeed;
                        b.SetActive(true);
                    }
                }
                _fireRate = FireRate;
            }
        }
        public override void Update()
        {
            if(_fireRate >= 0)
            {
                _fireRate -= Engine.EngineCore.DeltaTime;
            }
        }
        private Bullet FindFree()
        {
            foreach(var x in bulletsPull)
            {
                if (!x.IsActive())
                {
                    return x;
                }
            }
            return null;
        }
    }
}
