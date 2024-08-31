using PokerHadChecker.Interfaces;
using PokerHadChecker.PokerHandCheckers;
using PokerHadChecker.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHadChecker.Handlers
{
    public class PokerHandCheckHandler : IHandler<List<Card>, AbstractPokerHandChecker>
    {
        AbstractPokerHandChecker pokerHandChecker;
        IHandler<List<Card>, AbstractPokerHandChecker> nextHandler;
        public PokerHandCheckHandler(AbstractPokerHandChecker pokerHandChecker)
        {
            this.pokerHandChecker = pokerHandChecker;
        }
        private PokerHandCheckHandler()
        {

        }
        public IHandler<List<Card>, AbstractPokerHandChecker> SetNext(IHandler<List<Card>, AbstractPokerHandChecker> handler)
        {
            nextHandler = handler;
            return handler;
        }
        public AbstractPokerHandChecker Handle(List<Card> request)
        {
            if (request == null || request.Count == 0)
            {
                return null;
            }

            if (!pokerHandChecker.ContainsHand(request))
                return nextHandler?.Handle(request);

            return pokerHandChecker;
        }

    }
}
