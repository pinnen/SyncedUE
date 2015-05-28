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
    class HexagonZone : Zone
    {
        Random _random;
        Game _game;
        World _world;
        int _maximumBunshin;

        List<EvilCrystal> _evilCrystalList;
        List<EvilUnit> _evilUnitList;

        float _waitTime;
        ParticleEngine _particleEffects;

        public HexagonZone(Texture2D texture, Vector2 position, float rotation, Color color, Game game, World world)
            : base(texture, position, rotation, color, game, world)
        {
            _random = new Random();
            _evilCrystalList = new List<EvilCrystal>();
            _evilUnitList = new List<EvilUnit>();
            _game = game;
            _world = world;
            _maximumBunshin = 15;
            _waitTime = 0.4f;

            _particleEffects = new ParticleEngine(100, Library.Particle.trailTexture, position, color * 0.005f, Vector2.Zero, 0.2f, 0.0f, 0.5f, DrawingHelper.DrawingLevel.Medium, game);
            SyncedGameCollection.ComponentCollection.Add(_particleEffects);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _victims.Count; i++)
            {
                _victims[i].HexagonEffectTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_victims[i].HexagonEffectTimer <= 0.0f)
                {
                    _victims[i].HexagonEffectTimer = _waitTime;
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
                        if (other is Unit)
                        {
                            if (_evilCrystalList.Count + _evilUnitList.Count > _maximumBunshin && _evilUnitList.Count > 0)
                            {
                                world.RemoveBody(_evilUnitList[0].RigidBody);
                                SyncedGameCollection.ComponentCollection.Remove(_evilUnitList[0]);
                                _evilUnitList.RemoveAt(0);
                            }
                            CreateVictimBunshin((IVictim) other);
                            _victims.Add((IVictim)other);
                        }
                        else if (other is Crystal)
                        {
                            if (_evilCrystalList.Count + _evilUnitList.Count > _maximumBunshin && _evilCrystalList.Count > 0) 
                            {
                                world.RemoveBody(_evilCrystalList[0].RigidBody);
                                SyncedGameCollection.ComponentCollection.Remove(_evilCrystalList[0]);
                                _evilCrystalList.RemoveAt(0);
                            }
                            CreateVictimBunshin((IVictim) other);
                            _victims.Add((IVictim)other);
                        }
                    }
                }

            }
            return false;

        }

        private void CreateVictimBunshin(IVictim victim) 
        {
            if (victim is Crystal)
            {
                Vector2 randomDirection = new Vector2(_random.Next(-30,30),_random.Next(-30,30));
                randomDirection.Normalize();
                EvilCrystal tempEvilCrystal = new EvilCrystal(victim.VictimTexture, victim.Position, DrawingHelper.DrawingLevel.Medium, _game, _world, victim.Color);
                tempEvilCrystal.Direction = randomDirection;
                tempEvilCrystal.LinearVelocity = victim.VictimLinearVelocity;
                _evilCrystalList.Add(tempEvilCrystal);
                SyncedGameCollection.ComponentCollection.Add(tempEvilCrystal);
            }

            else if(victim is Unit)
            {
                float randomAngleOffset = (_random.Next(-180,180)/10);
                EvilUnit tempEvilUnit = new EvilUnit(victim.VictimTexture, victim.Position, victim.Color, _game, _world, DrawingHelper.DrawingLevel.Medium, (Unit)victim,randomAngleOffset);

                _evilUnitList.Add(tempEvilUnit);
                SyncedGameCollection.ComponentCollection.Add(tempEvilUnit);

            }
        }

        public override void Delete() 
        {
            while (_evilCrystalList.Count > 0)
            {
                world.RemoveBody(_evilCrystalList[0].RigidBody);
                _particleEffects.UpdatePosition(_evilCrystalList[0].Position);
                _particleEffects.ParticleColor = _evilCrystalList[0].Color * 0.01f;
                _particleEffects.GenerateClusterParticles();
                _particleEffects.ShatterParticles(50, 5);
                _particleEffects.ExpandAndRotate();
                SyncedGameCollection.ComponentCollection.Remove(_evilCrystalList[0]);
                _evilCrystalList.RemoveAt(0);
            }
            _evilCrystalList.Clear();
            while (_evilUnitList.Count > 0)
            {
                world.RemoveBody(_evilUnitList[0].RigidBody);
                _particleEffects.UpdatePosition(_evilUnitList[0].Position);
                _particleEffects.ParticleColor = _evilUnitList[0].Color * 0.01f;
                _particleEffects.GenerateClusterParticles();
                _particleEffects.ShatterParticles(50, 5);
                _particleEffects.ExpandAndRotate();
                SyncedGameCollection.ComponentCollection.Remove(_evilUnitList[0]);
                _evilUnitList.RemoveAt(0);
            }
            _evilUnitList.Clear();
            _evilCrystalList.Clear();
            _particleEffects.DeleteParticleEngine();
            base.Delete();
        }
    }
}
