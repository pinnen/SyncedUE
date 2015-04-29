using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Menu;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Player
{
    class Sprite : DrawableGameComponent, IDrawableObject
    {
        // Provide access to the spritebatch through game services. 
        SpriteBatch _spriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        }

        public Sprite(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game)
            : base(game)
        {
            Position = position;
            Texture = texture;
            DrawOrder = (int)drawingLevel;
        }

        public override void Initialize()
        {
            base.Initialize(); // Calls LoadContent
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(Texture, Position, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
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
    }
}
