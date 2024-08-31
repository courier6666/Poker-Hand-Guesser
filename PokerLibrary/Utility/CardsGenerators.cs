using PokerHadChecker.Enums;
using PokerHadChecker.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLibrary.Utility
{
    public static class CardsGenerators
    {
        public static List<Card> GenerateDeck()
        {
            List<Card> deck = new List<Card>();
            for (Suit currentSuit = Suit.Clubs; currentSuit <= Suit.Diamonds; ++currentSuit)
            {
                for (CardValue currentValue = CardValue.Two; currentValue <= CardValue.Ace; ++currentValue)
                {
                    deck.Add(new Card(currentSuit, currentValue));
                }
            }
            return deck;
        }

        public static List<Card> SelectNRandomCards(int n, List<Card> deck)
        {
            if (n > deck.Count || n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), $"Size of deck is: {deck.Count}, provided n: {n}");

            List<Card> copy = deck.ToList();
            
            Random rnd = new Random();

            List<Card> selectedCards = new List<Card>();

            for (int i = 0; i < n; ++i)
            {
                int ind = rnd.Next(copy.Count);
                selectedCards.Add(copy[ind]);
                copy.Remove(copy[ind]);
            }

            return selectedCards;
        }
    }
}
