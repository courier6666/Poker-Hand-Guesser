using PokerLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLibrary.PokerHandCheckers
{
    public class HighCardChecker : AbstractPokerHandChecker
    {
        public HighCardChecker()
        {
            this.CombinationRank = HandCombinationRank.HighCard;
        }
        public override bool ContainsHand(List<Card> cards)
        {
            cards = this.GetUniqueCards(cards);
            if (cards.Count < 1)
                return false;

            return true;
        }

        public override List<Card> GetHand(List<Card> cards)
        {
            cards = this.GetUniqueCards(cards);
            if (!this.ContainsHand(cards))
                return null;

            List<Card> returnHand = new List<Card>();

            List<Card> cardsCopy = new List<Card>(cards);
            cardsCopy.Sort(new CardValueComparer(-1));

            returnHand = cardsCopy.GetRange(0, 5);
            return returnHand;
        }
    }
}
