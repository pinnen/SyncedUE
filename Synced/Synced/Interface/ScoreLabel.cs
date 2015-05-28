using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Interface
{
    class ScoreLabel : Label
    {
        int _score;
        public PlayerIndex PlayerIndex;
        public ScoreLabel(PlayerIndex playerIndex, Rectangle rectangle, Game game)
            : base("", rectangle, game)
        {
            _score = 0;
            PlayerIndex = playerIndex;
            Content = _score.ToString();
        }

        public void IncreaseScore()
        {
            _score++;
            Content = _score.ToString();
        }
    }
}
