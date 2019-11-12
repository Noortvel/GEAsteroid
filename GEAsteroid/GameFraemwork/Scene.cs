using System;
using System.Collections.Generic;
using GEAsteroid.Core;
using GEAsteroid.Core.Render;
using GEAsteroid.GameFraemwork.Actors;

namespace GEAsteroid.GameFraemwork
{
    using ActRnr = Tuple<Actor, IRenderable>;
    using ActUpd = Tuple<Actor, IUpdatable>;
    public abstract class Scene : IRenderable, IUpdatable
    {
        private List<Actor> actorsOnScene = new List<Actor>();
        private List<ActRnr> renderables = new List<ActRnr>();
        private List<ActUpd> updatables = new List<ActUpd>();

        public abstract bool IsNeedDraw { get; }
        public abstract bool IsNeedUpdate { get; }

        public virtual void Draw(RenderAdapter render)
        {
            foreach(var x in renderables)
            {
                if (x.Item1.IsActive())
                {
                    x.Item2.Draw(render);
                }
            }
        }
        public virtual void Update()
        {
            foreach (var x in updatables)
            {
                if (x.Item1.IsActive())
                {
                    x.Item2.Update();
                }
            }
        }
        public void AddActor(Actor actor)
        {
            actorsOnScene.Add(actor);
            var renerbl = actor as IRenderable;
            if (renerbl != null)
            {
                renderables.Add(new ActRnr(actor, renerbl));
            }
            var updbl = actor as IUpdatable;
            if(updbl != null)
            {
                updatables.Add(new ActUpd(actor, updbl));
            }
        }
        public void RemoveActor(Actor actor)
        {
            actorsOnScene.Remove(actor);
            var renerbl = actor as IRenderable;
            if (renerbl != null)
            {
                var r = renderables.Find((obj) => obj.Item1 == actor);
                renderables.Remove(r);
            }
            var updbl = actor as IUpdatable;
            if (updbl != null)
            {
                var u = updatables.Find((obj) => obj.Item1 == actor);
                updatables.Remove(u);
            }
        }
        public IEnumerable<Actor> GetActorsOnScene()
        {
            return actorsOnScene;
        }
    }
}
