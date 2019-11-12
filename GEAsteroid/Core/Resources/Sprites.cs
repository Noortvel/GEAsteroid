using SFML.Graphics;

namespace GEAsteroid.Core.Resources
{
    public class Sprites
    {
        public SpriteInfo SpaceShip
        {
            private set;
            get;
        }
        public SpriteInfo Asteroid1
        {
            private set;
            get;
        }
        public SpriteInfo UFO
        {
            private set;
            get;
        }
        public SpriteInfo Laser
        {
            private set;
            get;
        }
        public SpriteInfo Bullet
        {
            private set;
            get;
        }
        public Sprites()
        {
            SpaceShip = new SpriteInfo(Resources.Textures.Atlas1, new IntRect(307, 27, 18, 38));
            Asteroid1 = new SpriteInfo(Resources.Textures.Atlas1, new IntRect(14, 14, 77, 60));
            UFO = new SpriteInfo(Resources.Textures.Atlas1, new IntRect(359, 22, 53, 35));
            Laser = new SpriteInfo(Resources.Textures.Atlas1, new IntRect(460, 4, 45, 84));
            Bullet = new SpriteInfo(Resources.Textures.Atlas1, new IntRect(310,88,12,15));
        }
    }
}
