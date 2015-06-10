using Microsoft.Xna.Framework;
using SevenEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Interface
{
    class ScoreLabel : Label
    {
        int _score;
        public int Score
        {
            get { return _score; }
        }
        public PlayerIndex PlayerIndex;
        public ScoreLabel(PlayerIndex playerIndex, Rectangle rectangle, Game game)
            : base("", rectangle, game)
        {
            _score = 0;
            PlayerIndex = playerIndex;
            Content = ScoreFormat();
            SetFont = Synced.Content.Library.Font.ScoreFont;
        }

        public void IncreaseScore()
        {
            _score++;
            Content = ScoreFormat();
        }
        private string ScoreFormat()
        {
            switch (PlayerIndex)
            {
                case PlayerIndex.Four:
                    return String.Format("{1} :{0}", PlayerIndex.ToString(), _score.ToString());
                case PlayerIndex.One:
                    return String.Format("{0}: {1}", PlayerIndex.ToString(), _score.ToString());
                case PlayerIndex.Three:
                    return String.Format("{0}: {1}", PlayerIndex.ToString(), _score.ToString());
                case PlayerIndex.Two:
                    return String.Format("{1} :{0}", PlayerIndex.ToString(), _score.ToString());
                default:
                    return "";
            }
        }
    }
}
