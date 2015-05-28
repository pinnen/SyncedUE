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
        [XmlElement(typeof(ObstacleData))]
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

            //Players
            //_objects.Add(new PlayerStart()
            //{
            //    PlayerIndex = PlayerIndex.One,
            //    Position = new Vector2(100, 100),
            //    TexturePath = ""
            //});
            //_objects.Add(new PlayerStart()
            //{
            //    PlayerIndex = PlayerIndex.Two,
            //    Position = new Vector2(300, 100),
            //    TexturePath = ""
            //});
            //_objects.Add(new PlayerStart()
            //{
            //    PlayerIndex = PlayerIndex.Three,
            //    Position = new Vector2(300, 300),
            //    TexturePath = ""
            //});
            //_objects.Add(new PlayerStart()
            //{
            //    PlayerIndex = PlayerIndex.Four,
            //    Position = new Vector2(100, 300),
            //    TexturePath = ""
            //});

            //Goals
            _objects.Add(new GoalData()
            {
                Position = new Vector2(300, 1080 / 2),
                TexturePath = "Maps/Paper/Goal",
                Texture2Path = "Gameobjects/GoalBorder",
                CollisionCategory = Category.All,
                Direction = GoalDirections.West
            });
            _objects.Add(new GoalData()
            {
                Position = new Vector2(1920 - 300, 1080 / 2),
                TexturePath = "Maps/Paper/Goal",
                Texture2Path = "Gameobjects/GoalBorder",
                CollisionCategory = Category.All,
                Direction = GoalDirections.East
            });
        }
    }
}
