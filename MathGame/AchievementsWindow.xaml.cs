using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MathGame
{
    /// <summary>
    /// Interaction logic for AchievementsWindow.xaml
    /// </summary>
    public partial class AchievementsWindow : Window, INotifyPropertyChanged
    {
        internal MainWindow RefToMainWindow { get; set; }
        private Achievement selectedUnlocked = new Achievement();
        private Achievement selectedLocked = new Achievement();
        private List<Achievement> lockedAchievements = new List<Achievement>(GameState.AllAchievements);

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to controls.
        /// </summary>
        private void BindLabels()
        {
            lblTypeUnlocked.DataContext = selectedUnlocked;
            lblDescriptionUnlocked.DataContext = selectedUnlocked;
            lblPointsUnlocked.DataContext = selectedUnlocked;
            lblTypeLocked.DataContext = selectedLocked;
            lblDescriptionLocked.DataContext = selectedLocked;
            lblPointsLocked.DataContext = selectedLocked;
            lstUnlocked.ItemsSource = GameState.CurrentPlayer.UnlockedAchievements;
            lstLocked.ItemsSource = lockedAchievements;
        }

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        /// <summary>
        /// Loads all Locked Achievements.
        /// </summary>
        private void LoadLocked()
        {
            foreach (Achievement ach in GameState.CurrentPlayer.UnlockedAchievements)
                lockedAchievements.Remove(ach);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public AchievementsWindow()
        {
            InitializeComponent();
            LoadLocked();
            BindLabels();
        }

        private void lstLocked_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLocked = (Achievement)lstLocked.SelectedValue;
            lblTypeLocked.DataContext = selectedLocked;
            lblDescriptionLocked.DataContext = selectedLocked;
            lblPointsLocked.DataContext = selectedLocked;
        }

        private void lstUnlocked_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUnlocked = (Achievement)lstUnlocked.SelectedValue;
            lblTypeUnlocked.DataContext = selectedUnlocked;
            lblDescriptionUnlocked.DataContext = selectedUnlocked;
            lblPointsUnlocked.DataContext = selectedUnlocked;
        }

        private void windowAchievements_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefToMainWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}