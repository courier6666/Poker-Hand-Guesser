# Poker hand guesser
Small program written in C# on .NET Core 3.1
for memorizing poker hand combinations.
## Project structure
Project consists of two main parts: PokerLibrary and an interface module.
### PokerLibrary
Poker library contains classes for defining poker cards and finding hand combinations in provided deck.
![photo_2024-05-28_18-20-00](https://github.com/courier6666/Poker-Hand-Guesser/assets/89982405/65f11564-f04a-4e30-8715-1fbd7b6cd341)

__Card:__
PokerLibrary contains class 'Card'. It represents a card in a deck.
Class 'Card' has two defined properties:
- 'Suit': represent a suit of the card. Can have value of: Hearts, Spades, Diamonds, Clubs.
- 'Value': represents value in hierarchy of cards. Its value ranges from 2 to 10, Jack, Queen, King and Ace.
```
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLibrary
{
    public enum Suit
    {
        Clubs = 1,
        Hearts = 2,
        Spades = 3,
        Diamonds = 4
    }
    public enum CardValue
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }
    public class Card
    {
        Suit suit;
        CardValue value;
        private Card()
        {

        }
        public Card(Suit suit, CardValue value)
        {
            this.suit = suit;
            this.value = value;
        }
        public Suit CardSuit
        {
            get
            {
                return this.suit;
            }
            set
            {
                this.suit = value;
            }
        }
        public CardValue Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}
```
__Poker Hand Checkers:__
These classes are responsible for receeving group of cards and checking it for presence of certain hand combination.
Every specific classes searches for specifica hand combination.
Class __'AbstractPokerHandChecker'__ is a base class for poker hand checkers:
```
using PokerLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLibrary.PokerHandCheckers
{
    public enum HandCombinationRank
    {
        RoyalFlush = 1,
        StraightFlush = 2,
        FourOfAKind = 3,
        FullHouse = 4,
        Flush = 5,
        Straight = 6,
        ThreeOfAKind = 7,
        TwoPair = 8,
        Pair = 9,
        HighCard = 10

    }
    public abstract class AbstractPokerHandChecker
    {
        public HandCombinationRank CombinationRank { get; protected set; }

        public abstract bool ContainsHand(List<Card> cards);
        public abstract List<Card> GetHand(List<Card> cards);
        protected List<Card> GetUniqueCards(List<Card> cards)
        {
            List<Card> uniqueCards = new List<Card>();

            var cardsSet = new Dictionary<Suit, HashSet<CardValue>>();
            cardsSet.Add(Suit.Clubs, new HashSet<CardValue>());
            cardsSet.Add(Suit.Diamonds, new HashSet<CardValue>());
            cardsSet.Add(Suit.Hearts, new HashSet<CardValue>());
            cardsSet.Add(Suit.Spades, new HashSet<CardValue>());

            foreach (var card in cards)
            {
                if (cardsSet[card.CardSuit].Add(card.Value))
                {
                    uniqueCards.Add(card);
                }
            }
            return uniqueCards;
        }
        protected class CardValueComparer : IComparer<Card>
        {
            private int sortOrder;
            public CardValueComparer(int order = 1)
            {
                sortOrder = order;
            }
            private CardValueComparer()
            {

            }
            public int Compare(Card a, Card b)
            {
                return (a.Value - b.Value) * sortOrder;
            }
        }
        protected class CardComparer : IComparer<Card>
        {
            private int sortOrder;
            public CardComparer(int order = 1)
            {
                this.sortOrder = order;
            }
            public int Compare(Card a, Card b)
            {
                if (a.CardSuit == b.CardSuit)
                {
                    return (a.Value - b.Value) * sortOrder;
                }

                return (a.CardSuit - b.CardSuit) * sortOrder;
            }
        }
    }
}
```
It contains such properties and methods:
- CombinationRank: variable that stores rank of hand combination that class tries to find.
- ContainsHand: return true if hand combination can be constructed given the cards.
- GetUniqueCards: auxilary method for finding only unique cards within set of cards.

__'AbstractPokerHandChecker'__ also contains auxilary comparer classes for sorting - __'CardValueComparer'__ and __'CardComparer'__.

So far there are 10 hand combinations and there are 10 poker hand checker classes respectively.
