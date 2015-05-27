using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Content;
using Synced.InGame.Actors;
using Synced.Static_Classes;
// Zone.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-27
// Edited by:
// Pontus Magnusson
// Lina Juuso
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Actors
{
    abstract class Zone : Sprite
    {
        enum ZoneState { None, Spawn, Active, Despawn, Delete }

        ZoneState _zoneState;

        //time
        float _timeSinceSpawn = 0.0f;
        float _lifetime = 10.0f;

        //List<IVictim> _victims;

        // Effects
        float _scaleTarget;
        ParticleEngine _particleEffects;

        public Zone(Texture2D texture, Vector2 position, Color color,Game game) 
            : base(texture,position,DrawingHelper.DrawingLevel.Low,game)
        {
            _zoneState = ZoneState.Spawn;
            //_victims = new List<IVictim>();
            Scale = 0.05f;
            _scaleTarget = 1.0f;
            _particleEffects = new ParticleEngine(100,Library.Particle.trailTexture,position,color,Vector2.Zero,1.0f,0.0f,10.0f,DrawingHelper.DrawingLevel.Medium,game);
        }

        public override void Update(GameTime gameTime)
        {
            switch (_zoneState)
            {
                case ZoneState.None:
                    break;
                case ZoneState.Spawn:
                    Scale += 0.1f;
                    if (Scale >= _scaleTarget)
                    {
                        _zoneState = ZoneState.Active;
                    }
                    break;
                case ZoneState.Active:
                    _timeSinceSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_timeSinceSpawn > _lifetime)
                    {
                        _zoneState = ZoneState.Despawn;
                    }
                    break;
                case ZoneState.Despawn:
                    Scale -= 0.1f;
                    if (Scale <= 0.0f)
                    {
                        _particleEffects.GenerateClusterParticles();
                        _particleEffects.ShatterParticles();

                        _zoneState = ZoneState.Delete;
                        _timeSinceSpawn = 0;
                        //DELETE COLLISION?
                    }
                    break;
                case ZoneState.Delete:
                    //_timeSinceSpawn
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }
    }
}
