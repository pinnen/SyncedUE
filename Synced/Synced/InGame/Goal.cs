// TestGoal.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Dennis Stockhaus
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

    class Goal : GameComponent
    {
        DummyCircle InnerCircle;
        Circle OuterCircle;
        TexturePolygon Border;

        public Goal(Texture2D goalTexture, Texture2D borderTexture, Vector2 position, GoalDirections direction, DrawingHelper.DrawingLevel drawingLevel, Game game, World world)
            : base(game)
        {
            InnerCircle = new DummyCircle(position, ((goalTexture.Width / 2) / 2) / 2, game, world);
            InnerCircle.setOnCollisionFunction(OnCollision);
            InnerCircle.SetCollisionCategory(Category.Cat10);
            OuterCircle = new Circle(goalTexture, position, goalTexture.Width / 2, game, world);
            OuterCircle.RigidBody.CollisionCategories = Category.Cat9;
            //OuterCircle.setOnCollisionFunction(OnCollision);

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

            Border = new TexturePolygon(borderTexture, borderPosition, borderRotation, DrawingHelper.DrawingLevel.High, game, world, false);
            Border.SetCollisionCategory(Category.Cat10);
            Border.SetCollideWithCategory(Category.All);

            SyncedGameCollection.ComponentCollection.Add(OuterCircle);
            SyncedGameCollection.ComponentCollection.Add(InnerCircle);
            SyncedGameCollection.ComponentCollection.Add(Border);
        }

        public bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            CollidingSprite crystal = SyncedGameCollection.GetCollisionComponent(f2);

            if (crystal.Tag == TagCategories.CRYSTAL)
            {
                if (Scored != null)
                {
                    PlayerIndex p = (crystal as Crystal).GetPlayerIndex();
                    if ((int)p != -1)
                    {
                        Scored(p);
                        Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.Score);
                    }
                }
                return false;
            }
            return true;
        }

        public delegate void IncreaseScore(PlayerIndex playerIndex);

        public event IncreaseScore Scored;
    }
}
