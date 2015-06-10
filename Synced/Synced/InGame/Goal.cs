// TestGoal.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Dennis Stockhaus
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SevenEngine.Drawing;
using Synced.CollisionShapes;
using Synced.Content;
using Synced.InGame;
using Synced.InGame.Actors;
using Synced.Interface;
using Synced.MapNamespace;
using Synced.Static_Classes;
using System;



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

    class Goal : DrawableGameComponent
    {
        DummyCircle _innerCircle;
        public Circle OuterCircle;
        public TexturePolygon Border;

        public Goal(Texture2D goalTexture, Texture2D borderTexture, Vector2 position, GoalDirections direction, DrawHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(game)
        {
            // TODO: Fråga dennis vad i helvete ----------------------------v
            _innerCircle = new DummyCircle(position, ((goalTexture.Width / 2) / 2) / 2, game, world);
            _innerCircle.SetCollisionCategory(Category.Cat10);
            _innerCircle.RigidBody.CollisionGroup = (short)CollisionCategory.GOAL;

            OuterCircle = new Circle(goalTexture, position, goalTexture.Width / 2, game, world);
            OuterCircle.RigidBody.CollisionCategories = Category.Cat9;

            float borderRotation = 0;
            Vector2 borderPosition = Vector2.Zero;

            #region direction cases
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
                    borderPosition = new Vector2((position.X + borderTexture.Width / 2) + 5.30973f, (position.Y) - 0.63129f); // TODO: hardcoded offset
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
                    borderPosition = new Vector2((position.X - borderTexture.Width / 2) - 5.30973f, (position.Y) + 0.63129f); // TODO: other solution for very specific offset for texture/vertice position
                    break;
                case GoalDirections.NorthWest:
                    borderRotation = 0;
                    borderPosition = new Vector2(position.X + borderTexture.Width / 2, position.Y);
                    break;
            }
            #endregion

            Border = new TexturePolygon(borderTexture, borderPosition, borderRotation, DrawHelper.DrawingLevel.High, game, world, false);
            Border.SetCollisionCategory(Category.Cat10);
            Border.SetCollideWithCategory(Category.All);
        }

        public bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            return true;
        }

    }
}
