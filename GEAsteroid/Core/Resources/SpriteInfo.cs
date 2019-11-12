using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Core.Resources
{
    public class SpriteInfo
    {
        public Texture Texture
        {
            get;
        }
        public IntRect Rect
        {
            get;
        }
        public Vector2f Origin
        {
            get;
        }
        public SpriteInfo(Texture texture, IntRect rect, Vector2f origin)
        {
            Texture = texture;
            Rect = rect;
            Origin = origin;
        }
        public SpriteInfo(Texture texture, IntRect rect)
        {
            Texture = texture;
            Rect = rect;
            Origin = new Vector2f(rect.Width / 2, rect.Height / 2);
        }
        public SpriteInfo(SpriteInfo spriteInfo)
        {
            Texture = spriteInfo.Texture;
            Rect = spriteInfo.Rect;
            Origin = spriteInfo.Origin;
        }
    }

}
