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
    class Movable : CollidingSprite
    {
        public Vector2 Direction { get; set; }
        float Force; 

        public Movable(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(texture, position, drawingLevel, game, world)
        {
            Force = 500000f;
        }

        public override void Update(GameTime gameTime)
        {
            if (Direction != Vector2.Zero)
                body.ApplyForce(Force * new Vector2(Direction.X, -Direction.Y));
                body.Rotation = (float)Math.Atan2(body.LinearVelocity.Y, body.LinearVelocity.X);

            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));

            base.Update(gameTime);
        }
    }
}
