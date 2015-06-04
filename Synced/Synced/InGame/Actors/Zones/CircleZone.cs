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
            : base(texture, position, rotation, color, game, world) { }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _victims.Count; i++)
            {
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
            base.Delete();
        }
    }
}
