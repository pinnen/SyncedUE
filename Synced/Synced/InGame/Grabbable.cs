// Grabbable.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-10
// Edited by:
// Dennis Stockhaus
// Lina Ju
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Content;
using Synced.InGame.Actors;
using Synced.Static_Classes;
using System;

namespace Synced.InGame
{
    abstract class Grabbable : MovableCollidable
    {

        #region Variables
        protected Unit owner = null;
        protected ParticleEngine _tail;
        float _shootForce;
        float _cooldownTimer;
        float _cooldownInSeconds;
        #endregion

        #region Properties
        public MovableCollidable Owner
        {
            get { return owner; }
        }
        public bool HasOwner
        {
            get {
                if (owner == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion

        public Grabbable(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world, Color color)
            : base(texture, position, drawingLevel, game, world)
        {
            /* Setting up Farseer Physics */
            RigidBody = BodyFactory.CreateCircle(this.world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position));
            RigidBody.BodyType = BodyType.Dynamic;
            RigidBody.CollisionCategories = Category.Cat5;
            RigidBody.CollidesWith = Category.All ^ Category.Cat9;
            RigidBody.Mass = 1f; 
            RigidBody.LinearDamping = 0.5f;
            RigidBody.Restitution = 1f;
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            /* Setting up Grabbable*/
            acceleration = maxAcceleration = 20;
            _shootForce = 2000f;
            _cooldownInSeconds = 1f;
            Color = color;
            _tail = new ParticleEngine(1, Library.Particle.trailTexture, position, color, Origin, 1.0f, 0.0f, 0.2f, DrawingHelper.DrawingLevel.Low, game);
            SyncedGameCollection.ComponentCollection.Add(_tail);
        }

        public virtual Grabbable PickUp(Unit own) //TODO: only let it be stolen or picked up if some conditions are met. 
        {
            if (_cooldownTimer > _cooldownInSeconds)
            {
                if (owner != null)
                {
                    // its a steal!
                    owner.SetItem(null);
                }

                owner = own;
                maxAcceleration = 100;
                RigidBody.LinearDamping = 10f;
                Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.CrystalGrab);
                _cooldownTimer = 0;
                return this;
            }
            return null;
        }

        public virtual void Release()
        {
            owner = null;
            maxAcceleration = 20;
        }

        public virtual void Shoot()
        {
            if (owner != null)
            {
                RigidBody.LinearDamping = 0.5f;
                RigidBody.ApplyForce(-_shootForce * new Vector2((float)Math.Cos(owner.Rotation), (float)Math.Sin(owner.Rotation)));
                Direction = Vector2.Zero;
                Release();
                Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.Shoot);
                _cooldownTimer = 0;
            }
        }

        public override void Update(GameTime gameTime)
        {

            float rotval = 0.002f * (float)gameTime.ElapsedGameTime.TotalMilliseconds; // TODO: Fix hardcode value
            RigidBody.Rotation += rotval;

            if (_cooldownTimer < _cooldownInSeconds)
                _cooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (owner != null)
            {
                Vector2 ownerOffsetPosition = Vector2.Zero;

                if (owner.Direction != Vector2.Zero)
                {
                    ownerOffsetPosition = new Vector2(owner.SimPosition.X + -(owner.LastNonZeroDirection.X / 4), owner.SimPosition.Y + (owner.LastNonZeroDirection.Y / 4));
                }
                else
                {
                    ownerOffsetPosition = new Vector2(owner.SimPosition.X + -(owner.LastNonZeroDirection.X), owner.SimPosition.Y + (owner.LastNonZeroDirection.Y));
                }
                
                float distance = (SimPosition.X - ownerOffsetPosition.X) * (SimPosition.X - ownerOffsetPosition.X) + (SimPosition.Y - ownerOffsetPosition.Y) * (SimPosition.Y - ownerOffsetPosition.Y);
                acceleration = maxAcceleration * distance;

                direction = new Vector2(ownerOffsetPosition.X - this.SimPosition.X, -(ownerOffsetPosition.Y - this.SimPosition.Y));
                direction.Normalize();
            }

            _tail.UpdatePosition(Position);
            _tail.GenerateTrailParticles();

            base.Update(gameTime);
        }

        public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            CollidingSprite other = SyncedGameCollection.GetCollisionComponent(f2);

            if (other.Tag == TagCategories.UNIT)
            {
                return false;
            }

            return true;
        }
    }
}
