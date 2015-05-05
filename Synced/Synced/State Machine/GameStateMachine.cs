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
        private State _currentState;
        public GameStateMachine(State gameState)
        {
            _currentState = gameState;
        }

        public State CurrentState
        {
            get { return _currentState; }
            set { _currentState = value;}
        }

        public void Update()
        {
            _currentState.Update(this);
        }
        public void Play()
        {
            _currentState.Play(this);
        }
        public void Pause()
        {
            _currentState.Pause(this);
        }
        public void Resume()
        {
            _currentState.Resume(this);
        }
        public void Finish()
        {
            _currentState.Finish(this);
        }
        public void Rematch()
        {
            _currentState.Rematch(this);
        }
        public void ReturnToMenu()
        {
            _currentState.ReturnToMenu(this);
        }
        public string GetStateName()
        {
            return _currentState.GetStateName();
        }
    }
}
