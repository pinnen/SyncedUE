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
    class MovableCollidable : CollidingSprite
    {
        #region Variables
        protected Vector2 direction;
        protected float acceleration;                   // Current Acceleration
        protected float accelerationRate;               // acceleration growth or acceleration "acceleration"
        protected float deaccelerationRate;
        protected float maxAcceleration;                // acceleration limit
        float force;                                    // product of mass and acceleration 
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

        public MovableCollidable(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(texture, position, drawingLevel, game, world)
        {
            direction = new Vector2(0, 0);
            acceleration = 0;           // TODO: Replace hardcoded values with some kind of standard values. 
            accelerationRate = 0.1f;
            deaccelerationRate = 0.1f;
            maxAcceleration = 10;
        }

        // TODO: WOAH long list, designflaw? 
        public MovableCollidable(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, 
                                 Game game, World world, Body body, BodyType bodyType, Category collidesWith,
                                 Category collisionCategory, float mass, float linearDamping, float restitution,
                                 float accelerationRate, float deaccelerationRate, float maxAcceleration)
            : base(texture, position, drawingLevel, game, world, body, bodyType, collidesWith, collisionCategory, mass,
                   linearDamping, restitution)
        {
            direction = new Vector2(0, 0);
            acceleration = 0;
            this.accelerationRate = accelerationRate;
            this.deaccelerationRate = deaccelerationRate;
            this.maxAcceleration = maxAcceleration;
        }

        public override void Update(GameTime gameTime)
        {                     
            // Movement
            force = body.Mass * acceleration;
            body.ApplyForce(force * new Vector2(direction.X, -direction.Y));
            // Rotate to direction
            // body.Rotation = (float)Math.Atan2(body.LinearVelocity.Y, body.LinearVelocity.X);

            base.Update(gameTime);
        }
    }
}
