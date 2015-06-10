using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SevenEngine.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SevenEngine.Interface
{
    public class Label : DrawableGameComponent
    {
        #region Variables
        string _content;
        Rectangle _rectangle;
        SpriteFont _font;
        DrawHelper.Alignment _alignment;
        #endregion

        #region Properties
        public DrawHelper.Alignment Alignment
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
            _alignment = DrawHelper.Alignment.Center;
            DrawOrder = (int)DrawHelper.DrawingLevel.Top;
            Visible = true;
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                DrawHelper.SpriteBatch(Game).Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
                DrawHelper.DrawString(Game, _font, _content, _rectangle, DrawHelper.Alignment.Center, 3, Color.Black);
                DrawHelper.SpriteBatch(Game).End();
            }

            base.Draw(gameTime);
        }
    }
}
