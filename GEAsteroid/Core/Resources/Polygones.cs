using GEAsteroid.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Core.Resources
{
    public class Polygones
    {
        public Polygon SpaceShip
        {
            private set;
            get;
        }
        public Polygon Bullet
        {
            private set;
            get;
        }
        public Polygon Asteroid1
        {
            private set;
            get;
        }
        public Polygon Asteroid1Middle
        {
            private set;
            get;
        }
        public Polygon Asteroid1Small
        {
            private set;
            get;
        }
        public Polygon UFO
        {
            private set;
            get;
        }
        public Polygones()
        {
            SpaceShip =
                new Polygon(
                new Vector2[3] { new Vector2(7.5f, 0), new Vector2(15, 38), new Vector2(0, 38) },
                new Vector2(7.5f, 19));
            Asteroid1 =
                new Polygon(
                    new Vector2[9] {
                            new Vector2(32,2), new Vector2(49,7), new Vector2(72, 32), new Vector2(76, 51),
                            new Vector2(58,58), new Vector2(11,45), new Vector2(3,33), new Vector2(5,6), new Vector2(16,1) },
                    new Vector2(36, 26));
            Asteroid1Middle = new Polygon(Asteroid1);
            Asteroid1Middle.Scale(new Vector2(0.6f, 0.6f));
            Asteroid1Small = new Polygon(Asteroid1);
            Asteroid1Small.Scale(new Vector2(0.3f, 0.3f));
            Bullet = new Polygon(new Vector2[4]
            {new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) });
            Bullet.Scale(new Vector2(1, 2));
            UFO = new Polygon(
                new Vector2[10] {
                new Vector2(2,13), new Vector2(12,10), new Vector2(17,1), new Vector2(35,1), new Vector2(42,11), 
                new Vector2(51,13),new Vector2(51,24), new Vector2(35,34),new Vector2(19,34),new Vector2(3,24)});
        }
    }
}
