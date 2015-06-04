using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.CollisionShapes;
using Synced.Content;
using Synced.InGame;
using Synced.InGame.Actors;
using Synced.Static_Classes;
// Zone.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-27
// Edited by:
// Pontus Magnusson
// Lina Juuso
// 
using System.Collections.Generic;

namespace Synced.InGame.Actors.Zones
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
        protected List<IVictim> _victims;

        public Zone(Texture2D texture, Vector2 position,float rotation, Color color,Game game, World world) 
            : base(texture,position, rotation ,DrawingHelper.DrawingLevel.Medium,game,world,false)
        {
            Color = color;

            RigidBody.CollisionCategories = Category.Cat5;
            RigidBody.CollidesWith = Category.All;
            
            _zoneState = ZoneState.Spawn;
            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.ZoneExpand);

            Scale = 0.05f;
            Alpha = 0.5f;
            _scaleTarget = 1.0f;
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
                        _zoneState = ZoneState.Delete;
                        _timeSinceSpawn = 0;
                    }
                    break;
                case ZoneState.Delete:
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
        }

    }
}
