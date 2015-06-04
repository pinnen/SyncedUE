// DummyCircle.cs
// Introduced: 2015-05-23
// Last edited: 2015-05-23
// Edited by:
// Dennis Stockhaus
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Synced.InGame.Actors;
using Synced.Static_Classes;

namespace Synced.CollisionShapes
{
    class DummyCircle : Circle
    {
        public DummyCircle(Vector2 position, float r, Game game, World world)
            :base (null, position, r, game, world)
        {
        }

        public void SetTag(TagCategories tag)
        {
            Tag = tag;
        }

        public override void Draw(GameTime gameTime)
        {
            // NO BASE CALL
        }
    }
}
