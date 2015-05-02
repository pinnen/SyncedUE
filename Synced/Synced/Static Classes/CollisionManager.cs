using Microsoft.Xna.Framework;
using Synced.InGame;
// InputManager.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
// Pontus Magnusson
//
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Static_Classes
{
    static class CollisionManager
    {
        public static void CircleCircleCollision(ICollidable a, ICollidable b)
        {
            Vector2 distance = new Vector2(b.Center.X - a.Center.X, b.Center.Y - a.Center.Y);
            double radiusSum = a.Radius + b.Radius;

            if (distance.X * distance.X + distance.Y * distance.Y <= radiusSum * radiusSum)
            {
                a.Response(b);
                b.Response(a);
            }

        }
    }
}
