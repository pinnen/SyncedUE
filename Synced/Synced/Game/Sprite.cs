using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Menu
{
    class Sprite : DrawableGameComponent
    {
        Texture2D _texture;
        Vector2 _position;

        public Sprite(string texturePath, Vector2 position, Game game)
            : base(game)
        {
            this._position = position;
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
