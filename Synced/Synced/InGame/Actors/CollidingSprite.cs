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
using Synced.Actors;

namespace Synced.InGame.Actors
{
    abstract class CollidingSprite : Sprite
    {
        #region Variables
        protected World world;
        protected Body body;
        Guid id;
        #endregion

        #region Properties
        /* "overrides" Sprite position/rotation to translate pixels to FarseerPhysics units. */
        public new Vector2 Position
        {
            get { return ConvertUnits.ToDisplayUnits(body.Position); }
            set { body.Position = ConvertUnits.ToSimUnits(value); }
        }
        public new float Rotation 
        {
            get { return body.Rotation; }
            protected set { body.Rotation = value; } 
        }
        public Guid ID
        {
            get { return id; }
        }
        #endregion

        /// <summary>
        /// Creates a default Colliding Sprite. 
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="drawingLevel"></param>
        /// <param name="game"></param>
        /// <param name="world"></param>
        public CollidingSprite(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(texture, position, drawingLevel, game)
        {
            this.world = world;
            body = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position));
            body.BodyType = BodyType.Dynamic;
            body.CollidesWith = Category.All;
            body.CollisionCategories = Category.All;
            body.LinearDamping = 5f;                    // TODO: replace hardcoded value with a standard value
            body.Mass = 1f;                             // TODO: replace hardcoded value with a standard value
            body.Restitution = 0;                       // no bounciness, max value = 1. 
            body.OnCollision += OnCollision;
            body.OnSeparation += OnSeparation;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);

            // Generate unique ID
            id = Guid.NewGuid();
            body.UserData = id.ToByteArray();
        }

        /// <summary>
        /// Creates a 'Custom' Colliding Sprite
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="drawingLevel"></param>
        /// <param name="game"></param>
        /// <param name="world"></param>
        /// <param name="body"></param>
        /// <param name="bodyType"></param>
        /// <param name="collidesWith"></param>
        /// <param name="collisionCategory"></param>
        /// <param name="mass"></param>
        /// <param name="linearDamping"></param>
        /// <param name="restitution"></param>
        public CollidingSprite(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, 
                               Game game, World world, Body body, BodyType bodyType, Category collidesWith, 
                               Category collisionCategory, float mass, float linearDamping, float restitution)
            : base(texture, position, drawingLevel, game)
        {
            this.world = world;
            this.body = body;
            this.body.BodyType = bodyType;
            this.body.CollidesWith = collidesWith;
            this.body.CollisionCategories = collisionCategory;
            this.body.Mass = mass;
            this.body.LinearDamping = linearDamping;
            this.body.Restitution = restitution;
            this.body.OnCollision += OnCollision;
            this.body.OnSeparation += OnSeparation;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            
            // Generate unique ID
            id = Guid.NewGuid();
            body.UserData = id.ToByteArray();
        }

        /// <summary>
        /// FarseerPhysics OnCollisionEvent. 
        /// </summary>
        /// <param name="f1">This GameObject</param>
        /// <param name="f2">Other GameObject</param>
        /// <param name="contact"></param>
        /// <returns>return false if collision should be ignored</returns>
        virtual public bool OnCollision(Fixture f1, Fixture f2, Contact contact) { return true; }
        virtual public void OnSeparation(Fixture f1, Fixture f2) {}

        public override void Draw(GameTime gameTime)
        {       
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(Texture, ConvertUnits.ToDisplayUnits(body.Position), null, Color, body.Rotation, Origin, 1.0f, SpriteEffects.None, 1f);
            _spriteBatch.End();
        }
    }
}
