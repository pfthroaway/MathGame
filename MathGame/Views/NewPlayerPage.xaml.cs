using Extensions.Encryption;
using MathGame.Classes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MathGame.Views
{
    /// <summary>Interaction logic for NewPlayerPage.xaml</summary>
    public partial class NewPlayerPage
    {
        #region Button-Click Methods

        /// <summary>Checks whether the Create Button should be enabled.</summary>
        private void CheckCreateButton() => BtnCreate.IsEnabled = TxtUsername.Text.Length > 0 && PswdPassword.Password.Length > 0 && PswdConfirmPassword.Password.Length > 0;

        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text;
            if (username.Length >= 4 && await GameState.CheckNewPlayerName(username) && PswdPassword.Password == PswdConfirmPassword.Password && PswdPassword.Password.Length >= 4)
            {
                string hashedPassword = PBKDF2.HashPassword(PswdPassword.Password);
                Player newPlayer = new Player(username, hashedPassword, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new List<Achievement>());
                if (await GameState.NewPlayer(newPlayer))
                {
                    GameState.CurrentPlayer = new Player(newPlayer);
                    GameState.Navigate(new GameOptionsPage { NewPlayer = true });
                }
            }
            else if (username.Length < 4)
                GameState.DisplayNotification("Usernames must be at least 4 characters in length.", "Math Game");
            else if (PswdPassword.Password != PswdConfirmPassword.Password)
                GameState.DisplayNotification("Please ensure that the entered passwords match.", "Math Game");
            else if (PswdPassword.Password.Length < 4)
                GameState.DisplayNotification("Passwords must be at least 4 characters in length.", "Math Game");
            else
                GameState.DisplayNotification("That username is already taken.", "Math Game");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage() => GameState.GoBack();

        public NewPlayerPage()
        {
            InitializeComponent();
            TxtUsername.Focus();
        }

        private void TxtUsername_TextChanged(object sender, TextChangedEventArgs e) => CheckCreateButton();

        private void PswdPassword_PasswordChanged(object sender, RoutedEventArgs e) => CheckCreateButton();

        private void PswdConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e) => CheckCreateButton();

        private void TxtUsername_GotFocus(object sender, RoutedEventArgs e) => TxtUsername.SelectAll();

        private void PswdPassword_GotFocus(object sender, RoutedEventArgs e) => PswdPassword.SelectAll();

        private void PswdConfirmPassword_GotFocus(object sender, RoutedEventArgs e) => PswdConfirmPassword.SelectAll();

        private void Page_Loaded(object sender, RoutedEventArgs e) => GameState.CalculateScale(Grid);

        #endregion Page-Manipulation Methods
    }
}