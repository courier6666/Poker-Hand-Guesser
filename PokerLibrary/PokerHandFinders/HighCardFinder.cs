using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerLibrary.Enums;
using PokerLibrary.Structs;

namespace PokerLibrary.PokerHandCheckers
{
    public class HighCardFinder : AbstractPokerHandFinder
    {
        public HighCardFinder()
        {
            CombinationRank = HandCombinationRank.HighCard;
        }
        public override bool ContainsHand(List<Card> cards)
        {
            cards = GetUniqueCards(cards);
            if (cards.Count < 1)
                return false;

            return true;
        }

        public override List<Card> GetHand(List<Card> cards)
        {
            cards = GetUniqueCards(cards);
            if (!ContainsHand(cards))
                return null;

            if (cards.Count < 5) return null;

            List<Card> returnHand = new List<Card>();

            List<Card> cardsCopy = new List<Card>(cards);
            cardsCopy.Sort(new CardValueComparer(-1));

            returnHand = cardsCopy.GetRange(0, 5);
            return returnHand.Any() ? returnHand : null;
        }
    }
}
