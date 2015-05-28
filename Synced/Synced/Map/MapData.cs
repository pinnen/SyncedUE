// MapData.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-13
// Edited by:
// Pontus Magnusson
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Synced.MapNameSpace;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    [XmlRoot("MapData")]
    public class MapData
    {
        List<MapObjectData> _objects;

        [XmlElement(typeof(PlayerStartData))]
        [XmlElement(typeof(CrystalSpawnData))]
        [XmlElement(typeof(BorderData))]
        [XmlElement(typeof(GoalData))]
        [XmlElement(typeof(MapObjectData))]
        public List<MapObjectData> Objects
        {
            get { return _objects; }
            set { _objects = value; }
        }

        // TODO Temporary ctor for first map file // TODO: had to comment everything
        public MapData()
        {
            _objects = new List<MapObjectData>();

            #region Players
            _objects.Add(new PlayerStartData()
            {
                TexturePath = "",
                drawingLevel = 3,
                PlayerIndex = PlayerIndex.One,
                Position = new Vector2(400, 400),
                Position2 = new Vector2(400, 450),
                Rotation = 0,
            });
            _objects.Add(new PlayerStartData()
            {
                TexturePath = "",
                drawingLevel = 3,
                PlayerIndex = PlayerIndex.Two,
                Position = new Vector2(1920 - 400, 400),
                Position2 = new Vector2(1920 - 400, 450),
                Rotation = 0,
            });
            _objects.Add(new PlayerStartData()
            {
                TexturePath = "",
                drawingLevel = 3,
                PlayerIndex = PlayerIndex.Three,
                Position = new Vector2(400, 1080 - 400),
                Position2 = new Vector2(400, 1080 - 450),
                Rotation = 0,
            });
            _objects.Add(new PlayerStartData()
            {
                TexturePath = "",
                drawingLevel = 3,
                PlayerIndex = PlayerIndex.Four,
                Position = new Vector2(1920 - 400, 1080 - 400),
                Position2 = new Vector2(1920 - 400, 1080 - 450),
                Rotation = 0,
            });
            #endregion

            #region Crystals
            _objects.Add(new CrystalSpawnData()
            {
                TexturePath = "GameObjects/Crystal",
                drawingLevel = 3,
                Position = new Vector2(1920 / 2, 1080 / 2),
                IsStart = true,
            });
            _objects.Add(new CrystalSpawnData()
            {
                TexturePath = "GameObjects/Crystal",
                drawingLevel = 3,
                Position = new Vector2(1920 / 2, 1080 / 2),
                IsStart = false,
            });
            _objects.Add(new CrystalSpawnData()
            {
                TexturePath = "GameObjects/Crystal",
                drawingLevel = 3,
                Position = new Vector2(1920 / 2, 1080 / 2),
                IsStart = false,
            });
            _objects.Add(new CrystalSpawnData()
            {
                TexturePath = "GameObjects/Crystal",
                drawingLevel = 3,
                Position = new Vector2(1920 / 2, 1080 / 2),
                IsStart = false,
            });
            _objects.Add(new CrystalSpawnData()
            {
                TexturePath = "GameObjects/Crystal",
                drawingLevel = 3,
                Position = new Vector2(1920 / 2, 1080 / 2),
                IsStart = false,
            });
            #endregion

            #region Map
            _objects.Add(new MapObjectData()
            {
                Position = new Vector2(129, 111),
                TexturePath = "Maps/Paper/background",
                drawingLevel = 1,
            });
            _objects.Add(new BorderData()
            {
                Position = new Vector2(1920 / 2, 1080 / 2),
                TexturePath = "Maps/Paper/Frame2",
                drawingLevel = 3,
            });
            #endregion

            #region Goals
            _objects.Add(new GoalData()
            {
                Position = new Vector2(300, 1080 / 2),
                TexturePath = "Maps/Paper/Goal",
                Texture2Path = "GameObjects/GoalBorder",
                drawingLevel = 3,
                Direction = GoalDirections.West,
            });
            _objects.Add(new GoalData()
            {
                Position = new Vector2(1920 - 300, 1080 / 2),
                TexturePath = "GameObjects/Goal",
                Texture2Path = "GameObjects/GoalBorder",
                drawingLevel = 3,
                Direction = GoalDirections.East,
            });
            #endregion
        }
    }
}
