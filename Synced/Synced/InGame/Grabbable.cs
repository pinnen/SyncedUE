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
        float _distanceToOwner;
        ParticleEngine _tail;
        float currentAcceleration;
        #endregion

        public Grabbable(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world, Color color)
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

            /* Setting up Grabbable*/
            acceleration = 20;
            Color = color;
            _tail = new ParticleEngine(1, Library.Particle.trailTexture, position, color, Origin, 1.0f, 0.0f, 0.2f, DrawingHelper.DrawingLevel.Medium, game);
            _distanceToOwner = 50; // TODO: fix hardcoded distance

            game.Components.Add(this);


        }
        
        public virtual Grabbable PickUp(MovableCollidable owner) 
        {
            _owner = owner;
            currentAcceleration = 100;
            RigidBody.LinearDamping = 10f;
            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.CrystalPickUp);
            return this;
        } 
        
        public virtual void Release() 
        {
            _owner = null;
        }
        
        public virtual void Shoot()
        {
            RigidBody.LinearDamping = 0f;
            RigidBody.ApplyForce(3000 * new Vector2(-(_owner.Direction.X), (_owner.Direction.Y))); // TODO: fix hardcoded shooting force
            Direction = Vector2.Zero;
            Release();
            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.CrystalShoot);
        }
        
        public override void Update(GameTime gameTime)
        {

            float rotval = (float)0.005 * gameTime.ElapsedGameTime.Milliseconds; // TODO: Fix hardcode value
            RigidBody.Rotation += rotval;

            if (_owner != null) // TODO a is this creating framedrop? 
            {
                Vector2 ownerOffsetPosition =  new Vector2(_owner.Position.X + -(_owner.Direction.X / 4), _owner.Position.Y + (_owner.Direction.Y / 4));
                float distance = (Position.X - ownerOffsetPosition.X) * (Position.X - ownerOffsetPosition.X) + (Position.Y - ownerOffsetPosition.Y) * (Position.Y - ownerOffsetPosition.Y);
                acceleration = currentAcceleration * distance;
        
                direction = new Vector2(ownerOffsetPosition.X - this.Position.X, -(ownerOffsetPosition.Y - this.Position.Y));
                direction.Normalize();
            }

            _tail.UpdatePosition(Position);
            _tail.GenerateTrailParticles();

            base.Update(gameTime);
        }

    }
}
