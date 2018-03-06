using NetworkCommsDotNet;
using Common;
using NetworkCommsDotNet.Connections;
using System;
using System.Threading;

namespace ServerApplication
{
    internal class ServerNetwork
    {
         private readonly Room _room;
        
       public ServerNetwork(ref Room room)
        {
            _room = room;
            NetworkComms.AppendGlobalConnectionEstablishHandler(OnConnectionEstablished);
            NetworkComms.AppendGlobalConnectionCloseHandler(OnConnectionClosed);
        }

        private void OnConnectionEstablished(Connection connection)
        {
            Console.WriteLine("Connection established! with '" + connection.ToString() + "'.");
            Thread.Sleep(100);
            var firstConnection = new Protocol {state = Protocol.State.Wait};
            connection.SendObject("Protocol", firstConnection);
            _room.AddPLayer(connection);
            _room.PrintPlayers();
        }

        private void OnConnectionClosed(Connection connection)
        {
            Console.WriteLine("Connection closed! with '" + connection.ToString() + "'.");
            _room.DelPlayer(connection);
            _room.PrintPlayers();
        }

        private void        ListenActivation()
        {
            Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0));
           
            Console.WriteLine("Server listening for TCP connection on:");
            var list = Connection.ExistingLocalListenEndPoints(ConnectionType.TCP);
            for (var index = 0; index < list.Count; index++)
            {
                var localEndPoint = (System.Net.IPEndPoint) list[index];
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);
            }
        }
        public void Run()
        {
            ListenActivation();
        }
    }
}
