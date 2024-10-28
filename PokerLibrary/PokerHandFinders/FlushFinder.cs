using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerLibrary.Enums;
using PokerLibrary.Structs;

namespace PokerLibrary.PokerHandCheckers
{
    public class FlushFinder : AbstractPokerHandFinder
    {
        public FlushFinder()
        {
            CombinationRank = HandCombinationRank.Flush;
        }
        public override bool ContainsHand(List<Card> cards)
        {
            cards = GetUniqueCards(cards);
            if (cards.Count < 5)
                return false;

            Dictionary<Suit, int> cardSuitOccurence = new Dictionary<Suit, int>();
            foreach (var card in cards)
            {
                if (!cardSuitOccurence.ContainsKey(card.CardSuit))
                    cardSuitOccurence.Add(card.CardSuit, 0);

                ++cardSuitOccurence[card.CardSuit];
            }
            foreach (var suitCount in cardSuitOccurence)
            {
                if (suitCount.Value >= 5)
                    return true;
            }

            return false;
        }

        public override List<Card> GetHand(List<Card> cards)
        {
            cards = GetUniqueCards(cards);
            if (!ContainsHand(cards))
                return null;

            if (cards.Count < 5) return null;

            List<Card> returnHand = null;

            Dictionary<Suit, List<Card>> cardSuitOccurrence = new Dictionary<Suit, List<Card>>();
            foreach (var card in cards)
            {
                if (!cardSuitOccurrence.ContainsKey(card.CardSuit))
                    cardSuitOccurrence.Add(card.CardSuit, new List<Card>());

                cardSuitOccurrence[card.CardSuit].Add(card);
            }

            foreach (var cardsToSuit in cardSuitOccurrence)
            {
                cardsToSuit.Value.Sort(new CardValueComparer(-1));

                if (cardsToSuit.Value.Count < 5)
                    continue;

                if (returnHand == null)
                {
                    returnHand = cardsToSuit.Value.GetRange(0, 5);
                }
                else
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        if (returnHand[i].Value > cardsToSuit.Value[i].Value)
                            break;
                        else if (returnHand[i].Value < cardsToSuit.Value[i].Value)
                        {
                            returnHand = cardsToSuit.Value.GetRange(0, 5);
                            break;
                        }
                    }
                }
            }

            return returnHand.Any() ? returnHand : null;
        }
    }
}
