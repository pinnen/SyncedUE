using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Content;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Actors.Zones
{
    class EvilCrystal : MovableCollidable
    {
        //ParticleEngine _tail;

        public EvilCrystal(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world, Color color)
            : base(texture, position, drawingLevel, game, world)
        {
            RigidBody = BodyFactory.CreateCircle(this.world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position));
            RigidBody.BodyType = BodyType.Dynamic;
            RigidBody.CollisionCategories = Category.Cat5;
            RigidBody.CollidesWith = Category.All ^ Category.Cat9;
            RigidBody.Mass = 1f;
            RigidBody.LinearDamping = 0.5f;
            RigidBody.Restitution = 1f;
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            Tag = TagCategories.UNDEFINED;

            //_tail = new ParticleEngine(1, Library.Particle.trailTexture, position, color, Origin, 1.0f, 0.0f, 0.2f, DrawingHelper.DrawingLevel.Low, game);
            //SyncedGameCollection.ComponentCollection.Add(_tail);
            //if(color == Color.White)_tail.ParticleColor = Color.LightGray;

            acceleration = maxAcceleration = 20;
            Color = color;
        }

        //public override bool OnCollision(Fixture f1, Fixture f2, FarseerPhysics.Dynamics.Contacts.Contact contact)
        //{
        //    //return false;
        //}

        public override void Update(GameTime gameTime)
        {
            //_tail.UpdatePosition(Position);
            //_tail.GenerateTrailParticles();

            base.Update(gameTime);
        }


        public void ChangeColor(Color newColor)
        {
            this.Color = newColor;
            //_tail.ParticleColor = newColor;
        }
        public void ResetColor()
        {
            this.Color = Color.White;
            //_tail.ParticleColor = Color.LightGray;
        }
    }
}
