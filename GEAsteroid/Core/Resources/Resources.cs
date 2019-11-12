
namespace GEAsteroid.Core.Resources
{
    public static class Resources
    {
        public static string ContentPath
        {
            private set;
            get;
        }
        public static Textures Textures
        {
            private set;
            get;
        }
        public static Sprites Sprites
        {
            private set;
            get;
        }
        public static Polygones Polygones
        {
            private set;
            get;
        }
        public static Fonts Fonts
        {
            private set;
            get;
        }
        public static void Load(string path)
        {
            ContentPath = path;
            Fonts = new Fonts();
            Textures = new Textures();
            Sprites = new Sprites();
            Polygones = new Polygones();
        }

    }
}
