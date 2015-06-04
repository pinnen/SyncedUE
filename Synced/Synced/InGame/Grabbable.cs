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
    abstract class Grabbable : MovableCollidable, IVictim
    {

        #region Variables
        protected Unit owner = null;
        //protected ParticleEngine _tail;
        
        float _shootForce;
        float _cooldownTimer;
        float _cooldownInSeconds;
        #endregion

        #region Properties
        public Unit Owner
        {
            get { return owner; }
        }
        public Unit PreviousOwner
        {
            get;
            set;
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
            _cooldownInSeconds = 0.5f;
            Color = color;
            
        }

        public virtual Grabbable PickUp(Unit own, Library.Colors.ColorName teamColor) //TODO: only let it be stolen or picked up if some conditions are met. 
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
                Color =  Library.Colors.getColor[Tuple.Create(teamColor, Library.Colors.ColorVariation.Other)];
                _cooldownTimer = 0;

                return this;
            }
            return null;
        }

        public void ForcedRelease()
        {
            owner.ForcedRelease();
            owner = null;
            maxAcceleration = 20;
            this.Color = Color.White;
            //_tail.ParticleColor = Color.LightGray;
            direction = Vector2.Zero;
        }

        public virtual void Release()
        {
            PreviousOwner = owner;
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

            
            base.Update(gameTime);
        }

        public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            //CollidingSprite other = SyncedGameCollection.GetCollisionComponent(f2);

            //if (other.Tag == TagCategories.UNIT)
            //{
            //    return false;
            //}

            return true;
        }

        // IVictim
        float _circleEffectTimer = 0.0f;
        float _triangleEffectTimer = 0.12f;
        float _hexagonEffectTimer = 0.4f;
        float _pentagonEffectTimer = 0.12f;
        bool _fadeOut = false;


        public float CircleEffectTimer { get { return _circleEffectTimer; } set { _circleEffectTimer = value; } }
        public float TriangleEffectTimer { get { return _triangleEffectTimer; } set { _triangleEffectTimer = value; } }
        public float HexagonEffectTimer { get { return _hexagonEffectTimer; } set { _hexagonEffectTimer = value; } }
        public float PentagonEffectTimer { get { return _pentagonEffectTimer; } set { _pentagonEffectTimer = value; } }
        public bool FadeOut { get { return _fadeOut; } set { _fadeOut = value; } }
        public Texture2D VictimTexture { get { return this.Texture; } }
        //public float ParticleLifetime { get { return _tail.ParticleLifetime; } }
        public float LocalTimeScale { get { return accelerationScaling; } set { accelerationScaling = value; } }
        public float InvisibilityAlpha { get { return this.Alpha; } set { this.Alpha = value; } }
        //public ParticleEngine TrailEngine { get { return _tail; } }
        public Vector2 VictimLinearVelocity { get { return LinearVelocity; } }
    }
}
