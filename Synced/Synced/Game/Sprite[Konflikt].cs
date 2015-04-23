using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Menu
{
    class Sprite : DrawableGameComponent
    {
        Texture2D _texture;
        string _texturePath;
        Vector2 _position;
        
        
        /// <summary>
        /// Provide access to the spritebatch through game services. 
        /// </summary>
        SpriteBatch _spriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        } 

        public Sprite(string texturePath, Vector2 position, Game game)
            : base(game)
        {
            this._position = position;
            this._texturePath = texturePath;
        }

        public override void Initialize()
        {
            base.Initialize(); // Calls LoadContent
        }
        protected override void LoadContent()
        {
            _texture = Game.Content.Load<Texture2D>(_texturePath);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(_texture, _position, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
