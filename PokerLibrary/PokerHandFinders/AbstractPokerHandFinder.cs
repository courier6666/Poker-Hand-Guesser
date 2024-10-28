using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerLibrary.Enums;
using PokerLibrary.Structs;

namespace PokerLibrary.PokerHandCheckers
{

    public abstract class AbstractPokerHandFinder
    {
        public HandCombinationRank CombinationRank { get; protected set; }
        /// <summary>
        /// Checks if hand contains combination.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public abstract bool ContainsHand(List<Card> cards);
        /// <summary>
        /// Gets hand combination that can be found, needs at least 5 cards to get hand. Order of cards by value in resulted hand is important!
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public abstract List<Card> GetHand(List<Card> cards);
        protected List<Card> GetUniqueCards(List<Card> cards)
        {
            return new HashSet<Card>(cards).ToList();
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
                sortOrder = order;
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
