using MathGame.Classes;
using MathGame.Classes.Enums;
using System.Windows;
using System.Windows.Controls;

namespace MathGame.Views
{
    /// <summary>Interaction logic for GameOptionsPage.xaml</summary>
    public partial class GameOptionsPage : Page
    {
        internal bool NewPlayer { get; set; }

        /// <summary>Opens the Question Page to play the game.</summary>
        /// <param name="gameType">What type of game is being played</param>
        /// <param name="difficulty">Difficulty of game</param>
        private void Play(Operation gameType, Difficulty difficulty)
        {
            QuestionPage questionPage = new QuestionPage();
            questionPage.LoadGame(gameType, difficulty);
            GameState.Navigate(questionPage);
        }

        #region Button-Click Methods

        private void BtnAchievements_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new AchievementsPage());

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        #endregion Button-Click Methods

        #region Addition Button-Click Methods

        private void BtnAdditionPractice_Click(object sender, RoutedEventArgs e) => Play(Operation.Addition, Difficulty.Practice);

        private void BtnAdditionEasy_Click(object sender, RoutedEventArgs e) => Play(Operation.Addition, Difficulty.Easy);

        private void BtnAdditionMedium_Click(object sender, RoutedEventArgs e) => Play(Operation.Addition, Difficulty.Medium);

        private void BtnAdditionHard_Click(object sender, RoutedEventArgs e) => Play(Operation.Addition, Difficulty.Hard);

        private void BtnAdditionVeryHard_Click(object sender, RoutedEventArgs e) => Play(Operation.Addition, Difficulty.VeryHard);

        #endregion Addition Button-Click Methods

        #region Subtraction Button-Click Methods

        private void BtnSubtractionPractice_Click(object sender, RoutedEventArgs e) => Play(Operation.Subtraction, Difficulty.Practice);

        private void BtnSubtractionEasy_Click(object sender, RoutedEventArgs e) => Play(Operation.Subtraction, Difficulty.Easy);

        private void BtnSubtractionMedium_Click(object sender, RoutedEventArgs e) => Play(Operation.Subtraction, Difficulty.Medium);

        private void BtnSubtractionHard_Click(object sender, RoutedEventArgs e) => Play(Operation.Subtraction, Difficulty.Hard);

        private void BtnSubtractionVeryHard_Click(object sender, RoutedEventArgs e) => Play(Operation.Subtraction, Difficulty.VeryHard);

        #endregion Subtraction Button-Click Methods

        #region Multiplication Button-Click Methods

        private void BtnMultiplicationPractice_Click(object sender, RoutedEventArgs e) => Play(Operation.Multiplication, Difficulty.Practice);

        private void BtnMultiplicationEasy_Click(object sender, RoutedEventArgs e) => Play(Operation.Multiplication, Difficulty.Easy);

        private void BtnMultiplicationMedium_Click(object sender, RoutedEventArgs e) => Play(Operation.Multiplication, Difficulty.Medium);

        private void BtnMultiplicationHard_Click(object sender, RoutedEventArgs e) => Play(Operation.Multiplication, Difficulty.Hard);

        private void BtnMultiplicationVeryHard_Click(object sender, RoutedEventArgs e) => Play(Operation.Multiplication, Difficulty.VeryHard);

        #endregion Multiplication Button-Click Methods

        #region Division Button-Click Methods

        private void BtnDivisionPractice_Click(object sender, RoutedEventArgs e) => Play(Operation.Division, Difficulty.Practice);

        private void BtnDivisionEasy_Click(object sender, RoutedEventArgs e) => Play(Operation.Division, Difficulty.Easy);

        private void BtnDivisionMedium_Click(object sender, RoutedEventArgs e) => Play(Operation.Division, Difficulty.Medium);

        private void BtnDivisionHard_Click(object sender, RoutedEventArgs e) => Play(Operation.Division, Difficulty.Hard);

        private void BtnDivisionVeryHard_Click(object sender, RoutedEventArgs e) => Play(Operation.Division, Difficulty.VeryHard);

        #endregion Division Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage() => GameState.GoBack();

        public GameOptionsPage() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (NewPlayer)
            {
                GameState.MainWindow.MainFrame.RemoveBackEntry();
                NewPlayer = false;
            }
            GameState.CalculateScale(Grid);
        }

        #endregion Page-Manipulation Methods
    }
}