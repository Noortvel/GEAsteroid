using GEAsteroid.GameFraemwork.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.GameFraemwork.ActorComponents.Base
{

    public abstract class ActorComponent : AObject
    {
        public Actor Actor
        {
            private set;
            get;
        }
        public ActorComponent(Actor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException(nameof(actor));
            }
            Actor = actor;
            actor.AddComponent(this);
        }
    }
}
