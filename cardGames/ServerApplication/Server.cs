using System;
using NetworkCommsDotNet;

namespace ServerApplication
{
    internal static class Server
    {
        private static void Main()
        {
            var room = new Room();
            var network = new ServerNetwork(ref room);
            network.Run();
         
            Console.WriteLine("\nPress any key to close server.");
            Console.ReadKey(true);
        
            NetworkComms.Shutdown();
        }
    }
}