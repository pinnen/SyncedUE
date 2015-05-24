﻿// Player.cs
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

namespace Synced.Actors
{
    class Player : GameComponent
    {
        bool _areTrailsActive;

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

        PlayerIndex _playerIndex;

        public Player(PlayerIndex playerIndex, Library.Character.Name character, Game game, World world)
            : base(game)
        {
            _playerIndex = playerIndex;
            Left = new Unit(Library.Character.GameTexture[character], new Vector2(200, 200), Color.Red, game, world);       // TODO: fix hardcoded values for positions. 
            Right = new Unit(Library.Character.GameTexture[character], new Vector2(200, 120), Color.DarkRed, game, world);
            _areTrailsActive = false;
            game.Components.Add(this);
        }

        public float GetDistanceBetweenUnits() 
        {
            float d_y = (Left.Position.Y - Right.Position.Y) * (Left.Position.Y - Right.Position.Y);
            float d_x = (Left.Position.X - Right.Position.X) * (Left.Position.X - Right.Position.X);
            return (float)Math.Sqrt(d_y + d_x);
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                Left.Direction = InputManager.LeftStickDirection(_playerIndex);
                Right.Direction = InputManager.RightStickDirection(_playerIndex);

                if (InputManager.LeftShoulderPressed(_playerIndex))
                    Left.Shoot();

                if (InputManager.RightShoulderPressed(_playerIndex))
                    Right.Shoot();

                if (InputManager.RightTriggerPressed(_playerIndex) != 0.0f)
                {
                    Right.TrailParticleLifetime += (1.5f * InputManager.RightTriggerPressed(_playerIndex));
                }
                if (InputManager.LeftTriggerPressed(_playerIndex) != 0.0f)
                {
                    Left.TrailParticleLifetime += (1.5f * InputManager.LeftTriggerPressed(_playerIndex));
                }
                _areTrailsActive = false;
                if ((InputManager.RightTriggerPressed(_playerIndex) > 0.0f) && (InputManager.LeftTriggerPressed(_playerIndex) > 0.0f))
                {
                    _areTrailsActive = true;
                }
                
                //*****Speed up-ability*****
                if (AreTrailsActive)
                {
                    
                    if (GetDistanceBetweenUnits() < 2.0f)// TODO: constant
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
                
            }
        }

    }
}
