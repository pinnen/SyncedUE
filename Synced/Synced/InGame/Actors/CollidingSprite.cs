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
using FarseerPhysics.Dynamics.Contacts;

namespace Synced.InGame.Actors
{
    abstract class CollidingSprite : Synced.Actors.Sprite
    {
        #region Variables
        protected World world;
        protected Body body;
        Vector2 bodyOrigin;
        #endregion

        #region Properties
        #endregion

        public CollidingSprite(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(texture, position, drawingLevel, game)
        {
            this.world = world;
            ConvertUnits.SetDisplayUnitToSimUnitRatio(35f);    // 35 pixels = 1 meter
            body = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position));
            body.BodyType = BodyType.Dynamic;
            body.CollidesWith = Category.All;
            body.CollisionCategories = Category.All;
            body.LinearDamping = 5f;
            body.Mass = 1f;
            body.OnCollision += OnCollision;
            
  
            bodyOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        // f1 = this gameObject, f2 = other gameObject
        virtual public bool OnCollision(Fixture f1, Fixture f2, Contact contact) { return true; }

        public override void Draw(GameTime gameTime)
        {       
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(Texture, ConvertUnits.ToDisplayUnits(body.Position), null, Color, body.Rotation, bodyOrigin, 1.0f, SpriteEffects.None, 1f); // TODO: use body pos/rot or Sprite pos/rot? 
            _spriteBatch.End();
        }
    }
}
