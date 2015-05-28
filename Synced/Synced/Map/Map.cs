﻿// Map.cs
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
        // Crystal Spawns
        // Player Spawns
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

        public Map(string path, Game game, World world) : base (game)
        {
            Data = Library.Serialization<MapData>.DeserializeFromXmlFile(path);
            World = world;
            
            //Process data
            foreach (var mapObject in Data.Objects)
            {
                SyncedGameCollection.ComponentCollection.Add(mapObject.GetComponent(game, World));
            }
        }
    }
}
