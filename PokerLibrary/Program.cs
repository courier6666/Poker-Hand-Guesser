using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using PokerLibrary.PokerHandCheckers;
using PokerLibrary.Enums;
using PokerLibrary.Handlers;
using PokerLibrary.Structs;
using PokerLibrary.PokerHandGenerators;

namespace PokerLibrary
{
    class Program
    {
        static public List<Card> GenerateDeck()
        {
            List<Card> deck = new List<Card>();
            for(Suit currentSuit = Suit.Clubs;currentSuit<=Suit.Diamonds;++currentSuit)
            {
                for(CardValue currentValue = CardValue.Two;currentValue<=CardValue.Ace;++currentValue)
                {
                    deck.Add(new Card(currentSuit, currentValue));
                }
            }
            return deck;
        }
        static int handsCount = 0;
        static async Task Main(string[] args)
        {
            BruteForceGenerator generator = new BruteForceGenerator();

            var hand = generator.Generate(7, HandCombinationRank.RoyalFlush);

            return;

            Dictionary<HandCombinationRank, int> handOccurrence = new Dictionary<HandCombinationRank, int>();

            for(var currentRank = HandCombinationRank.HighCard;currentRank>=HandCombinationRank.RoyalFlush;--currentRank)
            {
                handOccurrence.Add(currentRank, 0);
            }

            Random rnd = new Random();
            var tasks = new List<Task>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.For(0, 10, async (i, state ) => await CalculateOccurrence(handOccurrence));

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds*0.001);
            double d_n = 1 / (double)(handsCount);
            foreach (var occurrence in handOccurrence)
            {
                
                double occur = occurrence.Value;
                Console.WriteLine(occurrence.Key.ToString() + " - " + Math.Round(occur * d_n * 100f, 10) + "%");
            }
        }

        static async Task CalculateOccurrence(Dictionary<HandCombinationRank, int> handOccurrence)
        {
            int n = 100000;
            handsCount += n;

            ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());

            for (int i = 0; i < n; ++i)
            {
                List<Card> deckCopy = GenerateDeck();
                List<Card> generatedHand = new List<Card>();

                for (int j = 0; j < 5; ++j)
                {
                    int index = random.Value.Next(0, deckCopy.Count);
                    generatedHand.Add(deckCopy[index]);

                    deckCopy.Remove(deckCopy[index]);
                }

                AbstractPokerHandFinder checker = PokerChecker.GetPokerHandChecker(generatedHand);
                ++handOccurrence[checker.CombinationRank];
            }
            random.Dispose();
        }
       
    }
}
