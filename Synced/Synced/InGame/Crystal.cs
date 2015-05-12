// Crystal.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-10
// Edited by:
// Pontus Magnusson
// Göran Forsström

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Synced.Content;

namespace Synced.InGame
{
    class Crystal : Movable, IGrabbable, ICollidable
    {
        Movable _owner = null;
        float _distanceToOwner;
        public Crystal(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, SyncedGame game)
            : base(texture, position, drawingLevel, game)
        {
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            _distanceToOwner = 50; // TODO hardcoded distance

            Game.Components.Add(this);
        }

        public IGrabbable PickUp(Movable owner)
        {
            _owner = owner;
            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.CrystalPickUp);
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
            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.CrystalShoot);
        }

        public override void Update(GameTime gameTime)
        {
            if (_owner != null) // TODO a better formula for a more consistent Crystal Position
            {
                if (_owner.Direction  != Vector2.Zero)
                {

                    Position = new Vector2(_owner.Position.X - (_distanceToOwner * _owner.Direction.X),
                                           _owner.Position.Y - (_distanceToOwner * -_owner.Direction.Y));
                }
                //Position = new Vector2(_owner.Position.X - (_distanceToOwner),
                //                     _owner.Position.Y - (_distanceToOwner));
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
