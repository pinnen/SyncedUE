using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SevenEngine.State
{
    public sealed class ScreenManager : DrawableGameComponent
    {
        #region Delegates & Events & Raisers
        //TODO: FIX EVENTS AND DELEGATES FOR THIS SCREENMANAGER
        #endregion

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
            {
                _screenManager = new ScreenManager(game);
                _screenManager.Initialize();
            }
            if (_screenManager.Screens.Count >= 1)
                _screenManager.Screens.Clear();
        }

        /// <summary>
        /// Check if the ScreenManager have been properly initialized in the game components
        /// </summary>
        public static bool Initialized
        {
            get;
            private set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Private constructor of ScreenManager
        /// </summary>
        /// <param name="game"></param>
        private ScreenManager(Game game) : base(game) { }
        #endregion

        #region Screens Properties and Methods
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
            if (!Initialized || screen == null)
                return;

            //Binds events to screen
            //screen.OnExit += ScreenManager.Instance.Screen_OnScreenExit;
            //screen.OnActivated += ScreenManager.Instance.Screen_OnActivated;
            //screen.OnDeactivated += ScreenManager.Instance.Screen_OnDeactivated;
            //screen.OnTransition += ScreenManager.Instance.Screen_OnScreenTransition;
            //screen initialize components
            screen.Initialize();
            //adds screen to screen stack
            Screens.Push(screen);
        }


        public void AddScreen(List<Screen> screens)
        {
            screens.ForEach(AddScreen);
        }
        public void AddScreen(Screen[] screens)
        {
            Array.ForEach(screens, AddScreen);
        }

        public static Screen Pop()
        {
            if (Initialized)
            {
                ScreenManager.Instance.Screens.Peek().OnExitScreen(new EventArgs());
                return ScreenManager.Instance.Screens.Pop(); 
            }
            return null;
        }

        public static Screen CurrentScreen
        {
            get
            {
                if (ScreenManager.Instance.Screens.Count >= 1)
                    return ScreenManager.Instance.Screens.Peek();
                else return null;
            }
        }

        #endregion

        #region ScreenManager Events & Raisers
        private void Screen_OnScreenTransition(Screen screen, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Screen_OnDeactivated(Screen screen, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Screen_OnActivated(Screen screen, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Screen_OnScreenExit(EventArgs e)
        {
            Pop();
        }

        private void OnActivated(Screen screen, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void HandleBackEvent()
        {
            switch (ScreenManager.Instance.CurrentState)
            {
                case ScreenState.SplashScreen:
                    //Pop();
                    //if (Screens.Count <= 1)
                    //    CurrentState = ScreenState.MenuScreen;

                    break;
                case ScreenState.MenuScreen:
                    Game.Exit();
                    break;
                case ScreenState.GameScreen:
                    //TODO: From game to menu
                    //if (Screens.Peek() is GameScreen)
                    //{
                    //    //Screens.Pop();
                    //    //Screens.Peek().Activated();
                    //    //CurrentState = ScreenState.MenuScreen;
                    //}
                    //else
                    //{
                    //    //Screens.Pop();
                    //}

                    break;
                default:
                    break;
            }

        }
        #endregion
        #region ScreenManager Properties
        public ScreenState CurrentState
        {
            get;
            private set;
        }
        public enum ScreenState { SplashScreen, MenuScreen, GameScreen }

        #endregion

        #region DrawableGameComponent
        public override void Initialize()
        {
            ScreenManager.Initialized = true;
        }

        public override void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var item in Screens.OfType<IDrawable>().Where<IDrawable>(x => x.Visible))
                item.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            Screens.Clear();
            base.Dispose(disposing);
        }
        #endregion
    }
}
