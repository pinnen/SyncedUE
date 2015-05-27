using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.CollisionShapes;
using Synced.Content;
using Synced.InGame.Actors;
using Synced.Interface;
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
    abstract class Zone : TexturePolygon
    {
        enum ZoneState { None, Spawn, Active, Despawn, Delete }

        ZoneState _zoneState;

        //time
        float _timeSinceSpawn = 0.0f;
        float _lifetime = 10.0f;


        // Effects
        float _scaleTarget;
        ParticleEngine _particleEffects;
        protected ParticleEngine _victimParticles;

        public Zone(Texture2D texture, Vector2 position,float rotation, Color color,Game game, World world) 
            : base(texture,position, rotation ,DrawingHelper.DrawingLevel.Low,game,world,true)
        {
            Color = color;

            RigidBody.CollisionCategories = Category.Cat5;
            RigidBody.CollidesWith = Category.All;// ^ Category.Cat9;
            
            Origin = new Vector2(Texture.Width / 2, texture.Height / 2);
            _zoneState = ZoneState.Spawn;
            Scale = 0.05f;
            Alpha = 0.5f;
            _scaleTarget = 1.0f;
            _particleEffects = new ParticleEngine(100,Library.Particle.trailTexture,position,color*0.05f,Vector2.Zero,1.0f,0.0f,0.5f,DrawingHelper.DrawingLevel.Medium,game);
            SyncedGameCollection.ComponentCollection.Add(_particleEffects);
            
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
                    if (Scale <= 0.05f)
                    {
                        _particleEffects.GenerateClusterParticles();
                        _particleEffects.ShatterParticles(50,15);
                        _particleEffects.ExpandAndRotate();
                        

                        _zoneState = ZoneState.Delete;
                        _timeSinceSpawn = 0;
                    }
                    break;
                case ZoneState.Delete:
                    //_timeSinceSpawn
                    RigidBody.Dispose();
                    SyncedGameCollection.ComponentCollection.Remove(this);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

    }
}
