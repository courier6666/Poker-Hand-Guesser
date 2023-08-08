using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandGuesser.PokerLibrary.Interfaces
{
    public interface IHandler<T>
    {
        public IHandler<T> SetNext(IHandler<T> handler);
        public object Handle(object request);
    }
}
