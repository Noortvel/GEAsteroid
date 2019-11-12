using GEAsteroid.GameFraemwork.ActorComponents.Base;
using GEAsteroid.GameFraemwork.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Asteroids
{
    public class SceneTeleporter : UpdatableActorComponent
    {
        private SceneActor sceneActor;
        private Vector2 dimensions;
        public SceneTeleporter(SceneActor actor, Vector2 dimensions) : base(actor)
        {
            this.sceneActor = actor;
            this.dimensions = dimensions;
            SetActive(true);
        }
        public override void Update()
        {
            var pos = sceneActor.Transform.Position;
            Vector2 npos = pos; 
            if (pos.X < 0)
            {
                npos.X = dimensions.X - 1;
            }
            if(pos.X > dimensions.X)
            {
                npos.X = 1;
            }
            if (pos.Y < 0)
            {
                npos.Y = dimensions.Y - 1;
            }
            if (pos.Y > dimensions.Y)
            {
                npos.Y = 1;
            }
            sceneActor.Transform.Position = npos;
        }
    }
}
