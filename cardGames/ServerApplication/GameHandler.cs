using Common;
using System.Collections.Generic;
using System.Threading;

namespace ServerApplication
{
    internal class GameHandler
    {
        private int _num;
        private int _scoreT1;
        private int _scoreT2;
        private int _index;
        private int _maxrange = 9;
        private int _count = 1;
        private int _nbPlay = 1;
        private int _turnDown = 1;
        private Card.CardTypes _trump;
        private readonly Dictionary<int, Card> _board;
        private readonly List<Card> _rulesCheck;
        private bool _contract;
        private bool _doColor;
        private readonly Deck _deck;
        private readonly List<Player> _playerList;
        private readonly ServerMsgHandler _handler;
        private readonly PlayRule _ruler;

        public GameHandler(List<Player> tmp)
        {
            _deck = new Deck();
            _ruler = new PlayRule();
            _rulesCheck = new List<Card>();
            _playerList = tmp;
            _handler = new ServerMsgHandler(_playerList, this);
            _board = new Dictionary<int, Card>();
        }

        public void Run()
        {
            var welcome = new Protocol { Msg = "Welcome in a room game !", state = Protocol.State.Wait };
            _handler.SendToAll(welcome);
            _deck.ShuffleTime();
            _deck.ShuffleTime();
            FirstDistribution();
            SendDistribution();
            const int i = 20;
            var firstCard = new Protocol
            {
                state = Protocol.State.Wait,
                Msg = "The card in the middle is " + _deck.ConvertCardValueIntToString(_deck._deck[i].Value)
                      + " Of " + _deck.ConvertCardTypeIntToString(_deck._deck[i].Color)
            };
            _handler.SendToAll(firstCard);
            Contract();
        }
        
        private void FirstDistribution()
        {
            var y = 0;
            foreach (var it in _playerList)
            {
                for (var i = y; i < y + 5; i++)
                    it.Hand.Add(_deck._deck[i]);
                y += 5;
            }
        }
        
        private void SendDistribution()
        {
            foreach (var it in _playerList)
            {
                foreach (var tmp in it.Hand)
                {
                    var toSend = new Protocol
                    {
                        state = Protocol.State.PrintHand,
                        Msg = _deck.ConvertCardValueIntToString(tmp.Value) + " Of " +
                              _deck.ConvertCardTypeIntToString(tmp.Color)
                    };
                    _handler.SendToOne(it.GetCon(), toSend);
                    Thread.Sleep(100);
                }
            }
        }

        private void Contract()
        {
            var player = new Protocol
            {
                Msg = "Player " + _playerList[_index].Name + " turn.",
                state = Protocol.State.Wait
            };
            _handler.SendToAll(player);
            var doTake = new Protocol
            {
                Msg = "Please choose Take or Pass.",
                state = Protocol.State.Play
            };
            _handler.SendToOne(_playerList[_index].GetCon(), doTake);
        }
        
        private void ContinuDistrib()
        {
            var y = 20;
            while (y < 32)
            {
                if (y % 3 == 0)
                    _index = _index + 1 > 3 ? 0 : _index + 1;
                _playerList[_index].Hand.Add(_deck._deck[y]);
                ++y;
            }
            PrintHand();
        }
        
        private void PrintHand()
        {
            foreach (var it in _playerList)
            {
                foreach (var tmp in it.Hand)
                {
                    var toSend = new Protocol
                    {
                        state = Protocol.State.PrintHand,
                        Msg = _deck.ConvertCardValueIntToString(tmp.Value) + " Of " +
                          _deck.ConvertCardTypeIntToString(tmp.Color)
                    };
                    _handler.SendToOne(it.GetCon(), toSend);
                    Thread.Sleep(100);
                }
            }
        }
        
        private void LoopGame()
        {
            var i = 1;
            _handler.SendToOne(_playerList[_index].GetCon(), new Protocol() { Msg = "" });
            foreach (var tmp in _playerList[_index].Hand)
            {
                var toSend = new Protocol
                {
                    state = Protocol.State.PrintHand,
                    Msg = i + " " + _deck.ConvertCardValueIntToString(tmp.Value) + " Of " +
                      _deck.ConvertCardTypeIntToString(tmp.Color)
                };
                ++i;
                _handler.SendToOne(_playerList[_index].GetCon(), toSend);
                Thread.Sleep(100);
            }
            var turn = new Protocol
            {
                Msg = "Please choose a card with his number.",
                state = Protocol.State.Play
            };
            _handler.SendToOne(_playerList[_index].GetCon(), turn);
        }

        private int GetTeamScore()
        {
            if (_index == 0 || _index == 2)
                return _scoreT1;
            else
                return _scoreT2;
        }

        private void CountTurn()
        {
            CalculScore();
            _nbPlay = 1;
            _maxrange -= 1;
            var turn = new Protocol();
            if (_turnDown <= 8)
                turn.Msg = "Turn " + (_turnDown - 1) + " done.\nPlayer " + (_index + 1) + " mark " + GetTeamScore() + "\nPlayer " + (_index + 1) + " start the round " + _turnDown;
            else
                turn.Msg = "Turn " + (_turnDown - 1) + " done.\nPlayer " + (_index + 1) + " mark " + GetTeamScore();
            turn.state = Protocol.State.Wait;
            _handler.SendToAll(turn);
            LoopGame();
        }
        
        private void EndofGame()
        {
            CountTurn();
            var end = new Protocol
            {
                Msg = _scoreT1 > _scoreT2
                    ? "\nPlayer 1 with player 3 won the game !\nYou will be disconnected"
                    : "\nPlayer 2 with player 4 won the game !\nYou will be disconnected"
            };
            _handler.SendToAll(end);
            var disconnect = new Protocol {state = Protocol.State.Disconnect};
            _handler.SendToAll(disconnect);
        }
        
        private void CalculScore()
        {
            var isTrumpPlayed = false;

            foreach (var it in _board)
            {
                if (it.Value.Color == _trump)
                    isTrumpPlayed = true;
            }

            var maxCard = 0;
            foreach (var it in _board)
            {
               if (isTrumpPlayed)
                {
                    if (it.Value.Color != _trump || it.Value.Trumpvalue <= maxCard) continue;
                    maxCard = it.Value.Trumpvalue;
                    _index = it.Key;
                }
               else
                {
                    if (it.Value.Color == _trump || it.Value.NonTrumpvalue <= maxCard) continue;
                    maxCard = it.Value.NonTrumpvalue;
                    _index = it.Key;
                }
            }
            
            foreach (var it in _board)
            {
                if (_index == 0 || _index == 2)
                {
                    if (it.Value.Color == _trump)
                        _scoreT1 += it.Value.Trumpvalue;
                    else
                        _scoreT1 += it.Value.NonTrumpvalue;
                }
                else
                {
                    if (it.Value.Color == _trump)
                        _scoreT2 += it.Value.Trumpvalue;
                    else
                        _scoreT2 += it.Value.NonTrumpvalue;
                }
            }
            _board.Clear();
            _rulesCheck.Clear();
        }

        public void Treatmsg(Protocol tmp)
        {
            if (_contract)
            {
                try
                {
                    _num = int.Parse(tmp.Msg);
                }
                catch
                {
                    var range = new Protocol
                    {
                        Msg = "Please choose a number in range",
                        state = Protocol.State.Play
                    };
                    _handler.SendToOne(_playerList[_index].GetCon(), range);
                    return;
                }
                if (_num > 0 && _num < _maxrange)
                {
                    if (!_ruler.PlayConfirmed(_playerList[_index].Hand[_num -1],
                        _trump, _rulesCheck, _playerList[_index].Hand))
                    {
                        _handler.SendToOne(_playerList[_index].GetCon(),
                            new Protocol(){Msg = "You can't play this one please respect the belotte rules !", state = Protocol.State.Play});
                        return;
                    }
                    _nbPlay += 1;
                    var card = new Protocol
                    {
                        Msg = "Player " + (_index + 1) + " play " + _playerList[_index].Hand[_num - 1].Value + " Of " +
                              _playerList[_index].Hand[_num - 1].Color
                    };
                    _handler.SendToAll(card);
                    _board.Add(_index, _playerList[_index].Hand[_num - 1]);
                    _rulesCheck.Add(_playerList[_index].Hand[_num - 1]);
                    _playerList[_index].Hand.RemoveAt(_num - 1);
                    _index = _index + 1 > 3 ? 0 : _index + 1;
                    if (_nbPlay == 5)
                    {
                        _turnDown += 1;
                        if (_turnDown == 9)
                            EndofGame();
                        CountTurn();
                        return;
                    }
                    LoopGame();
                }
                else
                {
                    var range = new Protocol
                    {
                        Msg = "Please choose a number in range",
                        state = Protocol.State.Play
                    };
                    _handler.SendToOne(_playerList[_index].GetCon(), range);
                }
            }
            else if (!_contract)
            {
                if (tmp.Msg.Equals("Pass"))
                {
                    var pass = new Protocol {Msg = "Player " + (_index + 1) + " pass."};
                    _index = _index + 1 > 3 ? 0 : _index + 1;
                    _count += 1;
                    _handler.SendToAll(pass);
                    switch (_count)
                    {
                        case 5:
                            var second = new Protocol();
                            second.Msg = "Second contract, distribution of the deck in progress ...";
                            _handler.SendToAll(second);
                            ContinuDistrib();
                            break;
                        case 9:
                            ReDistribution();
                            return;
                    }
                    Contract();
                }
                else if (_doColor && tmp.Msg.Equals("Spade") || tmp.Msg.Equals("Heart") || tmp.Msg.Equals("Club") || tmp.Msg.Equals("Diamond"))
                {
                    _contract = true;
                    _trump = _deck.ConvertStringToCardType(tmp.Msg);
                    var trump = new Protocol
                    {
                        Msg = "Player " + (_index + 1) + " choose " + _trump + " as trump color\nPlayer " + (_index + 1) + " start the round " + _turnDown,
                        state = Protocol.State.Wait,
                    };
                    _handler.SendToAll(trump);
                    LoopGame();
                }
                else if (tmp.Msg.Equals("Take") && !_doColor)
                {
                    if (_count < 5)
                    {
                        _contract = true;
                        _trump = _deck._deck[20].Color;
                        var trump = new Protocol
                        {
                            Msg = "Player " + (_index + 1) + " take the card. The color as trump is " + _trump,
                            state = Protocol.State.Wait,
                        };
                        _handler.SendToAll(trump);
                        ContinuDistrib();
                        var game = new Protocol
                        {
                            Msg = "Player " + (_index + 1) + " start the round " + _turnDown,
                            state = Protocol.State.Wait
                        };
                        _handler.SendToAll(game);
                        LoopGame();
                    }
                    else if (_count >= 5)
                    {
                        WhatColor();
                        _doColor = true;
                    }
                }
                else
                {
                    _doColor = false;
                    var doTake = new Protocol
                    {
                        Msg = "Please choose Take or Pass.",
                        state = Protocol.State.Play
                    };
                    _handler.SendToOne(_playerList[_index].GetCon(), doTake);
                }
            }
        }
        
        private void WhatColor()
        {
            var color = new Protocol
            {
                Msg = "Please choose a color [Diamond] [Spade] [Heart] [Club]",
                state = Protocol.State.Play,
            };
            _handler.SendToOne(_playerList[_index].GetCon(), color);
        }
        
        private void ReDistribution()
        {
            _deck.ShuffleTime();
            _deck.ShuffleTime();
            _deck.ShuffleTime();
            var reDistrib = new Protocol
            {
                Msg = "Redistribution in progress ...",
                state = Protocol.State.Wait
            };
            _handler.SendToAll(reDistrib);
            var y = 0;
            foreach (var it in _playerList)
            {
                it.Hand.Clear();
                for (var j = y; j < y + 5; j++)
                    it.Hand.Add(_deck._deck[j]);
                y += 5;
            }
            PrintHand();
            const int i = 20;
            var firstCard = new Protocol
            {
                state = Protocol.State.Wait,
                Msg = "The card in the middle is " + _deck.ConvertCardValueIntToString(_deck._deck[i].Value)
                      + " Of " + _deck.ConvertCardTypeIntToString(_deck._deck[i].Color)
            };
            _handler.SendToAll(firstCard);
            _index = 0;
            _count = 1;
            Contract();
        }
    }
}
    