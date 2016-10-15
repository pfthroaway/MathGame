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
using System.Windows.Shapes;

namespace MathGame_WPF
{
    /// <summary>
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        private List<Question> questionList = new List<Question>();
        private string operation = "";
        private int questionIndex = 0;
        private int attemptsRemaining = 3;
        private int score = 0;
        private bool questionDone = false;

        internal MainWindow RefToMainWindow { get; set; }

        #region Math Methods

        /// <summary>
        /// Creates a list of Addition questions.
        /// </summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        internal void Addition(int high, int questions)
        {
            operation = " + ";

            for (int i = 0; i < questions; i++)
            {
                int int1 = ThreadSafeRandom.ThisThreadsRandom.Next(high / 2, high);
                int int2 = ThreadSafeRandom.ThisThreadsRandom.Next(high / 2, high);
                int correctAnswer = int1 + int2;
                List<int> possibleAnswers = new List<int>();
                possibleAnswers.Add(correctAnswer);
                for (int j = 0; j < 3; j++)
                {
                    int intNext = ThreadSafeRandom.ThisThreadsRandom.Next(high, high * 2);
                    while (possibleAnswers.Contains(intNext))
                        intNext = ThreadSafeRandom.ThisThreadsRandom.Next(high, high * 2);
                    possibleAnswers.Add(intNext);
                }

                possibleAnswers.Shuffle();
                questionList.Add(new Question(int1, int2, correctAnswer, possibleAnswers));
            }

            Display();
        }

        /// <summary>
        /// Creates a list of Subtraction questions.
        /// </summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        internal void Subtraction(int high, int questions)
        {
            operation = " - ";

            for (int i = 0; i < questions; i++)
            {
                int int1 = ThreadSafeRandom.ThisThreadsRandom.Next(high / 2, high);
                int int2 = ThreadSafeRandom.ThisThreadsRandom.Next(high / 4, high / 2);

                int correctAnswer = int1 - int2;
                List<int> possibleAnswers = new List<int>();
                possibleAnswers.Add(correctAnswer);
                for (int j = 0; j < 3; j++)
                {
                    int intNext = ThreadSafeRandom.ThisThreadsRandom.Next(high / 4, high);
                    while (possibleAnswers.Contains(intNext))
                        intNext = ThreadSafeRandom.ThisThreadsRandom.Next(high / 4, high);
                    possibleAnswers.Add(intNext);
                }

                possibleAnswers.Shuffle();
                questionList.Add(new Question(int1, int2, correctAnswer, possibleAnswers));
            }

            Display();
        }

        /// <summary>
        /// Creates a list of Multiplication questions.
        /// </summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        internal void Multiplication(int high, int questions)
        {
            operation = " * ";

            for (int i = 0; i < questions; i++)
            {
                int int1 = ThreadSafeRandom.ThisThreadsRandom.Next(high / 4, high);
                int int2 = ThreadSafeRandom.ThisThreadsRandom.Next(high / 4, high);

                int correctAnswer = int1 * int2;
                List<int> possibleAnswers = new List<int>();
                possibleAnswers.Add(correctAnswer);
                for (int j = 0; j < 3; j++)
                {
                    int intNext = ThreadSafeRandom.ThisThreadsRandom.Next(high / 4, high * high);
                    while (possibleAnswers.Contains(intNext))
                        intNext = ThreadSafeRandom.ThisThreadsRandom.Next(high / 4, high * high);
                    possibleAnswers.Add(intNext);
                }

                possibleAnswers.Shuffle();
                questionList.Add(new Question(int1, int2, correctAnswer, possibleAnswers));
            }

            Display();
        }

        /// <summary>
        /// Creates a list of Division questions.
        /// </summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        internal void Division(int high, int questions)
        {
            operation = " / ";

            for (int i = 0; i < questions; i++)
            {
                int int1 = ThreadSafeRandom.ThisThreadsRandom.Next(high / 2, high);
                int int2 = ThreadSafeRandom.ThisThreadsRandom.Next(2, int1 - 2);

                while (int1 % int2 != 0 && int1 != int2)
                {
                    int1 = ThreadSafeRandom.ThisThreadsRandom.Next(high / 2, high);
                    int2 = ThreadSafeRandom.ThisThreadsRandom.Next(2, int1 - 2);
                }
                int correctAnswer = int1 / int2;
                List<int> possibleAnswers = new List<int>();
                possibleAnswers.Add(correctAnswer);
                for (int j = 0; j < 3; j++)
                {
                    int intNextAnswer = ThreadSafeRandom.ThisThreadsRandom.Next(2, high - 2);
                    while (possibleAnswers.Contains(intNextAnswer))
                        intNextAnswer = ThreadSafeRandom.ThisThreadsRandom.Next(2, high - 2);
                    possibleAnswers.Add(intNextAnswer);
                }

                possibleAnswers.Shuffle();
                questionList.Add(new Question(int1, int2, correctAnswer, possibleAnswers));
            }

            Display();
        }

        #endregion Math Methods

        #region Display-Manipulation Methods

        /// <summary>
        /// Clear all radio checked.
        /// </summary>
        private void ClearChecked()
        {
            radA.IsChecked = false;
            radB.IsChecked = false;
            radC.IsChecked = false;
            radD.IsChecked = false;
        }

        /// <summary>
        /// Disable all the buttons.
        /// </summary>
        private void DisableButtions()
        {
            questionDone = true;
            btnCheck.IsEnabled = false;
            btnNext.IsEnabled = false;
            btnNewGame.IsEnabled = true;
        }

        /// <summary>
        /// Displays statistics and the current question.
        /// </summary>
        private void Display()
        {
            DisplayStatistics();
            DisplayQuestion();
        }

        /// <summary>
        /// Displays the statistics.
        /// </summary>
        private void DisplayStatistics()
        {
            lblScore.Text = score.ToString("N0");
            lblAttempts.Text = attemptsRemaining.ToString("N0");
            lblQuestionsRemaining.Text = (questionList.Count - 1 - questionIndex).ToString();
        }

        /// <summary>
        /// Displays the question and possible answers.
        /// </summary>
        private void DisplayQuestion()
        {
            lblQuestion.Text = questionList[questionIndex].Int1.ToString("N0") + operation + questionList[questionIndex].Int2.ToString("N0");
            radA.Content = questionList[questionIndex].PossibleSolutions[0].ToString("N0");
            radB.Content = questionList[questionIndex].PossibleSolutions[1].ToString("N0");
            radC.Content = questionList[questionIndex].PossibleSolutions[2].ToString("N0");
            radD.Content = questionList[questionIndex].PossibleSolutions[3].ToString("N0");
        }

        #endregion Display-Manipulation Methods

        /// <summary>
        /// Check whether the selected radio button is the correct answer.
        /// </summary>
        /// <param name="radChecked">Which radio button is checked</param>
        private void CheckCorrect(int radChecked)
        {
            if (questionList[questionIndex].Solution == questionList[questionIndex].PossibleSolutions[radChecked])
                Correct();
            else
                Incorrect();
        }

        #region Question Results

        /// <summary>
        /// You got the question correct.
        /// </summary>
        private void Correct()
        {
            questionDone = true;
            score += 10;
            btnCheck.IsEnabled = false;
            if (questionIndex != questionList.Count - 1)
                btnNext.IsEnabled = true;
            else
                btnNewGame.IsEnabled = true;
            DisplayStatistics();
            lblComment.Text = "Good job!";
        }

        /// <summary>
        /// You got the question incorrect.
        /// </summary>
        private void Incorrect()
        {
            attemptsRemaining--;
            score -= 5;
            DisplayStatistics();
            if (attemptsRemaining > 0)
                lblComment.Text = "Incorrect. Try again.";
            else
            {
                lblComment.Text = "You lose!";
                DisableButtions();
            }
        }

        #endregion Question Results

        #region Button-Click Methods

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (radA.IsChecked == true)
                CheckCorrect(0);
            else if (radB.IsChecked == true)
                CheckCorrect(1);
            else if (radC.IsChecked == true)
                CheckCorrect(2);
            else if (radD.IsChecked == true)
                CheckCorrect(3);
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            questionIndex++;
            questionDone = false;
            Display();
            lblComment.Text = "";
            btnNext.IsEnabled = false;
            ClearChecked();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        private void CloseWindow()
        {
            this.Close();
        }

        /// <summary>
        /// If one of the Radio buttons has its Checked state changed
        /// </summary>
        private void CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!questionDone)
            {
                if (radA.IsChecked == true || radB.IsChecked == true || radC.IsChecked == true || radD.IsChecked == true)
                    btnCheck.IsEnabled = true;
                else
                    btnCheck.IsEnabled = false;
            }
        }

        public QuestionWindow()
        {
            InitializeComponent();
        }

        private void windowQuestion_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefToMainWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}