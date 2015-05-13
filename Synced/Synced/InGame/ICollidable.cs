using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics.Contacts;

namespace Synced.InGame
{
    interface ICollidable
    {
        void OnCollision(Fixture f1, Fixture f2, Contact contact);
    }
}
