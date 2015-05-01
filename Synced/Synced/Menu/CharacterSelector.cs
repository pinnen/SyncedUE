// CharacterSelector.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson
//
// 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Synced.Content;
using Synced.InGame;
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
        Rectangle _rectangle;

        // Objects/Texts/Controllers
        Sprite _characterHolder;    // Sprite that displays the character
        Sprite _zoneHolder;         // Sprite that displays the zone
        Sprite _arrowHolder;        // Sprite that displays the selection arrows
        Text _stateText;

        PlayerIndex _playerIndex;
        GamePadState _previousState;
        #endregion

        #region Properties
        SpriteBatch _spriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        }
        public State CurrentState
        {
            get;
            private set;
        }
        Library.Character.Name SelectedCharacter
        {
            get;
            set;
        }
        #endregion

        public CharacterSelector(PlayerIndex playerIndex, Rectangle rectangle, Game game)
            : base(game)
        {
            CurrentState = State.Unconnected;
            _rectangle = rectangle;
            _playerIndex = playerIndex;

            // Add this component
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            int posX = _rectangle.X + _rectangle.Width / 2;
            int posY = _rectangle.Y + _rectangle.Height / 2;
            Vector2 position = new Vector2(posX, posY);

            // Position of elements
            Vector2 characterPosition = new Vector2(posX, posY - 50);
            Vector2 zoneTextPosition = new Vector2(posX, posY + 50);
            Vector2 arrowPosition = characterPosition;

            _characterHolder = new Sprite(Library.Character.InterfaceTexture[(Library.Character.Name)0], characterPosition, DrawingHelper.DrawingLevel.Medium, Game);
            _zoneHolder = new Sprite(Library.Character.InterfaceTextTexture[(Library.Character.Name)0], zoneTextPosition, DrawingHelper.DrawingLevel.Medium, Game);
            _arrowHolder = new Sprite(Library.Interface.Arrows, arrowPosition, DrawingHelper.DrawingLevel.Medium, Game);

            // TODO temporary origin fix. Include somewhere nice later
            _characterHolder.Origin = new Vector2(_characterHolder.Texture.Width / 2, _characterHolder.Texture.Height / 2);
            _zoneHolder.Origin = new Vector2(_zoneHolder.Texture.Width / 2, _zoneHolder.Texture.Height / 2);
            _arrowHolder.Origin = new Vector2(_arrowHolder.Texture.Width / 2, _arrowHolder.Texture.Height / 2);

            _stateText = new Text("Unconnected!", new Rectangle(posX, posY, 0, 0), Game);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _stateText.SetFont = Library.Font.MenuFont;
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            switch (CurrentState)
            {
                case State.Unconnected:
                    if (GamePad.GetState(_playerIndex).IsConnected) _connect();
                    break;
                case State.Connected:
                    if (GamePad.GetState(_playerIndex).IsButtonDown(Buttons.A) && _previousState.IsButtonUp(Buttons.A))
                    {
                        _join();
                    }
                    break;
                case State.Joined:
                    if (GamePad.GetState(_playerIndex).IsButtonDown(Buttons.A) && _previousState.IsButtonUp(Buttons.A))
                    {
                        _ready();
                    }
                    _readInput();
                    break;
                case State.Ready:
                    if (GamePad.GetState(_playerIndex).IsButtonDown(Buttons.B) && _previousState.IsButtonUp(Buttons.B))
                    {
                        _join();
                    }
                    break;
            }

            _previousState = GamePad.GetState(_playerIndex);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        void _connect()
        {
            CurrentState = State.Connected;
            _stateText.Content = "Press A to join!";
        }
        void _join() 
        {
            CurrentState = State.Joined;
            _stateText.Content = "";

            Game.Components.Add(_characterHolder);
            Game.Components.Add(_zoneHolder);
            Game.Components.Add(_arrowHolder);
        }
        void _ready()
        {
            CurrentState = State.Ready;
            _stateText.Content = "Ready!";

            Game.Components.Remove(_characterHolder);
            Game.Components.Remove(_zoneHolder);
            Game.Components.Remove(_arrowHolder);
        }
        void _nextCharacter(int direction)
        {
            int index = (int)SelectedCharacter + direction;
            if (index >= Enum.GetNames(typeof(Library.Character.Name)).Length)
                index = 0;
            else if (index < 0)
                index = Enum.GetNames(typeof(Library.Character.Name)).Length - 1;

            SelectedCharacter = (Library.Character.Name)index;

            _characterHolder.Texture = Library.Character.InterfaceTexture[(Library.Character.Name)index];
            _zoneHolder.Texture = Library.Character.InterfaceTextTexture[(Library.Character.Name)index];
        }

        void _readInput()
        {
            if (GamePad.GetState(_playerIndex).ThumbSticks.Left.X > 0f && _previousState.ThumbSticks.Left.X <= 0f)
                _nextCharacter(-1);

            if (GamePad.GetState(_playerIndex).ThumbSticks.Left.X < 0f && _previousState.ThumbSticks.Left.X >= 0f)
                _nextCharacter(1);
        }
    }
}
