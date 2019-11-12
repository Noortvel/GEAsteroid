using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace GEAsteroid.GameFraemwork.ActorComponents
{
    using GEAsteroid.Core.Extensions;
    using GEAsteroid.Core.Render;
    using GEAsteroid.Core.Resources;
    using GEAsteroid.GameFraemwork.ActorComponents.Base;
    using GEAsteroid.GameFraemwork.Actors;
    using SFML.System;
    using System.Numerics;

    public class SpriteRender : DrawableActorComponent
    {
        private DrawableActor sceneActor;
        private Sprite sprite;
        public Vector2 Scale
        {
            set;
            get;
        }
        public SpriteRender(DrawableActor actor, SpriteInfo texturePart) : base(actor)
        {
            sceneActor = actor;
            sprite = new Sprite(texturePart.Texture, texturePart.Rect);
            sprite.Origin = texturePart.Origin;
            Scale = sceneActor.Transform.Scale;
            SetActive(true);
        }
        public override void Draw(RenderAdapter render)
        {
            sprite.Position = sceneActor.Transform.Position.ToVector2f();
            sprite.Rotation = sceneActor.Transform.Rotation;
            sprite.Scale = Scale.ToVector2f();
            render.Draw(sprite);
        }

        ~SpriteRender()
        {
            sprite.Dispose();
        }
    }
}
