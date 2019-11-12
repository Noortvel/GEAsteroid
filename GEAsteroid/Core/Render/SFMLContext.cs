using SFML.Graphics;
using System;

namespace GEAsteroid.Core.Render
{
    public class SMFLContext : Drawable
    {
        public RenderTarget target;
        public RenderStates states;
        public void Draw(RenderTarget target, RenderStates states)
        {
            this.target = target;
            this.states = states;
        }
    }
}
