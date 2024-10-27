using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerLibrary.Enums;
using PokerLibrary.Structs;

namespace PokerLibrary.Interfaces
{
    public interface IPokerHandCombinationGenerator
    {
        #nullable enable
        List<Card>? Generate(int handSize, HandCombinationRank rank);
    }
}
