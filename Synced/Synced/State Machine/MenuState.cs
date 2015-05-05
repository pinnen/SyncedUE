using Microsoft.Xna.Framework;
using Synced.Content;
using Synced.Menu;
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
        MenuScreen _menu;

        public MenuState(Game game)
            : base(game)
        {
            _menu = new MenuScreen(Library.Interface.MenuBackground, game);
        }

        public override void Update(GameStateMachine gameStateMachine)
        {
            if (_menu.IsEveryoneReady())
            {
                gameStateMachine.CurrentState = new GameState(Game);
            } 
        }

        public override void Play(GameStateMachine gameStateMachine)
        {
        }
        public override string GetStateName() { return "Menu State"; }
        
    }
}
