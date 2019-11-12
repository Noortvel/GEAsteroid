using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Core.Resources
{
    public class Textures
    {
        public Textures()
        {
            Atlas1 = new Texture(Resources.ContentPath + "Textures\\AtlasF.png");
        }
        public Texture Atlas1
        {
            private set;
            get;
        }
    }
}
