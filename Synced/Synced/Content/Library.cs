// Library.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-30
// Edited by:
// Pontus Magnusson
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Synced.Content
{
    static class Library
    {
        public static class Loader
        {
            public static void Initialize(ContentManager content)
            {
                // Initialize
                Library.Interface.MenuBackground = content.Load<Texture2D>("Interface/ControllerSelectionBackground");

                #region Character
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
                #region Audio
                //Library.Audio.SongDictionary = new Dictionary<Audio.Songs, Song>()
                //{
                //    {Library.Audio.Songs.InGame, content.Load<Song>("")},
                //    {Library.Audio.Songs.Menu, content.Load<Song>("")}
                //};

                //Library.Audio.MenuSelect = content.Load<SoundEffect>("");
                //Library.Audio.MenuConfirm = content.Load<SoundEffect>("");
                //Library.Audio.GameStart = content.Load<SoundEffect>("");
                //Library.Audio.GameFinished = content.Load<SoundEffect>("");
                //Library.Audio.UnitMove = content.Load<SoundEffect>("");
                //Library.Audio.BallShoot = content.Load<SoundEffect>("");
                //Library.Audio.BallPickUp = content.Load<SoundEffect>("");
                //Library.Audio.ZoneShoot = content.Load<SoundEffect>("");
                //Library.Audio.ZonePickUp = content.Load<SoundEffect>("");
                //Library.Audio.ZoneCreate = content.Load<SoundEffect>("");
                //Library.Audio.ZoneExplosion = content.Load<SoundEffect>("");
                //Library.Audio.Score = content.Load<SoundEffect>("");
                #endregion
                #region Font
                Library.Font.MenuFont = content.Load<SpriteFont>("Fonts/menufont");
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
        public static class Audio
        {
            public enum Songs { Menu, InGame };
            private static Song _Menu;
            private static Song _InGame;
            public static Dictionary<Songs, Song> SongDictionary;
            public static SoundEffect MenuClick;
            public static SoundEffect MenuConfirm;
            public static SoundEffect GameStart;
            public static SoundEffect GameFinished;
            public static SoundEffect UnitMove; // ?
            public static SoundEffect BallShoot;
            public static SoundEffect BallPickUp;
            public static SoundEffect ZoneShoot;
            public static SoundEffect ZonePickUp;
            public static SoundEffect ZoneCreate;
            public static SoundEffect ZoneExplosion;
            public static SoundEffect Score;

            public static void PlaySong(Songs song)
            {
                if (MediaPlayer.State == MediaState.Playing)
                    MediaPlayer.Stop();
                MediaPlayer.Play(SongDictionary[song]);
            }
        }
        public static class Font
        {
            public static SpriteFont MenuFont;

        }
        public static class Zone
        {

        }
        
    }
}
