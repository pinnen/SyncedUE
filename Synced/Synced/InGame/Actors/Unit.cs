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
namespace Synced.Actors
{
    class Unit : Movable
    {
        #region Variables

        ParticleEngine trail;

        #endregion


        public IGrabbable Item { get; set; }
        public Unit(Texture2D texture, Vector2 position, Color color, Game game, World world)
            : base(texture, position, DrawingHelper.DrawingLevel.Medium, game, world)
        {
            this.world = world;

            body.UserData = "UNIT";

            // Centered origin
            Origin = new Vector2(ConvertUnits.ToSimUnits(Texture.Width / 2), ConvertUnits.ToSimUnits(texture.Height / 2));

            Color = color;

            trail = new ParticleEngine(1, Library.TrailParticle.Texture, position, color, Origin, 1.0f, 0.0f, 0.2f, DrawingHelper.DrawingLevel.Medium, game);

            game.Components.Add(this);
        }

        public void Shoot()
        {
            if (Item != null)
                Item.Shoot();
        }

        public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            if (f2.Body.UserData.ToString() == "UNIT") // TODO: maybe find better way to do this. 
            {
                // TODO: need to be able to cast object to collision class or fetch it in another way. 
                //Item = (f2 as Crystal).PickUp(this);
            }
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            trail.UpdatePosition(this.body.Position);
            trail.GenerateTrailParticles(1.0f, 0.2f);
            base.Update(gameTime);
        }
    }
}
