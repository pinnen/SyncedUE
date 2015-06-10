using FarseerPhysics.Dynamics;
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

namespace Synced.InGame.Actors
{
    class CompactZone : Grabbable
    {

        #region Variables
        Game _game;
        bool _isShot;
        Library.Zone.Name _shape;
        int _timer;
        int _detonationTime;

        internal Library.Zone.Name Shape
        {
            get { return _shape; }
        }
        #endregion

        #region Properties
        public bool IsShot
        {
            get { return _isShot; }
            set { _isShot = value; }
        }
        #endregion

        public CompactZone(Texture2D texture, Vector2 position, DrawHelper.DrawingLevel drawingLevel, Game game, World world,Color color, Library.Zone.Name shape)
            : base(texture, position, drawingLevel, game, world, color) 
        {
            _game = game;
            RigidBody.CollisionGroup = (short)CollisionCategory.COMPACTZONE;
            _isShot = false;
            _shape = shape;
            _detonationTime = 70;
            _timer = _detonationTime;

        }

        public override void Shoot()
        {
            base.Shoot();
            _isShot = true;
        }

        public bool UpdateCompactZone()
        {
            if (_isShot)
            {
                _timer--;
                if (_timer <= 0)
                {
                    return true;
                }
            }
            else
            {
                _timer = _detonationTime;
            
            }
            return false;
        }

        public void Detonate() 
        {
            world.RemoveBody(RigidBody);
        }
    }
}
