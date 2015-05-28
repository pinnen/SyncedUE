using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Content;
using Synced.Interface;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Actors.Zones
{
    class CircleZone : Zone
    {
        public CircleZone(Texture2D texture, Vector2 position, float rotation, Color color, Game game, World world) 
            : base(texture, position,rotation, color, game,world) 
        {
            _victimParticles = new ParticleEngine(1,Library.Particle.minusSignTexture,position,Color.White,Vector2.Zero,1.0f,0.0f,0.5f,DrawingHelper.DrawingLevel.Medium,game);
            SyncedGameCollection.ComponentCollection.Add(_victimParticles);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _victims.Count; i++)
            {
                _victimParticles.UpdatePosition(_victims[i].Position);
                _victimParticles.ParticleColor = _victims[i].Color;
                _victimParticles.GenerateEffectParticles(1.0f, 0.2f);

                _victims[i].CircleEffectTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_victims[i].CircleEffectTimer <= 0.0f)
                {
                    ResetEffect(_victims[i]);
                    _victims.RemoveAt(i);
                    i--;
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
                    if (!_victims.Contains((IVictim)other))
                    {
                        _victims.Add((IVictim)other);
                        SlowDown((IVictim)other);
                    } 
                }
                
            }
            return false;

        }
        private void SlowDown(IVictim victim) 
        {
            victim.LocalTimeScale = 0.2f;
        }
        private void ResetEffect(IVictim victim) 
        {
            victim.LocalTimeScale = 1.0f;
        }

        public override void Delete()
        {
            _victimParticles.DeleteParticleEngine();
            base.Delete();
        }
    }
}
