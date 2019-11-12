using GEAsteroid.GameFraemwork.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.GameFraemwork.ActorComponents.Base
{
    public abstract class UpdatableActorComponent : ActorComponent, IUpdatable
    {
        public UpdatableActorComponent(Actor actor) : base(actor)
        {

        }
        public abstract void Update();
    }
}
