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
        Vector2 bodyOrigin;
        // Collision info variables
        // Tag etc. 
        #endregion

        #region Properties
        #endregion

        public CollidingSprite(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(texture, position, drawingLevel, game)
        {
            this.world = world;
            ConvertUnits.SetDisplayUnitToSimUnitRatio(1000);
            body = BodyFactory.CreateCircle(world, texture.Width / 2, 0, position);
            body.BodyType = BodyType.Dynamic;
            body.CollidesWith = Category.All;
            body.CollisionCategories = Category.All;
            body.LinearDamping = 2f;

            bodyOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        virtual public bool OnCollision() { return true; }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(Texture, ConvertUnits.ToDisplayUnits(body.Position)/1000, null, Color, body.Rotation, bodyOrigin, 1.0f, SpriteEffects.None, 1f); // TODO: use body pos/rot or Sprite pos/rot? 
            _spriteBatch.End();

            Position = ConvertUnits.ToDisplayUnits(body.Position);
        }
    }
}
