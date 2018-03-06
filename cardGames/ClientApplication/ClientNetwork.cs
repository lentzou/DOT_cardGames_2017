using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using System;

namespace ClientApplication
{
    internal class ClientNetwork
    {
        private readonly string _serverIp;
        private readonly int _serverPort;
        private ConnectionInfo _conInfo;
        private Connection _tcPcon;
        public ClientNetwork(string ip, int port)
        {
            _serverIp = ip;
            _serverPort = port;
            NetworkComms.AppendGlobalConnectionEstablishHandler(OnConnectionEstablished);
            NetworkComms.AppendGlobalConnectionCloseHandler(OnConnectionClosed);
        }

        public Connection GetTcPcon()
        {
            return _tcPcon;
        }
        private void OnConnectionEstablished(Connection connection)
        {
            Console.WriteLine("Welcome in Coinche.Net Game Made by Lentzou & Bibz");
        }

        private void OnConnectionClosed(Connection connection)
        {
            Console.WriteLine("You have been disconnected from the server");
        }

        private void SetConnection()
        {
            _conInfo = new ConnectionInfo(_serverIp, _serverPort);
            _tcPcon = TCPConnection.GetConnection(_conInfo);
        }
        public void Run()
        {
            SetConnection();
        }
    }
}
