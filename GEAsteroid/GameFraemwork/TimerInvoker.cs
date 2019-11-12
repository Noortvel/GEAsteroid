using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GEAsteroid.GameFraemwork
{
    public static class TimerInvoker
    {
        private class TmAct
        {
            public float time;
            public Action act;

        }
        private static List<TmAct> tasks = new List<TmAct>();
        public static void Update()
        {
            if (tasks.Count > 0)
            {
                tasks.RemoveAll((obj) =>
                {
                    if (obj.time <= 0)
                    {
                        obj.act();
                        return true;
                    }
                    obj.time -= Engine.EngineCore.DeltaTime;
                    return false;
                });
            }
            
            //for (int i = 0; i < tasks.Count; i++)
            //{

            //    tasks[i].time -= Engine.EngineCore.DeltaTime;
            //    if (tasks[i].time < 0)
            //    {
            //        tasks[i].act();
            //        tasks.RemoveAt(i);
            //    }
            //}
        }
        public static void InvokeTroughtTime(float sec, Action act)
        {
            tasks.Add(new TmAct { time = sec, act = act });
        }
        public static void CancelAll()
        {
            tasks.Clear();
        }
     

    }
}
