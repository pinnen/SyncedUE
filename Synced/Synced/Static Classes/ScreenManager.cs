using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Content;
using Synced.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Static_Classes
{
    /// <summary>
    /// Singelton sealed ScreenManager, base class DrawableGameComponent
    /// </summary>
    sealed class ScreenManager : DrawableGameComponent
    {
        public enum ScreenState { SplashScreen, MenuScreen, GameScreen }
        

        #region Singelton
        private static ScreenManager _screenManager;
        public static ScreenManager Instance
        {
            get
            {
                return _screenManager;
            }
        }
        public static void InitializeScreenManager(Game game)
        {
            if (_screenManager == null)
                _screenManager = new ScreenManager(game);

            //Loads menu screen
            Screen screena = new MenuScreen(Library.Interface.MenuBackground, game);
            screena.Deactivated();
            _screenManager.Screens.Push(screena);

            //------------------------------------------------------------------------
            // Splash screens
            //------------------------------------------------------------------------
            //Second splash screen
            Screen screen2 = new SplashScreen(Library.Crystal.Texture, game);
            screen2.Deactivated();
            _screenManager.Screens.Push(screen2);
            screen2.ScreenExit += ScreenManager.Instance.OnScreenExit;

            //First splash screen
            Screen screen = new SplashScreen(Library.Interface.Arrows, game);
            screen.Activated();
            _screenManager.Screens.Push(screen);
            screen.ScreenExit += ScreenManager.Instance.OnScreenExit;
        }

       
        #endregion

        #region Constructor
        /// <summary>
        /// Private constructor of ScreenManager
        /// </summary>
        /// <param name="game"></param>
        private ScreenManager(Game game) : base(game) { }
        #endregion

        #region Screens
        /// <summary>
        /// Backgrounds screen for the applications
        /// </summary>
        public Stack<Screen> Screens = new Stack<Screen>();
        
        public int Count
        {
            get
            {
                return Screens.Count;
            }
        }
        public void AddScreen(Screen screen)
        {
            screen.Activated();
            Screens.Push(screen);
        }
        public void AddScreen(List<Screen> screens)
        {
            screens.ForEach(Screens.Push);
        }
        public void AddScreen(Screen[] screens)
        {
            Array.ForEach(screens, Screens.Push);
        }
        public static Screen Pop()
        {
            if (ScreenManager.Instance.Screens.Count < 1)
            {
                return null;
            }
            ScreenManager.Instance.Screens.Peek().Deactivated(); //NOT SURE WHAT TO DO HERE.
            Screen prev = ScreenManager.Instance.Screens.Pop(); // RETURN OR NOT RETURN IS THE QUESTION
            if (ActiveScreen != null)
                ScreenManager.Instance.Screens.Peek().Activated();

            return prev;
        }

        public static Screen ActiveScreen
        {
            get
            {
                if (ScreenManager.Instance.Screens.Count >= 1)
                    return ScreenManager.Instance.Screens.Peek();
                else return null;
            }
        }

        #endregion

        #region ScreenManager Events
        public void OnScreenExit(Screen screen, EventArgs e)
        {
            Pop();
        }
        #endregion

        public static bool Initialized
        {
            get;
            private set;
        }
        public override void Initialize()
        {
            ScreenManager.Initialized = true;
        }

        public override void Update(GameTime gameTime)
        {
            ActiveScreen.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var item in Screens)
            {
                if (item.Enabled)
                    item.Draw(gameTime);
            }
        }
        protected override void Dispose(bool disposing)
        {

        }
    }
}
