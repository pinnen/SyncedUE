using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame
{
    class Text : DrawableGameComponent
    {
        string _text;
        Rectangle _rectangle;
        SpriteFont _font;

        public string Text1
        {
            get { return _text; }
            set { _text = value; }
        }
        SpriteBatch _spriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        } 
        public Text(string text, Game game)
            :  base (game)
        {
            _text = text;
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
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            DrawingHelper.DrawString(_spriteBatch, _font, _text, _rectangle, DrawingHelper.Alignment.Center, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
