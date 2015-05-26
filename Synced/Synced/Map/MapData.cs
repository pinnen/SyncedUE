// MapData.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-13
// Edited by:
// Pontus Magnusson
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
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
        List<MapObject> _objects;

        [XmlElement(typeof(PlayerStart))]
        [XmlElement(typeof(Obstacle))]
        [XmlElement(typeof(GoalData))]
        [XmlElement(typeof(MapObject))]
        public List<MapObject> Objects
        {
            get { return _objects; }
            set { _objects = value; }
        }

        // TODO Temporary ctor for first map file // TODO: had to comment everything
        public MapData()
        {
            _objects = new List<MapObject>();

            //Players
            _objects.Add(new PlayerStart()
            {
                PlayerIndex = PlayerIndex.One,
                Position = new Vector2(100, 100),
                TexturePath = ""
            });
            _objects.Add(new PlayerStart()
            {
                PlayerIndex = PlayerIndex.Two,
                Position = new Vector2(300, 100),
                TexturePath = ""
            });
            _objects.Add(new PlayerStart()
            {
                PlayerIndex = PlayerIndex.Three,
                Position = new Vector2(300, 300),
                TexturePath = ""
            });
            _objects.Add(new PlayerStart()
            {
                PlayerIndex = PlayerIndex.Four,
                Position = new Vector2(100, 300),
                TexturePath = ""
            });

             //Background
            _objects.Add(new Obstacle()
            {
                Position = Vector2.Zero,
                TexturePath = "Maps/Paper/frame",
                CollisionCategory = Category.All
            });
            _objects.Add(new Obstacle()
           {
               Position = Vector2.Zero,
               TexturePath = "Maps/Paper/background",
               CollisionCategory = Category.None
           });

             //Goals
            _objects.Add(new GoalData()
            {
                Position = new Vector2(500, 500),
                TexturePath = "Maps/Paper/Goal",
                CollisionCategory = Category.All,
            });
        }
    }
}
