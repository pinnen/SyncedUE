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
namespace Synced.Actors
{
    class Unit : Movable
    {
        #region Variables
        #endregion


        public IGrabbable Item { get; set; }
        public Unit(Texture2D texture, Vector2 position, Color color, Game game, World world)
            : base(texture, position, DrawingHelper.DrawingLevel.Medium, game, world)
        {
            this.world = world;

            body = BodyFactory.CreateBody(world, position, 0f);
            body.BodyType = BodyType.Dynamic;
            body.CollisionCategories = Category.All;

            shape = new CircleShape(texture.Width / 2, 1f);

            fixture = body.CreateFixture(shape);
            fixture.OnCollision = this.OnCollision;

            // Centered origin
            Origin = new Vector2(ConvertUnits.ToSimUnits(Texture.Width / 2), ConvertUnits.ToSimUnits(texture.Height / 2));

            Color = color;

            game.Components.Add(this);
        }

        public void Shoot()
        {
            if (Item != null)
                Item.Shoot();
        }

        public bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
             //Item = (f1 as Crystal).PickUp(this);
            return true;
        }
    }
}
