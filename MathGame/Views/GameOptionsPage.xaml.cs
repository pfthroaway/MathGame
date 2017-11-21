using MathGame.Classes;
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
        private void Play(string gameType, string difficulty)
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

        private void BtnAdditionPractice_Click(object sender, RoutedEventArgs e) => Play("Addition", "Practice");

        private void BtnAdditionEasy_Click(object sender, RoutedEventArgs e) => Play("Addition", "Easy");

        private void BtnAdditionMedium_Click(object sender, RoutedEventArgs e) => Play("Addition", "Medium");

        private void BtnAdditionHard_Click(object sender, RoutedEventArgs e) => Play("Addition", "Hard");

        #endregion Addition Button-Click Methods

        #region Subtraction Button-Click Methods

        private void BtnSubtractionPractice_Click(object sender, RoutedEventArgs e) => Play("Subtraction", "Practice");

        private void BtnSubtractionEasy_Click(object sender, RoutedEventArgs e) => Play("Subtraction", "Easy");

        private void BtnSubtractionMedium_Click(object sender, RoutedEventArgs e) => Play("Subtraction", "Medium");

        private void BtnSubtractionHard_Click(object sender, RoutedEventArgs e) => Play("Subtraction", "Hard");

        #endregion Subtraction Button-Click Methods

        #region Multiplication Button-Click Methods

        private void BtnMultiplicationPractice_Click(object sender, RoutedEventArgs e) => Play("Multiplication", "Practice");

        private void BtnMultiplicationEasy_Click(object sender, RoutedEventArgs e) => Play("Multiplication", "Easy");

        private void BtnMultiplicationMedium_Click(object sender, RoutedEventArgs e) => Play("Multiplication", "Medium");

        private void BtnMultiplicationHard_Click(object sender, RoutedEventArgs e) => Play("Multiplication", "Hard");

        #endregion Multiplication Button-Click Methods

        #region Division Button-Click Methods

        private void BtnDivisionPractice_Click(object sender, RoutedEventArgs e) => Play("Division", "Practice");

        private void BtnDivisionEasy_Click(object sender, RoutedEventArgs e) => Play("Division", "Easy");

        private void BtnDivisionMedium_Click(object sender, RoutedEventArgs e) => Play("Division", "Medium");

        private void BtnDivisionHard_Click(object sender, RoutedEventArgs e) => Play("Division", "Hard");

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