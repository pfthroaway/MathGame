using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MathGame
{
    /// <summary>
    /// Interaction logic for NewPlayerWindow.xaml
    /// </summary>
    public partial class NewPlayerWindow : Window
    {
        internal LoginWindow RefToLoginWindow { get; set; }

        #region Button-Click Methods

        /// <summary>
        /// Checks whether the Create Button should be enabled.
        /// </summary>
        private void CheckCreateButton()
        {
            if (txtUsername.Text.Length > 0 && pswdPassword.Password.Length > 0 && pswdConfirmPassword.Password.Length > 0)
                btnCreate.IsEnabled = true;
            else
                btnCreate.IsEnabled = false;
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (pswdPassword.Password == pswdConfirmPassword.Password)
            {
                string username = txtUsername.Text;
                OleDbConnection con = new OleDbConnection();
                OleDbDataAdapter da = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                con.ConnectionString = GameState._DBPROVIDERANDSOURCE;

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
                    MessageBox.Show("This username has been taken. Please choose another.", "Math Game", MessageBoxButton.OK);
                }
                else
                {
                    string hashedPassword = PasswordHash.HashPassword(pswdPassword.Password);
                    OleDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Players([Username],[UserPassword])Values(@username,@hashedPassword)";

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    GameState.CurrentPlayer = new Player(username, hashedPassword, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new List<Achievement>());
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.RefToLoginWindow = RefToLoginWindow;
                    mainWindow.Show();
                    this.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                MessageBox.Show("Please ensure that the entered passwords match.", "MathGame", MessageBoxButton.OK);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        private void CloseWindow()
        {
            this.Close();
        }

        public NewPlayerWindow()
        {
            InitializeComponent();
            txtUsername.Focus();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckCreateButton();
        }

        private void pswdPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckCreateButton();
        }

        private void pswdConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckCreateButton();
        }

        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsername.SelectAll();
        }

        private void pswdPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pswdPassword.SelectAll();
        }

        private void pswdConfirmPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pswdConfirmPassword.SelectAll();
        }

        private void windowNewPlayer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefToLoginWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}