using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLibrary
{
    public enum Suit
    {
        Clubs = 1,
        Hearts = 2,
        Spades = 3,
        Diamonds = 4
    }
    public enum CardValue
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }
    public class Card
    {
        Suit suit;
        CardValue value;
        private Card()
        {

        }
        public Card(Suit suit, CardValue value)
        {
            this.suit = suit;
            this.value = value;
        }
        public Suit CardSuit
        {
            get
            {
                return this.suit;
            }
            set
            {
                this.suit = value;
            }
        }
        public CardValue Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}
