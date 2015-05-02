// Unit.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson
// 
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using Synced.Static_Classes;
using Microsoft.Xna.Framework.Graphics;
using Synced.Content;
using Synced.InGame;
namespace Synced.Actors
{
    class Unit : Sprite, ICollidable
    {
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public IGrabbable Item { get; set; }
        public Unit(Texture2D texture, Color color, Game game)
            : base(texture, Vector2.Zero, DrawingHelper.DrawingLevel.Medium, game)
        {
            // Centered origin
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            Color = color;
            Speed = 200.0f; // TODO hardcoded speed
            game.Components.Add(this);
        }
        public override void Update(GameTime gameTime)
        {
            // Move
            Position = new Vector2(Position.X + (Direction.X * Speed) * (float)gameTime.ElapsedGameTime.TotalSeconds,
                                   Position.Y + (-Direction.Y * Speed) * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Rotate
            Rotation = (float)Math.Atan2(-Direction.Y, Direction.X);
            base.Update(gameTime);
        }

        public Vector2 Center
        {
            get { return new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Height / 2); }
        }

        public float Radius 
        {
            get { return Texture.Width / 2; } 
        }

        public void Response(ICollidable c)
        {
            (c as Crystal).PickUp(this);
        }
    }
}
