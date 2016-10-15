using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MathGame_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Math Methods

        /// <summary>
        /// Opens the Question Window for Addition questions.
        /// </summary>
        /// <param name="high">Highest number allowed for selected difficulty.</param>
        /// <param name="questions">Number of questions to be generated.</param>
        private void Addition(int high, int questions, bool practice = false)
        {
            QuestionWindow questionWindow = new QuestionWindow();
            questionWindow.Addition(high, questions);
            questionWindow.Show();
            questionWindow.RefToMainWindow = this;
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Opens the Question Window for Subtraction questions.
        /// </summary>
        /// <param name="high">Highest number allowed for selected difficulty.</param>
        /// <param name="questions">Number of questions to be generated.</param>
        private void Subtraction(int high, int questions, bool practice = false)
        {
            QuestionWindow questionWindow = new QuestionWindow();
            questionWindow.Subtraction(high, questions);
            questionWindow.Show();
            questionWindow.RefToMainWindow = this;
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Opens the Question Window for Multiplication questions.
        /// </summary>
        /// <param name="high">Highest number allowed for selected difficulty.</param>
        /// <param name="questions">Number of questions to be generated.</param>
        private void Multiplication(int high, int questions, bool practice = false)
        {
            QuestionWindow questionWindow = new QuestionWindow();
            questionWindow.Multiplication(high, questions);
            questionWindow.Show();
            questionWindow.RefToMainWindow = this;
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Opens the Question Window for Division questions.
        /// </summary>
        /// <param name="high">Highest number allowed for selected difficulty.</param>
        /// <param name="questions">Number of questions to be generated.</param>
        private void Division(int high, int questions, bool practice = false)
        {
            QuestionWindow questionWindow = new QuestionWindow();
            questionWindow.Division(high, questions);
            questionWindow.Show();
            questionWindow.RefToMainWindow = this;
            this.Visibility = Visibility.Hidden;
        }

        #endregion Math Methods

        #region Addition Button-Click Methods

        private void btnAdditionPractice_Click(object sender, RoutedEventArgs e)
        {
            Addition(30, 100, true);
        }

        private void btnAdditionEasy_Click(object sender, RoutedEventArgs e)
        {
            Addition(10, 10);
        }

        private void btnAdditionMedium_Click(object sender, RoutedEventArgs e)
        {
            Addition(20, 15);
        }

        private void btnAdditionHard_Click(object sender, RoutedEventArgs e)
        {
            Addition(30, 20);
        }

        #endregion Addition Button-Click Methods

        #region Subtraction Button-Click Methods

        private void btnSubtractionPractice_Click(object sender, RoutedEventArgs e)
        {
            Subtraction(30, 100, true);
        }

        private void btnSubtractionEasy_Click(object sender, RoutedEventArgs e)
        {
            Subtraction(12, 10);
        }

        private void btnSubtractionMedium_Click(object sender, RoutedEventArgs e)
        {
            Subtraction(20, 15);
        }

        private void btnSubtractionHard_Click(object sender, RoutedEventArgs e)
        {
            Subtraction(30, 20);
        }

        #endregion Subtraction Button-Click Methods

        #region Multiplication Button-Click Methods

        private void btnMultiplicationPractice_Click(object sender, RoutedEventArgs e)
        {
            Multiplication(15, 100, true);
        }

        private void btnMultiplicationEasy_Click(object sender, RoutedEventArgs e)
        {
            Multiplication(5, 10);
        }

        private void btnMultiplicationMedium_Click(object sender, RoutedEventArgs e)
        {
            Multiplication(10, 15);
        }

        private void btnMultiplicationHard_Click(object sender, RoutedEventArgs e)
        {
            Multiplication(15, 30);
        }

        #endregion Multiplication Button-Click Methods

        #region Division Button-Click Methods

        private void btnDivisionPractice_Click(object sender, RoutedEventArgs e)
        {
            Division(50, 100, true);
        }

        private void btnDivisionEasy_Click(object sender, RoutedEventArgs e)
        {
            Division(20, 10);
        }

        private void btnDivisionMedium_Click(object sender, RoutedEventArgs e)
        {
            Division(40, 15);
        }

        private void btnDivisionHard_Click(object sender, RoutedEventArgs e)
        {
            Division(60, 20);
        }

        #endregion Division Button-Click Methods

        #region Window-Manipulation Methods

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Window-Manipulation Methods
    }
}