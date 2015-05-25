// Movable.cs
// Introduced: 2015-04-14
// Last edited: 2015-05-10
// Edited by:
// Pontus Magnusson
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.Static_Classes;

namespace Synced.InGame
{
    class Movable : Sprite
    {
        #region Variables
        protected Vector2 direction;
        protected float constantForce;
        #endregion

        #region Properties
        public Vector2 Direction
        {
            get { return Direction; }
            set
            {
                Direction = value;
                Direction.Normalize();
            }
        }
        #endregion

        public Movable(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world, Vector2 direction, float constantForce)
            : base(texture, position, drawingLevel, game)
        {
            this.direction = direction;
            this.constantForce = constantForce;
        }

        public override void Update(GameTime gameTime)
        {
            Position += Direction * constantForce * gameTime.ElapsedGameTime.Seconds; // TODO: Do this correctly // TODO: Adjust to screen ratio

            base.Update(gameTime);
        }
    }
}
