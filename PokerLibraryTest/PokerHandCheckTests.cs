using PokerLibrary.PokerHandCheckers;
using PokerLibrary.Structs;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLibraryTest
{
    internal class PokerHandCheckTests
    {
        public class PokerHandFinderWrapper : AbstractPokerHandFinder
        {
            public override bool ContainsHand(List<Card> cards)
            {
                throw new NotImplementedException();
            }

            public override List<Card> GetHand(List<Card> cards)
            {
                throw new NotImplementedException();
            }
            public List<Card> GetUniqueCardsCopy(List<Card> cards)
            {
                return this.GetUniqueCards(cards);
            }
        }
        private CardBuilder cardBuilder;
        [SetUp]
        public void Initialize()
        {
            cardBuilder = new CardBuilder();

        }
        public static IEnumerable<TestCaseData> PokerHandFindersTestCases
        {
            get
            {
                yield return new TestCaseData(new HighCardFinder());
                yield return new TestCaseData(new PairFinder());
                yield return new TestCaseData(new TwoPairFinder());
                yield return new TestCaseData(new ThreeOfAKindFinder());
                yield return new TestCaseData(new StraightFinder());
                yield return new TestCaseData(new FlushFinder());
                yield return new TestCaseData(new FullHouseFinder());
                yield return new TestCaseData(new FourOfAKindFinder());
                yield return new TestCaseData(new StraightFlushFinder());
                yield return new TestCaseData(new RoyalFlushFinder());
            }
        }
        [TestCaseSource(nameof(PokerHandFindersTestCases))]
        public void PokerHandFinder_GetHand_HandSizeLessThan5_ReturnsNull(AbstractPokerHandFinder pokerHandFinder)
        {
            //arrange
            List<Card> hand = new List<Card>
            {
                cardBuilder.Ace().Spades(),
                cardBuilder.Ace().Hearts(),
                cardBuilder.Ace().Clubs(),
                cardBuilder.Jack().Spades(),
            };

            //act
            var foundHand = pokerHandFinder.GetHand(hand);

            //assert
            foundHand.ShouldBeNull();
        }
        [TestCase]
        public void PokerHandFinder_GetUniqueCards_ReturnsUniqueCards()
        {
            //arrange
            PokerHandFinderWrapper pokerHandFinder = new PokerHandFinderWrapper();
            var hand = new List<Card>()
            {
                cardBuilder.Ace().Spades(),
                cardBuilder.Ace().Spades(),
                cardBuilder.Ace().Hearts(),
                cardBuilder.Ace().Clubs(),
                cardBuilder.Jack().Spades(),
                cardBuilder.Jack().Spades(),
            }.OrderBy(c => c).ToList();

            var expected = new List<Card>()
            {
                cardBuilder.Ace().Spades(),
                cardBuilder.Ace().Hearts(),
                cardBuilder.Ace().Clubs(),
                cardBuilder.Jack().Spades(),
            }.OrderBy(c => c).ToList();

            //act
            var uniqueCards = pokerHandFinder.GetUniqueCardsCopy(hand);

            //assert
            uniqueCards.ShouldBe(expected);
        }
    }
}
