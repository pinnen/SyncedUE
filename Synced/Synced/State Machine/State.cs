// GameState.cs
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
    abstract class State
    {
        public virtual void Play(GameStateMachine gameStateMachine) { }
        public virtual void Pause(GameStateMachine gameStateMachine) { }
        public virtual void Resume(GameStateMachine gameStateMachine) { }
        public virtual void Finish(GameStateMachine gameStateMachine) { }
        public virtual void Rematch(GameStateMachine gameStateMachine) { }
        public virtual void ReturnToMenu(GameStateMachine gameStateMachine) { }
        public virtual string GetStateName() { return "No State"; }
    }
}
