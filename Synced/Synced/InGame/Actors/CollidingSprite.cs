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

public enum CollisionCategory 
{
    UNDEFINED,
    CRYSTAL,
    UNIT,
    GOAL,
    COMPACTZONE,
    ZONE,
    BARRIER
}

namespace Synced.InGame.Actors
{
    abstract class CollidingSprite : Sprite // TODO: Add a Collidable interface
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
                rigidBody.UserData = id.ToString();
                rigidBody.OnCollision += OnCollision;
                rigidBody.OnSeparation += OnSeparation;
            }
        }
        public new Vector2 Position
        {
            get { return ConvertUnits.ToDisplayUnits(rigidBody.Position); } // TODO: check if it should be converted or not
            set { rigidBody.Position = ConvertUnits.ToSimUnits(value); }
        }
        public Vector2 SimPosition
        {
            get { return rigidBody.Position; } // TODO: check if it should be converted or not
            set { rigidBody.Position = value; }
        }
        public new float Rotation 
        {
            get { return rigidBody.Rotation; }
            set { rigidBody.Rotation = value; } 
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
            /* Setting up Farseer Physics*/
            this.world = world;

            /* Setting up CollidingSprite */
            id = Guid.NewGuid();
        }
        
        /// <summary>
        /// FarseerPhysics OnCollisionEvent. Called when Collision Occures.
        /// FarseerPhysics OnSeparationEvent. Called when Collision no longer Occures. 
        /// </summary>
        /// <param name="f1">This GameObject</param>
        /// <param name="f2">Other GameObject</param>
        /// <returns>return false if collision should be ignored</returns>
        virtual public bool OnCollision(Fixture f1, Fixture f2, Contact contact) { return true; }
        virtual public void OnSeparation(Fixture f1, Fixture f2) {}

        public override void Draw(GameTime gameTime)
        {       
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, ResolutionManager.GetTransformationMatrix());
            _spriteBatch.Draw(Texture, ConvertUnits.ToDisplayUnits(rigidBody.Position), null, Color * Alpha, rigidBody.Rotation, Origin, Scale, SpriteEffects.None, 1f);
            _spriteBatch.End();
        }
    }
}