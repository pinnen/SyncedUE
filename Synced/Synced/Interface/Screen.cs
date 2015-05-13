// CharacterSelector.cs
// Introduced: 2015-04-29
// Last edited: 2015-04-30
// Edited by:
// Robin Calmegård
//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Interface
{
    abstract class Screen : DrawableGameComponent, IDrawableObject
    {
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
        #endregion


        public Screen(Game game) :base(game)
        {
            GameComponents = new GameComponentCollection();
        }


        public override void Initialize()
        {
            foreach (GameComponent gc in this.GameComponents)
                gc.Initialize();
            Initialized = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
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
            //TODO: Need to draw itself

            //Draws every component in GameComponents
            foreach (IDrawable gc in this.GameComponents.OfType<IDrawable>().Where<IDrawable>(x => x.Visible).OrderBy<IDrawable, int>(x => x.DrawOrder))
                gc.Draw(gameTime);
            base.Draw(gameTime);
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
