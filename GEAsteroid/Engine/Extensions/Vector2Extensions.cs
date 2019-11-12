using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Engine.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 Normalized(this Vector2 vector)
        {
            float len = vector.Length();
            if (len > 0)
            {
                vector.X /= len;
                vector.Y /= len;
            }
            return vector;
        } 
    }
}
