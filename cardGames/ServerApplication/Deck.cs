using System;
using System.Collections.Generic;

namespace ServerApplication
{
    internal class Deck
    {
        public readonly List<Card> _deck = new List<Card>();

        private readonly Dictionary<Card.CardValues, int> _trumpValue = new Dictionary<Card.CardValues, int>();
        private readonly Dictionary<Card.CardValues, int> _nonTrumpValue = new Dictionary<Card.CardValues, int>();

        public Deck()
        {
            _trumpValue.Add(Card.CardValues.As, 11);
            _trumpValue.Add(Card.CardValues.King, 4);
            _trumpValue.Add(Card.CardValues.Queen, 3);
            _trumpValue.Add(Card.CardValues.Jack, 20);
            _trumpValue.Add(Card.CardValues.Ten, 10);
            _trumpValue.Add(Card.CardValues.Nine, 14);
            _trumpValue.Add(Card.CardValues.Eight, 0);
            _trumpValue.Add(Card.CardValues.Seven, 0);
            _nonTrumpValue.Add(Card.CardValues.As, 11);
            _nonTrumpValue.Add(Card.CardValues.King, 4);
            _nonTrumpValue.Add(Card.CardValues.Queen, 3);
            _nonTrumpValue.Add(Card.CardValues.Jack, 2);
            _nonTrumpValue.Add(Card.CardValues.Ten, 10);
            _nonTrumpValue.Add(Card.CardValues.Nine, 0);
            _nonTrumpValue.Add(Card.CardValues.Eight, 0);
            _nonTrumpValue.Add(Card.CardValues.Seven, 0);
            FillDeck();
        }

        public string ConvertCardValueIntToString(Card.CardValues cardValue)
        {
            switch (cardValue)
            {
                case Card.CardValues.As:
                    return "As";
                case Card.CardValues.King:
                    return "King";
                case Card.CardValues.Queen:
                    return "Queen";
                case Card.CardValues.Jack:
                    return "Jack";
                case Card.CardValues.Ten:
                    return "Ten";
                case Card.CardValues.Nine:
                    return "Nine";
                case Card.CardValues.Eight:
                    return "Eight";
                case Card.CardValues.Seven:
                    return "Seven";
                default:
                    return "NONE";
            }
        }
        public string ConvertCardTypeIntToString(Card.CardTypes cardType)
        {
            switch (cardType)
            {
                case Card.CardTypes.Club:
                    return "Club";
                case Card.CardTypes.Diamond:
                    return "Diamond";
                case Card.CardTypes.Spade:
                    return "Spade";
                case Card.CardTypes.Heart:
                    return "Heart";
                default:
                    return "UNKNOWN";
            }
        }
            public Card.CardTypes ConvertStringToCardType(string tmp)
            {
            switch (tmp)
            {
                case "Club":
                    return Card.CardTypes.Club;
                case "Diamond":
                    return Card.CardTypes.Diamond;
                case "Spade":
                    return Card.CardTypes.Spade;
                case "Heart":
                    return Card.CardTypes.Heart;
                default:
                    return Card.CardTypes.None;
            }
        }
        public void PrintDeck()
        {
            var nb = 0;
            foreach (var it in _deck)
            {
                ++nb;
                Console.WriteLine(nb + " : " + ConvertCardValueIntToString(it.Value) 
                    + " Of " + ConvertCardTypeIntToString(it.Color));
            }
        }

        public void ShuffleTime()
        {
            RandomiseList.Shuffle(_deck);
        }

        private void FillDeck()
        {
            var cardtype = Card.CardTypes.Club;
            Card.CardValues cardvalue;

            while (cardtype != Card.CardTypes.Heart + 1)
            {
                cardvalue = Card.CardValues.As;
                while (cardvalue != Card.CardValues.Seven + 1)
                {
                    var tmp = new Card(cardtype, cardvalue, _trumpValue[cardvalue], _nonTrumpValue[cardvalue]);
                    _deck.Add(tmp);
                    ++cardvalue;
                }
                ++cardtype;
            }
        }
    }
}
