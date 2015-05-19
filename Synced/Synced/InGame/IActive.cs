using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.InGame
{
    interface IActive
    {
        /// <summary>
        /// Virtual method, this method is called when a screen is activated 
        /// </summary>
        void Activated();

        /// <summary>
        /// Virtual method, this method is called when a screen is deactivated 
        /// </summary>
        void Deactivated();
    }
}
