using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Player
{
    static class Character
    {
        public enum Name { Circle, Triangle, Square, Pentagon, Hexagon }

        public static Dictionary<Name, string> GamePath = new Dictionary<Name, string>()
        {
            {Name.Circle, "Game Objects/Character/Circle"},
            {Name.Triangle, "Game Objects/Character/Triangle"},
            {Name.Square, "Game Objects/Character/Square"},
            {Name.Pentagon, "Game Objects/Character/Pentagon"},
            {Name.Hexagon, "Game Objects/Character/Hexagon"}
        };
    }
}
