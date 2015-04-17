// ScoreState.cs
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
    class ScoreState : State
    {
        public override void ReturnToMenu(GameStateMachine gameStateMachine)
        {
            gameStateMachine.CurrentState = new MenuState();
        }
    }
}
