using System.Collections.Generic;
using System.Linq;

namespace ServerApplication
{
    public class PlayRule
    {
        private Card.CardTypes _trump;
        private List<Card> _board;
        private List<Card> _hand;
        
        private Card GetHighestCard(bool isTrump)
        {
            var tmp = new Card(Card.CardTypes.None, Card.CardValues.None, 0, 0);

            foreach (var it in _board)
            {
                if (isTrump)
                {
                    if (tmp.Trumpvalue >= it.Trumpvalue) continue;
                    tmp.Color = it.Color;
                    tmp.NonTrumpvalue = it.NonTrumpvalue;
                    tmp.Trumpvalue = it.Trumpvalue;
                    tmp.Value = it.Value;
                }
                else if (tmp.NonTrumpvalue < it.NonTrumpvalue)
                {
                    tmp.Color = it.Color;
                    tmp.NonTrumpvalue = it.NonTrumpvalue;
                    tmp.Trumpvalue = it.Trumpvalue;
                    tmp.Value = it.Value;
                }
            }
            return (tmp);
        }

        private bool IsHigher(bool isTrump, Card tmp, Card tmp2)
        {
            if (isTrump)
                return tmp.Trumpvalue > tmp2.Trumpvalue;
            return tmp.NonTrumpvalue > tmp2.NonTrumpvalue;
        }

        private bool HasHigherCard(bool isTrump, Card highestCard, Card.CardTypes type)
        {
            foreach (var it in _hand)
            {
                if (isTrump)
                {
                    if (it.Trumpvalue > highestCard.Trumpvalue && it.Color == type)
                        return true;
                }
                else if (it.NonTrumpvalue > highestCard.NonTrumpvalue && it.Color == type)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckSameColor(Card tmp)
        {
            var isTrump = (tmp.Color == _trump);
            var tmp2 = GetHighestCard(isTrump);

            if (IsHigher(isTrump, tmp, tmp2))
                return true;
            return !HasHigherCard(isTrump, tmp2, tmp.Color);
        }

        private bool HasColorInHand()
        {
            return _hand.Any(it => it.Color == _board[0].Color);
        }

        private bool HasTrumpInHand()
        {
            return _hand.Any(it => it.Color == _trump);
        }

        private bool CheckDiffColor(Card tmp)
        {
            if (HasColorInHand())
                return false;
            if (tmp.Color == _trump)
                return true;
            return !HasTrumpInHand();
        }

        private bool HandleMissPlay(Card tmp)
        {
            if (_board.Count == 0)
                return true;
            return tmp.Color == _board[0].Color ? CheckSameColor(tmp) : CheckDiffColor(tmp);
        }

        public bool PlayConfirmed(Card tmp, Card.CardTypes trump, List<Card> board, List<Card> hand)
        {
            _board = board;
            _hand = hand;
            _trump = trump;
            return HandleMissPlay(tmp);
        }
    }
}