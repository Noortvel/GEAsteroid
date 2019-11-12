using GEAsteroid.GameFraemwork.ActorComponents.Base;
using System;
using System.Collections.Generic;


namespace GEAsteroid.GameFraemwork.Actors
{
    public abstract class Actor : AObject, IUpdatable
    {
        private struct CompUpd
        {
            public ActorComponent comp;
            public IUpdatable updp;
        }
        private List<ActorComponent> actorComponents = new List<ActorComponent>();
        private List<CompUpd> updatablesComponents = new List<CompUpd>();
        public string Name
        {
            get;
            set;
        }
        public Scene CurrentScene
        {
            private set;
            get;
        }
        public Actor(Scene scene)
        {
            CurrentScene = scene;
            scene.AddActor(this);
        }
        public ActorComponent[] GetComponents()
        {
            return actorComponents.ToArray();
        }
        public virtual void AddComponent(ActorComponent actorComponent)
        {
            actorComponents.Add(actorComponent);
            AddSubObject(actorComponent);
            var updbl = actorComponent as IUpdatable;
            if (updbl != null)
            {
                updatablesComponents.Add(new CompUpd { comp = actorComponent, updp = updbl });
            }
        }
        public virtual void Update()
        {
            foreach (var x in updatablesComponents)
            {
                if (x.comp.IsActive())
                {
                    x.updp.Update();
                }
            }
        }
    }
}
