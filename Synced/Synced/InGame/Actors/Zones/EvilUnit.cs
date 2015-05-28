using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Content;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Actors.Zones
{
    class EvilUnit : MovableCollidable
    {
         #region Variables
        ParticleEngine _trail;
        float _trailParticleLifetime;

        Vector2 lastNonZeroDirection;
        Unit _copyOf;
        float _offset;
    
        #endregion

        #region Properties
        public float TrailParticleLifetime
        {
            get { return _trailParticleLifetime; }
            set { _trailParticleLifetime = value; }
        }
        
        public Vector2 LastNonZeroDirection
        {
            get { return lastNonZeroDirection; }
        }
        #endregion

        public EvilUnit(Texture2D texture, Vector2 position, Color color, Game game, World world, DrawingHelper.DrawingLevel drawingLevel, Unit copyOf, float offset)
            : base(texture, position, drawingLevel, game, world)
        {
;
            RigidBody = BodyFactory.CreateCircle(this.world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position));
            RigidBody.BodyType = BodyType.Dynamic;
            RigidBody.CollisionCategories = Category.Cat1;
            RigidBody.CollidesWith = Category.All ^ Category.Cat1;  
            RigidBody.Mass = 10f;
            RigidBody.LinearDamping = 5f;
            RigidBody.Restitution = 0.1f;
        
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);

            acceleration = 40;
            Color = color;
            _trailParticleLifetime = 0.2f;
            _trail = new ParticleEngine(1, Library.Particle.trailTexture, position, color, Origin, 1.0f, 0.0f, _trailParticleLifetime, DrawingHelper.DrawingLevel.Low, game);

            SyncedGameCollection.ComponentCollection.Add(_trail);
            Tag = TagCategories.UNDEFINED;

            _copyOf = copyOf;
            _offset = offset;
        }



        public override void Update(GameTime gameTime)
        {
            this.direction = _copyOf.Direction;

            float newX = (float)(Math.Cos(_offset) * direction.X - Math.Sin(_offset) * direction.Y);
            float newY = (float)(Math.Sin(_offset) * direction.X - Math.Cos(_offset) * direction.Y);

            if (direction != Vector2.Zero)
            {
                direction = new Vector2(newX, newY);
                lastNonZeroDirection = direction;
                RigidBody.Rotation = (float)Math.Atan2(RigidBody.LinearVelocity.Y, RigidBody.LinearVelocity.X);
            }

            // Update Trail
            _trailParticleLifetime = _copyOf.ParticleLifetime;
            _trail.UpdatePosition(Position);
            _trail.GenerateTrailParticles(_trailParticleLifetime);
            _trailParticleLifetime = 0.2f;

            base.Update(gameTime);
        }


    }
}
