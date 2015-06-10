using FarseerPhysics;
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
using SevenEngine.Drawing;
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
        Vector2 spawnPosition;
        #endregion

        public delegate void IncreaseScore(PlayerIndex playerIndex);
        public event IncreaseScore Scored;

        public Crystal(Texture2D texture, Vector2 position, DrawHelper.DrawingLevel drawingLevel, Game game, World world, Color color)
            : base(texture, position, drawingLevel, game, world, color)
        {
            // Store original position
            spawnPosition = position;

            /* Setting up Farseer physics */
            RigidBody.CollisionCategories = Category.Cat5;
            RigidBody.CollidesWith = defaultCollisiosCategory = Category.All ^ Category.Cat9;
            RigidBody.CollisionGroup = (short)CollisionCategory.CRYSTAL;
            RigidBody.OnCollision += CrystalOnCollision;

            IsActive = true;
        }

        bool CrystalOnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            if (fixtureB.CollisionGroup == (short)CollisionCategory.GOAL)
            {
                if (Scored != null)
                {
                    Scored(GetPlayerIndex());
                    Library.Audio.PlaySoundEffect(Library.Audio.SoundEffects.Score);
                    deactivateCrystal();
                }
                return false;
            }
            else if (fixtureB.CollisionGroup == (short)CollisionCategory.UNIT)
            {
                PickUp(fixtureB.Body);
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (Owner != null)
            {
                Position = ConvertUnits.ToDisplayUnits(Owner.Position);
            }

            #region Trail
            if (IsActive)
            {
                // Update trail
            }
            else
            {
                inactiveTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (inactiveTime <= 0)
                {
                    activateCrystal();
                }
            }
            #endregion

            base.Update(gameTime);
        }

        public PlayerIndex GetPlayerIndex() // TODO: fix this
        {
            return (PreviousOwner != null) ? /* */(PlayerIndex)(-1) : (PlayerIndex)(-1);
        }
        void deactivateCrystal()
        {
            RigidBody.CollisionCategories = Category.None;
            RigidBody.LinearDamping = 100;
            Visible = false;
            // cool particle effect! TODO: 
            IsActive = false;
            inactiveTime = 2;
            PreviousOwner = null;
            owner = null;
        }
        void activateCrystal()
        {
            RigidBody.CollisionCategories = defaultCollisiosCategory;
            Visible = true;
            IsActive = true;
            Position = spawnPosition;
            RigidBody.LinearDamping = 0.5f; // TODO: hardcoded value
        }
    }
}
