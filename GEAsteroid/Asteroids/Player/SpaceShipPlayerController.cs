using System;
using GEAsteroid.Engine.Input;
using GEAsteroid.GameFraemwork.ActorComponents.Base;

namespace GEAsteroid.Asteroids
{
    class SpaceShipPlayerController : ActorComponent
    {
        private SpaceShip ship;
        public SpaceShipPlayerController(SpaceShip actor) : base(actor)
        {
            ship = actor;
            InputEvents.KeyBoard_W.KeyPressed += ship.MoveForwardStart;
            InputEvents.KeyBoard_W.KeyReleased += ship.MoveForwardStop;

            InputEvents.KeyBoard_A.KeyPressed += () => ship.RotateStart(SpaceShip.RotateDirection.Left);
            InputEvents.KeyBoard_A.KeyReleased += () => ship.RotateStop(SpaceShip.RotateDirection.Left);

            InputEvents.KeyBoard_D.KeyPressed += () => ship.RotateStart(SpaceShip.RotateDirection.Right);
            InputEvents.KeyBoard_D.KeyReleased += () => ship.RotateStop(SpaceShip.RotateDirection.Right);

            InputEvents.KeyBoard_K.KeyPressed += () => ship.Gun.SpawnBullet();
            InputEvents.KeyBoard_I.KeyPressed += () => ship.GunLaser.Shoot();

        }
    }
}
