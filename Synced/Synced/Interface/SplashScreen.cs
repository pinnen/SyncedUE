﻿// CharacterSelector.cs
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
    class SplashScreen : Screen
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
        

        #endregion


        public SplashScreen(Texture2D texture, Game game) : base (game)
        {
            _background = new Sprite(texture, Vector2.Zero, DrawingHelper.DrawingLevel.Back, game);
            // Temporary screen variables (Half of screen)
            int w = ResolutionManager.GetCenterPointWidth;
            int h = ResolutionManager.GetCenterPointHeight;
            //Splash screen members

            //TEST SPLASH SCREEN
            //How long to fade in & out
            FadeInTime = TimeSpan.FromSeconds(0.5); 
            FadeOutTime = TimeSpan.FromSeconds(0.5);

            //Adds members to game components
            GameComponents.Add(_background);
            Game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Fix fade in fade out 
            foreach (IUpdateable gc in this.GameComponents.OfType<IUpdateable>().Where<IUpdateable>(x => x.Enabled).OrderBy<IUpdateable, int>(x => x.UpdateOrder))
                gc.Update(gameTime);
            base.Update(gameTime);
        }
    }
}
