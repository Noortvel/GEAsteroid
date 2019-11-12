using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEAsteroid.Core.Extensions;
using GEAsteroid.Core.Render;
using GEAsteroid.Engine.Data;
using GEAsteroid.GameFraemwork.ActorComponents.Base;
using GEAsteroid.GameFraemwork.Actors;
using SFML.Graphics;

namespace GEAsteroid.GameFraemwork.ActorComponents
{
    public class PolygonRender : DrawableActorComponent
    {
        private Polygon polygon;
        private ConvexShape convexShape;
        private DrawableActor drawableActor;
        public PolygonRender(DrawableActor actor, Polygon polygon) : base(actor)
        {
            this.polygon = new Polygon(polygon);
            convexShape = new ConvexShape();
            var verts = this.polygon.GetVertices();
            convexShape.SetPointCount((uint)verts.Length);
            for (int i = 0; i < verts.Length; i++)
            {
                convexShape.SetPoint((uint)i, verts[i].ToVector2f());
            }
            convexShape.Origin = this.polygon.Center.ToVector2f();
            convexShape.OutlineThickness = 2;
            convexShape.OutlineColor = Color.White;
            convexShape.FillColor = Color.Transparent;
            drawableActor = actor;
            SetActive(true);
        }
        public override void Draw(RenderAdapter render)
        {
            convexShape.Position = drawableActor.Transform.Position.ToVector2f();
            convexShape.Rotation = drawableActor.Transform.Rotation;
            //convexShape.Scale = drawableActor.Transform.Scale.ToVector2f();
            render.Draw(convexShape);
        }
        ~PolygonRender()
        {
            convexShape.Dispose();
        }
    }
}
