using GEAsteroid.Core.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Engine.Input
{
    public static class InputEvents
    {
        public readonly static InputEvent KeyBoard_W = new InputEvent();
        public readonly static InputEvent KeyBoard_S = new InputEvent();
        public readonly static InputEvent KeyBoard_A = new InputEvent();
        public readonly static InputEvent KeyBoard_D = new InputEvent();
        public readonly static InputEvent KeyBoard_SPACE = new InputEvent();
        public readonly static InputEvent KeyBoard_K = new InputEvent();
        public readonly static InputEvent KeyBoard_LCTRL = new InputEvent();
        public readonly static InputEvent KeyBoard_I = new InputEvent();

        static InputEvents()
        {
            Inputs.AddKeyBoardEvent(KeyBoard_W, SFML.Window.Keyboard.Key.W);
            Inputs.AddKeyBoardEvent(KeyBoard_A, SFML.Window.Keyboard.Key.A);
            Inputs.AddKeyBoardEvent(KeyBoard_S, SFML.Window.Keyboard.Key.S);
            Inputs.AddKeyBoardEvent(KeyBoard_D, SFML.Window.Keyboard.Key.D);
            Inputs.AddKeyBoardEvent(KeyBoard_SPACE, SFML.Window.Keyboard.Key.Space);
            Inputs.AddKeyBoardEvent(KeyBoard_K, SFML.Window.Keyboard.Key.K);
            Inputs.AddKeyBoardEvent(KeyBoard_LCTRL, SFML.Window.Keyboard.Key.LControl);
            Inputs.AddKeyBoardEvent(KeyBoard_I, SFML.Window.Keyboard.Key.I);
        }
    }
}
