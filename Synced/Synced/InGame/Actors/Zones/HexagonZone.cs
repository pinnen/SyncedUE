using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
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
    }
}
