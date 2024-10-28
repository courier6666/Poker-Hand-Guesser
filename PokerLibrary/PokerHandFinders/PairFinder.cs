using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerLibrary.Enums;
using PokerLibrary.Structs;

namespace PokerLibrary.PokerHandCheckers
{
    public class PairFinder : AbstractPokerHandFinder
    {
        public PairFinder()
        {
            CombinationRank = HandCombinationRank.Pair;
        }
        public override bool ContainsHand(List<Card> cards)
        {
            cards = GetUniqueCards(cards);
            if (cards.Count < 2)
                return false;

            Dictionary<CardValue, int> cardValueOccurence = new Dictionary<CardValue, int>();

            foreach (var card in cards)
            {
                if (!cardValueOccurence.ContainsKey(card.Value))
                    cardValueOccurence.Add(card.Value, 0);

                ++cardValueOccurence[card.Value];
            }

            foreach (var cardValue in cardValueOccurence)
            {
                if (cardValue.Value >= 2)
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

            Dictionary<CardValue, List<Card>> cardValueOccurence = new Dictionary<CardValue, List<Card>>();

            foreach (var card in cards)
            {
                if (!cardValueOccurence.ContainsKey(card.Value))
                    cardValueOccurence.Add(card.Value, new List<Card>());

                cardValueOccurence[card.Value].Add(card);
            }


            CardValue greatestCardValue = 0;
            foreach (var cardValue in cardValueOccurence)
            {
                if (cardValue.Value.Count >= 2)
                {
                    if (greatestCardValue < cardValue.Key)
                    {
                        greatestCardValue = cardValue.Key;
                    }
                }
            }
            List<Card> cardsCopy = new List<Card>(cards);
            cardsCopy.Sort(new CardValueComparer(-1));
            for (int i = 0; i < 2; ++i)
            {
                returnHand.Add(cardValueOccurence[greatestCardValue][i]);
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
