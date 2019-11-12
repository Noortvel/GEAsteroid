using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GEAsteroid.Engine;
using GEAsteroid.GameFraemwork.ActorComponents.Base;
using GEAsteroid.GameFraemwork.Actors;

namespace GEAsteroid.Asteroids
{
    public class PhysicCore : UpdatableActorComponent
    {
        private SceneActor sceneActor;

        private Vector2 _velocity;
        public Vector2 Velocity
        {
            set
            {
                _velocity = value;
                Speed = Velocity.Length();
            }
            get
            {
                return _velocity;
            }
        }
        public float Speed
        {
            private set;
            get;
        }
        public float AngularSpeed
        {
            set;
            get;
        }
        public PhysicCore(SceneActor actor) : base(actor)
        {
            _velocity = new Vector2(0, 0);
            AngularSpeed = 0;
            sceneActor = actor;
            SetActive(true);
        } 
        public void AddForce(Vector2 force)
        {
            Velocity += force;
        }
        public void AddRotationForce(float force)
        {
            AngularSpeed += force;
        }
        public override void Update()
        {
            sceneActor.Transform.Position += (Velocity * EngineCore.DeltaTime);
            sceneActor.Transform.Rotation += (AngularSpeed * EngineCore.DeltaTime);
        }
    }
}
