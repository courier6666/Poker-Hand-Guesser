using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLibrary.Interfaces
{
    public interface IHandler<TRequest, TResult>
    {
        public IHandler<TRequest, TResult> SetNext(IHandler<TRequest, TResult> handler);
        public TResult Handle(TRequest request);
    }
}
