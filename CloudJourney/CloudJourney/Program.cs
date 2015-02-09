using System;

namespace CloudJourney
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Cloud game = new Cloud())
            {
                game.Run();
            }
        }
    }
#endif
}

