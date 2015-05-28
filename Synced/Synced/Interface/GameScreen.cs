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

        // TODO: Test objects. Remove later
        World world;
        //Sprite background;
        //Player player;
        //Crystal crystal;
        //TexturePolygon frame;
        // End TODO: Test objects. Remove Later

        public GameScreen(Game game) // TODO: tmp added world to parameters, might solve in a different way later. 
            : base (game)
        {
            world = new World(Vector2.Zero);
            SyncedGameCollection.InitializeSyncedGameCollection(game);
            GameComponents.Add(SyncedGameCollection.Instance);

            _map = new Map(Library.Map.Path[Library.Map.Name.Paper], game, world);
            GameComponents.Add(_map);
            
            // TODO: Test objects. Remove later       
            //background = new Sprite(game.Content.Load<Texture2D>("Maps/Paper/background"), new Vector2(129,111), DrawingHelper.DrawingLevel.Back, game);
            //player = new Player(PlayerIndex.One, Library.Character.Name.Triangle, Library.Colors.ColorName.Green, game, world);
            //crystal = new Crystal(Library.Crystal.Texture, new Vector2(1920 / 2, 1080 / 2), DrawingHelper.DrawingLevel.Medium, game, world, Color.White);
            //frame = new TexturePolygon(Library.Map.Texture2, new Vector2(1920 / 2, 1080 / 2), 0, DrawingHelper.DrawingLevel.Medium, game, world, false);
            //frame.SetCollisionCategory(Category.All);
            //frame.SetCollideWithCategory(Category.All);

            //SyncedGameCollection.ComponentCollection.Add(background);
            //SyncedGameCollection.ComponentCollection.Add(player);
            //SyncedGameCollection.ComponentCollection.Add(crystal);
            //SyncedGameCollection.ComponentCollection.Add(frame);
            // End TODO: Test objects. Remove Later
            
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

            