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
    class Crystal : Movable, IGrabbable, ICollidable
    {
        Movable _owner = null;
        float _distanceToOwner;
        public Crystal(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game1 game)
            : base(texture, position, drawingLevel, game)
        {
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            _distanceToOwner = 50; // TODO hardcoded distance

            Game.Components.Add(this);
        }

        public IGrabbable PickUp(Movable owner)
        {
            _owner = owner;
            return this;
        }
        
        public void Release()
        {
            _owner = null;
        }

        public void Shoot()
        {
            Release();
            Direction = -Direction;
        }

        public override void Update(GameTime gameTime)
        {
            if (_owner != null) // TODO a better formula for a more consistent Crystal Position
            {
                Position = new Vector2(_owner.Position.X - (_distanceToOwner * _owner.Direction.X),
                                       _owner.Position.Y - (_distanceToOwner * -_owner.Direction.Y));
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
        }
    }
}
