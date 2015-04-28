using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Synced.InGame.Player;
using Synced.Player;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Menu
{
    class CharacterSelector : DrawableGameComponent
    {
        public enum State 
        {
            Unconnected, Connected, Joined, Ready
        }

        #region Variables
        State _currentState;
        Rectangle _rectangle;
        Vector2 _center;

        // TODO This is a very restricted design
        Sprite _pressToJoin;
        Sprite _zone;
        Sprite _arrows;
        SpriteFont _font;
        List<Sprite> _characterSprites;
        List<Sprite> _abilityTexts;

        Color _color;
        PlayerIndex _playerIndex;
        int _selectedIndex;
        GamePadState _previousState;
        SpriteBatch _spriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        } 
        #endregion

        #region Properties
        public State GetState
        {
            get { return _currentState; }
        }
        #endregion

        public CharacterSelector(PlayerIndex playerIndex, Rectangle rectangle, Game game)
            : base(game)
        {
            // TODO This constructor looks very ugly. It is the same as the old code.
            _currentState = State.Unconnected;
            _rectangle = rectangle;

            _playerIndex = playerIndex;

            // Add this component
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            float posX = _rectangle.X + _rectangle.Width / 2;
            float posY = _rectangle.Y + _rectangle.Height / 2;
            _pressToJoin = new Sprite("Interface/PressAToJoin", new Vector2(posX, posY), DrawingHelper.DrawingLevel.Interface, Game);
            _arrows = new Sprite("Interface/SelectionArrows", new Vector2(posX, posY), DrawingHelper.DrawingLevel.Interface, Game);

            
            _characterSprites = new List<Sprite>();
            _abilityTexts = new List<Sprite>();
            for (int i = 0; i < Enum.GetNames(typeof(Library.Character.Name)).Length; i++)
            {
                _characterSprites.Add(new Sprite(Library.Character.InterfacePath[(Library.Character.Name)i], new Vector2(posX, posY), DrawingHelper.DrawingLevel.Interface, Game));
                _abilityTexts.Add(new Sprite(Library.Character.InterfaceTextPath[(Library.Character.Name)i], new Vector2(posX, posY), DrawingHelper.DrawingLevel.Interface, Game));
            }

            // TODO magic values :(
            _zone = new Sprite("Interface/zoneAbilityText", new Vector2(posX - 100, posY + 50), DrawingHelper.DrawingLevel.Interface, Game);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("Fonts/menufont");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            switch (_currentState)
            {
                case State.Unconnected:
                    if (GamePad.GetState(_playerIndex).IsConnected) Connect();
                    break;
                case State.Connected:
                    if (GamePad.GetState(_playerIndex).IsButtonDown(Buttons.A)) Join();
                    break;
                case State.Joined:
                    if (GamePad.GetState(_playerIndex).IsButtonDown(Buttons.Start)) Ready();
                    break;
                case State.Ready:
                    break;
            }

            _previousState = GamePad.GetState(_playerIndex);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());

            switch (_currentState)
            {
                case State.Unconnected:
                    DrawingHelper.DrawString(_spriteBatch, _font, "Unconnected", _rectangle, DrawingHelper.Alignment.Center, Color.White);
                    break;
                case State.Connected:
                    DrawingHelper.DrawString(_spriteBatch, _font, "Press A to Join", _rectangle, DrawingHelper.Alignment.Center, Color.White);
                    break;
                case State.Joined:
                    break;
                case State.Ready:
                    DrawingHelper.DrawString(_spriteBatch, _font, "Ready!", _rectangle,DrawingHelper.Alignment.Center, Color.White);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        void Connect()
        {
            _currentState = State.Connected;
        }
        void Join()
        {
            _currentState = State.Joined;
        }
        void Ready()
        {
            _currentState = State.Ready;
        }
    }
}
