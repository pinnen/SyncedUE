// PauseState.cs
// Introduced: 2015-04-17
// Last edited: 2015-04-17
// Edited by:
// Pontus Magnusson
//
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.State_Machine
{
    class PauseState : State
    {
        public override void Resume(GameStateMachine gameStateMachine)
        {
            gameStateMachine.CurrentState = new GameState();
        }
    }
}
