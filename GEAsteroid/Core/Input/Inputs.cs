using System;
using System.Collections.Generic;
using SFML.Window;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Core.Input
{
    public class Inputs
    {
        private struct InputEventKey
        {
            public InputEvent e;
            public Keyboard.Key key;
            public InputEventKey(InputEvent e, Keyboard.Key key)
            {
                this.e = e;
                this.key = key;
            }
        }
        private static List<InputEventKey> chekingEvents = new List<InputEventKey>();
        public static void AddKeyBoardEvent(InputEvent e, Keyboard.Key key)
        {
            chekingEvents.Add(new InputEventKey(e, key));
        }
        public void InputUpdate()
        {
            foreach(var x in chekingEvents)
            {
                if (Keyboard.IsKeyPressed(x.key))
                {
                    x.e.Press();
                } else
                {
                    x.e.Release();
                }
            }
        }
    }

}
