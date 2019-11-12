using System.Numerics;

namespace GEAsteroid.Core.Extensions
{
    public static class Vector2Extensions
    {
        public static SFML.System.Vector2f ToVector2f(this Vector2 vector)
        {
            return new SFML.System.Vector2f(vector.X, vector.Y);
        }
        public static Vector2 ToVector2(this SFML.System.Vector2f vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
    }
}
