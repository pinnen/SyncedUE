// Unit.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson
// 
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using Synced.Static_Classes;
using Microsoft.Xna.Framework.Graphics;
using Synced.Content;
using Synced.InGame;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics;
using Synced.InGame.Actors;
using Synced.MapNamespace;
using Synced.Interface;

namespace Synced.Actors
{
    class Unit : MovableCollidable, IVictim
    {
        #region Variables
        ParticleEngine _trail;
        float _trailParticleLifetime;
        ParticleEngine _effectParticles;
        bool _useEffectParticles;
        Library.Colors.ColorName _teamColor;
        Texture2D _texture;
        Vector2 lastNonZeroDirection; 
    
        #endregion

        #region Properties
        public float TrailParticleLifetime
        {
            get { return _trailParticleLifetime; }
            set { _trailParticleLifetime = value; }
        }
        public bool UseEffectParticles
        {
            get { return _useEffectParticles; }
            set { _useEffectParticles = value; }
        }
        public float Acceleration 
        {
            get { return acceleration; }
            set { acceleration = value; }
        }
        public Vector2 LastNonZeroDirection
        {
            get { return lastNonZeroDirection; }
        }
        #endregion

        public Grabbable Item { get; set; }
        public Unit(Texture2D texture, Vector2 position, Color color, Game game, World world,Library.Colors.ColorName teamColor)
            : base(texture, position, DrawingHelper.DrawingLevel.Medium, game, world)
        {
            /* Setting up Farseer Physics */
            RigidBody = BodyFactory.CreateCircle(this.world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position));
            RigidBody.BodyType = BodyType.Dynamic;
            RigidBody.CollisionCategories = Category.Cat1; /* UNIT Category & TEAM Category*/ // TODO: fix collisionCategory system. 
            RigidBody.CollidesWith = Category.All ^ Category.Cat1;         
            RigidBody.Mass = 10f;                          
            RigidBody.LinearDamping = 5f;                  
            RigidBody.Restitution = 0.1f;                  
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);

            /* Setting up Unit */
            acceleration = 40;
            Color = color;
            _trailParticleLifetime = 0.2f;
            _trail = new ParticleEngine(1, Library.Particle.trailTexture, position, color, Origin, 1.0f, 0.0f, _trailParticleLifetime, DrawingHelper.DrawingLevel.Low, game);
            _effectParticles = new ParticleEngine(1, Library.Particle.plusSignTexture, position, color, Origin, 0.7f, 0.0f, 0.5f, DrawingHelper.DrawingLevel.High, game);
            _useEffectParticles = false;
            _teamColor = teamColor;
            SyncedGameCollection.ComponentCollection.Add(_trail);
            SyncedGameCollection.ComponentCollection.Add(_effectParticles);
            Tag = TagCategories.UNIT;
            _texture = texture;
        }

        public void Shoot()
        {
            if (Item != null)
            {
                Item.Shoot();
                Item = null;
            }
        }

        public void SetItem(Grabbable item)
        {
            Item = item;
        }

        public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            CollidingSprite other = SyncedGameCollection.GetCollisionComponent(f2);

            if (other.Tag == TagCategories.CRYSTAL)
            {
                if (Item == null)
                {
                    Crystal crystal = other as Crystal;
                    Item = crystal.PickUp(this);
                    crystal.ChangeColor(Library.Colors.getColor[Tuple.Create(_teamColor, Library.Colors.ColorVariation.Other)]);
                }
                return false;
            }
            else if (other.Tag == TagCategories.UNIT)
            {
                return false;
            }
            else if (other.Tag == TagCategories.COMPACTZONE)
            {
                if (Item == null)
                {
                    CompactZone compactzone = other as CompactZone;
                    Item = compactzone.PickUp(this);
                }
            }
            else if (other.Tag == TagCategories.BARRIER)
            {
                return false;
            }

            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (direction != Vector2.Zero)
            {
                lastNonZeroDirection = direction;
                RigidBody.Rotation = (float)Math.Atan2(RigidBody.LinearVelocity.Y, RigidBody.LinearVelocity.X);
                
            }

            // Update Trail
            _trail.UpdatePosition(Position);
            _trail.GenerateTrailParticles(_trailParticleLifetime);
            _trailParticleLifetime = 0.2f;
            if (_useEffectParticles)
            {
                _effectParticles.UpdatePosition(Position);
                _effectParticles.GenerateEffectParticles();
            }

            base.Update(gameTime);
        }


        // IVictim
        float _circleEffectTimer = 0.0f;
        float _triangleEffectTimer = 0.12f;
        float _hexagonEffectTimer = 0.0f;
        float _pentagonEffectTimer = 0.0f;
        bool _fadeOut = false;

        public float CircleEffectTimer { get { return _circleEffectTimer; } set { _circleEffectTimer = value; } }
        public float TriangleEffectTimer{ get{ return _triangleEffectTimer;}set{_triangleEffectTimer = value;}}
        public float HexagonEffectTimer { get { return _hexagonEffectTimer; } set { _hexagonEffectTimer = value; } }
        public float PentagonEffectTimer {get{return _pentagonEffectTimer;} set{_pentagonEffectTimer = value;}}
        public bool FadeOut { get { return _fadeOut; } set { _fadeOut = value; } }
        public Texture2D VictimTexture{ get { return _texture; }}
        public float ParticleLifetime { get { return _trailParticleLifetime; } }
        public float LocalTimeScale { get { return accelerationScaling; } set { accelerationScaling = value; } }
        public float InvisibilityAlpha { get { return this.Alpha; } set { this.Alpha = value; } }
        public ParticleEngine TrailEngine { get { return _trail; } }
    }
}
