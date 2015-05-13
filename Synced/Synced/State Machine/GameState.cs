// InGameState.cs
// Introduced: 2015-04-17
// Last edited: 2015-05-12
// Edited by:
// Pontus Magnusson
//
using Synced.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.State_Machine
{
    class GameState : State
    {
        public GameState(Game game) : base(game)
        {
            Library.Audio.PlaySong(Library.Audio.Songs.InGame);
        }
        public override void Pause(GameStateMachine gameStateMachine)
        {
            gameStateMachine.CurrentState = new PauseState(Game);
        }

        public override void Finish(GameStateMachine gameStateMachine)
        {
            gameStateMachine.CurrentState = new ScoreState(Game);
        }
        public override string GetStateName() { return "Game State"; }
    }
}
