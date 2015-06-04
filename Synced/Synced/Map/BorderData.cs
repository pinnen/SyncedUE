using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.CollisionShapes;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.MapNamespace
{
    [Serializable]
    public class BorderData : MapObjectData
    {
        public override DrawableGameComponent GetComponent(Game game, World world)
        {
            return new TexturePolygon(game.Content.Load<Texture2D>(TexturePath), Position, 0, (DrawingHelper.DrawingLevel)drawingLevel, game, world, false);
        }
    }
}
