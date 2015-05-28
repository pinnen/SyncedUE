// ScreenManager.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson 
using Microsoft.Xna.Framework;
using Synced.Content;
using Synced.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Synced.Static_Classes
{
    /// <summary>
    /// Singelton sealed ScreenManager, base class DrawableGameComponent
    /// </summary>
    sealed class ScreenManager : DrawableGameComponent
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
            //------------------------------------------------------------------------
            // *********************Loads menu screen*********************************
            //Loads the menu screen and put in the stack
            //------------------------------------------------------------------------
            _screenManager.MenuScreen = new MenuScreen(game);
            _screenManager.MenuScreen.NewGame += Instance.NewGameEvent;
            _screenManager.AddScreen(_screenManager.MenuScreen);
            _screenManager.Screens.Peek().Deactivated();

            //------------------------------------------------------------------------
            // **********************Splash screens***********************************
            //------------------------------------------------------------------------
            //Second splash screen
            Screen screen2 = new SplashScreen(Library.Screens.SplashSeven, game);
            screen2.Deactivated();
            _screenManager.AddScreen(screen2);

            //First splash screen
            Screen screen = new SplashScreen(Library.Screens.SplashAlpha, game);
            screen.Activated();
            _screenManager.AddScreen(screen);
            _screenManager.CurrentState = ScreenState.SplashScreen;

            //------------------------------------------------------------------------
            // **********************Game Screen**************************************
            //FOR DEBUG
            //------------------------------------------------------------------------
            //_screenManager.AddScreen(Instance.GameScreen);
            //TODO: Add this functionality properly. 
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
            screen.OnScreenExit += ScreenManager.Instance.Screen_OnScreenExit;
            screen.OnActivated += ScreenManager.Instance.Screen_OnActivated;
            screen.OnDeactivated += ScreenManager.Instance.Screen_OnDeactivated;
            screen.OnTransition += ScreenManager.Instance.Screen_OnScreenTransition;
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
                //if (ScreenManager.Instance.Screens.Count < 1)
                //{
                //    //If we have no more screens we add a menu screen to back it up.
                //    ScreenManager.Instance.AddScreen(Instance.MenuScreen);
                //    ScreenManager.Instance.Screens.Peek().Activated();
                //    ScreenManager.Instance.CurrentState = ScreenState.MenuScreen;
                //    return null;
                //}
                ScreenManager.Instance.Screens.Peek().Deactivated(); //NOT SURE WHAT TO DO HERE.
                Screen prev = ScreenManager.Instance.Screens.Pop(); // RETURN OR NOT RETURN IS THE QUESTION
                if (CurrentScreen != null)
                    ScreenManager.Instance.Screens.Peek().Activated();

                return prev;
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

        private void Screen_OnScreenExit(Screen screen, EventArgs e)
        {
            Pop();
            if (Screens.Count < 1)
            {
                CurrentState = ScreenState.MenuScreen;
                AddScreen(new MenuScreen(Game));
                Screens.Peek().Activated();
                (Screens.Peek() as MenuScreen).NewGame += Instance.NewGameEvent;
            }
            else if (Screens.Count == 1)
            {
                CurrentState = ScreenState.MenuScreen;
            }
         
        }

        private void OnActivated(Screen screen, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NewGameEvent(MenuScreen screen, EventArgs e)
        {
            _screenManager.Screens.Pop();
            _screenManager.AddScreen(new GameScreen(Game,screen.SelectedCharacter));
            Screens.Peek().Activated();
            (Screens.Peek() as GameScreen).GameEnded += ScreenManager_GameEnded;
            CurrentState = ScreenState.GameScreen;
        }

        void ScreenManager_GameEnded(Library.Colors.ColorName color, EventArgs e)
        {
            (Screens.Peek() as GameScreen).ResetGame();
            Pop();
            AddScreen(new WinScreen(Game, color));
            Screens.Peek().Activated();
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
                    if (Screens.Peek() is GameScreen)
                    {
                        //Screens.Pop();
                        //Screens.Peek().Activated();
                        //CurrentState = ScreenState.MenuScreen;
                    }
                    else
                    {
                        //Screens.Pop();
                    }
                    
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

        public MenuScreen MenuScreen
        {
            get;
            private set;
        }
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
