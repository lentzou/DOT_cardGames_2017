namespace ServerApplication
{
    public class Card
    {
        public enum CardValues{ As = 0, King, Queen, Jack, Ten, Nine, Eight, Seven, None };
        public enum CardTypes { Club = 0, Diamond, Spade, Heart, None };
        public CardTypes Color;
        public CardValues Value;
        public int Trumpvalue;
        public int NonTrumpvalue;

        public Card(CardTypes color, CardValues value, int trumpvalue, int nonTrumpvalue)
        {
            Color = color;
            Value = value;
            Trumpvalue = trumpvalue;
            NonTrumpvalue = nonTrumpvalue;
        }
    }
}
