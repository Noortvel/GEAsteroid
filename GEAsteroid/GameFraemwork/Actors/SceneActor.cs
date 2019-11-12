using GEAsteroid.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GEAsteroid.GameFraemwork.Actors
{
    public abstract class SceneActor : Actor
    {
        public Transform2 Transform
        {
            private set;
            get;
        }
        public SceneActor(Scene scene) : base(scene)
        {
            Transform = new Transform2();
            Transform.Scale = new System.Numerics.Vector2(1, 1);
        }
    }
}
