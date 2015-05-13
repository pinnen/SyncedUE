// GameStateMachine.cs
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
    class GameStateMachine
    {
        public GameStateMachine(State gameState)
        {
            CurrentState = gameState;
        }

        public State CurrentState
        {
            get;
            set;
        }

        public void Update()
        {
            CurrentState.Update(this);
        }
        public void Play()
        {
            CurrentState.Play(this);
        }
        public void Pause()
        {
            CurrentState.Pause(this);
        }
        public void Resume()
        {
            CurrentState.Resume(this);
        }
        public void Finish()
        {
            CurrentState.Finish(this);
        }
        public void Rematch()
        {
            CurrentState.Rematch(this);
        }
        public void ReturnToMenu()
        {
            CurrentState.ReturnToMenu(this);
        }
        public string GetStateName()
        {
            return CurrentState.GetStateName();
        }
    }
}
