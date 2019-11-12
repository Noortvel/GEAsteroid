using GEAsteroid.Asteroids.Player;
using GEAsteroid.Core.Extensions;
using GEAsteroid.Core.Resources;
using GEAsteroid.Engine.Data;
using GEAsteroid.GameFraemwork;
using GEAsteroid.GameFraemwork.ActorComponents.Base;
using System;
using System.Numerics;

namespace GEAsteroid.Asteroids
{
    public class GunLaser : ActorComponent
    {
        public float ReloadTime
        {
            set;
            get;
        }
        public float RayTime
        {
            set;
            get;
        }
        public LaserRay LaserRay
        {
            private set;
            get;
        }
        private MainAsteroidsScene scene;
        public GunLaser(MainAsteroidsScene scene, SpaceShip actor, float reloadTime, float rayTime) : base(actor)
        {
            this.scene = scene;
            ReloadTime = reloadTime;
            RayTime = rayTime;
            LaserRay = new LaserRay(scene, actor);
            AddSubObject(LaserRay);
            LaserRay.SetActive(false);
        }
        private bool isReadyShoot = true;
        private float _reloadTime = -1;
        private float _rayTime = -1;
        public void Shoot()
        {
            if (isReadyShoot)
            {
                isReadyShoot = false;
                scene.LaserCounterText = "";
                TimerInvoker.InvokeTroughtTime(ReloadTime, Refresh);
                LaserRay.SetActive(true);
                TimerInvoker.InvokeTroughtTime(RayTime, () => LaserRay.SetActive(false));
            }
        }
        public void Refresh()
        {
            LaserRay.SetActive(false);
            isReadyShoot = true;
            scene.LaserCounterText = "I";
        }
    }
}
