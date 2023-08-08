using PokerLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLibrary.PokerHandCheckers
{
    public class RoyalFlushChecker : AbstractPokerHandChecker
    {
        public RoyalFlushChecker()
        {
            this.CombinationRank = HandCombinationRank.RoyalFlush;
        }
        public override bool ContainsHand(List<Card> cards)
        {
            cards = this.GetUniqueCards(cards);
            if (cards.Count < 5)
                return false;

            var suitToCardValueOccurrence = new Dictionary<Suit, Dictionary<CardValue, int>>();


            foreach (var card in cards)
            {
                if (!suitToCardValueOccurrence.ContainsKey(card.CardSuit))
                    suitToCardValueOccurrence.Add(card.CardSuit, new Dictionary<CardValue, int>());

                if (!suitToCardValueOccurrence[card.CardSuit].ContainsKey(card.Value))
                    suitToCardValueOccurrence[card.CardSuit].Add(card.Value, 0);

                ++suitToCardValueOccurrence[card.CardSuit][card.Value];
            }

            foreach (var suitToCardValue in suitToCardValueOccurrence)
            {
                List<CardValue> neededValuesForRoyalFlush = new List<CardValue>();
                for (CardValue cardValue = CardValue.Ace; cardValue >= CardValue.Ten; --cardValue)
                {
                    neededValuesForRoyalFlush.Add(cardValue);
                }

                bool allCardsPresent = true;

                foreach (var cardValue in neededValuesForRoyalFlush)
                {
                    if (!suitToCardValue.Value.ContainsKey(cardValue))
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

            List<Card> returnHand = null;

            var suitToCardValueOccurence = new Dictionary<Suit, Dictionary<CardValue, Card>>();
            foreach (var card in cards)
            {
                if (!suitToCardValueOccurence.ContainsKey(card.CardSuit))
                    suitToCardValueOccurence.Add(card.CardSuit, new Dictionary<CardValue, Card>());

                if (!suitToCardValueOccurence[card.CardSuit].ContainsKey(card.Value))
                    suitToCardValueOccurence[card.CardSuit].Add(card.Value, null);

                suitToCardValueOccurence[card.CardSuit][card.Value] = card;

            }

            foreach (var suitToCardValue in suitToCardValueOccurence)
            {
                List<CardValue> neededValuesForRoyalFlush = new List<CardValue>();
                for (CardValue cardValue = CardValue.Ace; cardValue >= CardValue.Ten; --cardValue)
                {
                    neededValuesForRoyalFlush.Add(cardValue);
                }

                bool allCardsPresent = true;
                List<Card> newHand = new List<Card>();

                foreach (var cardValue in neededValuesForRoyalFlush)
                {
                    if (!suitToCardValue.Value.ContainsKey(cardValue))
                    {
                        allCardsPresent = false;
                        break;
                    }
                    newHand.Add(suitToCardValue.Value[cardValue]);
                }

                if (allCardsPresent)
                {
                    returnHand = newHand;
                    return returnHand;
                }
            }
            return returnHand;
        }
    }
}
