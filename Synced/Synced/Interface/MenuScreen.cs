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
using Synced.Content;
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

        public MenuScreen(Game game)
            : base(game)
        {
            //Menu background 
            DrawOrder = (int)DrawingHelper.DrawingLevel.Back;
            // Temporary screen variables (Half of screen)
            int w = ResolutionManager.GetCenterPointWidth;
            int h = ResolutionManager.GetCenterPointHeight;

            // Add character selectors
            GameComponents.Add(new CharacterSelector(PlayerIndex.One, new Rectangle(0, 0, w, h), Color.Blue, Game));
            GameComponents.Add(new CharacterSelector(PlayerIndex.Two, new Rectangle(w, 0, w, h), Color.Green, Game));
            GameComponents.Add(new CharacterSelector(PlayerIndex.Three, new Rectangle(0, h, w, h), Color.Red, Game));
            GameComponents.Add(new CharacterSelector(PlayerIndex.Four, new Rectangle(w, h, w, h), Color.Yellow, Game));
            // Background
            GameComponents.Add(new Sprite(Library.Interface.MenuBackground, Vector2.Zero, DrawingHelper.DrawingLevel.Back, game));
        }
        public bool IsEveryoneReady()
        {
            int count = 0;
            foreach (var item in GameComponents)
            {
                if (item is CharacterSelector)
                {
                    if ((item as CharacterSelector).IsReady())
                        count++;
                }
            }
            return (count >= _minimumPlayersConstant);
        }
    }
}
