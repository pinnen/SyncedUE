using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Actors
{
    class CompactZone : Grabbable
    {

        #region Variables
        Game _game;
        #endregion

        public CompactZone(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world,Color color)
            : base(texture, position, drawingLevel, game, world, color) 
        {
            _game = game;

        }

        public void Detonate() 
        {
            _game.Components.Remove(this);
        }
    }
}
