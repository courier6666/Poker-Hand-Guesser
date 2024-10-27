using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerLibrary.Enums;
using PokerLibrary.Structs;

namespace PokerLibraryTest
{
    public class CardBuilder
    {
        private Card card = new Card();
        public CardBuilder() { }
        public CardBuilder Hearts()
        {
            card.CardSuit = Suit.Hearts;
            return this;
        }
        public CardBuilder Diamonds()
        {
            card.CardSuit = Suit.Diamonds;
            return this;
        }
        public CardBuilder Spades()
        {
            card.CardSuit = Suit.Spades;
            return this;
        }
        public CardBuilder Clubs()
        {
            card.CardSuit = Suit.Clubs;
            return this;
        }
        public CardBuilder Two()
        {
            card.Value = CardValue.Two;
            return this;
        }
        public CardBuilder Three()
        {
            card.Value = CardValue.Three;
            return this;
        }
        public CardBuilder Four()
        {
            card.Value = CardValue.Four;
            return this;
        }
        public CardBuilder Five()
        {
            card.Value = CardValue.Five;
            return this;
        }
        public CardBuilder Six()
        {
            card.Value = CardValue.Six;
            return this;
        }
        public CardBuilder Seven()
        {
            card.Value = CardValue.Seven;
            return this;
        }
        public CardBuilder Eight()
        {
            card.Value = CardValue.Eight;
            return this;
        }
        public CardBuilder Nine()
        {
            card.Value = CardValue.Nine;
            return this;
        }
        public CardBuilder Ten()
        {
            card.Value = CardValue.Ten;
            return this;
        }
        public CardBuilder Jack()
        {
            card.Value = CardValue.Jack;
            return this;
        }
        public CardBuilder Queen()
        {
            card.Value = CardValue.Queen;
            return this;
        }
        public CardBuilder King()
        {
            card.Value = CardValue.King;
            return this;
        }
        public CardBuilder Ace()
        {
            card.Value = CardValue.Ace;
            return this;
        }
        public static implicit operator Card(CardBuilder cardBuilder) => cardBuilder.Build();
        public Card Build()
        {
            var resCard = this.card;
            this.card = new Card();
            return resCard;
        }
    }
}
