// InputManager.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson
//
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Synced.InputCommands;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Synced.Game_Actors;

namespace Synced.Static_Classes
{
    class InputManager
    {
        public void HandleInput(GameActor gameActor)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed) 
                _buttonA.Execute(gameActor);
        }

        private Command _buttonX;
        private Command _buttonY;
        private Command _buttonA;
        private Command _buttonB;
        private Command _leftStickClick;
        private Command _rightStickClick;
        private Command _leftStick;
        private Command _rightStick;
        private Command _leftTrigger;
        private Command _rightTrigger;
        private Command _leftBumper;
        private Command _rightBumper;
        private Command _view; // Old start button
        private Command _menu; // Old select button
        private Command _guide;


    }
}
