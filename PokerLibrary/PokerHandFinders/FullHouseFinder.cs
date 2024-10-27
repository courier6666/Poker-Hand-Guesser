using System;
using System.Collections.Generic;
using System.Text;
using PokerLibrary.Enums;
using PokerLibrary.Structs;

namespace PokerLibrary.PokerHandCheckers
{
    public class FullHouseFinder : AbstractPokerHandFinder
    {
        public FullHouseFinder()
        {
            CombinationRank = HandCombinationRank.FullHouse;
        }
        public override bool ContainsHand(List<Card> cards)
        {
            cards = GetUniqueCards(cards);
            if (cards.Count < 5)
                return false;

            Dictionary<CardValue, int> cardValueOccurence = new Dictionary<CardValue, int>();

            foreach (var card in cards)
            {
                if (!cardValueOccurence.ContainsKey(card.Value))
                    cardValueOccurence.Add(card.Value, 0);

                ++cardValueOccurence[card.Value];
            }

            bool hasPair = false;
            bool hasThreeOfAKind = false;
            CardValue savedValue = CardValue.Two;

            foreach (var valueCount in cardValueOccurence)
            {

                if (valueCount.Value >= 3)
                {
                    if (hasThreeOfAKind)
                    {
                        if (savedValue < valueCount.Key)
                        {
                            savedValue = valueCount.Key;
                        }
                    }
                    else
                    {
                        hasThreeOfAKind = true;
                        savedValue = valueCount.Key;
                    }
                }
            }

            foreach (var valueCount in cardValueOccurence)
            {
                if (savedValue == valueCount.Key)
                    continue;

                if (valueCount.Value >= 2)
                {
                    hasPair = true;
                    continue;
                }
            }

            if (hasPair && hasThreeOfAKind)
                return true;

            return false;
        }

        public override List<Card> GetHand(List<Card> cards)
        {
            cards = GetUniqueCards(cards);
            if (!ContainsHand(cards))
                return null;

            if (cards.Count < 5) return null;

            List<Card> returnHand = new List<Card>();

            Dictionary<CardValue, List<Card>> cardValueOccurence = new Dictionary<CardValue, List<Card>>();

            foreach (var card in cards)
            {
                if (!cardValueOccurence.ContainsKey(card.Value))
                    cardValueOccurence.Add(card.Value, new List<Card>());

                cardValueOccurence[card.Value].Add(card);
            }

            CardValue greatestCardValueThreeOfAKind = 0;
            foreach (var cardValue in cardValueOccurence)
            {
                if (cardValue.Value.Count >= 3)
                {
                    if (greatestCardValueThreeOfAKind < cardValue.Key)
                    {
                        greatestCardValueThreeOfAKind = cardValue.Key;
                    }
                }
            }

            CardValue greatestCardValuePair = 0;

            foreach (var cardValue in cardValueOccurence)
            {
                if (cardValue.Value.Count >= 2 && cardValue.Key != greatestCardValueThreeOfAKind)
                {
                    if (greatestCardValuePair < cardValue.Key)
                    {
                        greatestCardValuePair = cardValue.Key;
                    }
                }
            }

            for (int i = 0; i < 3; ++i)
            {
                returnHand.Add(cardValueOccurence[greatestCardValueThreeOfAKind][i]);
            }
            for (int i = 0; i < 2; ++i)
            {
                returnHand.Add(cardValueOccurence[greatestCardValuePair][i]);
            }

            return returnHand;
        }
    }
}
