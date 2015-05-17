// Map.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-13
// Edited by:
// Pontus Magnusson

using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Content;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Synced.MapNamespace
{
    class Map
    {
        #region Variables
        List<GameComponent> _components;
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

        public Map(string path, Game game)
        {
            _components = new List<GameComponent>();
            Data = Library.Serialization<MapData>.DeserializeFromXmlFile(path);
            World = new World(Vector2.Zero); // Topdown games have no gravity

            // Process data
            foreach (var mapObject in Data.Objects)
            {
                if (mapObject is Obstacle)
                {
                    _components.Add(new Sprite(game.Content.Load<Texture2D>(mapObject.TexturePath), mapObject.Position, Static_Classes.DrawingHelper.DrawingLevel.Back, game));
                }
                else if (mapObject is Goal)
                {
                    _components.Add(new Sprite(game.Content.Load<Texture2D>(mapObject.TexturePath), mapObject.Position, Static_Classes.DrawingHelper.DrawingLevel.Back, game));
                }
                else if (mapObject is PlayerStart)
                {
                }
                else if (mapObject is MapObject)
                {
                    _components.Add(new Sprite(game.Content.Load<Texture2D>(mapObject.TexturePath), mapObject.Position, Static_Classes.DrawingHelper.DrawingLevel.Back, game));
                }
            }
            _components.ForEach(x => game.Components.Add(x));
        }
    }
}
