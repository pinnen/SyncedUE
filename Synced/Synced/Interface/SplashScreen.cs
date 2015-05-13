// CharacterSelector.cs
// Introduced: 2015-05-13
// Last edited: 2015-05-13
// Edited by:
// Robin Calmegård
//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.InGame;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Interface
{
    class SplashScreen : Screen, IUnloadable
    {
        #region Member variables
        Sprite _background;
        #endregion

        #region Properties
        public TimeSpan SplashTime
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


        public SplashScreen(Texture2D texture, Game game) : base (game)
        {
            _background = new Sprite(texture, Vector2.Zero, DrawingHelper.DrawingLevel.Back, game);
            // Temporary screen variables (Half of screen)
            int w = ResolutionManager.GetCenterPointWidth;
            int h = ResolutionManager.GetCenterPointHeight;
            //Splash screen members

            //Adds members to game components
            GameComponents.Add(_background);
            Game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Fix fade in fade out 
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            
            base.Dispose(disposing);
        }

        public void Unload()
        {
           
        }

    }
}
