using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SevenEngine.Drawing;
using Synced.MapNameSpace;
using Synced.Static_Classes;
// Goal.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson
using System;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    [Serializable]
    public class GoalData :MapObjectData
    {
        [XmlElement("Direction")]
        public GoalDirections Direction;

        [XmlElement("Texture2Path")]
        public string Texture2Path;

        public override DrawableGameComponent GetComponent(Microsoft.Xna.Framework.Game game, World world)
        {
            return new Goal(game.Content.Load<Texture2D>(TexturePath), game.Content.Load<Texture2D>(Texture2Path), Position, Direction, (DrawHelper.DrawingLevel)drawingLevel, game, world);
        }
    }
}
