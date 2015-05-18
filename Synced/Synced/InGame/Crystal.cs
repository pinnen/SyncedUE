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
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics.Contacts;

using Synced.Content;
using FarseerPhysics;

namespace Synced.InGame
{
    class Crystal : Movable, IGrabbable
    {
        #region Variables
        // General Variables
        Movable _owner = null;
        float _distanceToOwner;
        #endregion

        public Crystal(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, SyncedGame game, World world)
            : base(texture, position, drawingLevel, game, world)
        {
            this.world = world;

            body.UserData = "CRYSTAL";

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

        public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            if (f2.Body.UserData.ToString() == "UNIT") // TODO: better way to do this?
            {
                world.BodyList.ElementAt(1);
            }
            return true;
        }
    }
}
