using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.MapNamespace;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    public class CrystalSpawnData : MapObjectData
    {
        [XmlElement("IsStart")]
        public bool IsStart;
    }
}
