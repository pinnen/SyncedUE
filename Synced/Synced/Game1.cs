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
using Synced.Menu;
using Synced.Actors;
using Synced.State_Machine;
using Synced.Static_Classes;
using Synced.InGame;

namespace Synced
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        bool fullscreen = false;

        GameStateMachine _gameStateMachine;

        MenuScreen _menu;

        Player playerTest;
        Crystal crystalTest;

        KeyboardState _lastState;

        public Game1()
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

            // Create a new spritebatch and add it as service for access by other classes
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);

            // Load all content into a static library
            Library.Loader.Initialize(Content);

            // ------------------------------------------------------------
            // Initialize all content below
            // ------------------------------------------------------------

            _menu = new MenuScreen(Library.Interface.MenuBackground, this);
            _gameStateMachine = new GameStateMachine(new MenuState());

            // Tests
            playerTest = new Player(PlayerIndex.One, Library.Character.Name.Square, this);
            crystalTest = new Crystal(Library.Crystal.Texture, new Vector2(400, 400), DrawingHelper.DrawingLevel.Top, this);



            base.Initialize(); // Initializes all components
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // KB29: AudioManager...
            AudioManager.AudioLoadContent(this);

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            // KB29: AudioManager...
            AudioManager.UnloadAudioContent();
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

            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                _gameStateMachine.Play();

            if (Keyboard.GetState().IsKeyDown(Keys.F11) && _lastState.IsKeyUp(Keys.F11))
            {
                _graphics.ToggleFullScreen();
            }

            // TODO Object orient collision somehow...
            // Collision checks 
            CollisionManager.CircleCircleCollision(playerTest.Left, crystalTest);
            CollisionManager.CircleCircleCollision(playerTest.Right, crystalTest);
            
            // Update the debugger
            DebuggingHelper.Update();

            // KB29: Audio...
            AudioManager.AudioUpdate();


            _lastState = Keyboard.GetState();
            base.Update(gameTime);

            // Must be called after base. (This updates LastState)
            InputManager.Update(); 
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            ResolutionManager.BeginDraw(); // Clear and viewport fix

            base.Draw(gameTime);
        }
    }
}
