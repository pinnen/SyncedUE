// PlayerStart.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson 
using Microsoft.Xna.Framework;
using System;
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
