﻿// MenuScreen.cs
// Introduced: 2015-04-16
// Last edited: 2015-04-29
// Edited by:
// Pontus Magnusson
//
// 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
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
            int w = ResolutionManager.GetWidth / 2;
            int h = ResolutionManager.GetHeight / 2;

            // Add character selectors
            _characterSelectors = new List<CharacterSelector>();
            _characterSelectors.Add(new CharacterSelector(PlayerIndex.One, new Rectangle(0, 0, w, h), Color.Blue, Game));
            _characterSelectors.Add(new CharacterSelector(PlayerIndex.Two, new Rectangle(w, 0, w, h), Color.Green, Game));
            _characterSelectors.Add(new CharacterSelector(PlayerIndex.Three, new Rectangle(0, h, w, h), Color.Red, Game));
            _characterSelectors.Add(new CharacterSelector(PlayerIndex.Four, new Rectangle(w, h, w, h), Color.Yellow, Game));

            Game.Components.Add(_background);
            Game.Components.Add(this);
        }

        public bool IsEveryoneReady()
        {
            return (_characterSelectors.Where(p => p.IsReady()).Count() >= _minimumPlayersConstant);
        }
    }
}