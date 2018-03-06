using Common;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerApplication
{
    internal class ServerMsgHandler
    {
        private readonly GameHandler _game;    
        private readonly List<Player> _playerList;

        public ServerMsgHandler(List<Player> playerlist, GameHandler game)
        {
            _game = game;
            _playerList = playerlist;
            NetworkComms.AppendGlobalIncomingPacketHandler<Protocol>("Protocol", TreatPacketFromClient);
        }

        private bool ClientIsInMyRoom(Connection connection)
        {
            return _playerList.Any(it => it.GetCon() == connection);
        }

        private void TreatPacketFromClient(PacketHeader header, Connection connection, Protocol tmp)
        {
            if (!ClientIsInMyRoom(connection)) return;
            Console.WriteLine(tmp.Msg);
            _game.Treatmsg(tmp);
        }

        public void SendToAll(Protocol toSend)
        {
            foreach (var it in _playerList)
            {
                it.GetCon().SendObject("Protocol", toSend);
            }
        }
        public void SendToOne(Connection client, Protocol toSend)
        {
            client.SendObject("Protocol", toSend);
        }
    }
}
    