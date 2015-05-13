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

namespace Synced.Map
{
    [Serializable]
    [XmlRoot("MapObject")]
    public class MapObject
    {
        [XmlElement("position")]
        public Vector2 position;

        [XmlElement("path")]
        public string texturePath;

        [XmlElement("collision")]
        public Category collison;

        public MapObject()
        {
            position = new Vector2(10, 10);
            texturePath = "myTexture";
            collison = Category.Cat1 | Category.Cat2;
        }
    }
}
