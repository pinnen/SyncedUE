using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.MapNamespace;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    class CrystalSpawnData : MapObjectData
    {
        [XmlElement("IsStart")]
        public bool IsStart;
   
        public override GameComponent GetComponent(Game game, World world)
        {
            return new Sprite(game.Content.Load<Texture2D>(TexturePath), Position, (DrawingHelper.DrawingLevel)drawingLevel, game);
        }
    }
}
