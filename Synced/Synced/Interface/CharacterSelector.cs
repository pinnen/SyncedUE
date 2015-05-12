// CharacterSelector.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-10
// Edited by:
// Pontus Magnusson
// Göran Forsström
// 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Synced.Content;
using Synced.InGame;
using Synced.Actors;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// TODO:
// Create controls for all objects/texts/controllers
// Update the list to hide/show objects with:
// myList.ForEach(c => c.Enabled = false);
// http://stackoverflow.com/questions/1883920/call-a-function-for-each-value-in-a-generic-c-sharp-collection

namespace Synced.Interface
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
        Text _abilityTextHolder;     // Text that displays the ability text
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
        State _currentState
        {
            get;
            set;
        }
        public Library.Character.Name SelectedCharacter
        {
            get;
            private set;
        }
        Color _color { get; set; }
        #endregion

        public CharacterSelector(PlayerIndex playerIndex, Rectangle rectangle, Color color, Game game)
            : base(game)
        {
            _color = color;
            _currentState = State.Unconnected;
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
            Rectangle abilityTextRectangle = new Rectangle(posX, posY + 50, 0, 0);
            Vector2 arrowPosition = characterPosition;

            _characterHolder = new Sprite(Library.Character.InterfaceTexture[(Library.Character.Name)0], characterPosition, _color, DrawingHelper.DrawingLevel.Medium, true, Game);
            _abilityTextHolder = new Text("", abilityTextRectangle, Game);
            _arrowHolder = new Sprite(Library.Interface.Arrows, arrowPosition, _color, DrawingHelper.DrawingLevel.Medium, true, Game);

            _stateText = new Text("Unconnected!", new Rectangle(posX, posY, 0, 0), Game);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _stateText.SetFont = Library.Font.MenuFont;
            _abilityTextHolder.SetFont = Library.Font.MenuFont;
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            switch (_currentState)
            {
                case State.Unconnected:
                    if (GamePad.GetState(_playerIndex).IsConnected) _connect();
                    break;
                case State.Connected:
                    if (InputManager.IsButtonPressed(Buttons.A, _playerIndex))
                    {
                        _join();
                    }
                    break;
                case State.Joined:
                    if (InputManager.IsButtonPressed(Buttons.A, _playerIndex))
                    {
                        _ready();
                    }
                    _readInput();
                    break;
                case State.Ready:
                    if (InputManager.IsButtonPressed(Buttons.B, _playerIndex))
                    {
                        _join();
                    }
                    break;
            }

            CheckForDisconnect();

            _previousState = GamePad.GetState(_playerIndex);
            base.Update(gameTime);
        }
        public bool IsReady()
        {
            return _currentState == State.Ready;
        }
        void CheckForDisconnect()
        {
            if (!GamePad.GetState(_playerIndex).IsConnected)
            {
                _currentState = State.Unconnected;
                _stateText.Content = "Unconnected";
                _abilityTextHolder.Content = "";
                if (Game.Components.Contains(_characterHolder)) Game.Components.Remove(_characterHolder);
                if (Game.Components.Contains(_arrowHolder)) Game.Components.Remove(_arrowHolder);
            }
        }
        void _connect()
        {
            _currentState = State.Connected;
            _stateText.Content = "Press A to join!";
        }
        void _join()
        {
            _currentState = State.Joined;
            _stateText.Content = "";
            _abilityTextHolder.Content = Library.Character.AbilityText[SelectedCharacter];
            Game.Components.Add(_characterHolder);
            Game.Components.Add(_arrowHolder);

            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.MenuConfirm);
        }
        void _ready()
        {
            _currentState = State.Ready;
            _stateText.Content = "Ready!";
            _abilityTextHolder.Content = "";

            Game.Components.Remove(_characterHolder);
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
            _abilityTextHolder.Content = Library.Character.AbilityText[(Library.Character.Name)index];

            Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.MenuSelect);
        }

        void _readInput()
        {
            if (InputManager.LeftStickLeft(_playerIndex))
                _nextCharacter(-1);

            else if (InputManager.LeftStickRight(_playerIndex))
                _nextCharacter(1);
        }
    }
}
