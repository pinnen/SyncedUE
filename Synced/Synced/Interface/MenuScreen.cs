// MenuScreen.cs
// Introduced: 2015-04-16
// Last edited: 2015-04-29
// Edited by:
// Pontus Magnusson
//
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
    class MenuScreen : Screen
    {
        const int _minimumPlayersConstant = 1;

        Sprite _background;

        SpriteBatch _spriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        } 

        public MenuScreen(Texture2D texture, Game game)
            : base(game)
        {
            //_background = new Sprite(texture, Vector2.Zero, DrawingHelper.DrawingLevel.Back, game);
            //_background.Enabled = false;
            //_background.Visible = false;
            GameComponents.Add(new Sprite(texture, Vector2.Zero, DrawingHelper.DrawingLevel.Back, game));


            // Temporary screen variables (Half of screen)
            int w = ResolutionManager.GetCenterPointWidth;
            int h = ResolutionManager.GetCenterPointHeight;

            // Add character selectors
            //_characterSelectors = new List<CharacterSelector>();

            GameComponents.Add(new CharacterSelector(PlayerIndex.One, new Rectangle(0, 0, w, h), Color.Blue, Game));
            GameComponents.Add(new CharacterSelector(PlayerIndex.Two, new Rectangle(w, 0, w, h), Color.Green, Game));
            GameComponents.Add(new CharacterSelector(PlayerIndex.Three, new Rectangle(0, h, w, h), Color.Red, Game));
            GameComponents.Add(new CharacterSelector(PlayerIndex.Four, new Rectangle(w, h, w, h), Color.Yellow, Game));
            //foreach (CharacterSelector characterSelect in _characterSelectors)
            //{
            //    characterSelect.Enabled = false;
            //    characterSelect.Visible = false;
            //}
            GameComponents.Add(_background);
            Game.Components.Add(this);
        }
        
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void Dispose(bool disposing)
        { 
            base.Dispose(disposing);
        }

        public bool IsEveryoneReady()
        {
            return true;
        }
        public override void Activated()
        {
            _background.Enabled = true;
            _background.Visible = true;
            base.Activated();
   
        }

        public override void Deactivated()
        {

            base.Deactivated();
        }
    }
}
