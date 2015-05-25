using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Content;
using Synced.InGame.Actors;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame
{
    abstract class Grabbable : MovableCollidable
    {

        #region Variables
        MovableCollidable _owner = null;
        float _distanceToOwner;
        ParticleEngine _tail;
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


            Color = color;
            _tail = new ParticleEngine(1, Library.Particle.trailTexture, position, color, Origin, 1.0f, 0.0f, 0.2f, DrawingHelper.DrawingLevel.Medium, game);
            _distanceToOwner = 50; // TODO: fix hardcoded distance

            game.Components.Add(this);


        }
        
        public virtual Grabbable PickUp(MovableCollidable owner) 
        {
            _owner = owner;
            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.CrystalPickUp);
            return this;
        } 
        
        public virtual void Release() 
        {
            _owner = null;
        }
        
        public virtual void Shoot()
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
                    acceleration = 20;
                    direction = new Vector2(_owner.Position.X - this.Position.X, -(_owner.Position.Y - this.Position.Y));
                    direction.Normalize();
                    //Position = new Vector2(_owner.Position.X - (_distanceToOwner * _owner.Direction.X),
                    //                       _owner.Position.Y - (_distanceToOwner * -_owner.Direction.Y));
            }

            _tail.UpdatePosition(Position);
            _tail.GenerateTrailParticles();

            base.Update(gameTime);
        }

    }
}
