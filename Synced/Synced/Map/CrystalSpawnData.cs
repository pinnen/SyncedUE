using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.MapNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Synced.Map
{
    class CrystalSpawnData : MapObjectData
    {
        [XmlElement("IsStart")]
        public bool IsStart;

        [Serializable]
        public virtual GameComponent GetComponent(Game game, World world)
        {
            return new Sprite(game.Content.Load<Texture2D>(TexturePath), Position, Static_Classes.DrawingHelper.DrawingLevel.Medium, game);
        }
    }
}
