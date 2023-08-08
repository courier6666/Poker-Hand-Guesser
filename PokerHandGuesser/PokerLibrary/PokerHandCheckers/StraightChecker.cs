using PokerLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLibrary.PokerHandCheckers
{
    public class StraightChecker : AbstractPokerHandChecker
    {
        public StraightChecker()
        {
            this.CombinationRank = HandCombinationRank.Straight;
        }

        public override bool ContainsHand(List<Card> cards)
        {
            cards = this.GetUniqueCards(cards);
            if (cards.Count < 5)
                return false;

            Dictionary<CardValue, int> cardValueOccurence = new Dictionary<CardValue, int>();

            foreach (var card in cards)
            {
                if (!cardValueOccurence.ContainsKey(card.Value))
                    cardValueOccurence.Add(card.Value, 0);

                ++cardValueOccurence[card.Value];
            }

            for (CardValue valueBegin = CardValue.Ten; valueBegin != CardValue.King; valueBegin = (valueBegin == CardValue.Two) ? valueBegin = valueBegin = CardValue.Ace : --valueBegin)
            {
                List<CardValue> neededValuesForStraight = new List<CardValue>();
                for (int i = 0; i < 5; ++i)
                {
                    int cardValueMod = (((int)valueBegin) + i - 2) % 13 + 2;
                    neededValuesForStraight.Add((CardValue)(cardValueMod));
                }

                bool allCardsPresent = true;

                foreach (var cardValue in neededValuesForStraight)
                {
                    if (!cardValueOccurence.ContainsKey(cardValue))
                    {
                        allCardsPresent = false;
                        break;
                    }
                }

                if (allCardsPresent)
                    return true;

            }

            return false;
        }

        public override List<Card> GetHand(List<Card> cards)
        {
            cards = this.GetUniqueCards(cards);
            if (!this.ContainsHand(cards))
                return null;

            List<Card> returnHand = new List<Card>();

            Dictionary<CardValue, List<Card>> cardValueOccurence = new Dictionary<CardValue, List<Card>>();

            foreach (var card in cards)
            {
                if (!cardValueOccurence.ContainsKey(card.Value))
                    cardValueOccurence.Add(card.Value, new List<Card>());

                cardValueOccurence[card.Value].Add(card);
            }


            List<CardValue> bestNeededValuesForStraight = null;

            for (CardValue valueBegin = CardValue.Ten; valueBegin != CardValue.King; valueBegin = (valueBegin == CardValue.Two) ? valueBegin = valueBegin = CardValue.Ace : --valueBegin)
            {
                List<CardValue> neededValuesForStraight = new List<CardValue>();
                for (int i = 4; i >= 0; --i)
                {
                    int cardValueMod = (((int)valueBegin) + i - 2) % 13 + 2;
                    neededValuesForStraight.Add((CardValue)(cardValueMod));
                }

                bool allCardsPresent = true;

                foreach (var cardValue in neededValuesForStraight)
                {
                    if (!cardValueOccurence.ContainsKey(cardValue))
                    {
                        allCardsPresent = false;
                        break;
                    }
                }

                if (allCardsPresent)
                {
                    bestNeededValuesForStraight = neededValuesForStraight;
                    break;
                }
            }

            foreach (var cardValue in bestNeededValuesForStraight)
            {
                returnHand.Add(cardValueOccurence[cardValue][0]);
            }

            return returnHand;
        }
    }
}
