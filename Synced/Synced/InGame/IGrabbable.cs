using Synced.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame
{
    interface IGrabbable
    {
        IGrabbable PickUp(Movable owner);
        void Release();
        void Shoot();
    }
}
