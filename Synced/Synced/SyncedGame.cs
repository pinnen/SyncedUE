// Game1.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-29
// Edited by:
// Pontus Magnusson
//
// 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Synced.Content;
using Synced.Interface;
using Synced.Actors;
using Synced.Static_Classes;
using Synced.InGame;
using Synced.MapNamespace;
using FarseerPhysics.Dynamics;
using System;
using Synced.InGame.Actors;
using FarseerPhysics;
using Synced.MapNameSpace;
using Synced.CollisionShapes;

namespace Synced
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SyncedGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        bool fullscreen = false;
        KeyboardState _lastState;

        // TODO: Test objects. Remove later
        World world;
        Player player;
        Crystal crystal;
        TestGoal goalLeft;
        TestGoal goalRight;
        TexturePolygon frame;

        public SyncedGame()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initialize resolution and scaling
            ResolutionManager.Init(ref _graphics);
            ResolutionManager.SetVirtualResolution(1920, 1080); // TODO magic resolution values.
            ResolutionManager.SetResolution(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, fullscreen);
            // Initialize Simulator Unit to Pixel ratio
            ConvertUnits.SetDisplayUnitToSimUnitRatio(35f);     // 35 pixels = 1 meter // TODO: Use 'magic resolution values' to adapt. 

            // Create a new spritebatch and add it as service for access by other classes
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);

            // Load all content into a static library
            Library.Loader.Initialize(Content);
            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.GameStart); // ToDo: Test

            // ------------------------------------------------------------
            // Adds menu screen to ScreenManager
            // ------------------------------------------------------------
            ScreenManager.InitializeScreenManager(this);
            Components.Add(ScreenManager.Instance);

            // TODO: Test objects. Remove later
            world = new World(Vector2.Zero);
            player = new Player(PlayerIndex.One, Library.Character.Name.Circle, this, world);
            crystal = new Crystal(Library.Crystal.Texture, new Vector2(500, 500), DrawingHelper.DrawingLevel.Medium, this, world, Color.White);
            goalLeft = new TestGoal(Library.Goal.GoalTexture, Library.Goal.BorderTexture, new Vector2(300, 1080 / 2), GoalDirections.West, DrawingHelper.DrawingLevel.Low, this, world);
            goalRight = new TestGoal(Library.Goal.GoalTexture, Library.Goal.BorderTexture, new Vector2(1920 -300, 1080 /2), GoalDirections.East, DrawingHelper.DrawingLevel.Low, this, world);
            frame = new TexturePolygon(Library.Map.Texture2, new Vector2(1920 / 2, 1080 / 2), 0, DrawingHelper.DrawingLevel.Back, this, world, false);
            // End TODO: Test objects. Remove Later

            base.Initialize(); // Initializes all components
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #region Debug
            if (Keyboard.GetState().IsKeyDown(Keys.F11) && _lastState.IsKeyUp(Keys.F11))
            {
                _graphics.ToggleFullScreen();
            }

            // TODO: Test objects. Remove later
            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));

            // Update the statemachine

            // Update the debugger
            DebuggingHelper.Update();
            _lastState = Keyboard.GetState();
            #endregion
            base.Update(gameTime);
            InputManager.Update(); // Must be called after base. (This updates InputManager._LastStates)
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            ResolutionManager.BeginDraw(); // Clear and viewport fix
            

            base.Draw(gameTime);
        }
    }
}
