// Player.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-30
// Edited by:
// Pontus Magnusson
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Synced.Content;
using Synced.Static_Classes;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;
using Synced.InGame.Actors;
using FarseerPhysics;
using Synced.MapNamespace;
using Synced.Interface;

namespace Synced.Actors
{
    class Player : GameComponent
    {
        #region Variables
        bool _areTrailsActive;
        PlayerIndex _playerIndex;
        Library.Zone.Name shape;
        Library.Colors.ColorName _teamColor;
        CompactZone _compactZone;
        List<CompactZone> _compactZones;
        Game _game;
        World _world;
        Barrier _barrier;
        #endregion
        
        #region Properties
        public bool AreTrailsActive
        {
            get { return _areTrailsActive; }
            set { _areTrailsActive = value; }
        }

        public Unit Left
        { get; set; }

        public Unit Right
        {
            get;
            set;
        }
        #endregion

        
        public Player(PlayerIndex playerIndex, Library.Character.Name character,Library.Colors.ColorName teamcolor, Game game, World world)
            : base(game)
        {
            // TODO: fix hardcode positions. 
            _playerIndex = playerIndex;

            Left = new Unit(Library.Character.GameTexture[character], new Vector2(500, 500), Library.Colors.getColor[Tuple.Create(teamcolor,Library.Colors.ColorVariation.Left)], game, world, teamcolor);       // TODO: fix hardcoded values for positions. 
            Right = new Unit(Library.Character.GameTexture[character], new Vector2(500, 600), Library.Colors.getColor[Tuple.Create(teamcolor, Library.Colors.ColorVariation.Right)], game, world, teamcolor);

            _areTrailsActive = false;
            _game = game;
            _world = world;
            _barrier = new Barrier(Library.Particle.barrierParticle, Left, Right, world, game, Library.Colors.getColor[Tuple.Create(teamcolor, Library.Colors.ColorVariation.Other)]);
            shape = (Library.Zone.Name)character;
            _teamColor = teamcolor;
            _compactZones = new List<CompactZone>();

            SyncedGameCollection.ComponentCollection.Add(Left);
            SyncedGameCollection.ComponentCollection.Add(Right);
            SyncedGameCollection.ComponentCollection.Add(_barrier);
        }

        private float GetDistanceBetweenUnits()
        {
            //TODO: create more efficient way to do this
            float d_y = (Left.Position.Y - Right.Position.Y) * (Left.Position.Y - Right.Position.Y);
            float d_x = (Left.Position.X - Right.Position.X) * (Left.Position.X - Right.Position.X);
            return (float)Math.Sqrt(d_y + d_x);
        }

        #region Barrier-PowerUp
        bool isBarrierActive = false;
        private bool CheckBarrierActivationCondition()
        {
            if (ConvertUnits.ToSimUnits(GetDistanceBetweenUnits()) < 0.5f && _areTrailsActive && !isBarrierActive) // TODO: fix hardcoded distance value
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckBarrierDeactivationCondition()
        {
            if (ConvertUnits.ToSimUnits(GetDistanceBetweenUnits()) > 12f && isBarrierActive) // TODO: fix hardcoded distance value
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ActivateBarrier()
        {
            _barrier.Activate();
        }
        private void DeActivateBarrier()
        {
            _barrier.Deactivate();
        }
        #endregion

        #region SpeedUp-PowerUp
        private bool CheckSpeedUpActivationCondition()
        {
            return true;
        }
        private bool CheckSpeedUpDeactivationCondition()
        {
            return false;
        }
        #endregion

        #region CreateZone-PowerUp
        private bool CheckCreateZoneActivateCondition()
        {
            return false;
        }
        private void DetonateZones()
        {
            for (int i = 0; i < _compactZones.Count; i++)
            {
                if (_compactZones[i].IsShot)
                {
                    _compactZones[i].Detonate();
                    _compactZones.RemoveAt(i);
                }
            }
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                #region Input

                Left.Direction = InputManager.LeftStickDirection(_playerIndex);
                Right.Direction = InputManager.RightStickDirection(_playerIndex);

                if (InputManager.IsButtonPressed(Buttons.LeftShoulder,_playerIndex))
                {
                    DetonateZones();
                    Left.Shoot();
                    
                }

                if (InputManager.IsButtonPressed(Buttons.RightShoulder,_playerIndex))
                {
                    DetonateZones();
                    Right.Shoot();
                    
                }
                    
                if (InputManager.RightTriggerPressed(_playerIndex) != 0.0f)
                {
                    Right.TrailParticleLifetime += (1.5f * InputManager.RightTriggerPressed(_playerIndex)); // TODO: constant
                }
                if (InputManager.LeftTriggerPressed(_playerIndex) != 0.0f)
                {
                    Left.TrailParticleLifetime += (1.5f * InputManager.LeftTriggerPressed(_playerIndex)); // TODO: constant
                }
                _areTrailsActive = false;
                if ((InputManager.RightTriggerPressed(_playerIndex) > 0.0f) && (InputManager.LeftTriggerPressed(_playerIndex) > 0.0f))
                {
                    _areTrailsActive = true;
                }
                #endregion

                #region SpeedUp
                if (AreTrailsActive)
                {
                    
                    if (ConvertUnits.ToSimUnits(GetDistanceBetweenUnits()) < 2.0f)// TODO: constant
                    {
                        Math.Min(Left.Acceleration += 1.0f,60.0f); //TODO: constant
                        Math.Min(Right.Acceleration += 1.0f,60.0f); // TODO: constant
                        Left.UseEffectParticles = true;
                        Right.UseEffectParticles = true;
                    }
                    else
                    {
                        Left.Acceleration = 40.0f; //TODO: constant
                        Right.Acceleration = 40.0f; //TODO: constant
                        Left.UseEffectParticles = false;
                        Right.UseEffectParticles = false;
                    }
                }
                else
                {
                    Left.Acceleration = 40.0f; //TODO: constant
                    Right.Acceleration = 40.0f; //TODO: constant
                    Left.UseEffectParticles = false;
                    Right.UseEffectParticles = false;
                }
                #endregion

                #region Barrier
                if (!isBarrierActive)
                {
                    if (CheckBarrierActivationCondition())
                    {
                        _barrier.Activate();
                        isBarrierActive = true;
                    }
                }
                else
                {
                    if (CheckBarrierDeactivationCondition())
                    {
                        _barrier.Deactivate();
                        isBarrierActive = false;
                    }
                }
                #endregion

                #region Zones
                if (InputManager.LeftShoulderPressed(_playerIndex) && InputManager.RightShoulderPressed(_playerIndex)) // FOR TESTING
                {
                    Vector2 spawnPosition = new Vector2((Left.RigidBody.Position.X + Right.RigidBody.Position.X) / 2.0f, (Left.RigidBody.Position.Y + Right.RigidBody.Position.Y) / 2.0f);
                    _compactZone = new CompactZone(Library.Zone.CompactTexture[shape], ConvertUnits.ToDisplayUnits(spawnPosition), DrawingHelper.DrawingLevel.Medium, _game, _world, Library.Colors.getColor[Tuple.Create(_teamColor, Library.Colors.ColorVariation.Other)]);
                    SyncedGameCollection.ComponentCollection.Add(_compactZone);
                    _compactZones.Add(_compactZone);
                }
                #endregion            
            }
        }
    }
}
