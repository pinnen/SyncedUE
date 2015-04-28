using Microsoft.Xna.Framework;
// Unit.cs
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
using Synced.Static_Classes;
namespace Synced.Player
{
    class Unit : Sprite
    {
        public Unit(string texturePath, Game game)
            : base (texturePath, Vector2.Zero, DrawingHelper.DrawingLevel.Game, game)
        {
            game.Components.Add(this);
        }
    }
}
