using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame
{
    interface ICollidable
    {
        Vector2 Center { get; }
        float Radius { get; }
        void Response(ICollidable c);
    }
}
