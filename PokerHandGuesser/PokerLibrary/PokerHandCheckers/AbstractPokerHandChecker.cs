using PokerLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLibrary.PokerHandCheckers
{
    public enum HandCombinationRank
    {
        RoyalFlush = 1,
        StraightFlush = 2,
        FourOfAKind = 3,
        FullHouse = 4,
        Flush = 5,
        Straight = 6,
        ThreeOfAKind = 7,
        TwoPair = 8,
        Pair = 9,
        HighCard = 10

    }
    public abstract class AbstractPokerHandChecker
    {
        public HandCombinationRank CombinationRank { get; protected set; }

        public abstract bool ContainsHand(List<Card> cards);
        public abstract List<Card> GetHand(List<Card> cards);
        protected List<Card> GetUniqueCards(List<Card> cards)
        {
            List<Card> uniqueCards = new List<Card>();

            var cardsSet = new Dictionary<Suit, HashSet<CardValue>>();
            cardsSet.Add(Suit.Clubs, new HashSet<CardValue>());
            cardsSet.Add(Suit.Diamonds, new HashSet<CardValue>());
            cardsSet.Add(Suit.Hearts, new HashSet<CardValue>());
            cardsSet.Add(Suit.Spades, new HashSet<CardValue>());

            foreach (var card in cards)
            {
                if (cardsSet[card.CardSuit].Add(card.Value))
                {
                    uniqueCards.Add(card);
                }
            }
            return uniqueCards;
        }
        protected class CardValueComparer : IComparer<Card>
        {
            private int sortOrder;
            public CardValueComparer(int order = 1)
            {
                sortOrder = order;
            }
            private CardValueComparer()
            {

            }
            public int Compare(Card a, Card b)
            {
                return (a.Value - b.Value) * sortOrder;
            }
        }
        protected class CardComparer : IComparer<Card>
        {
            private int sortOrder;
            public CardComparer(int order = 1)
            {
                this.sortOrder = order;
            }
            public int Compare(Card a, Card b)
            {
                if (a.CardSuit == b.CardSuit)
                {
                    return (a.Value - b.Value) * sortOrder;
                }

                return (a.CardSuit - b.CardSuit) * sortOrder;
            }
        }
    }
}
