using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.InGame.Actors;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame
{
    abstract class MovableCollidable : CollidingSprite
    {
        #region Variables
        protected Vector2 direction;
        protected float acceleration;                
        float force;                                 
        #endregion

        #region Properties
        public Vector2 Direction
        {
            get { return direction; } 
            set { direction = value;} 
        }
        #endregion

        /// <summary>
        /// Creates a default MovableCollidable
        /// </summary>
        public MovableCollidable(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(texture, position, drawingLevel, game, world)
        {
            direction = Vector2.Zero;
            acceleration = 0;
        }

        public override void Update(GameTime gameTime)
        {                     
            // Movement
            if (direction != Vector2.Zero)
            {
                force = RigidBody.Mass * acceleration;
                RigidBody.ApplyForce(force * new Vector2(direction.X, -direction.Y));
            }

            base.Update(gameTime);
        }
    }
}
