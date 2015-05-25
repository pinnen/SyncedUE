// Program.cs
// Introduced: 2015-04-14
#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Synced
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new SyncedGame())
                game.Run();
        }
    }
#endif
}
