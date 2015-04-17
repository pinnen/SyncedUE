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
        public abstract void Play(GameStateMachine gameStateMachine);
        public abstract void Pause(GameStateMachine gameStateMachine);
        public abstract void Resume(GameStateMachine gameStateMachine);
        public abstract void Finish(GameStateMachine gameStateMachine);
        public abstract void Rematch(GameStateMachine gameStateMachine);
        public abstract void ReturnToMenu(GameStateMachine gameStateMachine);
    }
}
