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
        float _invisibilityAlphaFadeAmount;

        public PentagonZone(Texture2D texture, Vector2 position, float rotation, Color color, Game game, World world)
            : base(texture, position,rotation, color, game,world)
        {
            _invisibilityAlphaFadeAmount = 0.02f;
            _victimParticles = new ParticleEngine(1, Library.Particle.questionSignTexture, position, Color.White, Vector2.Zero, 1.0f, 0.0f, 0.5f, DrawingHelper.DrawingLevel.Medium, game);
            SyncedGameCollection.ComponentCollection.Add(_victimParticles);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _victims.Count; i++)
            {
                _victims[i].PentagonEffectTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_victims[i].PentagonEffectTimer <= 0)
                {
                    if (_victims[i].FadeOut) // FADE OUT
                    {
                        if (_victims[i].InvisibilityAlpha <= -1.0f)
                        {
                            _victims[i].FadeOut = false;
                        }
                        else
                        {
                            _victims[i].InvisibilityAlpha -= _invisibilityAlphaFadeAmount;
                            if (_victims[i].TrailEngine != null){ _victims[i].TrailEngine.SetParticleFadeAlpha(_victims[i].InvisibilityAlpha / 2.0f);}
                            
                        }
                    }
                    else // FADE IN
                    {
                        if (_victims[i].InvisibilityAlpha >= 0.6f)
                        {
                            _victims[i].FadeOut = true;
                        }
                        else
                        {
                            _victims[i].InvisibilityAlpha += _invisibilityAlphaFadeAmount;
                            if (_victims[i].TrailEngine != null){_victims[i].TrailEngine.SetParticleFadeAlpha(_victims[i].InvisibilityAlpha/2.0f);}
                        }
                    }

                    _victimParticles.UpdatePosition(_victims[i].Position);
                    _victimParticles.ParticleColor = _victims[i].Color;
                    _victimParticles.SetParticleFadeAlpha(_victims[i].InvisibilityAlpha);
                    _victimParticles.GenerateEffectParticles(1.0f, 0.2f);

                    if (this._zoneState == ZoneState.Despawn)
                    {
                        MakeVisible(_victims[i]);
                        if (_victims[i].TrailEngine != null) 
                        {
                            _victims[i].TrailEngine.SetParticleFadeAlpha(1.0f);
                            _victims[i].TrailEngine.GenerateTrailParticles();
                        }
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
                    if (!_victims.Contains((IVictim)other))
                    {
                        _victims.Add((IVictim)other);
                    } 
                }
                
            }
            return false;

        }

        private void MakeVisible(IVictim victim) 
        {
            victim.InvisibilityAlpha = 1.0f;
        }

    }
}
