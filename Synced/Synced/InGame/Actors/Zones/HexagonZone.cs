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
    class HexagonZone : Zone
    {
        Random _random;
        Game _game;
        World _world;

        List<EvilCrystal> _evilCrystalList;
        List<EvilUnit> _evilUnitList;

        float _waitTime;

        public HexagonZone(Texture2D texture, Vector2 position, float rotation, Color color, Game game, World world)
            : base(texture, position, rotation, color, game, world)
        {
            _random = new Random();
            _evilCrystalList = new List<EvilCrystal>();
            _evilUnitList = new List<EvilUnit>();
            _game = game;
            _world = world;
            _waitTime = 0.4f;
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
            return false;
        }

        private void CreateVictimBunshin(IVictim victim) 
        {
            if (victim is Crystal)
            {
                Vector2 randomDirection = new Vector2(_random.Next(-30,30),_random.Next(-30,30));
                randomDirection.Normalize();
                EvilCrystal tempEvilCrystal = new EvilCrystal(victim.VictimTexture, victim.Position, DrawHelper.DrawingLevel.Medium, _game, _world, victim.Color);
                tempEvilCrystal.Direction = randomDirection;
                tempEvilCrystal.LinearVelocity = victim.VictimLinearVelocity;
                _evilCrystalList.Add(tempEvilCrystal);
            }

            else if(victim is Unit)
            {
                float randomAngleOffset = (_random.Next(-180,180)/10);
                EvilUnit tempEvilUnit = new EvilUnit(victim.VictimTexture, victim.Position, victim.Color, _game, _world, DrawHelper.DrawingLevel.Medium, (Unit)victim,randomAngleOffset);

                _evilUnitList.Add(tempEvilUnit);

            }
        }

        public override void Delete() 
        {
            while (_evilCrystalList.Count > 0)
            {
                world.RemoveBody(_evilCrystalList[0].RigidBody);
                _evilCrystalList.RemoveAt(0);
            }
            _evilCrystalList.Clear();
            while (_evilUnitList.Count > 0)
            {
                world.RemoveBody(_evilUnitList[0].RigidBody);
                _evilUnitList.RemoveAt(0);
            }
            _evilUnitList.Clear();
            _evilCrystalList.Clear();
            base.Delete();
        }
    }
}
