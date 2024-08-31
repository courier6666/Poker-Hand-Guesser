using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHadChecker.Interfaces
{
    public interface IHandler<TRequest, TResult>
    {
        public IHandler<TRequest, TResult> SetNext(IHandler<TRequest, TResult> handler);
        public TResult Handle(TRequest request);
    }
}
