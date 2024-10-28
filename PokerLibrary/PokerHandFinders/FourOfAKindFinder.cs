using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerLibrary.Enums;
using PokerLibrary.Structs;

namespace PokerLibrary.PokerHandCheckers
{
    public class FourOfAKindFinder : AbstractPokerHandFinder
    {
        public FourOfAKindFinder()
        {
            CombinationRank = HandCombinationRank.FourOfAKind;
        }
        public override bool ContainsHand(List<Card> cards)
        {
            cards = GetUniqueCards(cards);
            if (cards.Count < 4)
                return false;

            Dictionary<CardValue, int> cardValueOccurence = new Dictionary<CardValue, int>();
            foreach (var card in cards)
            {
                if (!cardValueOccurence.ContainsKey(card.Value))
                    cardValueOccurence.Add(card.Value, 0);

                ++cardValueOccurence[card.Value];
            }

            foreach (var valueCount in cardValueOccurence)
            {
                if (valueCount.Value >= 4)
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

            List<Card> returnHand = new List<Card>();

            List<Card> cardsCopy = new List<Card>(cards);
            cardsCopy.Sort(new CardValueComparer(-1));

            Dictionary<CardValue, List<Card>> cardValueOccurence = new Dictionary<CardValue, List<Card>>();

            foreach (var card in cards)
            {
                if (!cardValueOccurence.ContainsKey(card.Value))
                    cardValueOccurence.Add(card.Value, new List<Card>());

                cardValueOccurence[card.Value].Add(card);
            }

            CardValue greatestCardValueFourOfAKind = 0;

            foreach (var cardValue in cardValueOccurence)
            {
                if (cardValue.Value.Count >= 4)
                {
                    if (greatestCardValueFourOfAKind < cardValue.Key)
                    {
                        greatestCardValueFourOfAKind = cardValue.Key;
                    }
                }
            }

            for (int i = 0; i < 4; ++i)
            {
                returnHand.Add(cardValueOccurence[greatestCardValueFourOfAKind][i]);
            }

            foreach (var card in cardsCopy)
            {
                if (!returnHand.Contains(card))
                    returnHand.Add(card);

                if (returnHand.Count >= 5)
                    break;
            }

            return returnHand.Any() ? returnHand : null;
        }
    }
}
