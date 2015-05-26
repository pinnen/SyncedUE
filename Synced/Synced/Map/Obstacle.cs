// Obstacle.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson 
using System;
using FarseerPhysics.Dynamics;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    public class Obstacle : MapObject
    {
        [XmlElement("World")]
        public World world;

        [XmlElement("CollisionCategory")]
        public Category CollisionCategory;

      
    }
}
