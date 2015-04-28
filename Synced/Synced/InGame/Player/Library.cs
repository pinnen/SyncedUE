// Library.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-26
// Edited by:
// Pontus Magnusson
//
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Player
{
    static class Library
    {
        public static class Character
        {
            public enum Name { Circle, Triangle, Square, Pentagon, Hexagon }

            public static Dictionary<Name, string> GamePath = new Dictionary<Name, string>()
            {
                {Name.Circle,   "Game Objects/Character/Circle"},
                {Name.Triangle, "Game Objects/Character/Triangle"},
                {Name.Square,   "Game Objects/Character/Square"},
                {Name.Pentagon, "Game Objects/Character/Pentagon"},
                {Name.Hexagon,  "Game Objects/Character/Hexagon"}
            };

            public static Dictionary<Name, string> InterfacePath = new Dictionary<Name, string>()
            {
                {Name.Circle,   "Interface/Character/Circle"},
                {Name.Triangle, "Interface/Character/Triangle"},
                {Name.Square,   "Interface/Character/Square"},
                {Name.Pentagon, "Interface/Character/Pentagon"},
                {Name.Hexagon,  "Interface/Character/Hexagon"}
            };

            public static Dictionary<Name, string> InterfaceTextPath = new Dictionary<Name, string>()
            {
                {Name.Circle,   "Interface/CharacterText/Circle"},
                {Name.Triangle, "Interface/CharacterText/Triangle"},
                {Name.Square,   "Interface/CharacterText/Square"},
                {Name.Pentagon, "Interface/CharacterText/Pentagon"},
                {Name.Hexagon,  "Interface/CharacterText/Hexagon"}
            };
        }
        public static class Font
        {

        }
        public static class Zone
        {

        }
    }
}
