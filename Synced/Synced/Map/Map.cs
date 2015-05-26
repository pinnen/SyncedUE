// Map.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-13
// Edited by:
// Pontus Magnusson
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Content;
using Synced.Interface;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using Synced.InGame;
using Synced.MapNameSpace;
using Synced.CollisionShapes;
using Synced.Static_Classes;

namespace Synced.MapNamespace
{
    class Map : DrawableGameComponent// : Screen
    {
        #region Variables

        #endregion
        #region Properties
        public MapData Data
        {
            get;
            set;
        }
        public World World
        {
            get;
            set;
        }
        #endregion

        public Map(string path, Game game) : base (game)
        {
            Data = Library.Serialization<MapData>.DeserializeFromXmlFile(path);
            World = new World(Vector2.Zero); // Topdown games have no gravity
            
            //Process data
            foreach (var mapObject in Data.Objects)
            {
                if (mapObject is Obstacle)
                {
                    SyncedGameCollection.ComponentCollection.Add(new Sprite(game.Content.Load<Texture2D>(mapObject.TexturePath), mapObject.Position, Static_Classes.DrawingHelper.DrawingLevel.Low, game));
                }
                else if (mapObject is Goal)
                {
                    SyncedGameCollection.ComponentCollection.Add(new Sprite(game.Content.Load<Texture2D>(mapObject.TexturePath), mapObject.Position, Static_Classes.DrawingHelper.DrawingLevel.Back, game));
                }
                else if (mapObject is PlayerStart)
                {
                    // TODO: Add player
                }
                else if (mapObject is MapObject)
                {
                    SyncedGameCollection.ComponentCollection.Add(new Sprite(game.Content.Load<Texture2D>(mapObject.TexturePath), mapObject.Position, Static_Classes.DrawingHelper.DrawingLevel.Back, game));
                }
            }
        }
    }
}
