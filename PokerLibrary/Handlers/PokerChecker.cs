using System;
using System.Collections.Generic;
using System.Text;
using PokerLibrary.PokerHandCheckers;
using PokerLibrary.Structs;

namespace PokerLibrary.Handlers
{

    public static class PokerChecker
    {
        static PokerHandCheckHandler handler;
        static PokerChecker()
        {
            PokerHandCheckHandler highCard = new PokerHandCheckHandler(new HighCardFinder());
            PokerHandCheckHandler pair = new PokerHandCheckHandler(new PairFinder());
            PokerHandCheckHandler twoPair = new PokerHandCheckHandler(new TwoPairFinder());
            PokerHandCheckHandler three = new PokerHandCheckHandler(new ThreeOfAKindFinder());
            PokerHandCheckHandler straight = new PokerHandCheckHandler(new StraightFinder());
            PokerHandCheckHandler flush = new PokerHandCheckHandler(new FlushFinder());
            PokerHandCheckHandler fullHouse = new PokerHandCheckHandler(new FullHouseFinder());
            PokerHandCheckHandler four = new PokerHandCheckHandler(new FourOfAKindFinder());
            PokerHandCheckHandler straightFlush = new PokerHandCheckHandler(new StraightFlushFinder());
            PokerHandCheckHandler royalFlush = new PokerHandCheckHandler(new RoyalFlushFinder());

            royalFlush.SetNext(straightFlush).SetNext(four).SetNext(fullHouse).SetNext(flush).SetNext(straight).SetNext(three).SetNext(twoPair).
                SetNext(pair).SetNext(highCard);

            handler = royalFlush;
        }
        public static AbstractPokerHandFinder GetPokerHandChecker(List<Card> cards)
        {
            return handler.Handle(cards) as AbstractPokerHandFinder;
        }
    }
}
