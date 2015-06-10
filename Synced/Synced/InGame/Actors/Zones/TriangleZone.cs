using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SevenEngine.Drawing;
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
        Random _random;

        public TriangleZone(Texture2D texture, Vector2 position, float rotation, Color color, Game game, World world)
            : base(texture, position, rotation, color, game, world)
        {
            _random = new Random();
        }

        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < _victims.Count; i++)
            {
                _victims[i].TriangleEffectTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_victims[i].TriangleEffectTimer <= 0)
                {
                    Teleport(_victims[i]);
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

        private void Teleport(IVictim victim)
        {
            float x = ResolutionManager.GetWidth / 2.0f;
            x = x + _random.Next(-(ResolutionManager.GetWidth / 4), (ResolutionManager.GetWidth / 4));
            float y = ResolutionManager.GetHeight / 2.0f;
            y = y + _random.Next(-(ResolutionManager.GetHeight / 4), (ResolutionManager.GetHeight/4));

            victim.Rotation = (float)(_random.Next(-180, 180) / 10);
            victim.Position = new Vector2(x, y);
        }
        public override void Delete()
        {
            base.Delete();
        }
    }
}
