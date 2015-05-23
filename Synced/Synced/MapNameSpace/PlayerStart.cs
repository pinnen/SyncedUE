using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    public class PlayerStart : MapObject
    {
        [XmlElement("PlayerIndex")]
        public PlayerIndex PlayerIndex;
    }
}
