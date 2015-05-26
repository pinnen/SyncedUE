using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Synced.CollisionShapes;
using Synced.InGame.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Static_Classes
{
    sealed class SyncedGameCollection : DrawableGameComponent
    {
        #region Singleton
        private static SyncedGameCollection syncedGameCollection;
        public static SyncedGameCollection Instance
        {
            get { return syncedGameCollection; }
        }
        public static void InitializeSyncedGameCollection(Game game)
        {
            if (syncedGameCollection == null)
            {
                syncedGameCollection = new SyncedGameCollection(game);
            }
        }
        #endregion

        #region Variables
        static GameComponentCollection componentCollection; 
        #endregion

        #region Properties
        public static GameComponentCollection ComponentCollection
        {
            get { return componentCollection; }
            set { componentCollection = value; }
        }
        #endregion

        private SyncedGameCollection(Game game) : base(game)
        {
            if (componentCollection != null)
            {
                componentCollection.Clear();
            }
            else
            {
                componentCollection = new GameComponentCollection();
            }
        }

        public static CollidingSprite GetCollisionComponent(Fixture other)
        {
            foreach (GameComponent gc in ComponentCollection)
            {
                if (gc is CollidingSprite)
                {
                    CollidingSprite cs = (CollidingSprite)gc;
                    if (cs.ID.ToString() == other.Body.UserData.ToString())
                    {
                        return cs;
                    }
                }
            }
            return new DummyComponent();
        }

        public override void Update(GameTime gameTime)
        {


            //Updates every component in GameComponents
            foreach (IUpdateable gc in ComponentCollection.OfType<IUpdateable>().Where<IUpdateable>(x => x.Enabled).OrderBy<IUpdateable, int>(x => x.UpdateOrder))
                gc.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Draws every component in GameComponents
            foreach (IDrawable gc in ComponentCollection.OfType<IDrawable>().Where<IDrawable>(x => x.Visible).OrderBy<IDrawable, int>(x => x.DrawOrder))
                gc.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
