using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerHadChecker.Enums;
using PokerHadChecker.Handlers;
using PokerHadChecker.Structs;
using PokerLibrary.Interfaces;
using PokerLibrary.Utility;

namespace PokerLibrary.PokerHandGenerators
{
    public class BruteForceGenerator : IPokerHandCombinationGenerator
    {
        public int TimeLimitSeconds { get; } = 5;
        public BruteForceGenerator(int timeLimitSeconds)
        {
            TimeLimitSeconds = timeLimitSeconds;
        }

        public BruteForceGenerator()
        {
            
        }
        #nullable enable
        public List<Card>? Generate(int handSize, HandCombinationRank rank)
        {
            var deck = CardsGenerators.GenerateDeck();
            var hand = CardsGenerators.SelectNRandomCards(handSize, deck);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while (PokerChecker.GetPokerHandChecker(hand).CombinationRank != rank)
            {
                hand = CardsGenerators.SelectNRandomCards(handSize, deck);
                if (stopwatch.ElapsedMilliseconds * 0.001 > TimeLimitSeconds)
                    return null;
            }

            return hand;
        }
    }
}
