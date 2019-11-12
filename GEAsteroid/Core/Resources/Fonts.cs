using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Core.Resources
{
    public class Fonts
    {
        public Font BigShouldersText {
            private set;
            get;
        }
        public Fonts()
        {
            BigShouldersText = new Font(Resources.ContentPath + "Fonts\\" + "BigShouldersText-Black.ttf");
        }
    }
}
