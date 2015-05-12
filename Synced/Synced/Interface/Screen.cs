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
    class Screen : DrawableGameComponent, IDrawableObject
    {
        public GameComponentCollection GameComponents { get; private set; }

        public Screen(Game game) :base(game)
        {
            GameComponents = new GameComponentCollection();
        }


        public override void Initialize()
        {
            foreach (GameComponent gc in this.GameComponents)
                gc.Initialize();
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

        public Vector2 Position
        {
            get;
            protected set;
        }

        public Texture2D Texture
        {
            get;
            protected set;
        }
    }
}
