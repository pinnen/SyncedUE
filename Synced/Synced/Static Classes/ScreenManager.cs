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
        public static Stack<Screen> Screens = new Stack<Screen>();

        public static int Count
        {
            get
            {
                return Screens.Count;
            }
        }

        public static bool Initialized
        {
            get;
            private set;
        }
        public static void InitializeScreenManager(Screen screen)
        {
            Initialized = true;
            screen.Activated();
            Screens.Push(screen);

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
            get
            {
                if (Screens.Count < 0) return Screens.Peek();
                else return null;
            }
        }

        /// <summary>
        /// Pops the current screen and returns it if needed.
        /// </summary>
        /// <returns>Previous screen</returns>
        public static Screen Pop()
        {
            if (Screens.Count < 1)
            {
                return null;
            }
            Screens.Peek().Deactivated(); //NOT SURE WHAT TO DO HERE.
            Screen prev = Screens.Pop(); // RETURN OR NOT RETURN IS THE QUESTION
            if (ActiveScreen != null)
                Screens.Peek().Activated();

            return prev;
        }


    }
}
