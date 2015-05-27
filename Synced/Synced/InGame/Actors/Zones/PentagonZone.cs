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
            for (int i = 0; i < _victims.Count; i++)
            {
                _victims[i].PentagonEffectTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_victims[i].PentagonEffectTimer <= 0)
                {
                    if (_victims[i].FadeOut)
                    {

                    }
                   
                }
            }
            base.Update(gameTime);
        }
        public override bool OnCollision(Fixture f1, Fixture f2, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            CollidingSprite other = SyncedGameCollection.GetCollisionComponent(f2);

            if (other != null)
            {
                if (other is IVictim) 
                {
                    _victims.Add((IVictim)other);
                }
                
            }
            return false;

        }

    }
}
