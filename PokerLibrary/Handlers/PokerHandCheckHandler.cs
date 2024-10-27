using PokerLibrary.Interfaces;
using PokerLibrary.PokerHandCheckers;
using PokerLibrary.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLibrary.Handlers
{
    public class PokerHandCheckHandler : IHandler<List<Card>, AbstractPokerHandFinder>
    {
        AbstractPokerHandFinder pokerHandChecker;
        IHandler<List<Card>, AbstractPokerHandFinder> nextHandler;
        public PokerHandCheckHandler(AbstractPokerHandFinder pokerHandChecker)
        {
            this.pokerHandChecker = pokerHandChecker;
        }
        private PokerHandCheckHandler()
        {

        }
        public IHandler<List<Card>, AbstractPokerHandFinder> SetNext(IHandler<List<Card>, AbstractPokerHandFinder> handler)
        {
            nextHandler = handler;
            return handler;
        }
        public AbstractPokerHandFinder Handle(List<Card> request)
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
