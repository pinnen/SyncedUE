using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Player
{
    class Sprite : DrawableGameComponent
    {
        
        Texture2D _texture;
        string _texturePath;
        Vector2 _position;
        
        
        // Provide access to the spritebatch through game services. 
        SpriteBatch _spriteBatch
        {
            get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        } 

        public Sprite(string texturePath, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game)
            : base(game)
        {
            _position = position;
            _texturePath = texturePath;
            DrawOrder = (int)drawingLevel;
        }

        /// <summary>
        /// Add the sprite to the components list
        /// </summary>
        public void Enable()
        {
            //if (!Game.Components.Contains(this)) Game.Components.Add(this);
            Enabled = true;
        }

        /// <summary>
        /// Remove the sprite from the components list
        /// </summary>
        public void Disable()
        {
            //if(Game.Components.Contains(this)) Game.Components.Remove(this);
            Enabled = false;
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
