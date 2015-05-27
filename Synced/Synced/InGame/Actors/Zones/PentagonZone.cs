using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Content;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Actors.Zones
{
    class PentagonZone : Zone
    {
        public PentagonZone(Texture2D texture, Vector2 position, float rotation, Color color, Game game, World world)
            : base(texture, position,rotation, color, game,world)
        {
            _victimParticles = new ParticleEngine(1, Library.Particle.questionSignTexture, position, Color.White, Vector2.Zero, 1.0f, 0.0f, 0.5f, DrawingHelper.DrawingLevel.Medium, game);
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
    }
}
