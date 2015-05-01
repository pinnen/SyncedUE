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
                // TODO: XML formatting maybe?
                // Initialize
                Interface.MenuBackground = content.Load<Texture2D>("Interface/ControllerSelectionBackground");
                Interface.Arrows = content.Load<Texture2D>("Interface/SelectionArrows");

                #region Character
                Character.InterfaceTexture = new Dictionary<Character.Name, Texture2D>()
                {
                    {Character.Name.Circle,   content.Load<Texture2D>("Interface/Character/Circle")},
                    {Character.Name.Triangle, content.Load<Texture2D>("Interface/Character/Triangle")},
                    {Character.Name.Square,   content.Load<Texture2D>("Interface/Character/Square")},
                    {Character.Name.Pentagon, content.Load<Texture2D>("Interface/Character/Pentagon")},
                    {Character.Name.Hexagon,  content.Load<Texture2D>("Interface/Character/Hexagon")}
                };
                Character.GameTexture = new Dictionary<Character.Name, Texture2D>()
                {
                    {Character.Name.Circle,   content.Load<Texture2D>("GameObjects/Characters/Circle")},
                    {Character.Name.Triangle, content.Load<Texture2D>("GameObjects/Characters/Triangle")},
                    {Character.Name.Square,   content.Load<Texture2D>("GameObjects/Characters/Square")},
                    {Character.Name.Pentagon, content.Load<Texture2D>("GameObjects/Characters/Pentagon")},
                    {Character.Name.Hexagon,  content.Load<Texture2D>("GameObjects/Characters/Hexagon")}
                };
                Character.AbilityText = new Dictionary<Character.Name, string>()
                {
                    {Character.Name.Circle,   "Tar Circle"},
                    {Character.Name.Triangle, "Bermuda Triangle"},
                    {Character.Name.Square,   "Prison Square"},
                    {Character.Name.Pentagon, "Pentagons Secret"},
                    {Character.Name.Hexagon,  "Hexacopy"}
                };
                #endregion
                #region Audio
                //Audio.SongDictionary = new Dictionary<Audio.Songs, Song>()
                //{
                //    {Audio.Songs.InGame, content.Load<Song>("")},
                //    {Audio.Songs.Menu, content.Load<Song>("")}
                //};

                //Audio.MenuSelect = content.Load<SoundEffect>("");
                //Audio.MenuConfirm = content.Load<SoundEffect>("");
                //Audio.GameStart = content.Load<SoundEffect>("");
                //Audio.GameFinished = content.Load<SoundEffect>("");
                //Audio.UnitMove = content.Load<SoundEffect>("");
                //Audio.BallShoot = content.Load<SoundEffect>("");
                //Audio.BallPickUp = content.Load<SoundEffect>("");
                //Audio.ZoneShoot = content.Load<SoundEffect>("");
                //Audio.ZonePickUp = content.Load<SoundEffect>("");
                //Audio.ZoneCreate = content.Load<SoundEffect>("");
                //Audio.ZoneExplosion = content.Load<SoundEffect>("");
                //Audio.Score = content.Load<SoundEffect>("");
                #endregion
                #region Font
                Font.MenuFont = content.Load<SpriteFont>("Fonts/menufont");
                #endregion
                #region Zone
                Zone.CompactTexture = new Dictionary<Zone.Name, Texture2D>()
                {
                    {Zone.Name.Circle, content.Load<Texture2D>("GameObjects/Zones/CircleZone")},
                    {Zone.Name.Hexagon, content.Load<Texture2D>("GameObjects/Zones/HexagonZone")},
                    {Zone.Name.Pentagon, content.Load<Texture2D>("GameObjects/Zones/PentagonZone")},
                    {Zone.Name.Square, content.Load<Texture2D>("GameObjects/Zones/SquareZone")},
                    {Zone.Name.Triangle, content.Load<Texture2D>("GameObjects/Zones/TriangleZone")}
                };
                Zone.Texture = new Dictionary<Zone.Name, Texture2D>()
                {
                    {Zone.Name.Circle, content.Load<Texture2D>("GameObjects/Zones/CompactCircleZone")},
                    {Zone.Name.Hexagon, content.Load<Texture2D>("GameObjects/Zones/CompactHexagonZone")},
                    {Zone.Name.Pentagon, content.Load<Texture2D>("GameObjects/Zones/CompactPentagonZone")},
                    {Zone.Name.Square, content.Load<Texture2D>("GameObjects/Zones/CompactSquareZone")},
                    {Zone.Name.Triangle, content.Load<Texture2D>("GameObjects/Zones/CompactTriangleZone")}
                };
                #endregion
            }
        }
        public static class Character
        {
            public enum Name { Circle, Triangle, Square, Pentagon, Hexagon }

            public static Dictionary<Name, Texture2D> GameTexture;
            public static Dictionary<Name, Texture2D> InterfaceTexture;
            public static Dictionary<Name, string> AbilityText;
        }
        public static class Interface
        {
            public static Texture2D MenuBackground;
            public static Texture2D Arrows;
        }
        public static class Audio
        {
            public enum Songs { Menu, InGame };
            //private static Song _Menu;
            //private static Song _InGame;
            public static Dictionary<Songs, Song> SongDictionary;
            //public static SoundEffect MenuClick;
            //public static SoundEffect MenuConfirm;
            //public static SoundEffect GameStart;
            //public static SoundEffect GameFinished;
            //public static SoundEffect UnitMove; // ?
            //public static SoundEffect BallShoot;
            //public static SoundEffect BallPickUp;
            //public static SoundEffect ZoneShoot;
            //public static SoundEffect ZonePickUp;
            //public static SoundEffect ZoneCreate;
            //public static SoundEffect ZoneExplosion;
            //public static SoundEffect Score;

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
            public enum Name { Circle, Triangle, Square, Pentagon, Hexagon }

            public static Dictionary<Name, Texture2D> CompactTexture;
            public static Dictionary<Name, Texture2D> Texture;
        }
        
    }
}
