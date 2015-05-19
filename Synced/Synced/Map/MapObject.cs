using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    public class MapObject
    {
        [XmlElement("Position")]
        public Vector2 Position;

        [XmlElement("TexturePath")]
        public string TexturePath;
    }
}
