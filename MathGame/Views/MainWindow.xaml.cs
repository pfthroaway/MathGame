using MathGame.Classes;
using System.Windows;

namespace MathGame.Views
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow
    {
        #region Window-Manipulation Methods

        public MainWindow()
        {
            InitializeComponent();
            GameState.MainWindow = this;
        }

        private async void WindowMain_Loaded(object sender, RoutedEventArgs e) => await GameState.LoadAll();

        #endregion Window-Manipulation Methods
    }
}