using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Synced.MapNameSpace;
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
    public class GoalData : Obstacle
    {
        [XmlElement("Direction")]
        public GoalDirections Direction;

        [XmlElement("Texture2Path")]
        public string Texture2Path;       

        public override Microsoft.Xna.Framework.GameComponent GetComponent(Microsoft.Xna.Framework.Game game)
        {
            return new Goal(game.Content.Load<Texture2D>(TexturePath), game.Content.Load<Texture2D>(Texture2Path), Position, Direction, Static_Classes.DrawingHelper.DrawingLevel.Medium, game, world);
        }
    }
}
