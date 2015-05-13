// Map.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-13
// Edited by:
// Pontus Magnusson

using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Synced.Content;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    class Map
    {
        #region Variables
        public MapData Data
        {
            get;
            private set;
        }
        #endregion
        #region Properties
        
        public World World
        {
            get;
            set;
        }

        public Map(string path)
        {
            Data = Library.Serialization<MapData>.DeserializeFromXmlFile(path);

            World = new World(Vector2.Zero); // Topdown games have no gravity
        }
        #endregion
    }
}
