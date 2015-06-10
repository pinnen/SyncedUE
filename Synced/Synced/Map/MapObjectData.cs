using FarseerPhysics.Dynamics;
// MapObject.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-13
// Edited by:
// Pontus Magnusson
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SevenEngine.Drawing;
using Synced.Actors;
using Synced.Static_Classes;
using System;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    public class MapObjectData
    {
        [XmlElement("Position")]
        public Vector2 Position;

        [XmlElement("TexturePath")]
        public string TexturePath;

        [XmlElement("DrawingLevel")]
        public DrawHelper.DrawingLevel drawingLevel; /* Low 1- 5 High */

        public virtual DrawableGameComponent GetComponent(Game game, World world)
        {
            return new Sprite(game.Content.Load<Texture2D>(TexturePath), Position, drawingLevel, game);
        }
    }
}
