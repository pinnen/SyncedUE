using Microsoft.Xna.Framework;
using SevenEngine.Drawing;
using SevenEngine.State;
using Synced.Actors;
using Synced.Content;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Interface
{
    class LoadingScreen : Screen
    {
        public LoadingScreen(Game game) : base(game)
        {
            GameComponents.Add(new Sprite(Library.Screens.LoadScreen, Vector2.Zero, DrawHelper.DrawingLevel.Top, game));
        }
    }
}
