using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Synced.Actors;
using Synced.InGame.Actors;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame
{
    class Movable : Sprite
    {
        #region Variables
        protected Vector2 direction;
        protected float constantForce;
        #endregion

        #region Properties
        public Vector2 Direction
        {
            get { return Direction; }
            set
            {
                Direction = value;
                Direction.Normalize();
            }
        }
        #endregion

        public Movable(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world, Vector2 direction, float constantForce)
            : base(texture, position, drawingLevel, game)
        {
            this.direction = direction;
            this.constantForce = constantForce;
        }

        public override void Update(GameTime gameTime)
        {
            Position += Direction * constantForce * gameTime.ElapsedGameTime.Seconds; // TODO: Do this correctly // TODO: Adjust to screen ratio

            base.Update(gameTime);
        }
    }
}
