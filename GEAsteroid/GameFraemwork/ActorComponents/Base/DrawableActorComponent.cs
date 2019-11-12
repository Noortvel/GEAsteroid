using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEAsteroid.Core.Render;
using GEAsteroid.GameFraemwork.Actors;

namespace GEAsteroid.GameFraemwork.ActorComponents.Base
{
    public abstract class DrawableActorComponent : ActorComponent, IRenderable
    {
        public DrawableActorComponent(Actor actor) : base(actor)
        {

        }
        public abstract void Draw(RenderAdapter render);
    }
}
