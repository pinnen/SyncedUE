// Player.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-30
// Edited by:
// Pontus Magnusson
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Synced.Content;
using Synced.Static_Classes;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;

namespace Synced.Actors
{
    class Player : GameComponent
    {
        public Unit Left
        { get; set; }

        public Unit Right
        {
            get;
            set;
        }

        PlayerIndex _playerIndex;

        public Player(PlayerIndex playerIndex, Library.Character.Name character, Game game, World world)
            : base(game)
        {
            _playerIndex = playerIndex;
            Left = new Unit(Library.Character.GameTexture[character], new Vector2(200, 200), Color.Red, game, world);       // TODO: fix hardcoded values for positions. 
            Right = new Unit(Library.Character.GameTexture[character], new Vector2(200, 120), Color.DarkRed, game, world);

            game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                Left.Direction = InputManager.LeftStickDirection(_playerIndex);
                Right.Direction = InputManager.RightStickDirection(_playerIndex);

                if (InputManager.LeftShoulderPressed(_playerIndex))
                    Left.Shoot();

                if (InputManager.RightShoulderPressed(_playerIndex))
                    Right.Shoot();
            }
        }

    }
}
