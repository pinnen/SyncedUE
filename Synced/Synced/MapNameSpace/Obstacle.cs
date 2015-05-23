using FarseerPhysics.Dynamics;
// Wall.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson
//
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    public class Obstacle : MapObject
    {
        [XmlElement("CollisionCategory")]
        public Category CollisionCategory;
    }
}
