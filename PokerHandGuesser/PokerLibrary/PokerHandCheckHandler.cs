using PokerHandGuesser.PokerLibrary.Interfaces;
using PokerLibrary.PokerHandCheckers;
using PokerLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandGuesser.PokerLibrary
{
    public class PokerHandCheckHandler : IHandler<AbstractPokerHandChecker>
    {
        AbstractPokerHandChecker pokerHandChecker;
        IHandler<AbstractPokerHandChecker> nextHandler;
        public PokerHandCheckHandler(AbstractPokerHandChecker pokerHandChecker)
        {
            this.pokerHandChecker = pokerHandChecker;
        }
        private PokerHandCheckHandler()
        {

        }
        public IHandler<AbstractPokerHandChecker> SetNext(IHandler<AbstractPokerHandChecker> handler)
        {
            this.nextHandler = handler;
            return handler;
        }
        public object Handle(object request)
        {
            if (!(request is List<Card>))
                return null;

            List<Card> cards = request as List<Card>;
            if (!pokerHandChecker.ContainsHand(cards))
                return this.nextHandler?.Handle(cards);

            return this.pokerHandChecker;
        }

    }
}
