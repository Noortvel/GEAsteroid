using SFML.Graphics;
using SFML.System;

namespace GEAsteroid.Core.Render
{
    public class RenderAdapter
    {
       
        public SMFLContext SFMLDrawable
        {
            get;
        }
        public RenderAdapter()
        {
            SFMLDrawable = new SMFLContext();
        }
        public void Draw(Drawable drawable)
        {
            SFMLDrawable.target.Draw(drawable, SFMLDrawable.states);
        }
    }
}