﻿// Crystal.cs
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
using Synced.InGame.Actors;

namespace Synced.InGame
{
    class Crystal : MovableCollidable, IGrabbable
    {
        #region Variables
        // General Variables
        MovableCollidable _owner = null;
        float _distanceToOwner;
        ParticleEngine _tail;
        #endregion

        public Crystal(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, SyncedGame game, World world)
            : base(texture, position, drawingLevel, game, world)
        {
            /* Setting up Farseer Physics */
            RigidBody = BodyFactory.CreateCircle(this.world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position)); // TODO: size to some scale? 
            RigidBody.BodyType = BodyType.Dynamic;
            RigidBody.CollisionCategories = Category.Cat1; /* Crystal Category */ // TODO: fix collisionCategory system. 
            RigidBody.CollidesWith = Category.All;
            RigidBody.Mass = 1f; // TODO: fix hardcoded value
            RigidBody.LinearDamping = 0.5f; // TODO: fix hardcoded value
            RigidBody.Restitution = 1f; // TODO: fix hardcoded value
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            /* Setting up Crystal */
            _distanceToOwner = 50; // TODO: fix hardcoded distance

            _tail = new ParticleEngine(1, Library.TrailParticle.Texture, position, Color.Magenta, Origin, 1.0f, 0.0f, 0.2f, DrawingHelper.DrawingLevel.Medium, game);

            Game.Components.Add(this);
        }

        public IGrabbable PickUp(MovableCollidable owner)
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
            float rotval = (float)0.002 * gameTime.ElapsedGameTime.Milliseconds; // TODO: Fix hardcode value
            RigidBody.Rotation += rotval;

            if (_owner != null) // TODO a better formula for a more consistent Crystal Position
            {
                if (_owner.Direction  != Vector2.Zero)
                {

                    Position = new Vector2(_owner.Position.X - (_distanceToOwner * _owner.Direction.X),
                                           _owner.Position.Y - (_distanceToOwner * -_owner.Direction.Y));
                }
            }
            _tail.UpdatePosition(Position);
            _tail.GenerateTrailParticles(1.0f, 0.2f);
            base.Update(gameTime);
        }

        public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            // GameComponents.GetComponent(f1.body.userData) // preferred way to fetch objects. 
            return true;
        }
    }
}
