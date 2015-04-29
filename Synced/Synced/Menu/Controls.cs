using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Menu
{
    class Controls : DrawableGameComponent, IDrawableObject
    {
        string _texturePath;

        public Controls(Game game, string texturePath) : base(game)
        {
            _texturePath = texturePath;
        }

        public Vector2 Position
        {
            get;
            protected set;
        }

        public Texture2D Texture
        {
            get;
            protected set;
        }

        #region Events
        protected override void OnUpdateOrderChanged(object sender, EventArgs args)
        {
            base.OnUpdateOrderChanged(sender, args);
        }
        protected override void OnEnabledChanged(object sender, EventArgs args)
        {
            base.OnEnabledChanged(sender, args);
        }
        #endregion

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>(_texturePath);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
