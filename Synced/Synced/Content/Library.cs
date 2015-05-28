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
                Interface.MenuBackground = content.Load<Texture2D>("Interface/ConrollerSelection");
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
                    {Character.Name.Circle,   "Circle of Tar"},
                    {Character.Name.Triangle, "Bermuda Triangle"},
                    {Character.Name.Square,   "Prison Square"},
                    {Character.Name.Pentagon, "Secret Pentagon"},
                    {Character.Name.Hexagon,  "Hexacopy"}
                };
                #endregion
                #region Audio
                // Ref: http://rbwhitaker.wikidot.com/playing-background-music
                Audio.SongDictionary = new Dictionary<Audio.Songs, Song>()
                {
                    {Audio.Songs.MenuSong1, content.Load<Song>(@"Audio\MenuSong1")},
                    {Audio.Songs.GameSong1, content.Load<Song>(@"Audio\GameSong1")},
                    {Audio.Songs.GameSong2, content.Load<Song>(@"Audio\GameSong2")},
                    {Audio.Songs.GameSong3, content.Load<Song>(@"Audio\GameSong3")}
                };

                Audio.SoundEffectDictionary = new Dictionary<Audio.SoundEffects, SoundEffect>()
                {
                    { Audio.SoundEffects.Bounce,        content.Load<SoundEffect>(@"Audio\Bounce")}, 
                    { Audio.SoundEffects.Click1,        content.Load<SoundEffect>(@"Audio\Click1")}, 
                    { Audio.SoundEffects.Click2,        content.Load<SoundEffect>(@"Audio\Click2")},
                    { Audio.SoundEffects.Countdown,     content.Load<SoundEffect>(@"Audio\Countdown")},
                    { Audio.SoundEffects.CrystalGrab,   content.Load<SoundEffect>(@"Audio\CrystalGrab")},
                    { Audio.SoundEffects.MenuConfirm,   content.Load<SoundEffect>(@"Audio\MenuConfirm")},
                    { Audio.SoundEffects.MenuSelect,    content.Load<SoundEffect>(@"Audio\MenuSelect")},
                    { Audio.SoundEffects.Pause,         content.Load<SoundEffect>(@"Audio\Pause")},
                    { Audio.SoundEffects.Score,         content.Load<SoundEffect>(@"Audio\Score")},
                    { Audio.SoundEffects.Shoot,         content.Load<SoundEffect>(@"Audio\Shoot")},
                    { Audio.SoundEffects.ZoneExpand,    content.Load<SoundEffect>(@"Audio\ZoneExpand")},
                    { Audio.SoundEffects.ZoneGrab,      content.Load<SoundEffect>(@"Audio\ZoneGrab")},
                    { Audio.SoundEffects.ZoneSpawn,     content.Load<SoundEffect>(@"Audio\ZoneSpawn")}
                };
                #endregion
                #region Font
                Font.MenuFont = content.Load<SpriteFont>("Fonts/menufont");
                #endregion
                #region Zone
                Zone.CompactTexture = new Dictionary<Zone.Name, Texture2D>()
                {
                    {Zone.Name.Circle, content.Load<Texture2D>("GameObjects/Zones/CompactCircleZone")},
                    {Zone.Name.Hexagon, content.Load<Texture2D>("GameObjects/Zones/CompactHexagonZone")},
                    {Zone.Name.Pentagon, content.Load<Texture2D>("GameObjects/Zones/CompactPentagonZone")},
                    {Zone.Name.Square, content.Load<Texture2D>("GameObjects/Zones/CompactSquareZone")},
                    {Zone.Name.Triangle, content.Load<Texture2D>("GameObjects/Zones/CompactTriangleZone")}
                };
                Zone.Texture = new Dictionary<Zone.Name, Texture2D>()
                {

                    {Zone.Name.Circle, content.Load<Texture2D>("GameObjects/Zones/CircleZone")},
                    {Zone.Name.Hexagon, content.Load<Texture2D>("GameObjects/Zones/HexagonZone")},
                    {Zone.Name.Pentagon, content.Load<Texture2D>("GameObjects/Zones/PentagonZone")},
                    {Zone.Name.Square, content.Load<Texture2D>("GameObjects/Zones/SquareZone")},
                    {Zone.Name.Triangle, content.Load<Texture2D>("GameObjects/Zones/TriangleZone")}
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
                #region Particles
                Particle.trailTexture = content.Load<Texture2D>("GameObjects/TrailParticle");
                Particle.plusSignTexture = content.Load<Texture2D>("GameObjects/plussign");
                Particle.minusSignTexture = content.Load<Texture2D>("GameObjects/minussign");
                Particle.questionSignTexture = content.Load<Texture2D>("GameObjects/questionsign");
                Particle.exclamationSignTexture = content.Load<Texture2D>("GameObjects/exclamationsign");
                Particle.starTexture = content.Load<Texture2D>("GameObjects/star");
                Particle.barrierParticle = content.Load<Texture2D>("GameObjects/BarrierParticle");
                #endregion
                #region Goal
                Goal.GoalTexture = content.Load<Texture2D>("GameObjects/Goal");
                Goal.BorderTexture = content.Load<Texture2D>("GameObjects/GoalBorder");
                #endregion
                #region Colors
                Colors.getColor = new Dictionary<Tuple<Colors.ColorName, Colors.ColorVariation>, Color>() 
                {
                    {Tuple.Create(Colors.ColorName.Blue,Colors.ColorVariation.Left),Color.Blue},
                    {Tuple.Create(Colors.ColorName.Blue,Colors.ColorVariation.Right),Color.DarkBlue},
                    {Tuple.Create(Colors.ColorName.Blue,Colors.ColorVariation.Other),new Color(0, 0, 180, 255)},

                    {Tuple.Create(Colors.ColorName.Green,Colors.ColorVariation.Left),new Color(0, 255, 0, 255)},
                    {Tuple.Create(Colors.ColorName.Green,Colors.ColorVariation.Right),new Color(0, 139, 0, 255)},
                    {Tuple.Create(Colors.ColorName.Green,Colors.ColorVariation.Other),new Color(0, 180, 0, 255)},

                    {Tuple.Create(Colors.ColorName.Red,Colors.ColorVariation.Left),Color.Red},
                    {Tuple.Create(Colors.ColorName.Red,Colors.ColorVariation.Right),Color.DarkRed},
                    {Tuple.Create(Colors.ColorName.Red,Colors.ColorVariation.Other),new Color(180, 0, 0, 255)},

                    {Tuple.Create(Colors.ColorName.Yellow,Colors.ColorVariation.Left),Color.Yellow},
                    {Tuple.Create(Colors.ColorName.Yellow,Colors.ColorVariation.Right),new Color(139, 139, 0, 255)},
                    {Tuple.Create(Colors.ColorName.Yellow,Colors.ColorVariation.Other),new Color(180, 180, 0, 255)},
                };
                #endregion
                #region Screens
                Screens.SplashAlpha = content.Load<Texture2D>("Interface/SplashAlpha");
                Screens.SplashSeven = content.Load<Texture2D>("Interface/Splash2");
                Screens.LoadScreen = content.Load<Texture2D>("Interface/LoadingScreen");
                #endregion

            }
        }
        public static class Screens
        {
            public static Texture2D SplashAlpha;
            public static Texture2D SplashSeven;
            public static Texture2D LoadScreen;
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
            public enum Songs 
            { 
                MenuSong1, 
                GameSong1,
                GameSong2,
                GameSong3
            };
            public static Dictionary<Songs, Song> SongDictionary;

            public enum SoundEffects 
            {
                Bounce,
                Click1,
                Click2,
                Countdown,
                CrystalGrab,
                MenuConfirm,
                MenuSelect,
                Pause,
                Score,
                Shoot,
                ZoneExpand,
                ZoneGrab,
                ZoneSpawn
            }

            public static Dictionary<SoundEffects, SoundEffect> SoundEffectDictionary;

            public static void PlaySong(Songs song)
            {
                MediaPlayer.Play(SongDictionary[song]);
                MediaPlayer.IsRepeating = true;
            }

            public static void PlaySoundEffect(SoundEffects soundEffect)
            {
                SoundEffectDictionary[soundEffect].Play();
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
            public enum ColorName {Blue, Green, Red, Yellow }
            public enum ColorVariation { Left, Right, Other }
            public static Dictionary<Tuple<ColorName, ColorVariation>,Color> getColor;

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
        public static class Particle 
        {
            public static Texture2D trailTexture;
            public static Texture2D plusSignTexture;
            public static Texture2D minusSignTexture;
            public static Texture2D exclamationSignTexture;
            public static Texture2D questionSignTexture;
            public static Texture2D starTexture;
            public static Texture2D barrierParticle;
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
