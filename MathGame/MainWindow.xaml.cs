using System.Windows;

namespace MathGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal LoginWindow RefToLoginWindow { get; set; }

        /// <summary>
        /// Opens the Question Window to play the game.
        /// </summary>
        /// <param name="gameType">What type of game is being played</param>
        /// <param name="difficulty">Difficulty of game</param>
        private void Play(string gameType, string difficulty)
        {
            QuestionWindow questionWindow = new QuestionWindow();
            questionWindow.Show();
            questionWindow.LoadGame(gameType, difficulty);
            questionWindow.RefToMainWindow = this;
            this.Visibility = Visibility.Hidden;
        }

        #region Button-Click Methods

        private void btnAchievements_Click(object sender, RoutedEventArgs e)
        {
            AchievementsWindow achievementsWindow = new AchievementsWindow();
            achievementsWindow.RefToMainWindow = this;
            achievementsWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Addition Button-Click Methods

        private void btnAdditionPractice_Click(object sender, RoutedEventArgs e)
        {
            Play("Addition", "Practice");
        }

        private void btnAdditionEasy_Click(object sender, RoutedEventArgs e)
        {
            Play("Addition", "Easy");
        }

        private void btnAdditionMedium_Click(object sender, RoutedEventArgs e)
        {
            Play("Addition", "Medium");
        }

        private void btnAdditionHard_Click(object sender, RoutedEventArgs e)
        {
            Play("Addition", "Hard");
        }

        #endregion Addition Button-Click Methods

        #region Subtraction Button-Click Methods

        private void btnSubtractionPractice_Click(object sender, RoutedEventArgs e)
        {
            Play("Subtraction", "Practice");
        }

        private void btnSubtractionEasy_Click(object sender, RoutedEventArgs e)
        {
            Play("Subtraction", "Easy");
        }

        private void btnSubtractionMedium_Click(object sender, RoutedEventArgs e)
        {
            Play("Subtraction", "Medium");
        }

        private void btnSubtractionHard_Click(object sender, RoutedEventArgs e)
        {
            Play("Subtraction", "Hard");
        }

        #endregion Subtraction Button-Click Methods

        #region Multiplication Button-Click Methods

        private void btnMultiplicationPractice_Click(object sender, RoutedEventArgs e)
        {
            Play("Multiplication", "Practice");
        }

        private void btnMultiplicationEasy_Click(object sender, RoutedEventArgs e)
        {
            Play("Multiplication", "Easy");
        }

        private void btnMultiplicationMedium_Click(object sender, RoutedEventArgs e)
        {
            Play("Multiplication", "Medium");
        }

        private void btnMultiplicationHard_Click(object sender, RoutedEventArgs e)
        {
            Play("Multiplication", "Hard");
        }

        #endregion Multiplication Button-Click Methods

        #region Division Button-Click Methods

        private void btnDivisionPractice_Click(object sender, RoutedEventArgs e)
        {
            Play("Division", "Practice");
        }

        private void btnDivisionEasy_Click(object sender, RoutedEventArgs e)
        {
            Play("Division", "Easy");
        }

        private void btnDivisionMedium_Click(object sender, RoutedEventArgs e)
        {
            Play("Division", "Medium");
        }

        private void btnDivisionHard_Click(object sender, RoutedEventArgs e)
        {
            Play("Division", "Hard");
        }

        #endregion Division Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void windowMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefToLoginWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}