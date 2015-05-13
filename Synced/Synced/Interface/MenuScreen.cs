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
    class MenuScreen : Screen, IUnloadable
    {
        const int _minimumPlayersConstant = 1;

        Sprite _background;
        List<CharacterSelector> _characterSelectors;

        SpriteBatch _spriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        } 

        public MenuScreen(Texture2D texture, Game game)
            : base(game)
        {
            _background = new Sprite(texture, Vector2.Zero, DrawingHelper.DrawingLevel.Back, game);

            // Temporary screen variables (Half of screen)
            int w = ResolutionManager.GetCenterPointWidth;
            int h = ResolutionManager.GetCenterPointHeight;

            // Add character selectors
            _characterSelectors = new List<CharacterSelector>();
            _characterSelectors.Add(new CharacterSelector(PlayerIndex.One, new Rectangle(0, 0, w, h), Color.Blue, Game));
            _characterSelectors.Add(new CharacterSelector(PlayerIndex.Two, new Rectangle(w, 0, w, h), Color.Green, Game));
            _characterSelectors.Add(new CharacterSelector(PlayerIndex.Three, new Rectangle(0, h, w, h), Color.Red, Game));
            _characterSelectors.Add(new CharacterSelector(PlayerIndex.Four, new Rectangle(w, h, w, h), Color.Yellow, Game));
            //GameComponents.Add(new CharacterSelector(PlayerIndex.One, new Rectangle(0, 0, w, h), Color.Blue, Game));
            //GameComponents.Add(new CharacterSelector(PlayerIndex.Two, new Rectangle(w, 0, w, h), Color.Green, Game));
            //GameComponents.Add(new CharacterSelector(PlayerIndex.Three, new Rectangle(0, h, w, h), Color.Red, Game));
            //GameComponents.Add(new CharacterSelector(PlayerIndex.Four, new Rectangle(w, h, w, h), Color.Yellow, Game));

            Game.Components.Add(_background);
            Game.Components.Add(this);
        }
        protected override void Dispose(bool disposing)
        { 
            foreach (var c in _characterSelectors)
                Game.Components.Remove(c);
            Game.Components.Remove(_background);
            if (Game.Components.Contains(this)) Game.Components.Remove(this);
            base.Dispose(disposing);
        }

        public bool IsEveryoneReady()
        {
            return (_characterSelectors.Where(p => p.IsReady()).Count() >= _minimumPlayersConstant);
        }
        public void Unload()
        {
            _characterSelectors.ForEach(x => x.Unload());
            if (Game.Components.Contains(this)) Game.Components.Remove(this);
        }
    }
}
