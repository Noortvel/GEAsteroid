using GEAsteroid.Core.Input;
using GEAsteroid.Core.Render;
using GEAsteroid.Engine;
using SFML.Graphics;
using System;
using System.Diagnostics;


namespace GEAsteroid.Core
{
    static class Application
    {
        public static int Width
        {
            private set;
            get;
        }
        public static int Height
        {
            private set;
            get;
        }
        static void Main()
        {
            Width = 1280;
            Height = 720;
            RenderWindow renderWindow = 
                new RenderWindow(new SFML.Window.VideoMode((uint)Width,(uint)Height), "windoname",  SFML.Window.Styles.Close);
            renderWindow.Closed += (obj, e)=> renderWindow.Close();

            Stopwatch stopwatch = new Stopwatch();

            Resources.Resources.Load("..\\..\\..\\Content\\");
            RenderAdapter render = new RenderAdapter();
            Debug.SetRenderBridge(render);
            Inputs input = new Inputs();

            EngineCore.Start();

            float deltaTime = 0;
            stopwatch.Start();
            
            while (renderWindow.IsOpen)
            {
                renderWindow.DispatchEvents();
                renderWindow.Clear(Color.Black);
                renderWindow.Draw(render.SFMLDrawable);

                input.InputUpdate();
                EngineCore.Update(deltaTime);
                EngineCore.Draw(render);

                renderWindow.Display();

                Debug.ClearDraww();
                
                stopwatch.Stop();
                deltaTime = (float)stopwatch.Elapsed.TotalSeconds;
                stopwatch.Reset();
                stopwatch.Start();
            }

        }
    }
}
