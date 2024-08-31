using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using PokerHadChecker.Enums;
using PokerHadChecker.PokerHandCheckers;
using PokerHadChecker.Structs;
using PokerHadChecker.Handlers;
using PokerLibrary.Interfaces;
using PokerLibrary.PokerHandGenerators;

namespace PokerHandGuesser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPokerHandCombinationGenerator _generator = new BruteForceGenerator();
        Dictionary<Suit, string> suitToImageName = new Dictionary<Suit, string>();
        Dictionary<CardValue, string> valueToImageName = new Dictionary<CardValue, string>();
        Dictionary<HandCombinationRank, string> handRankToString = new Dictionary<HandCombinationRank, string>();
        List<Card> deck;
        List<Card> currentHand;
        int maxCardImageWidth = 145;
        public List<Card> GenerateDeck()
        {
            List<Card> deck = new List<Card>();
            for (Suit currentSuit = Suit.Clubs; currentSuit <= Suit.Diamonds; ++currentSuit)
            {
                for (CardValue currentValue = CardValue.Two; currentValue <= CardValue.Ace; ++currentValue)
                {
                    deck.Add(new Card(currentSuit, currentValue));
                }
            }
            return deck;
        }
        public List<Card> GenerateHand(List<Card> cardsDeck, int numberOfCards = 7)
        {
            List<Card> deckCopy = new List<Card>(cardsDeck);
            List<Card> generatedHand = new List<Card>();
            Random random = new Random();
            for (int j = 0; j < numberOfCards; ++j)
            {
                int index = random.Next(0, deckCopy.Count);
                generatedHand.Add(deckCopy[index]);

                deckCopy.Remove(deckCopy[index]);
            }
            return generatedHand;
        }
        public void DisplayCards(List<Card> cards, Grid displayGrid)
        {
            displayGrid.Children.Clear();
            displayGrid.ColumnDefinitions.Clear();
            for(int i = 0;i<cards.Count;++i)
            {
                displayGrid.ColumnDefinitions.Add(new ColumnDefinition());
                Image image = new Image();
                var path = AppDomain.CurrentDomain.BaseDirectory;
                image.Source = new BitmapImage(new Uri($"{path}\\CardsImagesJPG\\{this.suitToImageName[cards[i].CardSuit]}{this.valueToImageName[cards[i].Value]}.jpg"));
                image.Width = 700 / cards.Count;
                image.Margin = new Thickness(5, 10, 5, 10);
                image.Opacity = 0f;


                Grid.SetColumn(image, i);
                displayGrid.Children.Add(image);

                AnimateImage(image, i);

            }

        }
        private void AnimateImage(Image image, int index)
        {
            var animation = new DoubleAnimation();
            animation.From = 0.0f;
            animation.To = 1f;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(250));

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(250 * index);
            timer.Tick += (sender, e) =>
            {
                image.BeginAnimation(Image.OpacityProperty, animation);
                timer.Stop();
            };
            timer.Start();
        }
        public MainWindow()
        {
            InitializeComponent();
            this.deck = this.GenerateDeck();

            suitToImageName.Add(Suit.Clubs, "k");
            suitToImageName.Add(Suit.Diamonds, "l");
            suitToImageName.Add(Suit.Spades, "p");
            suitToImageName.Add(Suit.Hearts, "s");

            valueToImageName.Add(CardValue.Two, "2");
            valueToImageName.Add(CardValue.Three, "3");
            valueToImageName.Add(CardValue.Four, "4");
            valueToImageName.Add(CardValue.Five, "5");
            valueToImageName.Add(CardValue.Six, "6");
            valueToImageName.Add(CardValue.Seven, "7");
            valueToImageName.Add(CardValue.Eight, "8");
            valueToImageName.Add(CardValue.Nine, "9");
            valueToImageName.Add(CardValue.Ten, "10");
            valueToImageName.Add(CardValue.Jack, "j");
            valueToImageName.Add(CardValue.Queen, "q");
            valueToImageName.Add(CardValue.King, "k");
            valueToImageName.Add(CardValue.Ace, "a");

            handRankToString.Add(HandCombinationRank.RoyalFlush, "Royal flush");
            handRankToString.Add(HandCombinationRank.StraightFlush, "Straight flush");
            handRankToString.Add(HandCombinationRank.FourOfAKind, "Four of a kind");
            handRankToString.Add(HandCombinationRank.FullHouse, "Full house");
            handRankToString.Add(HandCombinationRank.Flush, "Flush");
            handRankToString.Add(HandCombinationRank.Straight, "Straight");
            handRankToString.Add(HandCombinationRank.ThreeOfAKind, "Three of a kind");
            handRankToString.Add(HandCombinationRank.TwoPair, "Two pair");
            handRankToString.Add(HandCombinationRank.Pair, "Pair");
            handRankToString.Add(HandCombinationRank.HighCard, "High card");

            foreach(var rank in handRankToString)
            {
                this.selectHandRankComboBox.Items.Add(rank.Value);
            }

            SetupHandGuessing();
        }
        public void SetupHandGuessing()
        {
            this.resultOutputLabel.Content = "Guess...";
            this.resultOutputLabel.Foreground = Brushes.White;
            Random rnd = new Random();
            this.currentHand = _generator.Generate(7, (HandCombinationRank)rnd.Next(1, 11));
            DisplayCards(currentHand, this.displayCardPanel);
            this.selectHandRankComboBox.SelectedItem = null;
            this.viewHandButton.IsEnabled = false;
        }

        private void nextHandButton_Click(object sender, RoutedEventArgs e)
        {
            SetupHandGuessing();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectHandRankComboBox.SelectedItem == null)
            {
                MessageBox.Show("Select name of the hand rank!");
                return;
            }

            AbstractPokerHandChecker handChecker = PokerChecker.GetPokerHandChecker(this.currentHand);
            DisplayCards(handChecker.GetHand(this.currentHand), this.displayCardPanel);



            if (this.selectHandRankComboBox.SelectedItem.ToString() == this.handRankToString[handChecker.CombinationRank])
            {
                this.resultOutputLabel.Content = "You are correct!";
                this.resultOutputLabel.Foreground = Brushes.LightGreen;
            }
            else
            {
                this.resultOutputLabel.Content = "You are wrong!";
                this.resultOutputLabel.Foreground = Brushes.Red;
            }

            this.resultOutputLabel.Content += $" The hand is: {this.handRankToString[handChecker.CombinationRank]}";
            this.viewHandButton.IsEnabled = true;

        }

        private void viewHandButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayCards(currentHand, this.displayCardPanel);
        }
    }
}
