// IActive.cs
// Introduced: 2015-05-14
// Last edited: 2015-05-14
// Edited by:
// Robin Calmegård

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
