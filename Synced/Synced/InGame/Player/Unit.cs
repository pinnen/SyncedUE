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
using Microsoft.Xna.Framework.Graphics;
namespace Synced.Player
{
    class Unit : Sprite
    {
        public Unit(Texture2D texture, Game game)
            : base (texture, Vector2.Zero, DrawingHelper.DrawingLevel.Game, game)
        {
            game.Components.Add(this);
        }
    }
}
