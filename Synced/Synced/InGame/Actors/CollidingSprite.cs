using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Content;
using Synced.InGame;
using Synced.Static_Classes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics;

namespace Synced.InGame.Actors
{
    abstract class CollidingSprite : Synced.Actors.Sprite
    {
        #region Variables
        // FarseerPhysics variables
        protected World world;
        protected Body body;
        protected Shape shape;
        protected Fixture fixture;
        // Collision info variables
        // Tag etc. 
        #endregion

        #region Properties
        #endregion

        public CollidingSprite(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(texture, position, drawingLevel, game)
        {
            this.world = world;
            body = new Body(world, ConvertUnits.ToSimUnits(position), 0f);

        }

        virtual public bool OnCollision() { return true; }

        public override void Draw(GameTime gameTime)
        {
            // set display ratio
            //ConvertUnits.SetDisplayUnitToSimUnitRatio(10f);
            //float width = ConvertUnits.ToSimUnits((float)ResolutionManager.GetWidth);
            //float height = ConvertUnits.ToSimUnits((float)ResolutionManager.GetHeight);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(Texture, ConvertUnits.ToDisplayUnits(body.Position)/1000, null, Color, body.Rotation, Origin, 1.0f, SpriteEffects.None, 1.0f); // TODO: use body pos/rot or Sprite pos/rot? 
            _spriteBatch.End();

            Position = ConvertUnits.ToDisplayUnits(body.Position);
        }
    }
}
