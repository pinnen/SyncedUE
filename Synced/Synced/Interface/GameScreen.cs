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
            : base (game)
        {
            world = new World(Vector2.Zero);
            SyncedGameCollection.InitializeSyncedGameCollection(game);
            GameComponents.Add(SyncedGameCollection.Instance);

            _map = new Map(Library.Map.Path[Library.Map.Name.Paper], game, world);
            GameComponents.Add(_map);
            
            // Audio
            Library.Audio.PlaySong(Library.Audio.Songs.GameSong3);
        }

        public void InitializeGameScreen(Game game, List<Library.Character.Name> playerinfo) // Send in playerinformation
        {
            _map.LoadMap(game, playerinfo); // send in player information
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

            