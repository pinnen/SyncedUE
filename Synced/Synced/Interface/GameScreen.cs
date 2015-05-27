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
        List<Player> _players;

        // TODO: Test objects. Remove later
        World world;
        Sprite background;
        Player player;
        Crystal crystal;
        Goal goalLeft;
        Goal goalRight;
        TexturePolygon frame;
        // End TODO: Test objects. Remove Later

        public GameScreen(Game game) // TODO: tmp added world to parameters, might solve in a different way later. 
            : base (game)
        {
            SyncedGameCollection.InitializeSyncedGameCollection(game);
            GameComponents.Add(SyncedGameCollection.Instance);

            _map = new Map(Library.Map.Path[Library.Map.Name.Paper], game);
            GameComponents.Add(_map);

            // TODO: Test objects. Remove later
            world = new World(Vector2.Zero);
            background = new Sprite(game.Content.Load<Texture2D>("Maps/Paper/background"), new Vector2(129,111), DrawingHelper.DrawingLevel.Back, game);
            player = new Player(PlayerIndex.One, Library.Character.Name.Triangle, Library.Colors.ColorName.Yellow, game, world);
            crystal = new Crystal(Library.Crystal.Texture, new Vector2(1920 / 2, 1080 / 2), DrawingHelper.DrawingLevel.Medium, game, world, Color.White);
            goalLeft = new Goal(Library.Goal.GoalTexture, Library.Goal.BorderTexture, new Vector2(300, 1080 / 2), GoalDirections.West, DrawingHelper.DrawingLevel.Medium, game, world);
            goalRight = new Goal(Library.Goal.GoalTexture, Library.Goal.BorderTexture, new Vector2(1920 - 300, 1080 / 2), GoalDirections.East, DrawingHelper.DrawingLevel.Medium, game, world);
            frame = new TexturePolygon(Library.Map.Texture2, new Vector2(1920 / 2, 1080 / 2), 0, DrawingHelper.DrawingLevel.Medium, game, world, false);
            frame.SetCollisionCategory(Category.All);
            frame.SetCollideWithCategory(Category.All);

            SyncedGameCollection.ComponentCollection.Add(background);
            SyncedGameCollection.ComponentCollection.Add(player);
            SyncedGameCollection.ComponentCollection.Add(crystal);
            SyncedGameCollection.ComponentCollection.Add(frame);
            // End TODO: Test objects. Remove Later


            _players = new List<Player>();
            //foreach (var item in _map.Data.Objects)
            //{
            //    if (item is PlayerStart)
            //    {
            //        PlayerStart temp = item as PlayerStart;

            //        _players.Add(new Player(temp.PlayerIndex, Library.Character.Name.Circle, Library.Colors.ColorName.Blue, game, _map.World)); // TODO: All collision objects need world!
            //    }
            //}

            // Audio
            Library.Audio.PlaySong(Library.Audio.Songs.GameSong3);
        }

        public override void Update(GameTime gameTime)
        {
            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            base.Update(gameTime);
        }
    }
}

            