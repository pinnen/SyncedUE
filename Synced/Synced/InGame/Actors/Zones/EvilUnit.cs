using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SevenEngine.Drawing;
using Synced.Actors;
using Synced.Content;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Actors.Zones
{
    class EvilUnit : MovableCollidable
    {
         #region Variables
        Vector2 lastNonZeroDirection;
        Unit _copyOf;
        float _offset;
    
        #endregion

        #region Properties
        public Vector2 LastNonZeroDirection
        {
            get { return lastNonZeroDirection; }
        }
        #endregion

        public EvilUnit(Texture2D texture, Vector2 position, Color color, Game game, World world, DrawHelper.DrawingLevel drawingLevel, Unit copyOf, float offset)
            : base(texture, position, drawingLevel, game, world)
        {
;
            RigidBody = BodyFactory.CreateCircle(this.world, ConvertUnits.ToSimUnits(texture.Width / 2), 0, ConvertUnits.ToSimUnits(position));
            RigidBody.BodyType = BodyType.Dynamic;
            RigidBody.CollisionCategories = Category.Cat1;
            RigidBody.CollidesWith = Category.All ^ Category.Cat1;  
            RigidBody.Mass = 10f;
            RigidBody.LinearDamping = 5f;
            RigidBody.Restitution = 0.1f;
            RigidBody.CollisionGroup = (short)CollisionCategory.UNDEFINED;
        
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);

            acceleration = 40;
            Color = color;

            _copyOf = copyOf;
            _offset = offset;
        }



        public override void Update(GameTime gameTime)
        {
            this.direction = _copyOf.Direction;

            float newX = (float)(Math.Cos(_offset) * direction.X - Math.Sin(_offset) * direction.Y);
            float newY = (float)(Math.Sin(_offset) * direction.X - Math.Cos(_offset) * direction.Y);

            if (direction != Vector2.Zero)
            {
                direction = new Vector2(newX, newY);
                lastNonZeroDirection = direction;
                RigidBody.Rotation = (float)Math.Atan2(RigidBody.LinearVelocity.Y, RigidBody.LinearVelocity.X);
            }
            
            base.Update(gameTime);
        }


    }
}
