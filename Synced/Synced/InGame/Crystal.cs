using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame
{
    class Crystal : Sprite, IGrabbable, ICollidable
    {
        Sprite _owner = null;

        public Crystal(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game1 game)
            : base(texture, position, drawingLevel, game)
        {
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            Game.Components.Add(this);
        }

        public IGrabbable PickUp(Sprite owner)
        {
            _owner = owner;
            return this;
        }
        
        public void Release()
        {
            _owner = null;
        }

        public override void Update(GameTime gameTime)
        {
            if (_owner != null)
            {
                Position = _owner.Position;
            }

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
            // TODO Temporary stuff here
        }
    }
}
