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
        Sprite _background;

        public float SplashTime
        {
            get;
            set;
        }

        public SplashScreen(Texture2D texture, Game game) : base (game)
        {
            _background = new Sprite(texture, Vector2.Zero, DrawingHelper.DrawingLevel.Back, game);
            // Temporary screen variables (Half of screen)
            int w = ResolutionManager.GetCenterPointWidth;
            int h = ResolutionManager.GetCenterPointHeight;
            //Splash screen members

            //Adds members to game components
            Game.Components.Add(_background);
            Game.Components.Add(this);
        }

        protected override void Dispose(bool disposing)
        {
            
            base.Dispose(disposing);
        }


        public void Unload()
        {
            throw new NotImplementedException();
        }
    }
}
