// CharacterSelector.cs
// Introduced: 2015-04-29
// Last edited: 2015-04-29
// Edited by:
// Robin Calmegård
//
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
