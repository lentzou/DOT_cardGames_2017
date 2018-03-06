using System;
using System.Linq;
using NetworkCommsDotNet;

namespace ClientApplication
{
    internal static class Client
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome\nPlease enter the server IP and port in the format 192.168.0.1:10000 and press return:");
            var serverInfo = Console.ReadLine();

            try
            {
                if (serverInfo != null)
                {
                    var serverIp = serverInfo.Split(':').First();
                    var serverPort = int.Parse(serverInfo.Split(':').Last());
                    var network = new ClientNetwork(serverIp, serverPort);
                    network.Run();
                    var handler = new ClientMsgHandler(network.GetTcPcon());
                    string input;
                    while (true)
                    {
                        input = Console.ReadLine();
                        if (String.CompareOrdinal(input, "quit") == 0)
                            break;
                        handler.TreatInputFromUser(input);
                    }
                }
            }
            catch (ArgumentNullException)
            {
                if (serverInfo != null) Console.WriteLine("{0} Argument Null", serverInfo.Split(':').Last());
            }
            catch (FormatException)
            {
                if (serverInfo != null) Console.WriteLine("{0}: Bad Format", serverInfo.Split(':').Last());
            }
            catch (OverflowException)
            {
                if (serverInfo != null) Console.WriteLine("{0}: Overflow", serverInfo.Split(':').Last());
            }
            catch (CommsException)
            {
                Console.WriteLine("CommsExecption Occured");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Wrong Port argument");
            }
            NetworkComms.Shutdown();
        }
    }
}