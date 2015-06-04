// InputManager.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-27
// Edited by:
// Pontus Magnusson 
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Synced.Static_Classes
{
    static class InputManager
    {
        static Dictionary<PlayerIndex, GamePadState> _LastStates = new Dictionary<PlayerIndex, GamePadState>();

        public static void Update()
        {
            foreach (PlayerIndex p in Enum.GetValues(typeof(PlayerIndex)))
                _LastStates[p] = GamePad.GetState(p);
        }

        // Button press
        public static bool IsButtonPressed(PlayerIndex playerIndex, Buttons button)
        {
            return (GamePad.GetState(playerIndex).IsButtonDown(button) && _LastStates[playerIndex].IsButtonUp(button));
        }

        // Button down
        public static bool IsButtonDown(PlayerIndex playerIndex, Buttons button)
        {
            return GamePad.GetState(playerIndex).IsButtonDown(button);
        }

        // Use stick as Dpad
        public static bool LeftStickLeft(PlayerIndex playerIndex)
        {
            return ((GamePad.GetState(playerIndex).ThumbSticks.Left.X > 0f && _LastStates[playerIndex].ThumbSticks.Left.X <= 0f))
                || (IsButtonPressed(playerIndex, Buttons.DPadLeft));
        }
        public static bool LeftStickRight(PlayerIndex playerIndex)
        {
            return ((GamePad.GetState(playerIndex).ThumbSticks.Left.X < 0f && _LastStates[playerIndex].ThumbSticks.Left.X >= 0f))
                || (IsButtonPressed(playerIndex, Buttons.DPadRight));
        }
        public static bool LeftStickUp(PlayerIndex playerIndex)
        {
            return (GamePad.GetState(playerIndex).ThumbSticks.Left.Y > 0f && _LastStates[playerIndex].ThumbSticks.Left.Y <= 0f)
                || (IsButtonPressed(playerIndex, Buttons.DPadUp));
        }
        public static bool LeftStickDown(PlayerIndex playerIndex)
        {
            return (GamePad.GetState(playerIndex).ThumbSticks.Left.Y < 0f && _LastStates[playerIndex].ThumbSticks.Left.Y >= 0f)
                || (IsButtonPressed(playerIndex, Buttons.DPadDown));
        }
        public static float LeftTriggerValue(PlayerIndex playerIndex)
        {
            return GamePad.GetState(playerIndex).Triggers.Left;
        }
        public static float RightTriggerValue(PlayerIndex playerIndex)
        {
            return GamePad.GetState(playerIndex).Triggers.Right;
        }
        public static Vector2 LeftStickDirection(PlayerIndex playerIndex)
        {
            return GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Left;
        }
        public static Vector2 RightStickDirection(PlayerIndex playerIndex)
        {
            return GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right;
        }
    }
}
