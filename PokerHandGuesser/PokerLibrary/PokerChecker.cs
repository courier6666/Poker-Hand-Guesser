using System;
using System.Collections.Generic;
using System.Text;
using PokerHandGuesser.PokerLibrary;
using PokerHandGuesser.PokerLibrary.Interfaces;
using PokerLibrary.PokerHandCheckers;

namespace PokerLibrary
{
    
    static class PokerChecker
    {
        static PokerHandCheckHandler handler;
        static PokerChecker()
        {
            PokerHandCheckHandler highCard = new PokerHandCheckHandler(new HighCardChecker());
            PokerHandCheckHandler pair = new PokerHandCheckHandler(new PairChecker());
            PokerHandCheckHandler twoPair = new PokerHandCheckHandler(new TwoPairChecker());
            PokerHandCheckHandler three = new PokerHandCheckHandler(new ThreeOfAKindChecker());
            PokerHandCheckHandler straight = new PokerHandCheckHandler(new StraightChecker());
            PokerHandCheckHandler flush = new PokerHandCheckHandler(new FlushChecker());
            PokerHandCheckHandler fullHouse = new PokerHandCheckHandler(new FullHouseChecker());
            PokerHandCheckHandler four = new PokerHandCheckHandler(new FourOfAKindChecker());
            PokerHandCheckHandler straightFlush = new PokerHandCheckHandler(new StraightFlushChecker());
            PokerHandCheckHandler royalFlush = new PokerHandCheckHandler(new RoyalFlushChecker());

            royalFlush.SetNext(straightFlush).SetNext(four).SetNext(fullHouse).SetNext(flush).SetNext(straight).SetNext(three).SetNext(twoPair).
                SetNext(pair).SetNext(highCard);

            PokerChecker.handler = royalFlush;
        }
        public static AbstractPokerHandChecker GetPokerHandChecker(List<Card> cards)
        {
            return PokerChecker.handler.Handle(cards) as AbstractPokerHandChecker;
        }
    }
}
