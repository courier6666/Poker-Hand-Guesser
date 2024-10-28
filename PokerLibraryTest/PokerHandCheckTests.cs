using PokerLibrary.Enums;
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
        [SetUp]
        public static void Initialize()
        {

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
            CardBuilder cardBuilder = new CardBuilder();
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
            CardBuilder cardBuilder = new CardBuilder();
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
            };

            var expected = new List<Card>()
            {
                cardBuilder.Ace().Spades(),
                cardBuilder.Ace().Hearts(),
                cardBuilder.Ace().Clubs(),
                cardBuilder.Jack().Spades(),
            };

            //act
            var uniqueCards = pokerHandFinder.GetUniqueCardsCopy(hand);

            //assert
            uniqueCards.ShouldBe(expected);
        }
        public static IEnumerable<TestCaseData> HighCardFindersGetHandTestCases
        {

            get
            {
                CardBuilder cardBuilder = new CardBuilder();
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Spades(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Two().Spades(),
                        cardBuilder.Nine().Clubs(),
                        cardBuilder.Five().Diamonds()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Spades(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Nine().Clubs()
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.King().Clubs(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Nine().Diamonds(),
                        cardBuilder.Seven().Spades(),
                        cardBuilder.Five().Clubs(),
                        cardBuilder.Three().Diamonds(),
                        cardBuilder.Two().Hearts()
                    },
                    new List<Card>
                    {
                        cardBuilder.King().Clubs(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Nine().Diamonds(),
                        cardBuilder.Seven().Spades(),
                        cardBuilder.Five().Clubs()
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.Ten().Hearts(),
                        cardBuilder.Eight().Spades(),
                        cardBuilder.Six().Clubs(),
                        cardBuilder.Four().Diamonds(),
                        cardBuilder.Three().Hearts(),
                        cardBuilder.Ace().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.Ten().Hearts(),
                        cardBuilder.Eight().Spades(),
                        cardBuilder.Six().Clubs()
                    }
                );
            }
        }
        [TestCaseSource(nameof(HighCardFindersGetHandTestCases))]
        public void HighCardFinder_GetHand_ReturnsCorrectHand(List<Card> cardsToFindHandFrom, List<Card> expectedHand)
        {
            //assert
            HighCardFinder highCardFinder = new HighCardFinder();

            //act
            var hand = highCardFinder.GetHand(cardsToFindHandFrom);

            //assert
            hand.ShouldBe(expectedHand);
        }
        public static IEnumerable<TestCaseData> PairFinderGetHandTestCases
        {
            get
            {
                CardBuilder cardBuilder = new CardBuilder();

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Spades(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Nine().Clubs(),
                        cardBuilder.Five().Diamonds()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Spades(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.Ten().Diamonds(),
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.King().Clubs(),
                        cardBuilder.King().Diamonds(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Nine().Diamonds(),
                        cardBuilder.Seven().Spades(),
                        cardBuilder.Five().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.King().Clubs(),
                        cardBuilder.King().Diamonds(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Nine().Diamonds(),
                        cardBuilder.Seven().Spades(),
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.Ten().Hearts(),
                        cardBuilder.Eight().Spades(),
                        cardBuilder.Six().Clubs(),
                        cardBuilder.Four().Diamonds(),
                        cardBuilder.Three().Hearts(),
                        cardBuilder.Three().Spades()
                    },
                    new List<Card>
                    {
                        cardBuilder.Three().Hearts(),
                        cardBuilder.Three().Spades(),
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.Ten().Hearts(),
                        cardBuilder.Eight().Spades(),
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Two().Clubs(),
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Three().Diamonds(),
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.Five().Spades()
                    },
                    new List<Card>
                    {
                        cardBuilder.Two().Clubs(),
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.Five().Spades(),
                        cardBuilder.Four().Hearts(),
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Ten().Hearts(),
                        cardBuilder.Nine().Clubs(),
                        cardBuilder.Five().Diamonds(),
                        cardBuilder.Six().Spades()
                    },
                    null
                );
            }
        }
        [TestCaseSource(nameof(PairFinderGetHandTestCases))]
        public void PairFinder_GetHand_ReturnsCorrectHand(List<Card> cardsToFindHandFrom, List<Card> expectedHand)
        {
            //assert
            PairFinder pairFinder = new PairFinder();

            //act
            var hand = pairFinder.GetHand(cardsToFindHandFrom);

            //assert
            hand.ShouldBe(expectedHand);
        }
        public static IEnumerable<TestCaseData> TwoPairFinderGetHandTestCases
        {
            get
            {
                CardBuilder cardBuilder = new CardBuilder();

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Spades(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.King().Clubs(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Nine().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Spades(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.King().Clubs(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Clubs(),
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Three().Diamonds(),
                        cardBuilder.Three().Hearts(),
                        cardBuilder.Four().Clubs(),
                        cardBuilder.Four().Spades(),
                        cardBuilder.Seven().Diamonds(),
                        cardBuilder.Nine().Hearts(),
                        cardBuilder.Ten().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Four().Clubs(),
                        cardBuilder.Four().Spades(),
                        cardBuilder.Three().Diamonds(),
                        cardBuilder.Three().Hearts(),
                        cardBuilder.Ten().Clubs()
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Two().Spades(),
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Five().Hearts(),
                        cardBuilder.Five().Clubs(),
                        cardBuilder.Eight().Diamonds(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Queen().Spades()
                    },
                    new List<Card>
                    {
                        cardBuilder.Five().Hearts(),
                        cardBuilder.Five().Clubs(),
                        cardBuilder.Two().Spades(),
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Queen().Spades()
                    }
                );
            }
        }
        [TestCaseSource(nameof(TwoPairFinderGetHandTestCases))]
        public void TwoPairFinder_GetHand_ReturnsCorrectHand(List<Card> cardsToFindHandFrom, List<Card> expectedHand)
        {
            //assert
            TwoPairFinder twoPairFinder = new TwoPairFinder();

            //act
            var hand = twoPairFinder.GetHand(cardsToFindHandFrom);

            //assert
            hand.ShouldBe(expectedHand);
        }
        public static IEnumerable<TestCaseData> ThreeOfAKindFinderGetHandTestCases
        {
            get
            {
                CardBuilder cardBuilder = new CardBuilder();

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Spades(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Nine().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Spades(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Clubs(),
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Four().Clubs(),
                        cardBuilder.Four().Diamonds(),
                        cardBuilder.Seven().Spades(),
                        cardBuilder.Eight().Diamonds(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Ten().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Four().Clubs(),
                        cardBuilder.Four().Diamonds(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Ten().Clubs()
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Two().Spades(),
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Nine().Hearts(),
                        cardBuilder.Nine().Clubs(),
                        cardBuilder.Nine().Spades(),
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.King().Hearts()
                    },
                    new List<Card>
                    {
                        cardBuilder.Nine().Hearts(),
                        cardBuilder.Nine().Clubs(),
                        cardBuilder.Nine().Spades(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Diamonds(),
                    }
                );
            }
        }
        [TestCaseSource(nameof(ThreeOfAKindFinderGetHandTestCases))]
        public void ThreeOfAKindFinder_GetHand_ReturnsCorrectHand(List<Card> cardsToFindHandFrom, List<Card> expectedHand)
        {
            //assert
            ThreeOfAKindFinder threeOfAKindFinder = new ThreeOfAKindFinder();

            //act
            var hand = threeOfAKindFinder.GetHand(cardsToFindHandFrom);

            //assert
            hand.ShouldBe(expectedHand);
        }
        public static IEnumerable<TestCaseData> StraightFinderGetHandTestCases
        {
            get
            {
                CardBuilder cardBuilder = new CardBuilder();

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.King().Spades(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.Jack().Diamonds(),
                        cardBuilder.Ten().Hearts(),
                        cardBuilder.Four().Diamonds(),
                        cardBuilder.Two().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.King().Spades(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.Jack().Diamonds(),
                        cardBuilder.Ten().Hearts()
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Nine().Hearts(),
                        cardBuilder.Eight().Clubs(),
                        cardBuilder.Seven().Spades(),
                        cardBuilder.Six().Diamonds(),
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Two().Spades()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Nine().Hearts(),
                        cardBuilder.Eight().Clubs(),
                        cardBuilder.Seven().Spades(),
                        cardBuilder.Six().Diamonds()
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Eight().Spades(),
                        cardBuilder.Seven().Diamonds(),
                        cardBuilder.Six().Hearts(),
                        cardBuilder.Five().Clubs(),
                        cardBuilder.Four().Spades(),
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.Two().Diamonds()
                    },
                    new List<Card>
                    {
                        cardBuilder.Eight().Spades(),
                        cardBuilder.Seven().Diamonds(),
                        cardBuilder.Six().Hearts(),
                        cardBuilder.Five().Clubs(),
                        cardBuilder.Four().Spades()
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Three().Diamonds(),
                        cardBuilder.Two().Clubs(),
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.Five().Spades(),
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Seven().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Five().Spades(),
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Three().Diamonds(),
                        cardBuilder.Two().Clubs(),
                        cardBuilder.Ace().Hearts(),
                        
                    }
                );

                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Three().Diamonds(),
                        cardBuilder.Two().Clubs(),
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.Five().Spades(),
                        cardBuilder.Six().Diamonds(),
                        cardBuilder.Seven().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Seven().Clubs(),
                        cardBuilder.Six().Diamonds(),
                        cardBuilder.Five().Spades(),
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Three().Diamonds(),
                    }
                );
            }
        }

        [TestCaseSource(nameof(StraightFinderGetHandTestCases))]
        public void StraightFinder_GetHand_ReturnsCorrectHand(List<Card> cardsToFindHandFrom, List<Card> expectedHand)
        {
            //assert
            StraightFinder straightFinder = new StraightFinder();

            //act
            var hand = straightFinder.GetHand(cardsToFindHandFrom);

            //assert
            hand.ShouldBe(expectedHand);
        }
        public static IEnumerable<TestCaseData> FlushFinderGetHandTestCases
        {
            get
            {
                CardBuilder cardBuilder = new CardBuilder();

                // Test case 1: Standard flush (all cards of the same suit)
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Hearts(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Ten().Hearts(),
                        cardBuilder.Four().Clubs(),
                        cardBuilder.Two().Spades()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Hearts(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Ten().Hearts()
                    }
                );

                // Test case 2: Flush with lower cards
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Five().Diamonds(),
                        cardBuilder.Four().Diamonds(),
                        cardBuilder.Three().Diamonds(),
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.Nine().Spades(),
                        cardBuilder.Eight().Hearts()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.Five().Diamonds(),
                        cardBuilder.Four().Diamonds(),
                        cardBuilder.Three().Diamonds(),
                        cardBuilder.Two().Diamonds(),
                    }
                );

                // Test case 3: Mixed suits with a valid flush
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.King().Clubs(),
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.Jack().Clubs(),
                        cardBuilder.Ten().Clubs(),
                        cardBuilder.Seven().Hearts(),
                        cardBuilder.Six().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.King().Clubs(),
                        cardBuilder.Jack().Clubs(),
                        cardBuilder.Ten().Clubs(),
                        cardBuilder.Six().Clubs()
                    }
                );
            }
        }

        [TestCaseSource(nameof(FlushFinderGetHandTestCases))]
        public void FlushFinder_GetHand_ReturnsCorrectHand(List<Card> cardsToFindHandFrom, List<Card> expectedHand)
        {
            //assert
            FlushFinder flushFinder = new FlushFinder();

            //act
            var hand = flushFinder.GetHand(cardsToFindHandFrom);

            //assert
            hand.ShouldBe(expectedHand);
        }
        public static IEnumerable<TestCaseData> FullHouseFinderGetHandTestCases
        {
            get
            {
                CardBuilder cardBuilder = new CardBuilder();

                // Test case 1: Standard full house 
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.King().Spades(),
                        cardBuilder.King().Diamonds(),
                        cardBuilder.Queen().Hearts(),
                        cardBuilder.Two().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.King().Spades(),
                        cardBuilder.King().Diamonds()
                    }
                );

                // Test case 2: Full house with lower ranks
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Five().Diamonds(),
                        cardBuilder.Five().Clubs(),
                        cardBuilder.Five().Hearts(),
                        cardBuilder.Two().Spades(),
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Seven().Hearts(),
                        cardBuilder.Nine().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Five().Diamonds(),
                        cardBuilder.Five().Clubs(),
                        cardBuilder.Five().Hearts(),
                        cardBuilder.Two().Spades(),
                        cardBuilder.Two().Diamonds()
                    }
                );

                // Test case 3: Full house with mixed suits
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Queen().Spades(),
                        cardBuilder.Queen().Hearts(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.Jack().Diamonds(),
                        cardBuilder.Jack().Spades(),
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Two().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Queen().Spades(),
                        cardBuilder.Queen().Hearts(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.Jack().Diamonds(),
                        cardBuilder.Jack().Spades()
                    }
                );
            }
        }
        [TestCaseSource(nameof(FullHouseFinderGetHandTestCases))]
        public void FullHouseFinder_GetHand_ReturnsCorrectHand(List<Card> cardsToFindHandFrom, List<Card> expectedHand)
        {
            //assert
            FullHouseFinder fullHouseFinder = new FullHouseFinder();

            //act
            var hand = fullHouseFinder.GetHand(cardsToFindHandFrom);

            //assert
            hand.ShouldBe(expectedHand);
        }
        public static IEnumerable<TestCaseData> FourOfAKindFinderGetHandTestCases
        {
            get
            {
                CardBuilder cardBuilder = new CardBuilder();

                // Test case 1: Standard four of a kind (four Aces and one other card)
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.Ace().Spades(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Three().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.Ace().Spades(),
                        cardBuilder.King().Hearts(),
                    }
                );

                // Test case 2: Four of a kind with lower ranks
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Four().Diamonds(),
                        cardBuilder.Four().Clubs(),
                        cardBuilder.Four().Spades(),
                        cardBuilder.Three().Hearts(),
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Ace().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Four().Diamonds(),
                        cardBuilder.Four().Clubs(),
                        cardBuilder.Four().Spades(),
                        cardBuilder.Ace().Clubs()
                    }
                );

                // Test case 3: Four of a kind with mixed suits and additional cards
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Jack().Spades(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Jack().Diamonds(),
                        cardBuilder.Jack().Clubs(),
                        cardBuilder.Ten().Spades(),
                        cardBuilder.Nine().Hearts(),
                        cardBuilder.Eight().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Jack().Spades(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Jack().Diamonds(),
                        cardBuilder.Jack().Clubs(),
                        cardBuilder.Ten().Spades(),
                    }
                );
            }
        }

        [TestCaseSource(nameof(FourOfAKindFinderGetHandTestCases))]
        public void FourOfAKindFinder_GetHand_ReturnsCorrectHand(List<Card> cardsToFindHandFrom, List<Card> expectedHand)
        {
            //assert
            FourOfAKindFinder fourOfAKindFinder = new FourOfAKindFinder();

            //act
            var hand = fourOfAKindFinder.GetHand(cardsToFindHandFrom);

            //assert
            hand.ShouldBe(expectedHand);
        }
        public static IEnumerable<TestCaseData> StraightFlushFinderGetHandTestCases
        {
            get
            {
                CardBuilder cardBuilder = new CardBuilder();

                // Test case 1: Standard straight flush (A-2-3-4-5 of hearts)
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.Two().Hearts(),
                        cardBuilder.Three().Hearts(),
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Five().Hearts(),
                        cardBuilder.Seven().Diamonds(),
                        cardBuilder.Nine().Spades()
                    },
                    new List<Card>
                    {
                        cardBuilder.Five().Hearts(),
                        cardBuilder.Four().Hearts(),
                        cardBuilder.Three().Hearts(),
                        cardBuilder.Two().Hearts(),
                        cardBuilder.Ace().Hearts(),
                    }
                );

                // Test case 2: High straight flush (10-J-Q-K-A of spades)
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ten().Spades(),
                        cardBuilder.Jack().Spades(),
                        cardBuilder.Queen().Spades(),
                        cardBuilder.King().Spades(),
                        cardBuilder.Ace().Spades(),
                        cardBuilder.Eight().Clubs(),
                        cardBuilder.Four().Diamonds()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Spades(),
                        cardBuilder.King().Spades(),
                        cardBuilder.Queen().Spades(),
                        cardBuilder.Jack().Spades(),
                        cardBuilder.Ten().Spades(), 
                    }
                );

                // Test case 3: Mixed cards with a straight flush in the middle (5-6-7-8-9 of diamonds)
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Two().Hearts(),
                        cardBuilder.Three().Clubs(),
                        cardBuilder.Five().Diamonds(),
                        cardBuilder.Six().Diamonds(),
                        cardBuilder.Seven().Diamonds(),
                        cardBuilder.Eight().Diamonds(),
                        cardBuilder.Nine().Diamonds()
                    },
                    new List<Card>
                    {
                        cardBuilder.Nine().Diamonds(),
                        cardBuilder.Eight().Diamonds(),
                        cardBuilder.Seven().Diamonds(),
                        cardBuilder.Six().Diamonds(),
                        cardBuilder.Five().Diamonds(),
                    }
                );

                // Test case 4: Non-straight flush (testing the condition where cards are not in sequence)
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Four().Diamonds(),
                        cardBuilder.Six().Diamonds(),
                        cardBuilder.Eight().Diamonds(),
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.King().Clubs()
                    },
                    null
                );
            }
        }
        [TestCaseSource(nameof(StraightFlushFinderGetHandTestCases))]
        public void StraightFlushFinder_GetHand_ReturnsCorrectHand(List<Card> cardsToFindHandFrom, List<Card> expectedHand)
        {
            //assert
            StraightFlushFinder straightFlushFinder = new StraightFlushFinder();

            //act
            var hand = straightFlushFinder.GetHand(cardsToFindHandFrom);

            //assert
            hand.ShouldBe(expectedHand);
        }
        public static IEnumerable<TestCaseData> RoyalFlushFinderGetHandTestCases
        {
            get
            {
                CardBuilder cardBuilder = new CardBuilder();

                // Test case 1: Classic royal flush in hearts
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ten().Hearts(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Queen().Hearts(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.Four().Clubs(),
                        cardBuilder.Seven().Diamonds()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.King().Hearts(),
                        cardBuilder.Queen().Hearts(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Ten().Hearts(),
                    }
                );

                // Test case 2: Royal flush in spades
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ten().Spades(),
                        cardBuilder.Jack().Spades(),
                        cardBuilder.Queen().Spades(),
                        cardBuilder.King().Spades(),
                        cardBuilder.Ace().Spades(),
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Three().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Spades(),
                        cardBuilder.King().Spades(),
                        cardBuilder.Queen().Spades(),
                        cardBuilder.Jack().Spades(),
                        cardBuilder.Ten().Spades(),
                    }
                );

                // Test case 3: Royal flush in diamonds
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Jack().Diamonds(),
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.King().Diamonds(),
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.Three().Hearts(),
                        cardBuilder.Five().Clubs()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Diamonds(),
                        cardBuilder.King().Diamonds(),
                        cardBuilder.Queen().Diamonds(),
                        cardBuilder.Jack().Diamonds(),
                        cardBuilder.Ten().Diamonds(),
                    }
                );

                // Test case 4: Royal flush in clubs
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ten().Clubs(),
                        cardBuilder.Jack().Clubs(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.King().Clubs(),
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.Six().Diamonds(),
                        cardBuilder.Eight().Hearts()
                    },
                    new List<Card>
                    {
                        cardBuilder.Ace().Clubs(),
                        cardBuilder.King().Clubs(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.Jack().Clubs(),
                        cardBuilder.Ten().Clubs(),
                    }
                );

                // Test case 5: Mixed cards with a straight
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Ten().Diamonds(),
                        cardBuilder.Jack().Hearts(),
                        cardBuilder.Queen().Clubs(),
                        cardBuilder.King().Spades(),
                        cardBuilder.Ace().Hearts(),
                        cardBuilder.Two().Clubs(),
                        cardBuilder.Four().Diamonds()
                    },
                    null
                );

                // Test case 6: No royal flush present (missing key cards)
                yield return new TestCaseData(
                    new List<Card>
                    {
                        cardBuilder.Two().Diamonds(),
                        cardBuilder.Three().Hearts(),
                        cardBuilder.Four().Clubs(),
                        cardBuilder.Five().Spades(),
                        cardBuilder.Six().Diamonds(),
                        cardBuilder.Seven().Clubs(),
                        cardBuilder.Eight().Hearts()
                    },
                    null
                );
            }
        }
        [TestCaseSource(nameof(RoyalFlushFinderGetHandTestCases))]
        public void RoyalFlushFinder_GetHand_ReturnsCorrectHand(List<Card> cardsToFindHandFrom, List<Card> expectedHand)
        {
            //assert
            RoyalFlushFinder royalFlushFinder = new RoyalFlushFinder();

            //act
            var hand = royalFlushFinder.GetHand(cardsToFindHandFrom);

            //assert
            hand.ShouldBe(expectedHand);
        }
    }
}
