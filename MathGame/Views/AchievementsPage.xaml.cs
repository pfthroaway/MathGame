using MathGame.Classes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MathGame.Views
{
    /// <summary>Interaction logic for AchievementsPage.xaml</summary>
    public partial class AchievementsPage
    {
        private Achievement selectedUnlocked = new Achievement();
        private Achievement selectedLocked = new Achievement();
        private readonly List<Achievement> lockedAchievements = new List<Achievement>(GameState.AllAchievements);

        /// <summary>Binds information to controls.</summary>
        private void BindLabels()
        {
            lstUnlocked.ItemsSource = GameState.CurrentPlayer.UnlockedAchievements;
            lstLocked.ItemsSource = lockedAchievements;
            GrpLocked.DataContext = selectedLocked;
            GrpUnlocked.DataContext = selectedUnlocked;
        }

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
            BindLabels();
        }

        private void LstUnlocked_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUnlocked = (Achievement)lstUnlocked.SelectedValue;
            BindLabels();
        }

        #endregion Page-Manipulation Methods
    }
}