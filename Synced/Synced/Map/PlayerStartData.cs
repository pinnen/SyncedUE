using FarseerPhysics.Dynamics;
// PlayerStart.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Static_Classes;
using System;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    public class PlayerStartData : MapObjectData
    {
        [XmlElement("PlayerIndex")]
        public PlayerIndex PlayerIndex;

        [XmlElement("Rotation")]
        public float Rotation;

        [XmlElement("Position2")]
        public Vector2 Position2;

        // TODO: what should this return? 
        public virtual GameComponent GetComponent(Game game, World world)
        {
            return new Sprite(game.Content.Load<Texture2D>(TexturePath), Position, (DrawingHelper.DrawingLevel)drawingLevel, game);
        }
    }
}
