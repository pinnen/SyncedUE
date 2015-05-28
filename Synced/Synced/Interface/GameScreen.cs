// CharacterSelector.cs
// Introduced: 2015-04-29
// Last edited: 2015-05-18
// Edited by:
// Pontus Magnusson
// Dennis Stockhaus
// Robin Calmegård
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.CollisionShapes;
using Synced.Content;
using Synced.InGame;
using Synced.InGame.Actors;
using Synced.MapNamespace;
using Synced.MapNameSpace;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;

namespace Synced.Interface
{
    class GameScreen : Screen
    {
        Map _map;
        World world;

        public GameScreen(Game game) // TODO: tmp added world to parameters, might solve in a different way later. 
            : base(game)
        {
            world = new World(Vector2.Zero);
            SyncedGameCollection.InitializeSyncedGameCollection(game);
            GameComponents.Add(SyncedGameCollection.Instance);

            _map = new Map(Library.Map.Path[Library.Map.Name.Paper], game, world);
            GameComponents.Add(_map);

            // Score labels
            GameComponents.Add(new ScoreLabel(PlayerIndex.One, new Rectangle(10, 10, 40, 40), game));
            GameComponents.Add(new ScoreLabel(PlayerIndex.Two, new Rectangle(10, 1030, 40, 40), game));
            GameComponents.Add(new ScoreLabel(PlayerIndex.Three, new Rectangle(1870, 10, 40, 40), game));
            GameComponents.Add(new ScoreLabel(PlayerIndex.Four, new Rectangle(1870, 1030, 40, 40), game));

            // Audio
            Library.Audio.PlaySong(Library.Audio.Songs.GameSong3);
        }

        void GameScreen_Scored(PlayerIndex playerIndex)
        {
            foreach (var ob in GameComponents)
            {
                if (ob is ScoreLabel)
                {
                    if ((ob as ScoreLabel).PlayerIndex == playerIndex)
                    {
                        (ob as ScoreLabel).IncreaseScore();

                        // Move Crystal
                        foreach (var crys in SyncedGameCollection.ComponentCollection)
                        {
                            if (crys is Crystal)
                            {
                                (crys as Crystal).DeactivateCrystal(Map.crystalSpawnList[new Random().Next(0,Map.crystalSpawnList.Count)].Position);
                                break;
                            }
                        }

                    }
                }
            }
        }

        public void InitializeGameScreen(Game game, List<Library.Character.Name> playerinfo) // Send in playerinformation
        {
            _map.LoadMap(game, playerinfo); // send in player information

            // Add score event
            foreach (var ob in SyncedGameCollection.ComponentCollection)
            {
                if (ob is Goal)
                {
                    (ob as Goal).Scored += GameScreen_Scored;
                }
            }
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            foreach (var ob in GameComponents)
            {
                if (ob is ScoreLabel)
                {
                    (ob as ScoreLabel).SetFont = Library.Font.MenuFont;
                }
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));

            base.Update(gameTime);
        }
        protected override void Dispose(bool disposing)
        {
            ResetGame();
            base.Dispose(disposing);
        }
        private void ResetGame()
        {
            SyncedGameCollection.ComponentCollection.Clear();
            world.Clear();
        }
    }
}

