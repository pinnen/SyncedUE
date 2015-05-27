using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Actors.Zones
{
    class HexagonZone : Zone
    {
        public HexagonZone(Texture2D texture, Vector2 position, float rotation, Color color, Game game, World world)
            : base(texture, position, rotation, color, game, world)
        {

        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        public override bool OnCollision(Fixture f1, Fixture f2, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            CollidingSprite other = SyncedGameCollection.GetCollisionComponent(f2);

            if (other != null)
            {
                if (other.Tag == TagCategories.UNIT)
                {
                    Color = Color.Magenta;
                    return false;
                }
            }
            return false;

        }
    }
}
