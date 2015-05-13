using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
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
        [XmlElement(typeof(Goal))]
        [XmlElement(typeof(MapObject))]
        public List<MapObject> Objects
        {
            get { return _objects; }
            set { _objects = value; }
        }

        // TODO Temporary ctor for first map file
        //public MapData()
        //{
        //    _objects = new List<MapObject>();

        //    // Players
        //    _objects.Add(new PlayerStart()
        //    {
        //        PlayerIndex = PlayerIndex.One,
        //        Position = new Vector2(100, 100),
        //        TexturePath = ""
        //    });
        //    _objects.Add(new PlayerStart()
        //    {
        //        PlayerIndex = PlayerIndex.Two,
        //        Position = new Vector2(300, 100),
        //        TexturePath = ""
        //    });
        //    _objects.Add(new PlayerStart()
        //    {
        //        PlayerIndex = PlayerIndex.Three,
        //        Position = new Vector2(300, 300),
        //        TexturePath = ""
        //    });
        //    _objects.Add(new PlayerStart()
        //    {
        //        PlayerIndex = PlayerIndex.Four,
        //        Position = new Vector2(100, 300),
        //        TexturePath = ""
        //    });

        //    // Background
        //    _objects.Add(new Obstacle()
        //    {
        //        Position = Vector2.Zero,
        //        TexturePath = "Maps/Paper/frame",
        //        CollisionCategory = Category.All
        //    });
        //    _objects.Add(new Obstacle()
        //   {
        //       Position = Vector2.Zero,
        //       TexturePath = "Maps/Paper/background",
        //       CollisionCategory = Category.None
        //   });

        //    // Goals
        //    _objects.Add(new Goal()
        //    {
        //        Position = new Vector2(500, 500),
        //        TexturePath = "Maps/Paper/Goal",
        //        CollisionCategory = Category.All,
        //        Radius = 50
        //    });
        //}
    }
}
