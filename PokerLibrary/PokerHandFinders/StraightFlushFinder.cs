using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerLibrary.Enums;
using PokerLibrary.Structs;

namespace PokerLibrary.PokerHandCheckers
{
    public class StraightFlushFinder : AbstractPokerHandFinder
    {
        public StraightFlushFinder()
        {
            CombinationRank = HandCombinationRank.StraightFlush;
        }
        public override bool ContainsHand(List<Card> cards)
        {
            cards = GetUniqueCards(cards);
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
                for (CardValue valueBegin = CardValue.Ten; valueBegin != CardValue.King; valueBegin = valueBegin == CardValue.Two ? valueBegin = valueBegin = CardValue.Ace : --valueBegin)
                {
                    List<CardValue> neededValuesForStraight = new List<CardValue>();
                    for (int i = 0; i < 5; ++i)
                    {
                        int cardValueMod = ((int)valueBegin + i - 2) % 13 + 2;
                        neededValuesForStraight.Add((CardValue)cardValueMod);
                    }

                    bool allCardsPresent = true;

                    foreach (var cardValue in neededValuesForStraight)
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

            var suitToCardValueOccurence = new Dictionary<Suit, Dictionary<CardValue, Card>>();
            foreach (var card in cards)
            {
                if (!suitToCardValueOccurence.ContainsKey(card.CardSuit))
                    suitToCardValueOccurence.Add(card.CardSuit, new Dictionary<CardValue, Card>());

                if (!suitToCardValueOccurence[card.CardSuit].ContainsKey(card.Value))
                    suitToCardValueOccurence[card.CardSuit].Add(card.Value, Card.EmptyCard);

                suitToCardValueOccurence[card.CardSuit][card.Value] = card;

            }

            foreach (var suitToCardValue in suitToCardValueOccurence)
            {
                for (CardValue valueBegin = CardValue.Ten; valueBegin != CardValue.King; valueBegin = valueBegin == CardValue.Two ? valueBegin = valueBegin = CardValue.Ace : --valueBegin)
                {
                    List<CardValue> neededValuesForStraight = new List<CardValue>();
                    for (int i = 4; i >= 0; --i)
                    {
                        int cardValueMod = ((int)valueBegin + i - 2) % 13 + 2;
                        neededValuesForStraight.Add((CardValue)cardValueMod);
                    }

                    bool allCardsPresent = true;
                    List<Card> newHand = new List<Card>();
                    foreach (var cardValue in neededValuesForStraight)
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
                        if (returnHand == null)
                        {
                            returnHand = newHand;
                        }

                        if (returnHand[0].Value < newHand[0].Value)
                            returnHand = newHand;
                        break;
                    }

                }
            }

            return returnHand.Any() ? returnHand : null;
        }
    }
}
