// Map.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-13
// Edited by:
// Pontus Magnusson

using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Synced.Map
{
    [Serializable]
    abstract class Map
    {
        #region Variables
        [XmlElement("MapObjects")]
        List<GameComponent> _mapObjects;
        #endregion
        #region Properties
        World World
        {
            get;
            set;
        }
        #endregion
    }
}
