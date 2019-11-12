using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.Core.Input
{
    public class InputEvent
    {
        public event Action KeyPressed = delegate { };
        public event Action KeyReleased = delegate { };
        public bool IsPressed
        {
            private set;
            get;
        }
        public void Press()
        {
            if (!IsPressed)
            {
                IsPressed = true;
                KeyPressed();
            }
        }
        public void Release()
        {
            if (IsPressed)
            {
                IsPressed = false;
                KeyReleased();
            }
        }
    }

}
