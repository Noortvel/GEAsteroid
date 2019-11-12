using GEAsteroid.Core.Render;
using GEAsteroid.Core.Extensions;

using SFML.Graphics;
using SFML.System;
using GEAsteroid.GameFraemwork.Collisions;

namespace GEAsteroid.Core
{
    public static class Debug
    {
        private static RenderAdapter render;
        private static CircleShape[] circles;
        private static int cIndex = 0;
        private static ConvexShape[] polygones;
        private static int pIndex = 0;

        public static void SetRenderBridge(RenderAdapter render)
        {
            Debug.render = render;
            int maxBuffer = 2000;
            circles = new CircleShape[maxBuffer];
            polygones = new ConvexShape[maxBuffer];
            for (int i = 0; i < maxBuffer; i++)
            {
                circles[i] = new CircleShape(5);
                circles[i].FillColor = Color.Red;
                polygones[i] = new ConvexShape(3);
                polygones[i].FillColor = Color.Cyan;
            }
        }
        public static void DrawPoint(Vector2f position)
        {
            if (cIndex < circles.Length)
            {
                circles[cIndex].Position = position;
                render.Draw(circles[cIndex]) ;
                cIndex++;
            }
        }
        public static void DrawPolygon(PolygonT polygon)
        {
            if(pIndex < polygones.Length)
            {
                var vert = polygon.GetVertices();
                polygones[pIndex].Position = polygon.GetPosition().ToVector2f();
                var origin = polygon.GetOrigin();
                polygones[pIndex].Origin = origin.ToVector2f();
                polygones[pIndex].SetPointCount((uint)vert.Length);
                for (int i = 0; i < vert.Length; i++)
                {
                    polygones[pIndex].SetPoint((uint)i, vert[i].ToVector2f());

                }

                render.Draw(polygones[pIndex]);
                pIndex++;
            }
        }
        public static void ClearDraww()
        {
            //Console.WriteLine("Cindex: {0}\nTindex: {1}", cIndex,tIndex);
            cIndex = 0;
            pIndex = 0;
            
        }
    }
}
