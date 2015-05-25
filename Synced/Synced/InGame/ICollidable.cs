// ICollidable.cs
// Introduced: 2015-05-14
// Last edited: 2015-05-15
// Edited by:
// Dennis Stockhaus
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace Synced.InGame
{
    interface ICollidable
    {
        void OnCollision(Fixture f1, Fixture f2, Contact contact);
    }
}
