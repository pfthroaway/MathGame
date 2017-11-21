using Extensions;
using Extensions.Enums;
using MathGame.Classes.Database;
using MathGame.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MathGame.Classes
{
    /// <summary>Represents the current state of the game.</summary>
    internal static class GameState
    {
        internal static Player CurrentPlayer = new Player();
        internal static List<Achievement> AllAchievements = new List<Achievement>();
        private static readonly SQLiteDatabaseInteraction DatabaseInteraction = new SQLiteDatabaseInteraction();

        #region Navigation

        /// <summary>Instance of MainWindow currently loaded</summary>
        internal static MainWindow MainWindow { get; set; }

        /// <summary>Width of the Page currently being displayed in the MainWindow</summary>
        internal static double CurrentPageWidth { get; set; }

        /// <summary>Height of the Page currently being displayed in the MainWindow</summary>
        internal static double CurrentPageHeight { get; set; }

        /// <summary>Calculates the scale needed for the MainWindow.</summary>
        /// <param name="grid">Grid of current Page</param>
        internal static void CalculateScale(Grid grid)
        {
            CurrentPageHeight = grid.ActualHeight;
            CurrentPageWidth = grid.ActualWidth;
            MainWindow.CalculateScale();

            Page newPage = MainWindow.MainFrame.Content as Page;
            if (newPage != null)
                newPage.Style = (Style)MainWindow.FindResource("PageStyle");
        }

        /// <summary>Navigates to selected Page.</summary>
        /// <param name="newPage">Page to navigate to.</param>
        internal static void Navigate(Page newPage) => MainWindow.MainFrame.Navigate(newPage);

        /// <summary>Navigates to the previous Page.</summary>
        internal static void GoBack()
        {
            if (MainWindow.MainFrame.CanGoBack)
                MainWindow.MainFrame.GoBack();
        }

        #endregion Navigation

        /// <summary>Checks if a name can be added to the database.</summary>
        /// <param name="name">Name to be checked</param>
        /// <returns>True if name can be added</returns>
        internal static async Task<bool> CheckNewPlayerName(string name) => await DatabaseInteraction.CheckNewPlayerName(name);

        /// <summary>Loads all required information from the database.</summary>
        internal static async Task LoadAll()
        {
            DatabaseInteraction.VerifyDatabaseIntegrity();
            AllAchievements = await DatabaseInteraction.LoadAchievements();
        }

        /// <summary>Attempts to log in a Player.</summary>
        /// <param name="username">Username</param>
        /// <param name="password">Plaintext password</param>
        /// <returns>True if Player successfully logged in</returns>
        internal static async Task<bool> Login(string username, string password)
        {
            if (await DatabaseInteraction.VerifyPassword(username, password))
            {
                CurrentPlayer = await DatabaseInteraction.LoadPlayer(username);
                return true;
            }

            DisplayNotification("Unable to verify credentials.", "Math Game");
            return false;
        }

        /// <summary>Adds a new Player to the database.</summary>
        /// <param name="newPlayer">Player to be added</param>
        /// <returns>True if successful</returns>
        public static async Task<bool> NewPlayer(Player newPlayer) => await DatabaseInteraction.NewPlayer(newPlayer);

        /// <summary>Saves the current Player.</summary>
        internal static async void SavePlayer() => await DatabaseInteraction.SavePlayer(CurrentPlayer);

        #region Notification Management

        /// <summary>Displays a new Notification in a thread-safe way.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification window</param>
        internal static void DisplayNotification(string message, string title) => Application.Current.Dispatcher.Invoke(
            () => new Notification(message, title, NotificationButtons.OK, MainWindow).ShowDialog());

        /// <summary>Displays a new Notification in a thread-safe way and retrieves a boolean result upon its closing.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification window</param>
        /// <returns>Returns value of clicked button on Notification.</returns>
        internal static bool YesNoNotification(string message, string title) => Application.Current.Dispatcher.Invoke(() => (new Notification(message, title, NotificationButtons.YesNo, MainWindow).ShowDialog() == true));

        #endregion Notification Management
    }
}