using System;

namespace _0x46696E616C
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
            using (var game = new 0x46696E616CGame())
                game.Run();
        }
    }
#endif
}
