// Command.cs
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
using Synced.Game_Actors;

namespace Synced.InputCommands
{
    abstract class Command
    {
        public abstract void Execute(GameActor actor);
    }
}
