using System;
using System.Collections.Generic;
using System.Text;
using PokerLibrary.Enums;

namespace PokerLibrary.Structs
{
    public struct Card : IComparable<Card>
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

        public int CompareTo(Card other)
        {
            return other.Value.CompareTo(this.Value);
        }
    }
}
