using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        float _velocity { get; set; }
        public Movable(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(texture, position, drawingLevel, game, world)
        {
            _velocity = 200;
        }

        public override void Update(GameTime gameTime)
        {
            // Move
            //Position += new Vector2(Direction.X * _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds,
            //                       -Direction.Y * _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            //body.Position = ConvertUnits.ToSimUnits(Position);
            body.ApplyForce(Direction*100000);

            // Rotate
            if (Direction != Vector2.Zero)
                Rotation = (float)Math.Atan2(-Direction.Y, Direction.X);

            base.Update(gameTime);
        }
    }
}
