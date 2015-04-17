using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Game_Actors
{
    abstract class GameActor
    {
        // WHAT IS A GAME ACTOR???
        // Anything that Input controlled actions
        // The unit/player, the menu buttons, etc
        // 
        // Create abstract method for any input controlled method a game actor can have
        public abstract void Fire();
        public abstract void Move();
        public abstract void Pause();
        public abstract void Select();
        public abstract void Trail();
        public abstract void Switch();
    }
}
