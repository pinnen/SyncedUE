using Microsoft.Xna.Framework;
using Synced.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Static_Classes
{
    static class ScreenManager : DrawableGameComponent
    {
        public static Stack<Screen> Screens
        {
            get;
            set;
        }
        public static Screen ActiveScreen
        {
            get { return Screens.Peek(); }
        }

        public static Screen Pop()
        {
            if (Screens.Count < 1)
                return null;
            Screen prev = Screens.Pop();
            if (ActiveScreen != null)
                ActiveScreen.Activated();

            return prev;
        }
    }
}
