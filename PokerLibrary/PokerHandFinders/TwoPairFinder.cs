using System;
using System.Collections.Generic;
using System.Text;
using PokerLibrary.Enums;
using PokerLibrary.Structs;

namespace PokerLibrary.PokerHandCheckers
{
    public class TwoPairFinder : AbstractPokerHandFinder
    {
        public TwoPairFinder()
        {
            CombinationRank = HandCombinationRank.TwoPair;
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

            bool hasPair = false;

            foreach (var cardValue in cardValueOccurence)
            {
                if (cardValue.Value >= 2)
                {
                    if (hasPair)
                        return true;

                    hasPair = true;
                }
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


            CardValue greatestCardValueFirstPair = 0;
            foreach (var cardValue in cardValueOccurence)
            {
                if (cardValue.Value.Count >= 2)
                {
                    if (greatestCardValueFirstPair < cardValue.Key)
                    {
                        greatestCardValueFirstPair = cardValue.Key;
                    }
                }
            }

            CardValue greatestCardValueSecondPair = 0;

            foreach (var cardValue in cardValueOccurence)
            {
                if (cardValue.Value.Count >= 2 && cardValue.Key != greatestCardValueFirstPair)
                {
                    if (greatestCardValueSecondPair < cardValue.Key)
                    {
                        greatestCardValueSecondPair = cardValue.Key;
                    }
                }
            }

            for (int i = 0; i < 2; ++i)
            {
                returnHand.Add(cardValueOccurence[greatestCardValueFirstPair][i]);
            }
            for (int i = 0; i < 2; ++i)
            {
                returnHand.Add(cardValueOccurence[greatestCardValueSecondPair][i]);
            }

            foreach (var card in cardsCopy)
            {
                if (!returnHand.Contains(card))
                {
                    returnHand.Add(card);
                    break;
                }
            }

            return returnHand;
        }
    }
}
