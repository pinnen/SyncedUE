using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Menu
{
    class MenuScreen : DrawableGameComponent
    {
        const int _minimumPlayersConstant = 1;

        Sprite _background;
        List<Button> _buttons;
        string _texturePath;

        public MenuScreen(string texturePath, Game game)
            : base(game)
        {
            this._background = new Sprite(texturePath, Vector2.Zero, game);
            this.Game.Components.Add(this);
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
