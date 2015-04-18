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
        Texture2D _background;

        List<Button> _buttons;
        SpriteBatch _spriteBatch;
        string _texturePath;

        public MenuScreen(string texturePath, Game game)
            : base(game)
        {
            this._texturePath = texturePath;

            this.Game.Components.Add(this);
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(this.GraphicsDevice);
            _background = this.Game.Content.Load<Texture2D>(_texturePath);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
