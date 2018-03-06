using NetworkCommsDotNet.Connections;
using System.Collections.Generic;

namespace ServerApplication
{
    internal class Player
    {
        private readonly Connection _client;
        public readonly int Name;
        public readonly List<Card> Hand = new List<Card>();

        public Connection GetCon()
        {
            return _client;
        }

        public Player(Connection player, int name)
        {
            _client = player;
            Name = name + 1;
        }
    }
}
