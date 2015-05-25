// Circle.cs
// Introduced: 2015-05-23
// Last edited: 2015-05-23
// Edited by:
// Dennis Stockhaus
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.InGame.Actors;
using Synced.Static_Classes;

namespace Synced.CollisionShapes
{
    class Circle : CollidingSprite
    {
        public Circle(Texture2D texture, Vector2 position, float r, Game game, World world)
            : base(texture, position, DrawingHelper.DrawingLevel.Back, game, world)
        {
            /* Setting up Farseer Physics */
            RigidBody = BodyFactory.CreateCircle(this.world, ConvertUnits.ToSimUnits(r), 0, ConvertUnits.ToSimUnits(position)); // TODO: size to some scale? 
            RigidBody.BodyType = BodyType.Static;
            RigidBody.CollisionCategories = Category.All; /* Collision Category */ // TODO: fix collisionCategory system. 
            RigidBody.CollidesWith = Category.All;
            Origin = new Vector2(r, r);

            game.Components.Add(this); // TODO: Remove this later
        }

        public void setOnCollisionFunction(OnCollisionEventHandler onCollisionFunc)
        {
            RigidBody.OnCollision += onCollisionFunc;
        }

        public void SetCollisionCategory(Category collisionCategory)
        {
            RigidBody.CollisionCategories = collisionCategory;
        }

        public void SetCollideWithCategory(Category collideWithCategory)
        {
            RigidBody.CollidesWith = collideWithCategory;
        }
    }
}
