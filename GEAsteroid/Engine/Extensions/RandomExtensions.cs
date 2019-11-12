using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Engine.Extensions
{
    public static class RandomExtensions
    {
        public static float NextSingle(this Random random, float min, float max)
        {
            float dt = max - min;
            float val = ((float)random.NextDouble() * dt) + min;
            return val;
        }
    }
}
