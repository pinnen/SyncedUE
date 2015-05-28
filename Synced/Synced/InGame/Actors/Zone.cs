using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.CollisionShapes;
using Synced.Content;
using Synced.InGame;
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
        protected enum ZoneState { None, Spawn, Active, Despawn, Delete }

        protected ZoneState _zoneState;

        //time
        float _timeSinceSpawn = 0.0f;
        float _lifetime = 10.0f;


        // Effects
        float _scaleTarget;
        ParticleEngine _particleEffects;
        protected ParticleEngine _victimParticles;
        protected List<IVictim> _victims;

        public Zone(Texture2D texture, Vector2 position,float rotation, Color color,Game game, World world) 
            : base(texture,position, rotation ,DrawingHelper.DrawingLevel.Medium,game,world,false)
        {
            Color = color;

            RigidBody.CollisionCategories = Category.Cat5;
            RigidBody.CollidesWith = Category.All;// ^ Category.Cat9;
            
            _zoneState = ZoneState.Spawn;
            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.ZoneExpand);

            Scale = 0.05f;
            Alpha = 0.5f;
            _scaleTarget = 1.0f;
            _particleEffects = new ParticleEngine(100,Library.Particle.trailTexture,position,color*0.05f,Vector2.Zero,1.0f,0.0f,0.5f,DrawingHelper.DrawingLevel.Medium,game);
            SyncedGameCollection.ComponentCollection.Add(_particleEffects);
            _victims = new List<IVictim>();
            
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
                        _particleEffects.SetParticleEngineOneShot(4);

                        _zoneState = ZoneState.Delete;
                        _timeSinceSpawn = 0;
                    }
                    break;
                case ZoneState.Delete:
                    //_timeSinceSpawn
                    this.Delete();
                    
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        public virtual void Delete() 
        {
            world.RemoveBody(RigidBody);
            
            //SyncedGameCollection.ComponentCollection.Remove(_particleEffects);
            SyncedGameCollection.ComponentCollection.Remove(this);      
        }

    }
}
