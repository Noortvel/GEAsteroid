using GEAsteroid.Core.Extensions;
using GEAsteroid.Core.Resources;
using GEAsteroid.Engine.Data;
using GEAsteroid.GameFraemwork.ActorComponents;
using GEAsteroid.GameFraemwork.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;


namespace GEAsteroid.Asteroids.Player
{
    public class LaserRay : DrawableActor
    {
        private PolygonCollider polygonCollider;
        public PolygonRender PolygonRender
        {
            private set;
            get;
        }
        public SpriteRender SpriteRender
        {
            private set;
            get;
        }
        private SpaceShip spaceShip;
        public LaserRay(MainAsteroidsScene scene, SpaceShip space) : base(scene)
        {
            this.spaceShip = space;
            var polygon = new Polygon(Resources.Polygones.Bullet);
            float maxDim = Math.Max(scene.Dimensions.X, scene.Dimensions.Y);
            polygon.Scale(new Vector2(4, maxDim));
            polygon.Center = new Vector2(0, maxDim);
            polygonCollider = new PolygonCollider(this, polygon, scene.collidersWorld, 0);
            polygonCollider.Tag = "Bullet";
            PolygonRender = new PolygonRender(this, polygon);

            var li = Resources.Sprites.Laser;
            var origin = new Vector2(li.Rect.Width / 2, li.Rect.Height);
            var spritInfo = new SpriteInfo(li.Texture, li.Rect, origin.ToVector2f());

            SpriteRender = new SpriteRender(this, spritInfo);
            SpriteRender.Scale = new Vector2(1, maxDim / spritInfo.Rect.Height);
            SpriteRender.SetActive(false);
        }
        public override void Update()
        {
            base.Update();
            Transform.Position = spaceShip.Transform.Position;
            Transform.Rotation = spaceShip.Transform.Rotation;
        }
    }
}
