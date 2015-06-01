// Player.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-30
// Edited by:
// Pontus Magnusson
// 
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Synced.Content;
using Synced.Static_Classes;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;
using Synced.InGame.Actors;
using FarseerPhysics;
using Synced.InGame.Actors.Zones;

namespace Synced.Actors
{
    class Player : GameComponent
    {
        #region Variables
        bool _areTrailsActive;
        bool _haveSwitched;
        PlayerIndex _playerIndex;
        Library.Zone.Name shape;
        Library.Colors.ColorName _teamColor;
        CompactZone _compactZone;
        List<CompactZone> _compactZones;
        World _world;
        Barrier _barrier;
        List<Zone> _zones;
        float _zoneCreationCooldown;
        bool _canCreateZone;
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

        public Library.Colors.ColorName TeamColor
        {
            get { return _teamColor; }
        }
        #endregion

        
        public Player(PlayerIndex playerIndex, Library.Character.Name character,Library.Colors.ColorName teamcolor, Game game, World world, Vector2 positionLeft, Vector2 positionRight)
            : base(game)
        {
            // TODO: fix hardcode positions. 
            _playerIndex = playerIndex;

            Left = new Unit(playerIndex, Library.Character.GameTexture[character], positionLeft, Library.Colors.getColor[Tuple.Create(teamcolor, Library.Colors.ColorVariation.Left)], game, world, teamcolor);       // TODO: fix hardcoded values for positions. 
            Right = new Unit(playerIndex, Library.Character.GameTexture[character], positionRight, Library.Colors.getColor[Tuple.Create(teamcolor, Library.Colors.ColorVariation.Right)], game, world, teamcolor);

            _areTrailsActive = false;
            _haveSwitched = false;
            _world = world;
            _barrier = new Barrier(Library.Particle.barrierParticle, Left, Right, world, game, Library.Colors.getColor[Tuple.Create(teamcolor, Library.Colors.ColorVariation.Other)]);
            shape = (Library.Zone.Name)character;
            _teamColor = teamcolor;
            _compactZones = new List<CompactZone>();
            _zones = new List<Zone>();
            _zoneCreationCooldown = 10.0f;
            _canCreateZone = true;

            SyncedGameCollection.ComponentCollection.Add(Left);
            SyncedGameCollection.ComponentCollection.Add(Right);
            SyncedGameCollection.ComponentCollection.Add(_barrier);
        }

        private float GetDistanceBetweenUnits()
        {
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
            _barrier.Activated();
        }
        private void DeActivateBarrier()
        {
            _barrier.Deactivated();
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
                    CreateZone(_compactZones[i].Shape, _compactZones[i].SimPosition, _compactZones[i].Rotation, _compactZones[i].Color);
                    _compactZones[i].Detonate();
                    _compactZones.RemoveAt(i);
                }
            }
        }

        public void CreateZone(Library.Zone.Name zoneshape, Vector2 position, float rotation, Color color)
        {
            switch (zoneshape)
            {
                case Library.Zone.Name.Circle:
                    CircleZone circleZ = new CircleZone(Library.Zone.Texture[shape], ConvertUnits.ToDisplayUnits(position), rotation, color, Game, _world);
                    _zones.Add(circleZ);
                    SyncedGameCollection.ComponentCollection.Add(circleZ);
                    break;
                case Library.Zone.Name.Triangle:
                    TriangleZone triangleZ = new TriangleZone(Library.Zone.Texture[shape], ConvertUnits.ToDisplayUnits(position), rotation, color, Game, _world);
                    _zones.Add(triangleZ);
                    SyncedGameCollection.ComponentCollection.Add(triangleZ);
                    break;
                //case Library.Zone.Name.Square:
                //    SquareZone squareZ = new SquareZone(Library.Zone.Texture[shape], ConvertUnits.ToDisplayUnits(position), rotation, color, Game, _world);
                //    _zones.Add(squareZ);
                //    SyncedGameCollection.ComponentCollection.Add(squareZ);
                //    break;
                case Library.Zone.Name.Pentagon:
                    PentagonZone pentagonZ = new PentagonZone(Library.Zone.Texture[shape], ConvertUnits.ToDisplayUnits(position), rotation, color, Game, _world);
                    _zones.Add(pentagonZ);
                    SyncedGameCollection.ComponentCollection.Add(pentagonZ);
                    break;
                case Library.Zone.Name.Hexagon:
                    HexagonZone hexagonZ = new HexagonZone(Library.Zone.Texture[shape], ConvertUnits.ToDisplayUnits(position), rotation, color, Game, _world);
                    _zones.Add(hexagonZ);
                    SyncedGameCollection.ComponentCollection.Add(hexagonZ);
                    break;
                default:
                    break;
            }
            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.ZoneExpand);
        }
        
        #endregion

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                #region Input

                Left.Direction = InputManager.LeftStickDirection(_playerIndex);
                Right.Direction = InputManager.RightStickDirection(_playerIndex);

                // Switch unit positions
                if (InputManager.IsButtonPressed(Buttons.LeftStick, _playerIndex) && InputManager.IsButtonPressed(Buttons.RightStick, _playerIndex) && !_haveSwitched)
                {
                    _haveSwitched = true;
                    Vector2 tmp = Left.Position;
                    Left.Position = Right.Position;
                    Right.Position = tmp;
                }
                else if (!InputManager.IsButtonPressed(Buttons.LeftStick, _playerIndex) && !InputManager.IsButtonPressed(Buttons.RightStick, _playerIndex))
                {
                    _haveSwitched = false;
                }

                if (InputManager.IsButtonPressed(Buttons.LeftShoulder, _playerIndex))
                {
                    DetonateZones();
                    Left.Shoot();
                }

                if (InputManager.IsButtonPressed(Buttons.RightShoulder,_playerIndex))
                {
                    DetonateZones();
                    Right.Shoot();
                }

                if (InputManager.RightTriggerValue(_playerIndex) != 0.0f)                    
                {
                    Right.TrailParticleLifetime += (1.5f * InputManager.RightTriggerValue(_playerIndex));
                }
                if (InputManager.LeftTriggerValue(_playerIndex) != 0.0f)
                {
                    Left.TrailParticleLifetime += (1.5f * InputManager.LeftTriggerValue(_playerIndex));
                }
                _areTrailsActive = false;
                if ((InputManager.RightTriggerValue(_playerIndex) > 0.0f) && (InputManager.LeftTriggerValue(_playerIndex) > 0.0f))
                {
                    _areTrailsActive = true;
                }
                #endregion

                #region SpeedUp
                Left.Acceleration = 40.0f;
                Right.Acceleration = 40.0f;
                Left.UseEffectParticles = false;
                Right.UseEffectParticles = false;
                if (AreTrailsActive && ConvertUnits.ToSimUnits(GetDistanceBetweenUnits()) < 2.0f)
                {
                    Left.Acceleration = 70.0f;
                    Right.Acceleration = 70.0f;
                    Left.UseEffectParticles = true;
                    Right.UseEffectParticles = true;
                }
                //if (AreTrailsActive)
                //{
                //    if (ConvertUnits.ToSimUnits(GetDistanceBetweenUnits()) < 2.0f)
                //    {
                //        Left.Acceleration = 70.0f;
                //        Right.Acceleration = 70.0f;
                //        Left.UseEffectParticles = true;
                //        Right.UseEffectParticles = true;
                //    }
                //    else
                //    {
                //        Left.Acceleration = 40.0f;
                //        Right.Acceleration = 40.0f;
                //        Left.UseEffectParticles = false;
                //        Right.UseEffectParticles = false;
                //    }
                //}
                //else
                //{
                //    Left.Acceleration = 40.0f;
                //    Right.Acceleration = 40.0f;
                //    Left.UseEffectParticles = false;
                //    Right.UseEffectParticles = false;
                //}
                #endregion

                #region Barrier
                if (!isBarrierActive)
                {
                    if (CheckBarrierActivationCondition())
                    {
                        _barrier.Activated();
                        isBarrierActive = true;
                    }
                }
                else
                {
                    if (CheckBarrierDeactivationCondition())
                    {
                        _barrier.Deactivated();
                        isBarrierActive = false;
                    }
                }
                #endregion

                #region Zones
                if (InputManager.IsButtonPressed(Buttons.B, _playerIndex) && _canCreateZone) // TODO: Remove. This is for testing
                {
                    Vector2 spawnPosition = new Vector2((Left.RigidBody.Position.X + Right.RigidBody.Position.X)/2.0f,(Left.RigidBody.Position.Y + Right.RigidBody.Position.Y)/2.0f);
                    _compactZone = new CompactZone(Library.Zone.CompactTexture[shape], ConvertUnits.ToDisplayUnits(spawnPosition), DrawingHelper.DrawingLevel.Medium, Game, _world, Library.Colors.getColor[Tuple.Create(_teamColor, Library.Colors.ColorVariation.Other)],shape);
                    SyncedGameCollection.ComponentCollection.Add(_compactZone);
                    _compactZones.Add(_compactZone);
                    _canCreateZone = false;
                    Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.ZoneSpawn);
                }

                for (int i = 0; i < _compactZones.Count; i++)
                {
                    if (_compactZones[i].UpdateCompactZone())
                    {
                        CreateZone(_compactZones[i].Shape, _compactZones[i].SimPosition, _compactZones[i].Rotation, _compactZones[i].Color);
                        _compactZones[i].Detonate();
                        _compactZones.RemoveAt(i);
                        i--;
                    }
                }
                #endregion        
    
                _zoneCreationCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_zoneCreationCooldown <= 0)
                {
                    Left.TrailEngine.ParticleColor = Library.Colors.getColor[Tuple.Create(_teamColor, Library.Colors.ColorVariation.Left)];
                    Right.TrailEngine.ParticleColor = Library.Colors.getColor[Tuple.Create(_teamColor, Library.Colors.ColorVariation.Right)];
                    _canCreateZone = true;
                    _zoneCreationCooldown = 10.0f;
                }
                
            }
        }
    }
}
