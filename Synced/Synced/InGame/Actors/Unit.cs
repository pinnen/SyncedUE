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
using System.Linq;

namespace Synced.Actors
{
    class Unit : MovableCollidable
    {
        #region Variables
        ParticleEngine trail;
        #endregion

        #region Properties
        #endregion

        public IGrabbable Item { get; set; }
        public Unit(Texture2D texture, Vector2 position, Color color, Game game, World world)
            : base(texture, position, DrawingHelper.DrawingLevel.Medium, game, world)
        {
            /* Setting up Farseer Physics */
            RigidBody = BodyFactory.CreateCircle(this.world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position)); // TODO: size to some scale?
            RigidBody.BodyType = BodyType.Dynamic;
            RigidBody.CollisionCategories = Category.Cat2; /* Unit Category */ // TODO: fix collisionCategory system. 
            RigidBody.CollidesWith = Category.All | Category.Cat2;         
            RigidBody.Mass = 10f;                          // TODO: fix hardcoded value
            RigidBody.LinearDamping = 5f;                  // TODO: fix hardcoded value
            RigidBody.Restitution = 0.1f;                  // TODO: fix hardcoded value
            Origin = new Vector2(Texture.Width / 2, texture.Height / 2);

            /* Setting up Unit */
            acceleration = 40;
            Color = color;
            trail = new ParticleEngine(1, Library.TrailParticle.Texture, position, color, Origin, 1.0f, 0.0f, 0.2f, DrawingHelper.DrawingLevel.Medium, game);
        }

        public void Shoot()
        {
            if (Item != null)
                Item.Shoot();
        }

        public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            foreach (CollidingSprite gc in Map.MapComponentCollection)
            {
                if (gc is CollidingSprite)
                {
                    if (gc.ID.ToString() == f1.Body.UserData.ToString())
                    {
                        if (gc.Tag == "CRYSTAL")
                        {
                            Crystal crystal = gc as Crystal;
                            crystal.PickUp(this);
                        }
                    }
                }
            }
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (direction != Vector2.Zero)
            {
                RigidBody.Rotation = (float)Math.Atan2(RigidBody.LinearVelocity.Y, RigidBody.LinearVelocity.X);
                
            }
            // Update Trail
            trail.UpdatePosition(Position); // TODO: might have to use ConvertUnits function. 
            trail.GenerateTrailParticles(1.0f, 0.2f);

            

            base.Update(gameTime);
        }
    }
}
