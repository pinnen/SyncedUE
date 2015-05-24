// Library.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-24
// Edited by:
// Pontus Magnusson
// Göran Forsström

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;

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
                // Ref: http://rbwhitaker.wikidot.com/playing-background-music
                Audio.SongDictionary = new Dictionary<Audio.Songs, Song>()
                {
                    {Audio.Songs.InGame, content.Load<Song>(@"Audio\song_InGame")},
                    {Audio.Songs.InMenu, content.Load<Song>(@"Audio\song_InMenu")}
                };

                Audio.SoundEffectDictionary = new Dictionary<Audio.SoundEffects, SoundEffect>()
                {
                    // ToDo: other sound?
                    { Audio.SoundEffects.MenuSelect,    content.Load<SoundEffect>(@"Audio\MenuSelect")}, 
                    { Audio.SoundEffects.MenuConfirm,   content.Load<SoundEffect>(@"Audio\MenuConfirm")}, 
                    { Audio.SoundEffects.GameStart,     content.Load<SoundEffect>(@"Audio\GameStart")},
                    { Audio.SoundEffects.GameFinished,  content.Load<SoundEffect>(@"Audio\GameFinished")},
                    { Audio.SoundEffects.UnitMove,      content.Load<SoundEffect>(@"Audio\UnitMove")},
                    { Audio.SoundEffects.CrystalShoot,  content.Load<SoundEffect>(@"Audio\CrystalShoot")},
                    { Audio.SoundEffects.CrystalPickUp, content.Load<SoundEffect>(@"Audio\CrystalPickUp")},
                    { Audio.SoundEffects.ZoneShoot,     content.Load<SoundEffect>(@"Audio\ZoneShoot")},
                    { Audio.SoundEffects.ZonePickUp,    content.Load<SoundEffect>(@"Audio\ZonePickUp")},
                    { Audio.SoundEffects.ZoneCreate,    content.Load<SoundEffect>(@"Audio\ZoneCreate")},
                    { Audio.SoundEffects.ZoneExplosion, content.Load<SoundEffect>(@"Audio\ZoneExplosion")},
                    { Audio.SoundEffects.Score,         content.Load<SoundEffect>(@"Audio\Score")},
                };

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
                #region Crystal
                Crystal.Texture = content.Load<Texture2D>("GameObjects/Crystal");
                #endregion
                #region Map
                Map.Path = new Dictionary<Map.Name, string>()
                {
                    {Map.Name.Paper, "Content/Maps/Paper/map.xml"}
                };
                Map.Texture2 = content.Load<Texture2D>("Maps/Paper/Frame2");
                #endregion
                #region TrailParticle
                TrailParticle.Texture = content.Load<Texture2D>("GameObjects/TrailParticle");
                #endregion
                Goal.GoalTexture = content.Load<Texture2D>("GameObjects/Goal");
                Goal.BorderTexture = content.Load<Texture2D>("GameObjects/GoalBorder");
                #region Goal
                
                #endregion
            }
        }
        public static class SplashScreens
        {
            //TODO SplashScreen loading
        }
        public static class Crystal
        {
            public static Texture2D Texture;
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
            public enum Songs { InMenu, InGame };
            public static Dictionary<Songs, Song> SongDictionary;

            public enum SoundEffects { MenuSelect, MenuConfirm, GameStart, GameFinished, UnitMove, CrystalShoot, CrystalPickUp, ZoneShoot, ZonePickUp, ZoneCreate, ZoneExplosion, Score }
            public static Dictionary<SoundEffects, SoundEffect> SoundEffectDictionary;

            public static void PlaySong(Songs song)
            {
                if (MediaPlayer.State == MediaState.Playing)
                    MediaPlayer.Stop();
                MediaPlayer.Play(SongDictionary[song]);
                MediaPlayer.IsRepeating = true;
            }

            public static void PlaySoundEffect(SoundEffects soundEffect)
            {
                //SoundEffectDictionary[soundEffect].Play();
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
        public static class Colors
        {
                public static Color RedLeft { get { return Color.Red; } }
                public static Color RedRight { get { return Color.DarkRed; }}
                public static Color RedCrystal { get { return new Color(180, 0, 0, 255); }}

                public static Color BlueLeft { get { return Color.Blue; }}
                public static Color BlueRight { get { return Color.DarkBlue; }}
                public static Color BlueCrystal { get { return new Color(0, 0, 180, 255); }}

                public static Color GreenLeft { get { return new Color(0, 255, 0, 255); }}
                public static Color GreenRight { get { return new Color(0, 139, 0, 255); }}
                public static Color GreenCrystal { get { return new Color(0, 180, 0, 255); }}

                public static Color YellowLeft { get { return Color.Yellow; }}
                public static Color YellowRight { get { return new Color(139, 139, 0, 255); }}
                public static Color YellowCrystal { get { return new Color(180, 180, 0, 255); }}
        }
        public static class Map
        {
            public enum Name
            {
                Paper
            }

            public static Dictionary<Name, string> Path;

            public static Synced.MapNamespace.Map LoadMap()
            {
                return null;
            }

            public static Texture2D Texture2;
        }
        public static class TrailParticle 
        {
            public static Texture2D Texture;
        }
        public static class Goal
        {
            public static Texture2D GoalTexture;
            public static Texture2D BorderTexture;
        }
        public static class Serialization<T> where T : class
        {

            public static T DeserializeFromXmlFile(string fileName)
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }

                using (Stream stream = File.OpenRead(fileName))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(stream) as T;
                }
            }
            public static void SerializeToXmlFile(T obj, string fileName)
            {
                var serializer = new XmlSerializer(typeof(T));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                using (var writer = new StreamWriter(fileName))
                {
                    serializer.Serialize(writer, obj, ns);
                }
            }
        }
    }
}
