using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Menu
{
    interface IDrawableObject
    {
        Vector2 Position { get; }
        Texture2D Texture { get; }
        
    }
}
