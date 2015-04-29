// MenuState.cs
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
    class MenuState : State
    {
        public MenuState()
        {

        }

        public override void Update()
        {

            base.Update();
        }

        public override void Play(GameStateMachine gameStateMachine)
        {
            gameStateMachine.CurrentState = new GameState();
        }
        public override string GetStateName() { return "Menu State"; }
        
    }
}
