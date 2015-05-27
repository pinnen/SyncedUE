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
        #endregion

        public Crystal(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world, Color color)
            : base(texture, position, drawingLevel, game, world, color)
        {
            RigidBody.CollisionCategories = Category.Cat5;
            /* Setting up Farseer physics */
            RigidBody.CollidesWith = Category.All ^ Category.Cat9;

            /* Setting up Crystal */
            Tag = TagCategories.CRYSTAL;

            _tail = new ParticleEngine(1, Library.Particle.trailTexture, position, color, Origin, 1.0f, 0.0f, 0.2f, DrawingHelper.DrawingLevel.Low, game);
            SyncedGameCollection.ComponentCollection.Add(_tail);
            _tail.ParticleColor = Color.LightGray;
        }

        public override void Update(GameTime gameTime)
        {
            _tail.UpdatePosition(Position);
            _tail.GenerateTrailParticles();

            base.Update(gameTime);
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


        

    }
}
