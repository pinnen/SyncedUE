using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Actors.Zones
{
    class SquareZone : Zone
    {
        public SquareZone (Texture2D texture, Vector2 position, Color color, Game game) 
            : base(texture, position, color, game) 
        { }
    }
}
