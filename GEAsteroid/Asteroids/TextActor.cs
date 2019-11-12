using GEAsteroid.Core.Extensions;
using GEAsteroid.Core.Render;
using GEAsteroid.GameFraemwork;
using GEAsteroid.GameFraemwork.Actors;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Asteroids
{
    public class TextActor : DrawableActor
    {
        private Text text;
        public TextActor(Scene scene) : base(scene)
        {
            text = new Text();
            text.Font = Core.Resources.Resources.Fonts.BigShouldersText;
            text.CharacterSize = 50;
            text.FillColor = Color.White;
        }
        public string Text
        {
            set
            {
                text.DisplayedString = value;
            }
            get
            {
                return text.DisplayedString;
            }
        }
        public override void Draw(RenderAdapter render)
        {
            text.Position = text.Position = Transform.Position.ToVector2f();
            base.Draw(render);
            render.Draw(text);
        }
        ~TextActor()
        {
            text.Dispose();
        }
    }
}
