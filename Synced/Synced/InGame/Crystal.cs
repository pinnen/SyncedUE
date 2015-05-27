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
using Synced.Static_Classes;


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

            _tail.ParticleColor = Color.LightGray;
        }

        public override bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {    
            return true;
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
