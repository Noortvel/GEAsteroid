using System;
using System.Numerics;
using GEAsteroid.Engine.Mathn;


namespace GEAsteroid.Engine.Data
{
    public class Transform2
    {
        public Vector2 Position
        {
            set;
            get;
        }
        public float Rotation
        {
            set;
            get;
        }
        public Vector2 Scale
        {
            set;
            get;
        }
        public Vector2 Origin
        {
            set;
            get;
        }
        public Vector2 CaclForward()
        {

            float rad = Constants.Deg2Rad * Rotation;
            float sin = (float)Math.Sin(rad);
            float cos = (float)Math.Cos(rad);
            Vector2 fwd = Vector2.Normalize(new Vector2(sin, -cos));
            return fwd;
        }
        public Transform2()
        {
            Scale = new Vector2(1, 1);
        }
    }
}
