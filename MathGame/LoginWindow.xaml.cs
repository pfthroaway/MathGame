using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MathGame
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private List<Achievement> LoadAchievements(string achievements)
        {
            List<Achievement> unlockedAchievements = new List<Achievement>();

            string[] arrAchievements = achievements.Split(',');
            foreach (string str in arrAchievements)
                unlockedAchievements.Add(GameState.AllAchievements.Find(ach => ach.Name == str));

            return unlockedAchievements;
        }

        #region Button-Click Methods

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            con.ConnectionString = GameState._DBPROVIDERANDSOURCE;
            string username = txtUsername.Text;
            string password = pswdPassword.Password;

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    string sql = "SELECT * FROM Players WHERE [Username] ='" + username + "'";
                    string table = "Players";
                    da = new OleDbDataAdapter(sql, con);
                    da.Fill(ds, table);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Filling DataSet", MessageBoxButton.OK);
                }
                finally { con.Close(); }
            });

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (PasswordHash.ValidatePassword(password, ds.Tables[0].Rows[0]["UserPassword"].ToString()))
                {
                    GameState.CurrentPlayer.Name = username;
                    GameState.CurrentPlayer.Password = password;
                    GameState.CurrentPlayer.TotalWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["TotalWins"].ToString());
                    GameState.CurrentPlayer.EasyAdditionWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["EasyAdditionWins"].ToString());
                    GameState.CurrentPlayer.MediumAdditionWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["MediumAdditionWins"].ToString());
                    GameState.CurrentPlayer.HardAdditionWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["HardAdditionWins"].ToString());
                    GameState.CurrentPlayer.EasySubtractionWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["EasySubtractionWins"].ToString());
                    GameState.CurrentPlayer.MediumSubtractionWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["MediumSubtractionWins"].ToString());
                    GameState.CurrentPlayer.HardSubtractionWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["HardSubtractionWins"].ToString());
                    GameState.CurrentPlayer.EasyMultiplicationWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["EasyMultiplicationWins"].ToString());
                    GameState.CurrentPlayer.MediumMultiplicationWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["MediumMultiplicationWins"].ToString());
                    GameState.CurrentPlayer.HardMultiplicationWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["HardMultiplicationWins"].ToString());
                    GameState.CurrentPlayer.EasyDivisionWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["EasyDivisionWins"].ToString());
                    GameState.CurrentPlayer.MediumDivisionWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["MediumDivisionWins"].ToString());
                    GameState.CurrentPlayer.HardDivisionWins = Int32Helper.Parse(ds.Tables[0].Rows[0]["HardDivisionWins"].ToString());
                    if (ds.Tables[0].Rows[0]["AchievementsUnlocked"].ToString().Length > 0)
                        GameState.CurrentPlayer.UnlockedAchievements = LoadAchievements(ds.Tables[0].Rows[0]["AchievementsUnlocked"].ToString());

                    txtUsername.Clear();
                    pswdPassword.Clear();
                    txtUsername.Focus();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.RefToLoginWindow = this;
                    mainWindow.Show();
                    this.Visibility = Visibility.Hidden;
                }
                else
                    MessageBox.Show("Invalid login", "MathGame", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Invalid login", "MathGame", MessageBoxButton.OK);
        }

        private void btnNewPlayer_Click(object sender, RoutedEventArgs e)
        {
            NewPlayerWindow newPlayerWindow = new NewPlayerWindow();
            newPlayerWindow.RefToLoginWindow = this;
            newPlayerWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        public LoginWindow()
        {
            InitializeComponent();
            GameState.LoadAll();
            txtUsername.Focus();
        }

        /// <summary>
        /// Enables the Login Button if both input boxes have text.
        /// </summary>
        private void TextChanged()
        {
            if (txtUsername.Text.Length > 0 && pswdPassword.Password.Length > 0)
                btnLogin.IsEnabled = true;
            else
                btnLogin.IsEnabled = false;
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged();
        }

        private void pswdPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TextChanged();
        }

        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsername.SelectAll();
        }

        private void pswdPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pswdPassword.SelectAll();
        }

        #endregion Window-Manipulation Methods
    }
}