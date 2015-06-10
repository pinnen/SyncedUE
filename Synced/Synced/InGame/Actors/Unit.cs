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
using SevenEngine.Drawing;

namespace Synced.Actors
{
    class Unit : MovableCollidable, IVictim
    {
        #region Variables
        Library.Colors.ColorName _teamColor;
        Texture2D _texture;
        Vector2 lastNonZeroDirection; 
    
        #endregion

        #region Properties
        public Vector2 LastNonZeroDirection
        {
            get { return lastNonZeroDirection; }
        }
        public PlayerIndex PlayerIndex
        {
            get;
            private set;
        }
        public Body Item { get; set; }
        #endregion

        public Unit(PlayerIndex playerIndex, Texture2D texture, Vector2 position, Color color, Game game, World world,Library.Colors.ColorName teamColor)
            : base(texture, position, DrawHelper.DrawingLevel.Medium, game, world)
        {
            PlayerIndex = playerIndex;

            /* Setting up Farseer Physics */
            RigidBody = BodyFactory.CreateCircle(this.world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position));
            RigidBody.BodyType = BodyType.Dynamic;
            RigidBody.CollisionCategories = Category.Cat1; /* UNIT Category & TEAM Category*/ // TODO: fix collisionCategory system. 
            RigidBody.CollidesWith = Category.All ^ Category.Cat1;         
            RigidBody.Mass = 10f;                          
            RigidBody.LinearDamping = 5f;                  
            RigidBody.Restitution = 0.1f;
            RigidBody.CollisionGroup = (short)CollisionCategory.UNIT;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);

            /* Setting up Unit */
            acceleration = 40;
            Color = color;
            _teamColor = teamColor;
            _texture = texture;
        }

        public void ForcedRelease()
        {
            if (Item != null)
            {
                Item = null;
            }
        }

        public void Shoot()
        {
        }

        public void SetItem(Body item)
        {
            Item = item;
        }

        public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (direction != Vector2.Zero)
            {
                lastNonZeroDirection = direction;
                RigidBody.Rotation = (float)Math.Atan2(RigidBody.LinearVelocity.Y, RigidBody.LinearVelocity.X);
                
            }

            base.Update(gameTime);
        }


        // IVictim
        float _circleEffectTimer = 0.0f;
        float _triangleEffectTimer = 0.12f;
        float _hexagonEffectTimer = 0.4f;
        float _pentagonEffectTimer = 0.12f;
        bool _fadeOut = false;

        public float CircleEffectTimer { get { return _circleEffectTimer; } set { _circleEffectTimer = value; } }
        public float TriangleEffectTimer{ get{ return _triangleEffectTimer;}set{_triangleEffectTimer = value;}}
        public float HexagonEffectTimer { get { return _hexagonEffectTimer; } set { _hexagonEffectTimer = value; } }
        public float PentagonEffectTimer {get{return _pentagonEffectTimer;} set{_pentagonEffectTimer = value;}}
        public bool FadeOut { get { return _fadeOut; } set { _fadeOut = value; } }
        public Texture2D VictimTexture{ get { return _texture; }}
        public float LocalTimeScale { get { return accelerationScaling; } set { accelerationScaling = value; } }
        public float InvisibilityAlpha { get { return this.Alpha; } set { this.Alpha = value; } }
        public Vector2 VictimLinearVelocity { get { return LinearVelocity; } }
    }
}
