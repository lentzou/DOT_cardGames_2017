using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ServerApplication
{
    internal class Room
    {
        private readonly List<Player> _playersList = new List<Player>();

        public void AddPLayer(Connection player)
        {
            var tmp = new Player(player, _playersList.Count);
            _playersList.Add(tmp);
            StartGame();
        }

        public void DelPlayer(Connection player)
        {
            foreach (var it in _playersList)
                if (player == it.GetCon())
                {
                    _playersList.Remove(it);
                    break;
                }
        }

        public void PrintPlayers()
        {
            var nb = 0;
            foreach (var it in _playersList)
                ++nb;
            Console.WriteLine("Il y a actuellement : " + nb + " players");
        }

        private void StartGame()
        {
            var nb = 0;
            foreach (var it in _playersList)
                ++nb;
            if (nb != 4) return;
            var T = new Thread(InitThread);
            T.Start();
            Thread.Sleep(1000);
            _playersList.Clear();
        }
        private void InitThread()
        {
            var players = new List<Player>();
            players.AddRange(_playersList);
            var game = new GameHandler(players);
            game.Run();
        }
    }
}
