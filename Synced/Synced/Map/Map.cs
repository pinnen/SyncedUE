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
        int CrystalStartIndex;
        public static List<CrystalSpawnData> crystalSpawnList;
        public static List<PlayerStartData> playerStartData;

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
        public static List<CrystalSpawnData> CrystalSpawnList
        {
            get { return crystalSpawnList; }
        }
        #endregion

        public Map(string path, Game game, World world) : base (game)
        {
            /* Set up data containers */
            if (crystalSpawnList == null)
            {
                crystalSpawnList = new List<CrystalSpawnData>();
            }
            else
            {
                crystalSpawnList.Clear();
            }

            if (playerStartData == null)
            {
                playerStartData = new List<PlayerStartData>();
            }
            else
            {
                playerStartData.Clear();
            }

            Data = Library.Serialization<MapData>.DeserializeFromXmlFile(path);
            World = world;
        }

        public void LoadMap(Game game, List<Library.Character.Name> playerinfo) // playerData
        {
            //Process data
            foreach (var mapObject in Data.Objects)
            {
                if (mapObject is CrystalSpawnData)
                {
                    crystalSpawnList.Add((CrystalSpawnData)mapObject);
                    if (crystalSpawnList[crystalSpawnList.Count - 1].IsStart)
                    {
                        CrystalStartIndex = crystalSpawnList.Count - 1;
                    }
                }
                else if (mapObject is PlayerStartData)
                {
                    playerStartData.Add((PlayerStartData)mapObject);
                }
                else if (mapObject is BorderData)
                {
                    TexturePolygon tmp = (TexturePolygon)mapObject.GetComponent(game, World);
                    tmp.SetCollisionCategory(Category.All);
                    tmp.SetCollideWithCategory(Category.All);
                    //SyncedGameCollection.ComponentCollection.Add(tmp);
                }
                else
                {
                    //SyncedGameCollection.ComponentCollection.Add(mapObject.GetComponent(game, World));
                }
            }
            SetupPlayers(playerinfo);
            SetupCrystal();
        }
        public void ClearData()
        {
            crystalSpawnList.Clear();
            playerStartData.Clear();
        }

        private void SetupPlayers(List<Library.Character.Name> playerinfo)
        {
            //for (int i = 0; i < playerinfo.Count; i++)
            //{
            //    SyncedGameCollection.ComponentCollection.Add(new Player(playerStartData[i].PlayerIndex, playerinfo[i], (Library.Colors.ColorName)i, Game, World, playerStartData[i].Position, playerStartData[i].Position2)); //TODO: get color from menuscreen
            //}
        }
        private void SetupCrystal()
        {
            //SyncedGameCollection.ComponentCollection.Add(new Crystal(Library.Crystal.Texture, crystalSpawnList[CrystalStartIndex].Position, DrawingHelper.DrawingLevel.Medium, Game, World, Color.LightGray));
        }
    }
}
