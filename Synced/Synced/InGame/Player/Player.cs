using Microsoft.Xna.Framework;
using Synced.InGame.Player;
// Player.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson
//
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Player
{
    class Player : GameComponent
    {
        Unit _left;
        Unit _right;

        public Player(Library.Character.Name character, Game game)
            : base(game)
        {
            _left = new Unit(Library.Character.GamePath[character], game);
            _right = new Unit(Library.Character.GamePath[character], game);
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
