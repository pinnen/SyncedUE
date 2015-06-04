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
                        }
                    }

                    if (this._zoneState == ZoneState.Despawn)
                    {
                        MakeVisible(_victims[i]);
                    }
                   
                }
            }
            base.Update(gameTime);
        }
        public override bool OnCollision(Fixture f1, Fixture f2, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            return false;
        }

        private void MakeVisible(IVictim victim) 
        {
            victim.InvisibilityAlpha = 1.0f;
        }

        public override void Delete()
        {
            base.Delete();
        }

    }
}
