using Microsoft.Xna.Framework;
using Synced.InGame.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.CollisionShapes
{
    class DummyComponent : CollidingSprite
    {
        public DummyComponent(): base(null, Vector2.Zero, Static_Classes.DrawingHelper.DrawingLevel.Back, null, null)
        {
            RigidBody.CollisionGroup = (short)CollisionCategory.UNDEFINED;
        }

        public override void Update(GameTime gameTime)
        {
            // No base call
        }
        public override void Draw(GameTime gameTime)
        {
            // No base call
        }
    }
}
