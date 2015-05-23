using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Actors;
using Synced.CollisionShapes;
using Synced.InGame.Actors;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.MapNameSpace
{
    public enum GoalDirections
    { 
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }

    class TestGoal
    {
        DummyCircle InnerCircle;
        Circle OuterCircle;
        TexturePolygon Border;

        public TestGoal(Texture2D goalTexture, Texture2D borderTexture, Vector2 position, GoalDirections direction, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
        {
            InnerCircle = new DummyCircle(position, (goalTexture.Width / 2), game, world);
            InnerCircle.setOnCollisionFunction(OnCollision);
            OuterCircle = new Circle(goalTexture, position, goalTexture.Width / 2, game, world);
            OuterCircle.setOnCollisionFunction(OnCollision);
            OuterCircle.RigidBody.CollidesWith = Category.Cat2;

            float borderRotation = 0;
            Vector2 borderPosition = Vector2.Zero;

            switch (direction) // TODO: Add all directions. 
            {
                case GoalDirections.North:
                    borderRotation = 0;
                    borderPosition = new Vector2(position.X + borderTexture.Width / 2, position.Y);
                    break;
                case GoalDirections.NorthEast:
                    borderRotation = 0;
                    borderPosition = new Vector2(position.X + borderTexture.Width / 2, position.Y);
                    break;
                case GoalDirections.East:
                    borderRotation = 0;
                    borderPosition = new Vector2(position.X + borderTexture.Width / 2, position.Y);
                    break;
                case GoalDirections.SouthEast:
                    borderRotation = 0;
                    borderPosition = new Vector2(position.X + borderTexture.Width / 2, position.Y);
                    break;
                case GoalDirections.South:
                    borderRotation = 0;
                    borderPosition = new Vector2(position.X + borderTexture.Width / 2, position.Y);
                    break;
                case GoalDirections.SouthWest:
                    borderRotation = 0;
                    borderPosition = new Vector2(position.X + borderTexture.Width / 2, position.Y);
                    break;
                case GoalDirections.West:
                    borderRotation = (float)Math.PI;
                    borderPosition = new Vector2(position.X - borderTexture.Width / 2, position.Y);
                    break;
                case GoalDirections.NorthWest:
                    borderRotation = 0;
                    borderPosition = new Vector2(position.X + borderTexture.Width / 2, position.Y);
                    break;
            }

            Border = new TexturePolygon(borderTexture, borderPosition, borderRotation, DrawingHelper.DrawingLevel.Medium, game, world);
        }

        public bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            return true;
        }
    }
}
