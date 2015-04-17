// Program.cs
// Introduced: 2015-04-14
// Last edited: 2015-04-14
// Edited by:
//
// 
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
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
