using Extensions.DataTypeHelpers;
using MathGame.Classes;
using MathGame.Classes.Enums;
using System.Windows;
using System.Windows.Controls;

namespace MathGame.Views
{
    /// <summary>Interaction logic for GameOptionsPage.xaml</summary>
    public partial class GameOptionsPage : Page
    {
        /// <summary>Is this screen being loaded for a new <see cref="Player"/>?</summary>
        internal bool NewPlayer { get; set; }

        /// <summary>Opens the Question Page to play the game.</summary>
        /// <param name="gameType">What type of game is being played</param>
        /// <param name="cmbBox">ComboBox representing the difficulty of the game</param>
        private void Play(Operation gameType, ComboBox cmbBox)
        {
            Difficulty difficulty = EnumHelper.Parse<Difficulty>(cmbBox.Text.Replace(" ", ""));
            QuestionPage questionPage = new QuestionPage();
            questionPage.LoadGame(gameType, difficulty);
            GameState.Navigate(questionPage);
        }

        #region Button-Click Methods

        private void BtnAchievements_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new AchievementsPage());

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        private void BtnAddition_Click(object sender, RoutedEventArgs e) => Play(Operation.Addition, CmbAddition);

        private void BtnSubtraction_Click(object sender, RoutedEventArgs e) => Play(Operation.Subtraction, CmbSubtraction);

        private void BtnMultiplication_Click(object sender, RoutedEventArgs e) => Play(Operation.Multiplication, CmbMultiplication);

        private void BtnDivision_Click(object sender, RoutedEventArgs e) => Play(Operation.Division, CmbDivision);

        #endregion Button-Click Methods

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
        }

        #endregion Page-Manipulation Methods
    }
}