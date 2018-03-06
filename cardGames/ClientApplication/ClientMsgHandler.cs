using Common;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;

namespace ClientApplication
{
    internal class ClientMsgHandler
    {
        private Protocol _tmp = new Protocol();
        private readonly Connection _tcPcon;
        public ClientMsgHandler(Connection connection)
        {
            _tcPcon = connection;
            NetworkComms.AppendGlobalIncomingPacketHandler<Protocol>("Protocol", TreatMsgFromServer);
        }

        private void TreatMsgFromServer(PacketHeader header, Connection connection, Protocol tmp)
        {
            _tmp = tmp;
            switch (tmp.state)
            {
                case Protocol.State.Wait: Console.WriteLine(tmp.Msg); return;
                case Protocol.State.Play: Console.WriteLine(tmp.Msg); return;
                case Protocol.State.PrintHand: Console.WriteLine(tmp.Msg); return;
                case Protocol.State.Disconnect: NetworkComms.Shutdown(); return;
                default: return;
            }
        }
        public void TreatInputFromUser(string input)
        {
            var send = new Protocol {Msg = input};

            switch (_tmp.state)
            {
                case Protocol.State.Wait: Console.WriteLine("You must wait to play :)"); break;
                case Protocol.State.Play: _tcPcon.SendObject("Protocol", send); break;
                case Protocol.State.PrintHand: break;
            }
        }
    }
}
