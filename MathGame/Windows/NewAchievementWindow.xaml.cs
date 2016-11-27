using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MathGame
{
    /// <summary>
    /// Interaction logic for NewAchievementWindow.xaml
    /// </summary>
    public partial class NewAchievementWindow : Window, INotifyPropertyChanged
    {
        private Achievement newAchievement = new Achievement();

        internal QuestionWindow RefToQuestionWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        /// <summary>
        /// Loads the achievement into the Window.
        /// </summary>
        /// <param name="achievement">Newly earned achievement</param>
        internal void LoadAchievement(Achievement achievement)
        {
            newAchievement = achievement;
            DataContext = newAchievement;
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
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

        public NewAchievementWindow()
        {
            InitializeComponent();
        }

        private void windowNewAchievement_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        #endregion Window-Manipulation Methods
    }
}