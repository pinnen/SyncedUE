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
        // TODO: Test objects. Remove later
        World world;
        Player player;
        Crystal crystal;
        TestGoal goalLeft;
        TestGoal goalRight;
        TexturePolygon frame;
        // End TODO: Test objects. Remove Later
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

            // TODO: Test objects. Remove later
            world = new World(Vector2.Zero);
            player = new Player(PlayerIndex.One, Library.Character.Name.Circle, game, world);
            crystal = new Crystal(Library.Crystal.Texture, new Vector2(500, 500), DrawingHelper.DrawingLevel.Medium, game, world);
            goalLeft = new TestGoal(Library.Goal.GoalTexture, Library.Goal.BorderTexture, new Vector2(300, 1080 / 2), GoalDirections.West, DrawingHelper.DrawingLevel.Medium, game, world);
            goalRight = new TestGoal(Library.Goal.GoalTexture, Library.Goal.BorderTexture, new Vector2(1920 - 300, 1080 / 2), GoalDirections.East, DrawingHelper.DrawingLevel.Medium, game, world);
            frame = new TexturePolygon(Library.Map.Texture2, new Vector2(1920 / 2, 1080 / 2), 0, DrawingHelper.DrawingLevel.Medium, game, world, false);

            GameScreen.ComponentCollection.Add(crystal);
            GameScreen.ComponentCollection.Add(frame);
            // End TODO: Test objects. Remove Later
            
            //Process data
            foreach (var mapObject in Data.Objects)
            {
                if (mapObject is Obstacle)
                {
                    GameScreen.ComponentCollection.Add(new Sprite(game.Content.Load<Texture2D>(mapObject.TexturePath), mapObject.Position, Static_Classes.DrawingHelper.DrawingLevel.Low, game));
                }
                else if (mapObject is Goal)
                {
                    GameScreen.ComponentCollection.Add(new Sprite(game.Content.Load<Texture2D>(mapObject.TexturePath), mapObject.Position, Static_Classes.DrawingHelper.DrawingLevel.Back, game));
                }
                else if (mapObject is PlayerStart)
                {
                    // TODO: Add player
                }
                else if (mapObject is MapObject)
                {
                    GameScreen.ComponentCollection.Add(new Sprite(game.Content.Load<Texture2D>(mapObject.TexturePath), mapObject.Position, Static_Classes.DrawingHelper.DrawingLevel.Back, game));
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Test objects. Remove later
            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));

            //Updates every component in GameComponents
            foreach (IUpdateable gc in GameScreen.ComponentCollection.OfType<IUpdateable>().Where<IUpdateable>(x => x.Enabled).OrderBy<IUpdateable, int>(x => x.UpdateOrder))
                gc.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Draws every component in GameComponents
            foreach (IDrawable gc in GameScreen.ComponentCollection.OfType<IDrawable>().Where<IDrawable>(x => x.Visible).OrderBy<IDrawable, int>(x => x.DrawOrder))
                gc.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
