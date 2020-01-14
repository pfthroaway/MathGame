using MathGame.Classes;
using System.Windows;

namespace MathGame.Views
{
    /// <summary>Interaction logic for NewAchievementPage.xaml</summary>
    public partial class NewAchievementPopup
    {
        private Achievement newAchievement = new Achievement();

        /// <summary>Loads the achievement into the Page.</summary>
        /// <param name="achievement">Newly earned achievement</param>
        internal void LoadAchievement(Achievement achievement)
        {
            newAchievement = achievement;
            DataContext = newAchievement;
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e) => ClosePage();

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage() => Close();

        public NewAchievementPopup(Achievement newAchievement, Window owner)
        {
            InitializeComponent();
            LoadAchievement(newAchievement);
            Owner = owner;
        }

        #endregion Page-Manipulation Methods
    }
}