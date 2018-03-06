using System;
using ServerApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class PlayerRuleTest
    {
        private PlayRule _rule = new PlayRule();
        private List<Card> _hand = new List<Card>();
        private List<Card> _board = new List<Card>();

        [TestMethod]
        public void PlayConfirmed_Test1()
        {
            if (_hand.Count > 0)
                _hand.Clear();
            if (_board.Count > 0)
                _board.Clear();

            var roundTrump = Card.CardTypes.Heart;
            var cardPlayerWantToPlay = new Card(Card.CardTypes.Club, Card.CardValues.As, 11, 11);
            _hand.Add(new Card(Card.CardTypes.Club, Card.CardValues.As, 11, 11));
            _hand.Add(new Card(Card.CardTypes.Club, Card.CardValues.King, 4, 10));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Queen, 3, 3));
            _hand.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Seven, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.Nine, 14, 0));

            var result = _rule.PlayConfirmed(cardPlayerWantToPlay, roundTrump, _board, _hand);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void PlayConfirmed_Test2()
        {
            if (_hand.Count > 0)
                _hand.Clear();
            if (_board.Count > 0)
                _board.Clear();

            var roundTrump = Card.CardTypes.Heart;
            var cardPlayerWantToPlay = new Card(Card.CardTypes.Heart, Card.CardValues.As, 11, 11);

            _hand.Add(new Card(Card.CardTypes.Heart, Card.CardValues.As, 11, 11));
            _hand.Add(new Card(Card.CardTypes.Club, Card.CardValues.King, 4, 10));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Queen, 3, 3));
            _hand.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Seven, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.Nine, 14, 0));

            _board.Add(new Card(Card.CardTypes.Club, Card.CardValues.Seven, 0, 0));
            _board.Add(new Card(Card.CardTypes.Club, Card.CardValues.Nine, 14, 0));

            var result = _rule.PlayConfirmed(cardPlayerWantToPlay, roundTrump, _board, _hand);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void PlayConfirmed_Test3()
        {
            if (_hand.Count > 0)
                _hand.Clear();
            if (_board.Count > 0)
                _board.Clear();

            var roundTrump = Card.CardTypes.Heart;
            var cardPlayerWantToPlay = new Card(Card.CardTypes.Heart, Card.CardValues.As, 11, 11);

            _hand.Add(new Card(Card.CardTypes.Heart, Card.CardValues.As, 11, 11));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.King, 4, 10));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Queen, 3, 3));
            _hand.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Seven, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.Nine, 14, 0));

            _board.Add(new Card(Card.CardTypes.Club, Card.CardValues.Seven, 0, 0));
            _board.Add(new Card(Card.CardTypes.Club, Card.CardValues.Nine, 14, 0));

            var result = _rule.PlayConfirmed(cardPlayerWantToPlay, roundTrump, _board, _hand);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void PlayConfirmed_Test4()
        {
            if (_hand.Count > 0)
                _hand.Clear();
            if (_board.Count > 0)
                _board.Clear();

            var roundTrump = Card.CardTypes.Heart;
            var cardPlayerWantToPlay = new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0);

            _hand.Add(new Card(Card.CardTypes.Heart, Card.CardValues.As, 11, 11));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.King, 4, 10));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Queen, 3, 3));
            _hand.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Seven, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.Nine, 14, 0));

            _board.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Seven, 0, 0));
            _board.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Nine, 14, 0));

            var result = _rule.PlayConfirmed(cardPlayerWantToPlay, roundTrump, _board, _hand);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void PlayConfirmed_Test5()
        {
            if (_hand.Count > 0)
                _hand.Clear();
            if (_board.Count > 0)
                _board.Clear();

            var roundTrump = Card.CardTypes.Heart;
            var cardPlayerWantToPlay = new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0);

            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.As, 11, 11));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.King, 4, 10));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Queen, 3, 3));
            _hand.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Seven, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.Nine, 14, 0));

            _board.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Seven, 0, 0));
            _board.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Nine, 14, 0));

            var result = _rule.PlayConfirmed(cardPlayerWantToPlay, roundTrump, _board, _hand);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void PlayConfirmed_Test6()
        {
            if (_hand.Count > 0)
                _hand.Clear();
            if (_board.Count > 0)
                _board.Clear();

            var roundTrump = Card.CardTypes.Spade;
            var cardPlayerWantToPlay = new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0);

            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.As, 11, 11));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.King, 4, 10));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Queen, 3, 3));
            _hand.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Seven, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.Nine, 14, 0));

            _board.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Seven, 0, 0));
            _board.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Nine, 14, 0));
            _board.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Nine, 14, 0));

            var result = _rule.PlayConfirmed(cardPlayerWantToPlay, roundTrump, _board, _hand);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void PlayConfirmed_Test7()
        {
            if (_hand.Count > 0)
                _hand.Clear();
            if (_board.Count > 0)
                _board.Clear();

            var roundTrump = Card.CardTypes.Spade;
            var cardPlayerWantToPlay = new Card(Card.CardTypes.Diamond, Card.CardValues.King, 4, 10);

            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.As, 11, 11));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.King, 4, 10));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Queen, 3, 3));
            _hand.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Spade, Card.CardValues.Seven, 0, 0));

            _board.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.Eight, 0, 0));
            _board.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.Nine, 14, 0));
            _board.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.Ten, 10, 10));

            var result = _rule.PlayConfirmed(cardPlayerWantToPlay, roundTrump, _board, _hand);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void PlayConfirmed_Test8()
        {
            if (_hand.Count > 0)
                _hand.Clear();
            if (_board.Count > 0)
                _board.Clear();

            var roundTrump = Card.CardTypes.Spade;
            var cardPlayerWantToPlay = new Card(Card.CardTypes.Club, Card.CardValues.Seven, 0, 0);

            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.As, 11, 11));
            _hand.Add(new Card(Card.CardTypes.Diamond, Card.CardValues.King, 4, 10));
            _hand.Add(new Card(Card.CardTypes.Club, Card.CardValues.Queen, 3, 3));
            _hand.Add(new Card(Card.CardTypes.Club, Card.CardValues.Eight, 0, 0));
            _hand.Add(new Card(Card.CardTypes.Club, Card.CardValues.Seven, 0, 0));

            _board.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Eight, 0, 0));
            _board.Add(new Card(Card.CardTypes.Heart, Card.CardValues.Nine, 14, 0));

            var result = _rule.PlayConfirmed(cardPlayerWantToPlay, roundTrump, _board, _hand);
            Assert.AreEqual(true, result);
        }
    }
}
