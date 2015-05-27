using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame.Actors
{
    class CompactZone : Grabbable
    {

        #region Variables
        Game _game;
        bool _isShot;
        #endregion

        #region Properties
        public bool IsShot
        {
            get { return _isShot; }
            set { _isShot = value; }
        }
        #endregion

        public CompactZone(Texture2D texture, Vector2 position, DrawingHelper.DrawingLevel drawingLevel, Game game, World world,Color color)
            : base(texture, position, drawingLevel, game, world, color) 
        {
            _game = game;
            Tag = TagCategories.COMPACTZONE;
            _isShot = false;

        }

        public override void Shoot()
        {
            base.Shoot();
            _isShot = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Detonate() 
        {
            SyncedGameCollection.ComponentCollection.Remove(this);
        }
    }
}
