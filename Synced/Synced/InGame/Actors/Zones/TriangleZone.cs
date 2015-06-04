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
    class TriangleZone : Zone
    {
        Random _random;

        public TriangleZone(Texture2D texture, Vector2 position, float rotation, Color color, Game game, World world)
            : base(texture, position, rotation, color, game, world)
        {
            //_victimParticles = new ParticleEngine(5, Library.Particle.starTexture, position, Color.White, Vector2.Zero, 0.5f, 0.0f, 0.5f, DrawingHelper.DrawingLevel.Medium, game);
            //SyncedGameCollection.ComponentCollection.Add(_victimParticles);
            _random = new Random();
        }

        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < _victims.Count; i++)
            {
                //_victimParticles.UpdatePosition(_victims[i].Position);
                //_victimParticles.ParticleColor = _victims[i].Color;
                //_victimParticles.GenerateEffectParticles(0.5f, 0.5f);
                //_victimParticles.ExpandAndRotate(0.2f,0.05f);

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
            //CollidingSprite other = SyncedGameCollection.GetCollisionComponent(f2);

            //if (other != null)
            //{
            //    if (other is IVictim)
            //    {
            //        if (!_victims.Contains((IVictim)other))
            //        {
            //            if (other is Unit)
            //            {
            //                (other as Unit).Shoot();
            //            }
            //            _victims.Add((IVictim)other);
                        
            //        }
            //    }

            //}
            return false;
        }

        private void Teleport(IVictim victim)
        {
            float x = ResolutionManager.GetWidth / 2.0f;
            x = x + _random.Next(-(ResolutionManager.GetWidth / 4), (ResolutionManager.GetWidth / 4));
            float y = ResolutionManager.GetHeight / 2.0f;
            y = y + _random.Next(-(ResolutionManager.GetHeight / 4), (ResolutionManager.GetHeight/4));

            //Vector2 randomDirection = new Vector2(_random.Next(-10,10),_random.Next(-10,10));
            //randomDirection.Normalize();

            //victim.Direction = randomDirection;
            victim.Rotation = (float)(_random.Next(-180, 180) / 10);
            victim.Position = new Vector2(x, y);
        }
        public override void Delete()
        {
            //_victimParticles.DeleteParticleEngine();
            base.Delete();
        }
    }
}
