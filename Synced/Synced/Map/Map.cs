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
    class Map : DrawableGameComponent
    {
        #region Variables
        public Vector2 crystalSpawnPosition;
        public List<PlayerStartData> playerStartData;
        public List<DrawableGameComponent> Components;
        // World
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

        public Map(string path, Game game, World world)
            : base(game)
        {
            World = world;

            /* Set up data containers */
            Components = new List<DrawableGameComponent>();
            playerStartData = new List<PlayerStartData>();

            Data = new MapData(); // Library.Serialization<MapData>.DeserializeFromXmlFile(path);
            processData();

        }

        void processData()
        {
            foreach (var mapObject in Data.Objects)
            {
                if (mapObject is CrystalSpawnData)
                {
                    crystalSpawnPosition = (mapObject as CrystalSpawnData).Position;
                }
                else if (mapObject is PlayerStartData)
                {
                    playerStartData.Add((PlayerStartData)mapObject);
                }
                else if (mapObject is BorderData)
                {
                    TexturePolygon tmp = (TexturePolygon)mapObject.GetComponent(Game, World);
                    tmp.SetCollisionCategory(Category.All);
                    tmp.SetCollideWithCategory(Category.All);
                    Components.Add(tmp);
                }
                else if (mapObject is GoalData)
                {
                    Goal tmp = (Goal)mapObject.GetComponent(Game, World);
                    Components.Add(tmp);
                    Components.Add(tmp.Border);
                    Components.Add(tmp.OuterCircle);
                }
                else
                {
                    Components.Add(mapObject.GetComponent(Game, World));
                }
            }
        }
    }
}
