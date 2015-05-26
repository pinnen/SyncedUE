// Text.cs
// Introduced: 2015-04-28
// Last edited: 2015-04-29
// Edited by:
// Pontus Magnusson
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Static_Classes;

namespace Synced.Interface
{
    class Label : DrawableGameComponent
    {
        #region Variables
        string _content;
        Rectangle _rectangle;
        SpriteFont _font;
        DrawingHelper.Alignment _alignment;
        #endregion

        #region Properties
        SpriteBatch _spriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        } 
        public DrawingHelper.Alignment Alignment
        {
            get { return _alignment; }
            set { _alignment = value; }
        }
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        public SpriteFont SetFont
        {
            set { _font = value; }
        }
        #endregion

        public Label(string content, Rectangle rectangle, Game game)
            : base(game)
        {
            _content = content;
            _rectangle = rectangle;
            _alignment = DrawingHelper.Alignment.Center;
            DrawOrder = (int)DrawingHelper.DrawingLevel.Top;
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            DrawingHelper.DrawString(_spriteBatch, _font, _content, _rectangle, DrawingHelper.Alignment.Center, 3, Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
