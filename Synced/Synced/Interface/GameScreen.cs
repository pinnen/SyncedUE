// CharacterSelector.cs
// Introduced: 2015-04-29
// Last edited: 2015-05-18
// Edited by:
// Pontus Magnusson
// Dennis Stockhaus
// Robin Calmegård
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Synced.Content;
using Synced.InGame;
using Synced.MapNamespace;
using Synced.MapNameSpace;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;

namespace Synced.Interface
{
    class GameScreen : Screen
    {
        public int GameScore = 5;
        public event EndGame GameEnded;
        Map _map;
        World world;

        public GameScreen(Game game, List<Library.Character.Name> playerinfo) // TODO: tmp added world to parameters, might solve in a different way later. 
            : base(game)
        {
            //SyncedGameCollection.InitializeSyncedGameCollection(game);
            world = new World(Vector2.Zero);

            //GameComponents.Add(SyncedGameCollection.Instance);

            _map = new Map(Library.Map.Path[Library.Map.Name.Paper], game, world);
            GameComponents.Add(_map);

            Rectangle[] rectangles = new Rectangle[] { new Rectangle(140, 20, 40, 40), new Rectangle(1700, 20, 40, 40),new Rectangle(140, 1020, 40, 40), new Rectangle(1700, 1020, 40, 40) };
            // Controls
            for (int i = 0; i < playerinfo.Count; i++)
            {
                GameComponents.Add(new ScoreLabel((PlayerIndex)i, rectangles[i], Game));
            }
            // Audio
            Library.Audio.PlaySong(Library.Audio.Songs.GameSong3);

            _map.LoadMap(game, playerinfo); // send in player information

            //foreach (var ob in SyncedGameCollection.ComponentCollection)
            //{
            //    if (ob is Goal)
            //    {
            //        (ob as Goal).Scored += GameScreen_Scored;
            //    }
            //}
        }

        public bool Winner(ref Library.Colors.ColorName winner)
        {
            foreach (var ob in GameComponents)
            {
                if (ob is ScoreLabel)
                {
                    if ((ob as ScoreLabel).Score >= GameScore)
                    {
                        winner = (Library.Colors.ColorName)((int)((ob as ScoreLabel).PlayerIndex));
                        return true;
                    }
                }
            }
            return false;
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
                        //foreach (var crys in SyncedGameCollection.ComponentCollection)
                        //{
                        //    if (crys is Crystal)
                        //    {
                        //        (crys as Crystal).DeactivateCrystal(Map.crystalSpawnList[new Random().Next(0,Map.crystalSpawnList.Count)].Position);
                        //        break;
                        //    }
                        //}

                    }
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));

            // Have we won?
            Library.Colors.ColorName color = Library.Colors.ColorName.Blue;
            if (Winner(ref color))
            {
                GameEnded(color, new EventArgs());
            }

            base.Update(gameTime);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ResetGame()
        {
            //SyncedGameCollection.ComponentCollection.Clear();
            world.Clear();
            GameComponents.Clear();
        }
    }
}

