using MathGame.Classes;
using System.Windows;
using System.Windows.Controls;

namespace MathGame.Views
{
    /// <summary>Interaction logic for LoginPage.xaml</summary>
    public partial class LoginPage
    {
        #region Button-Click Methods

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (await GameState.Login(TxtUsername.Text, PswdPassword.Password))
            {
                TxtUsername.Clear();
                PswdPassword.Clear();
                TxtUsername.Focus();

                GameState.Navigate(new GameOptionsPage());
            }
        }

        private void BtnNewPlayer_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new NewPlayerPage());

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        public LoginPage()
        {
            InitializeComponent();
            TxtUsername.Focus();
        }

        /// <summary>Enables the Login Button if both input boxes have text.</summary>
        private void TextChanged() => BtnLogin.IsEnabled = TxtUsername.Text.Length > 0 && PswdPassword.Password.Length > 0;

        private void TxtUsername_TextChanged(object sender, TextChangedEventArgs e) => TextChanged();

        private void PswdPassword_PasswordChanged(object sender, RoutedEventArgs e) => TextChanged();

        private void TxtUsername_GotFocus(object sender, RoutedEventArgs e) => TxtUsername.SelectAll();

        private void PswdPassword_GotFocus(object sender, RoutedEventArgs e) => PswdPassword.SelectAll();

        #endregion Page-Manipulation Methods
    }
}