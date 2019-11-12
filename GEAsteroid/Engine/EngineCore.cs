using GEAsteroid.Asteroids;
using GEAsteroid.Core.Render;
using GEAsteroid.GameFraemwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Engine
{
    public static class EngineCore
    {
        private static Scene currentScene;
        public static float DeltaTime
        {
            private set;
            get;
        }
        public static void Start()
        {
            currentScene = new MainAsteroidsScene();
        }
        public static void Update(float dt)
        {
            DeltaTime = dt;
            if (currentScene.IsNeedUpdate)
            {
                currentScene.Update();
            }
            TimerInvoker.Update();
        }
        public static void Draw(RenderAdapter renderBridge)
        {
            if (currentScene.IsNeedDraw)
            {
                currentScene.Draw(renderBridge);
            }
        }
    }
}
