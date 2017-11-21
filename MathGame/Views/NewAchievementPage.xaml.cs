using MathGame.Classes;
using System.ComponentModel;
using System.Windows;

namespace MathGame.Views
{
    /// <summary>Interaction logic for NewAchievementPage.xaml</summary>
    public partial class NewAchievementPage : INotifyPropertyChanged
    {
        private Achievement newAchievement = new Achievement();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

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
        private void ClosePage() => GameState.GoBack();

        public NewAchievementPage() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e) => GameState.CalculateScale(Grid);

        #endregion Page-Manipulation Methods
    }
}