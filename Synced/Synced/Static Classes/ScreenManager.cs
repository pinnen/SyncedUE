using Microsoft.Xna.Framework;
using Synced.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Static_Classes
{
    static class ScreenManager
    {
        public static Stack<Screen> Screens
        {
            get
            {
                if (Screens == null)
                    _screens = new Stack<Screen>();
                return _screens;
            }
        }
        private static Stack<Screen> _screens;

        public static int Count
        {
            get
            {
                return _screens.Count;
            }
        }

        public static bool Initialized
        {
            get;
            private set;
        }

        public static void AddScreen(Screen screen)
        {
            Screens.Push(screen);
        }
        public static void AddScreen(List<Screen> screens)
        {
            screens.ForEach(Screens.Push);
        }
        public static void AddScreen(Screen[] screens)
        {
            Array.ForEach(screens, Screens.Push);
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
