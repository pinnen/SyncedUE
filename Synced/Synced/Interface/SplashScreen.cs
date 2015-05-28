// CharacterSelector.cs
// Introduced: 2015-05-13
// Last edited: 2015-05-13
// Edited by:
// Robin Calmegård
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Static_Classes;
using System;

namespace Synced.Interface
{
    class SplashScreen : Screen
    {
        #region Member variables

        #endregion

        #region Properties
        public TimeSpan SplashTime
        {
            get;
            set;
        }


        #endregion


        public SplashScreen(Texture2D texture, Game game)
            : base(game)
        {
            //How long to fade in & out
            DrawOrder = (int)DrawingHelper.DrawingLevel.Top;
            FadeInTime = TimeSpan.FromSeconds(0.5);
            FadeOutTime = TimeSpan.FromSeconds(0.5);
            SplashTime = TimeSpan.FromSeconds(2.1);
            // Temporary screen variables (Half of screen)
            int w = ResolutionManager.GetCenterPointWidth;
            int h = ResolutionManager.GetCenterPointHeight;
            GameComponents.Add(new Sprite(texture, new Vector2(w, h), Color.White, DrawingHelper.DrawingLevel.Top, true, game));
            
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Fix fade in fade out 
            SplashTime -= gameTime.ElapsedGameTime;

            if (SplashTime < TimeSpan.Zero)
            {
                OnExitScreen(this, new EventArgs());
            }
            base.Update(gameTime);
        }
    }
}
