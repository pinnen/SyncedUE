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
    class TriangleZone : Zone
    {
        public TriangleZone(Texture2D texture, Vector2 position, Color color, Game game)
            : base(texture, position, color, game)
        {//STAR
            _victimParticles = new ParticleEngine(1, Library.Particle.barrierParticle, position, Color.White, Vector2.Zero, 1.0f, 0.0f, 0.5f, DrawingHelper.DrawingLevel.Medium, game);
        }
    }
}
