// Crystal.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-23
// Edited by:
// Pontus Magnusson
// Göran Forsström
// Dennis Stockhaus
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Content;
using Synced.InGame.Actors;
using Synced.Static_Classes;
using System;


namespace Synced.InGame
{
    class Crystal : Grabbable
    {
        #region Variables
        Category defaultCollisiosCategory;
        bool IsActive;
        float inactiveTime;
        Vector2 nextSpawnPosition;
        #endregion

        public Crystal(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world, Color color)
            : base(texture, position, drawingLevel, game, world, color)
        {
            RigidBody.CollisionCategories = Category.Cat5;
            /* Setting up Farseer physics */
            RigidBody.CollidesWith = defaultCollisiosCategory = Category.All ^ Category.Cat9;

            /* Setting up Crystal */
            Tag = TagCategories.CRYSTAL;

            _tail = new ParticleEngine(1, Library.Particle.trailTexture, position, color, Origin, 1.0f, 0.0f, 0.2f, DrawingHelper.DrawingLevel.Low, game);
            SyncedGameCollection.ComponentCollection.Add(_tail);
            _tail.ParticleColor = Color.LightGray;
            IsActive = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                _tail.UpdatePosition(Position);
                _tail.GenerateTrailParticles();
            }
            else
            {
                inactiveTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (inactiveTime <= 0)
                {
                    ActivateCrystal();
                }
            }

            base.Update(gameTime);
        }

        public PlayerIndex GetPlayerIndex()
        {
            return PreviousOwner.PlayerIndex;
        }

        public void ChangeColor(Color newColor) 
        {
            this.Color = newColor;
            _tail.ParticleColor = newColor;
        }
        public void ResetColor() 
        {
            this.Color = Color.White;
            _tail.ParticleColor = Color.LightGray;
        }

        public void DeactivateCrystal(Vector2 position) //TODO: weird with position. 
        {
            RigidBody.CollisionCategories = Category.None;
            nextSpawnPosition = position;
            RigidBody.LinearDamping = 100;
            //RigidBody.BodyType = BodyType.Static;
            Visible = false;
            // cool particle effect! TODO: 
            IsActive = false;
            inactiveTime = 2;
            PreviousOwner = null;
            owner = null;
            
            ResetColor();
        }
        public void ActivateCrystal()
        {
            RigidBody.CollisionCategories = defaultCollisiosCategory;
            Visible = true;
            IsActive = true;
            Position = nextSpawnPosition;
            RigidBody.LinearDamping = 0.5f; // TODO: hardcoded value
            //RigidBody.BodyType = BodyType.Dynamic;
        }
    }
}
