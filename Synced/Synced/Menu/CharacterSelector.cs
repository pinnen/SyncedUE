using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Menu
{
    class CharacterSelector : DrawableGameComponent
    {
        enum State 
        {
            Unconnected, Connected, Joined, Ready
        }

        State _currentState;

        // TODO This is a very restricted design
        Sprite _pressToJoin;
        Sprite _zone;
        Sprite _arrows; 
        List<Sprite> _characterSprites;
        List<Sprite> _abilityTexts;

        Color _color;
        PlayerIndex _playerIndex;
        int _selectedIndex;
        GamePadState _previousState;

        
        public CharacterSelector(PlayerIndex playerIndex, Rectangle rectangle, Game game)
            : base(game)
        {
            // TODO This constructor looks very ugly. It is the same as the old code.
            _currentState = State.Unconnected;
            _pressToJoin = new Sprite("Interface/PressAToJoin", new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2), game);
            _arrows = new Sprite("Interface/SelectionArrows", new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2), game);

            _characterSprites = new List<Sprite>()
            {
                new Sprite("SelectCircle", new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2), game),
                new Sprite("SelectTriangle", new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2), game),
                new Sprite("SelectSquare", new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2), game),
                new Sprite("SelectPentagon", new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2), game),
                new Sprite("SelectHexagon", new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2), game)                
            };

            _zone = new Sprite("zoneAbilityText", new Vector2((rectangle.X + rectangle.Width / 2) - 100, (rectangle.Y + rectangle.Height / 2) + 50), game);
            _abilityTexts = new List<Sprite>()
            {
                new Sprite("CirkelText", new Vector2((rectangle.X + rectangle.Width / 2) + 65, (rectangle.Y + rectangle.Height / 2) + 47), game),
                new Sprite("TriangelText", new Vector2((rectangle.X + rectangle.Width / 2) + 90, (rectangle.Y + rectangle.Height / 2) + 51), game),
                new Sprite("FyrkantText", new Vector2((rectangle.X + rectangle.Width / 2) + 80, (rectangle.Y + rectangle.Height / 2) + 51), game),
                new Sprite("PentagonText", new Vector2((rectangle.X + rectangle.Width / 2) + 105, (rectangle.Y + rectangle.Height / 2) + 52), game),
                new Sprite("HexagonText", new Vector2((rectangle.X + rectangle.Width / 2) + 50, (rectangle.Y + rectangle.Height / 2) + 52), game)   
            };

            _playerIndex = playerIndex;

            // Add this component
            game.Components.Add(this);
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
                    break;
                case State.Ready:
                    break;
            }

            _previousState = GamePad.GetState(_playerIndex);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
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
