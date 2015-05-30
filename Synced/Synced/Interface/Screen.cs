// CharacterSelector.cs
// Introduced: 2015-04-29
// Last edited: 2015-04-30
// Edited by:
// Robin Calmegård
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Content;
using Synced.InGame;
using Synced.Static_Classes;
using System;
using System.Linq;

namespace Synced.Interface
{
    class ScreenEventArgs : EventArgs
    {
        ScreenManager.ScreenState State { get; set; }
    }
    abstract class Screen : DrawableGameComponent, IDrawableObject, IActive
    {
        #region Delegates & Events & Raisers 
        public delegate void OnScreenActivateEventHandler(Screen screen, EventArgs e);
        public delegate void OnScreenDeactivateEventHandler(Screen screen, EventArgs e);
        public delegate void OnScreenTransitionEventHandler(Screen screen, EventArgs e);
        public delegate void OnScreenExitEventHandler(Screen screen, EventArgs e);
        //Special event for new game 
        public delegate void StartNewGame(MenuScreen screen, EventArgs e);

        public delegate void EndGame(Library.Colors.ColorName color, EventArgs e);

        public event OnScreenActivateEventHandler OnActivated;
        public event OnScreenDeactivateEventHandler OnDeactivated;
        public event OnScreenTransitionEventHandler OnTransition;
        public event OnScreenExitEventHandler OnScreenExit;

        protected void OnExitScreen(Screen screen, EventArgs e)
        {
            if (OnScreenExit!= null)
                OnScreenExit(screen, e);
        }
        protected void OnActivadedScreen(Screen screen, EventArgs e)
        {
            if (OnActivated != null)
                OnActivated(screen, e);
        }
        protected void OnDeactivadedScreen(Screen screen, EventArgs e)
        {
            if (OnDeactivated != null)
                OnDeactivated(screen, e);
        }
        protected void OnTransitionScreen(Screen screen, EventArgs e)
        {
            if (OnTransition!=null)
                OnTransition(screen, e);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Screen Position
        /// </summary>
        public Vector2 Position
        {
            get;
            protected set;
        }

        /// <summary>
        /// Screen texture
        /// </summary>
        public Texture2D Texture
        {
            get;
            protected set;
        }

        /// <summary>
        /// List of Components that this screen contains
        /// </summary>
        public GameComponentCollection GameComponents
        {
            get;
            protected set;
        }

        /// <summary>
        /// Used to check if this screen is Initialized.
        /// </summary>
        public bool Initialized
        { 
            get; 
            private set;
        }

        public bool IsPopUp
        {
            get;
            set;
        }

        public TimeSpan FadeOutTime
        {
            get;
            set;
        }

        public TimeSpan FadeInTime
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public Screen(Game game)
            : base(game)
        {
            GameComponents = new GameComponentCollection();

        }
        #endregion
        
        #region DrawableGameComponents Methods
        public override void Initialize()
        {
            foreach (DrawableGameComponent gc in this.GameComponents)
                gc.Initialize();
            Initialized = true;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Updates every component in GameComponents
            foreach (IUpdateable gc in this.GameComponents.OfType<IUpdateable>().Where<IUpdateable>(x => x.Enabled).OrderBy<IUpdateable, int>(x => x.UpdateOrder))
                gc.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Draws every component in GameComponents
            foreach (IDrawable gc in this.GameComponents.OfType<IDrawable>().Where<IDrawable>(x => x.Visible).OrderBy<IDrawable, int>(x => x.DrawOrder))
                gc.Draw(gameTime);
            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            GameComponents.Clear();
            base.Dispose(disposing);
        }
        #endregion

        #region IActive
        /// <summary>
        /// Virtual method, this method is called when a screen is activated 
        /// </summary>
        public virtual void Activated()
        {
            this.Enabled = true;
            this.Visible = true;
            foreach (DrawableGameComponent gc in GameComponents)
            {
                gc.Enabled = true;
                gc.Visible = true;
            }
        }

        /// <summary>
        /// Virtual method, this method is called when a screen is deactivated 
        /// </summary>
        public virtual void Deactivated()
        {
            this.Enabled = false;
            this.Visible = false;
            foreach (DrawableGameComponent gc in GameComponents)
            {
                gc.Enabled = false;
                gc.Visible = false;
            }
        }
        #endregion
    }
}
