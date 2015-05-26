// MapObject.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-13
// Edited by:
// Pontus Magnusson
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using System;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    public class MapObject
    {
        [XmlElement("Position")]
        public Vector2 Position;

        [XmlElement("TexturePath")]
        public string TexturePath;

        //public virtual GameComponent GetComponent(Game game)
        //{
        //    return new Sprite(game.Content.Load<Texture2D>(TexturePath), Position, Static_Classes.DrawingHelper.DrawingLevel.Medium, game);
        //}
    }
}
