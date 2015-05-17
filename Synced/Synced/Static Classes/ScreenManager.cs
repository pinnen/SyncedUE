using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public static Screen Pop()
        {
            if (Screens.Count < 1)
            {
                return null;
            }

            Screen prev = Screens.Pop();
            if (ActiveScreen != null)
                ActiveScreen.Activated();

            return prev;
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var item in Screens)
            {
                if (item.Enabled)
                    item.Update(gameTime);
            }
        }
        
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var item in Screens)
            {
                if (item.Enabled)
                    item.Draw(gameTime);
            }
        }

    }
}
