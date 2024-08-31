using System;
using System.Collections.Generic;
using System.Text;
using PokerHadChecker.Enums;

namespace PokerHadChecker.Structs
{
    public struct Card
    {
        public static Card EmptyCard => new Card() { CardSuit = 0, Value = 0 };
        public Card(Suit suit, CardValue value)
        {
            CardSuit = suit;
            Value = value;
        }
        public Suit CardSuit { get; set; }
        public CardValue Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is Card card)
                return card.CardSuit == CardSuit && card.Value == Value;

            return false;
        }

        public override int GetHashCode()
        {
            return CardSuit.GetHashCode() ^ Value.GetHashCode();
        }
    }
}
