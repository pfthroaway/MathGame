using MathGame.Classes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MathGame.Views
{
    /// <summary>Interaction logic for AchievementsPage.xaml</summary>
    public partial class AchievementsPage : INotifyPropertyChanged
    {
        private Achievement selectedUnlocked = new Achievement();
        private Achievement selectedLocked = new Achievement();
        private readonly List<Achievement> lockedAchievements = new List<Achievement>(GameState.AllAchievements);

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Binds information to controls.</summary>
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

        protected virtual void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        /// <summary>Loads all Locked Achievements.</summary>
        private void LoadLocked()
        {
            foreach (Achievement ach in GameState.CurrentPlayer.UnlockedAchievements)
                lockedAchievements.Remove(ach);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage() => GameState.GoBack();

        public AchievementsPage()
        {
            InitializeComponent();
            LoadLocked();
            BindLabels();
        }

        private void LstLocked_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLocked = (Achievement)lstLocked.SelectedValue;
            lblTypeLocked.DataContext = selectedLocked;
            lblDescriptionLocked.DataContext = selectedLocked;
            lblPointsLocked.DataContext = selectedLocked;
        }

        private void LstUnlocked_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUnlocked = (Achievement)lstUnlocked.SelectedValue;
            lblTypeUnlocked.DataContext = selectedUnlocked;
            lblDescriptionUnlocked.DataContext = selectedUnlocked;
            lblPointsUnlocked.DataContext = selectedUnlocked;
        }

        #endregion Page-Manipulation Methods
    }
}