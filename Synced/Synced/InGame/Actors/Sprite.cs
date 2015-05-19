// Sprite.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-30
// Edited by:
// Pontus Magnusson
//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Interface;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Actors
{
    class Sprite : DrawableGameComponent, IDrawableObject
    {
        // Provide access to the spritebatch through game services. 
        protected SpriteBatch _spriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        }
        public Vector2 Origin { get; set; }
        public Color Color { get; protected set; }
        public float Rotation { get; protected set; }

        public Sprite(Texture2D texture, Vector2 position, Color color, DrawingHelper.DrawingLevel drawingLevel, bool centered, Game game)
            : base(game)
        {
            Color = color;
            Position = position;
            Texture = texture;

            if (centered) Origin = new Vector2(texture.Width / 2, texture.Height / 2);

            DrawOrder = (int)drawingLevel;
        }
        public Sprite(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game)
            : this(texture, position, Color.White, drawingLevel, false, game) { }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, 1.0f, SpriteEffects.None, 1.0f);
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
            set;
        }

    }
}
