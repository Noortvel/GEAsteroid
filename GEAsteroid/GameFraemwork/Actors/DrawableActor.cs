using GEAsteroid.Core.Render;
using GEAsteroid.GameFraemwork.ActorComponents.Base;
using System.Collections.Generic;

namespace GEAsteroid.GameFraemwork.Actors
{
    public abstract class DrawableActor : SceneActor, IRenderable
    {
        private struct CompRendr
        {
            public ActorComponent comp;
            public IRenderable rndbl;
        }
        private List<CompRendr> renderables = new List<CompRendr>();
        public DrawableActor(Scene scene) : base(scene)
        {
        }
        public override void AddComponent(ActorComponent actorComponent)
        {
            base.AddComponent(actorComponent);
            var renrdbl = actorComponent as IRenderable;
            if (renrdbl != null)
            {
                renderables.Add(new CompRendr { comp = actorComponent, rndbl = renrdbl});
            }
        }
        public virtual void Draw(RenderAdapter render)
        {
            foreach (var x in renderables)
            {
                if (x.comp.IsActive())
                {
                    x.rndbl.Draw(render);
                }
            }
        }
    }
}
