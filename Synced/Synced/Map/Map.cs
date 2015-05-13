// Map.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-13
// Edited by:
// Pontus Magnusson

using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Synced.Map
{
    abstract class Map
    {
        #region Variables
        List<GameComponent> _obstacles;
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
