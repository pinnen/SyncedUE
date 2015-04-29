using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        public static class Loader
        {
            public static void Initialize(ContentManager content)
            {
                // Initialize
                Library.Interface.MenuBackground = content.Load<Texture2D>("Interface/ControllerSelectionBackground");

                #region Dictionarys

                Library.Character.InterfacePath = new Dictionary<Library.Character.Name, Texture2D>()
                {
                    {Library.Character.Name.Circle,   content.Load<Texture2D>("Interface/Character/Circle")},
                    {Library.Character.Name.Triangle, content.Load<Texture2D>("Interface/Character/Triangle")},
                    {Library.Character.Name.Square,   content.Load<Texture2D>("Interface/Character/Square")},
                    {Library.Character.Name.Pentagon, content.Load<Texture2D>("Interface/Character/Pentagon")},
                    {Library.Character.Name.Hexagon,  content.Load<Texture2D>("Interface/Character/Hexagon")}
                };

                Library.Character.GamePath = new Dictionary<Library.Character.Name, Texture2D>()
                {
                    {Library.Character.Name.Circle,   content.Load<Texture2D>("GameObjects/Characters/Circle")},
                    {Library.Character.Name.Triangle, content.Load<Texture2D>("GameObjects/Characters/Triangle")},
                    {Library.Character.Name.Square,   content.Load<Texture2D>("GameObjects/Characters/Square")},
                    {Library.Character.Name.Pentagon, content.Load<Texture2D>("GameObjects/Characters/Pentagon")},
                    {Library.Character.Name.Hexagon,  content.Load<Texture2D>("GameObjects/Characters/Hexagon")}
                };

                

                Library.Character.InterfaceTextPath = new Dictionary<Library.Character.Name, Texture2D>()
                {
                    {Library.Character.Name.Circle,   content.Load<Texture2D>("Interface/CharacterText/Circle")},
                    {Library.Character.Name.Triangle, content.Load<Texture2D>("Interface/CharacterText/Triangle")},
                    {Library.Character.Name.Square,   content.Load<Texture2D>("Interface/CharacterText/Square")},
                    {Library.Character.Name.Pentagon, content.Load<Texture2D>("Interface/CharacterText/Pentagon")},
                    {Library.Character.Name.Hexagon,  content.Load<Texture2D>("Interface/CharacterText/Hexagon")}
                };
                #endregion

            }
        }
        public static class Character
        {
            // TODO: XML formatting
            public enum Name { Circle, Triangle, Square, Pentagon, Hexagon }

            public static Dictionary<Name, Texture2D> GamePath;
            public static Dictionary<Name, Texture2D> InterfacePath;
            public static Dictionary<Name, Texture2D> InterfaceTextPath;
        }
        public static class Interface
        {
            public static Texture2D MenuBackground;
        }
        public static class Font
        {

        }
        public static class Zone
        {

        }
    }
}
