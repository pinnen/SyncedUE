using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Content;
using Synced.InGame;
using Synced.Static_Classes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Synced.InGame.Actors
{
    class CollidingSprite : Synced.Actors.Sprite
    {
        #region Variables

        #endregion

        #region Properties
        #endregion

        public CollidingSprite(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game)
        : base(texture, position, drawingLevel, game)
        { }
    }
}
