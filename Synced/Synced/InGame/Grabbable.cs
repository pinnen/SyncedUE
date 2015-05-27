// Grabbable.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-10
// Edited by:
// Dennis Stockhaus
// Lina Ju
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Content;
using Synced.InGame.Actors;
using Synced.Static_Classes;
using System;

namespace Synced.InGame
{
    abstract class Grabbable : MovableCollidable
    {

        #region Variables
        MovableCollidable _owner = null;
        protected ParticleEngine _tail;
        float shootForce;
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
            shootForce = 3000f;
            Color = color;
            _tail = new ParticleEngine(1, Library.Particle.trailTexture, position, color, Origin, 1.0f, 0.0f, 0.2f, DrawingHelper.DrawingLevel.Low, game);
            SyncedGameCollection.ComponentCollection.Add(_tail);
        }
        
        public virtual Grabbable PickUp(MovableCollidable owner) 
        {
            _owner = owner;
            maxAcceleration = 100;
            RigidBody.LinearDamping = 10f;
            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.CrystalPickUp);
            return this;
        } 
        
        public virtual void Release() 
        {
            _owner = null;
            maxAcceleration = 20;
        }
        
        public virtual void Shoot()
        {
            if (_owner != null)
            {
                RigidBody.LinearDamping = 0.5f;
                RigidBody.ApplyForce(shootForce * new Vector2(-(_owner.Direction.X), (_owner.Direction.Y)));
                Direction = Vector2.Zero;
                Release();
                Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.CrystalShoot);
            }
        }
        
        public override void Update(GameTime gameTime)
        {

            float rotval = (float)0.002 * gameTime.ElapsedGameTime.Milliseconds; // TODO: Fix hardcode value
            RigidBody.Rotation += rotval;

            if (_owner != null)
            {
                Vector2 ownerOffsetPosition =  new Vector2(_owner.Position.X + -(_owner.Direction.X / 4), _owner.Position.Y + (_owner.Direction.Y / 4));
                float distance = (Position.X - ownerOffsetPosition.X) * (Position.X - ownerOffsetPosition.X) + (Position.Y - ownerOffsetPosition.Y) * (Position.Y - ownerOffsetPosition.Y);
                acceleration = maxAcceleration * distance;
        
                direction = new Vector2(ownerOffsetPosition.X - this.Position.X, -(ownerOffsetPosition.Y - this.Position.Y));
                direction.Normalize();
            }

            _tail.UpdatePosition(Position);
            _tail.GenerateTrailParticles();

            base.Update(gameTime);
        }

    }
}
