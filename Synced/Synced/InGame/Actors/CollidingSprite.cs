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
        Body rigidBody;
        Guid id;
        #endregion

        #region Properties       
        public Body RigidBody
        {
            get { return rigidBody; }
            set 
            {
                rigidBody = value;
                rigidBody.UserData = id.ToByteArray();
                rigidBody.OnCollision += OnCollision;
                rigidBody.OnSeparation += OnSeparation;
            }
        }
        /* "overrides" Sprite position/rotation to translate pixels to FarseerPhysics units. */
        public new Vector2 Position
        {
            get { return rigidBody.Position; }
            set { rigidBody.Position = ConvertUnits.ToSimUnits(value); }
        }
        public new float Rotation 
        {
            get { return rigidBody.Rotation; }
            protected set { rigidBody.Rotation = value; } 
        }
        public Guid ID
        {
            get { return id; }
        }
        #endregion

        /// <summary>
        /// Creates a default Colliding Sprite. 
        /// </summary>
        public CollidingSprite(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(texture, position, drawingLevel, game)
        {
            this.world = world;

            // Generate unique ID
            id = Guid.NewGuid();
        }
        
        /// <summary>
        /// FarseerPhysics OnCollisionEvent. 
        /// </summary>
        /// <param name="f1">This GameObject</param>
        /// <param name="f2">Other GameObject</param>
        /// <returns>return false if collision should be ignored</returns>
        virtual public bool OnCollision(Fixture f1, Fixture f2, Contact contact) { return true; }
        virtual public void OnSeparation(Fixture f1, Fixture f2) {}

        public override void Draw(GameTime gameTime)
        {       
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(Texture, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color, rigidBody.Rotation, Origin, 1.0f, SpriteEffects.None, 1f);
            _spriteBatch.End();
        }
    }
}