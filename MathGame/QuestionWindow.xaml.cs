using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace MathGame
{
    /// <summary>
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window, INotifyPropertyChanged
    {
        private List<Question> _questionList = new List<Question>();
        private int _questionIndex = 0, _attemptsRemaining = 0, _score = 0;
        private bool _questionDone = false;
        private string _gameType = "", _difficulty = "", _comment = "";
        private Question SelectedQuestion = new Question();

        #region Properties

        internal MainWindow RefToMainWindow { get; set; }

        public bool QuestionDone
        {
            get
            {
                return _questionDone;
            }

            set
            {
                _questionDone = value;
            }
        }

        public string GameType
        {
            get { return _gameType; }
            set { _gameType = value; OnPropertyChanged("GameType"); }
        }

        public string Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value; OnPropertyChanged("Difficulty"); }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; OnPropertyChanged("ScoreToString"); }
        }

        public string ScoreToString
        {
            get { return Score.ToString("N0"); }
        }

        public int AttemptsRemaining
        {
            get { return _attemptsRemaining; }
            set { _attemptsRemaining = value; OnPropertyChanged("AttemptsRemaining"); OnPropertyChanged("AttemptsRemainingToString"); }
        }

        public string AttemptsRemainingToString
        {
            get { return AttemptsRemaining.ToString("N0"); }
        }

        public int QuestionIndex
        {
            get { return _questionIndex; }
            set { _questionIndex = value; OnPropertyChanged("QuestionsRemaining"); }
        }

        public string QuestionsRemaining
        {
            get { return (QuestionList.Count - QuestionIndex - 1).ToString("N0"); }
        }

        internal List<Question> QuestionList
        {
            get { return _questionList; }
            set { _questionList = value; OnPropertyChanged("QuestionList"); }
        }

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; OnPropertyChanged("Comment"); }
        }

        #endregion Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to controls.
        /// </summary>
        private void BindLabels()
        {
            DataContext = this;
            lblQuestion.DataContext = SelectedQuestion;
        }

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Achievement-Manipulation

        /// <summary>
        /// Checks all Achievements.
        /// </summary>
        private void CheckAllAchievements()
        {
            CheckAchievements();
            CheckGeneralAchievements();
        }

        /// <summary>
        /// Checks general Achievements.
        /// </summary>
        private void CheckGeneralAchievements()
        {
            switch (GameState.CurrentPlayer.TotalWins)
            {
                case 1:
                    EarnAchievement("First Game");
                    break;

                case 10:
                    EarnAchievement("10 Wins");
                    break;

                case 25:

                    EarnAchievement("25 Wins");
                    break;

                case 50:

                    EarnAchievement("50 Wins");
                    break;

                case 100:
                    EarnAchievement("100 Wins");
                    break;

                case 250:
                    EarnAchievement("250 Wins");
                    break;
            }
        }

        /// <summary>
        /// Checks Achievements for this game type.
        /// </summary>
        private void CheckAchievements()
        {
            switch (GameType)
            {
                case "Addition":
                    switch (Difficulty)
                    {
                        case "Easy":
                            GameState.CurrentPlayer.EasyAdditionWins += 1;
                            if (GameState.CurrentPlayer.EasyAdditionWins == 5)
                                EarnAchievement("5 Easy Addition Wins");
                            else if (GameState.CurrentPlayer.EasyAdditionWins == 20)
                                EarnAchievement("20 Easy Addition Wins");

                            break;

                        case "Medium":
                            GameState.CurrentPlayer.MediumAdditionWins += 1;
                            if (GameState.CurrentPlayer.MediumAdditionWins == 5)
                                EarnAchievement("5 Medium Addition Wins");
                            else if (GameState.CurrentPlayer.MediumAdditionWins == 20)
                                EarnAchievement("20 Medium Addition Wins");
                            break;

                        case "Hard":
                            GameState.CurrentPlayer.HardAdditionWins += 1;
                            if (GameState.CurrentPlayer.MediumAdditionWins == 5)
                                EarnAchievement("5 Medium Addition Wins");
                            else if (GameState.CurrentPlayer.MediumAdditionWins == 20)
                                EarnAchievement("20 Medium Addition Wins");

                            if (AttemptsRemaining == 3 && !GameState.CurrentPlayer.UnlockedAchievements.Contains(GameState.AllAchievements.Find(ach => ach.Name == "Perfect Game Hard Addition")))
                                EarnAchievement("Perfect Game Hard Addition");
                            break;
                    }
                    break;

                case "Subtraction":
                    switch (Difficulty)
                    {
                        case "Easy":
                            if (GameState.CurrentPlayer.EasySubtractionWins == 5)
                                EarnAchievement("5 Easy Subtraction Wins");
                            else if (GameState.CurrentPlayer.EasySubtractionWins == 20)
                                EarnAchievement("20 Easy Subtraction Wins");

                            break;

                        case "Medium":
                            if (GameState.CurrentPlayer.MediumSubtractionWins == 5)
                                EarnAchievement("5 Medium Subtraction Wins");
                            else if (GameState.CurrentPlayer.MediumSubtractionWins == 20)
                                EarnAchievement("20 Medium Subtraction Wins");
                            break;

                        case "Hard":
                            GameState.CurrentPlayer.HardAdditionWins += 1;
                            if (GameState.CurrentPlayer.HardSubtractionWins == 5)
                                EarnAchievement("5 Hard Subtraction Wins");
                            else if (GameState.CurrentPlayer.HardSubtractionWins == 20)
                                EarnAchievement("20 Hard Subtraction Wins");

                            if (AttemptsRemaining == 3 && !GameState.CurrentPlayer.UnlockedAchievements.Contains(GameState.AllAchievements.Find(ach => ach.Name == "Perfect Game Hard Subtraction")))
                                EarnAchievement("Perfect Game Hard Subtraction");
                            break;
                    }
                    break;

                case "Multiplication":
                    switch (Difficulty)
                    {
                        case "Easy":
                            if (GameState.CurrentPlayer.EasyMultiplicationWins == 5)
                                EarnAchievement("5 Easy Multiplication Wins");
                            else if (GameState.CurrentPlayer.EasyMultiplicationWins == 20)
                                EarnAchievement("20 Easy Multiplication Wins");

                            break;

                        case "Medium":
                            if (GameState.CurrentPlayer.MediumMultiplicationWins == 5)
                                EarnAchievement("5 Medium Multiplication Wins");
                            else if (GameState.CurrentPlayer.MediumMultiplicationWins == 20)
                                EarnAchievement("20 Medium Multiplication Wins");
                            break;

                        case "Hard":
                            if (GameState.CurrentPlayer.HardMultiplicationWins == 5)
                                EarnAchievement("5 Hard Multiplication Wins");
                            else if (GameState.CurrentPlayer.HardMultiplicationWins == 20)
                                EarnAchievement("20 Hard Multiplication Wins");

                            if (AttemptsRemaining == 3 && !GameState.CurrentPlayer.UnlockedAchievements.Contains(GameState.AllAchievements.Find(ach => ach.Name == "Perfect Game Hard Multiplication")))
                                EarnAchievement("Perfect Game Hard Multiplication");
                            break;
                    }
                    break;

                case "Division":
                    switch (Difficulty)
                    {
                        case "Easy":
                            if (GameState.CurrentPlayer.EasyDivisionWins == 5)
                                EarnAchievement("5 Easy Division Wins");
                            else if (GameState.CurrentPlayer.EasyDivisionWins == 20)
                                EarnAchievement("20 Easy Division Wins");

                            break;

                        case "Medium":
                            if (GameState.CurrentPlayer.MediumDivisionWins == 5)
                                EarnAchievement("5 Medium Division Wins");
                            else if (GameState.CurrentPlayer.MediumDivisionWins == 20)
                                EarnAchievement("20 Medium Division Wins");
                            break;

                        case "Hard":
                            if (GameState.CurrentPlayer.HardDivisionWins == 5)
                                EarnAchievement("5 Hard Division Wins");
                            else if (GameState.CurrentPlayer.HardDivisionWins == 20)
                                EarnAchievement("20 Hard Division Wins");

                            if (AttemptsRemaining == 3 && !GameState.CurrentPlayer.UnlockedAchievements.Contains(GameState.AllAchievements.Find(ach => ach.Name == "Perfect Game Hard Division")))
                                EarnAchievement("Perfect Game Hard Division");
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Earns the Player an achievement.
        /// </summary>
        private void EarnAchievement(string achievementName)
        {
            GameState.CurrentPlayer.UnlockedAchievements.Add(new Achievement(GameState.AllAchievements.Find(ach => ach.Name == achievementName)));
            NewAchievementWindow newAchievementWindow = new NewAchievementWindow();
            newAchievementWindow.LoadAchievement(GameState.AllAchievements.Find(ach => ach.Name == achievementName));
            newAchievementWindow.Show();
        }

        #endregion Achievement-Manipulation

        /// <summary>
        /// Loads the game.
        /// </summary>
        /// <param name="mathType">What math operation to use</param>
        /// <param name="difficulty">Difficulty of game</param>
        internal void LoadGame(string mathType, string difficulty)
        {
            GameType = mathType;
            Difficulty = difficulty;
            switch (GameType)
            {
                case "Addition":
                    switch (Difficulty)
                    {
                        case "Practice":
                            Addition(30, 100);
                            break;

                        case "Easy":
                            Addition(10, 10);
                            break;

                        case "Medium":
                            Addition(20, 15);
                            break;

                        case "Hard":
                            Addition(30, 20);
                            break;
                    }
                    break;

                case "Subtraction":
                    switch (Difficulty)
                    {
                        case "Practice":
                            Subtraction(30, 100);
                            break;

                        case "Easy":
                            Subtraction(12, 10);
                            break;

                        case "Medium":
                            Subtraction(20, 15);
                            break;

                        case "Hard":
                            Subtraction(30, 20);
                            break;
                    }
                    break;

                case "Multiplication":
                    switch (Difficulty)
                    {
                        case "Practice":
                            Multiplication(15, 100);
                            break;

                        case "Easy":
                            Multiplication(5, 10);
                            break;

                        case "Medium":
                            Multiplication(10, 15);
                            break;

                        case "Hard":
                            Multiplication(15, 30);
                            break;
                    }
                    break;

                case "Division":
                    switch (Difficulty)
                    {
                        case "Practice":
                            Division(50, 100);
                            break;

                        case "Easy":
                            Division(20, 10);
                            break;

                        case "Medium":
                            Division(40, 15);
                            break;

                        case "Hard":
                            Division(60, 20);
                            break;
                    }
                    break;
            }

            switch (Difficulty)
            {
                case "Practice":
                    AttemptsRemaining = 100;
                    break;

                case "Easy":
                    AttemptsRemaining = 6;
                    break;

                case "Medium":
                    AttemptsRemaining = 4;
                    break;

                case "Hard":
                    AttemptsRemaining = 3;
                    break;
            }

            SelectedQuestion = QuestionList[QuestionIndex];
            BindLabels();
            Display();
        }

        #region Math Methods

        /// <summary>
        /// Creates a list of Addition questions.
        /// </summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        private void Addition(int high, int questions)
        {
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
                QuestionList.Add(new Question(int1, int2, " + ", correctAnswer, possibleAnswers));
            }
        }

        /// <summary>
        /// Creates a list of Subtraction questions.
        /// </summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        private void Subtraction(int high, int questions)
        {
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
                QuestionList.Add(new Question(int1, int2, " - ", correctAnswer, possibleAnswers));
            }
        }

        /// <summary>
        /// Creates a list of Multiplication questions.
        /// </summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        private void Multiplication(int high, int questions)
        {
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
                QuestionList.Add(new Question(int1, int2, " * ", correctAnswer, possibleAnswers));
            }
        }

        /// <summary>
        /// Creates a list of Division questions.
        /// </summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        private void Division(int high, int questions)
        {
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
                QuestionList.Add(new Question(int1, int2, " / ", correctAnswer, possibleAnswers));
            }
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
            QuestionDone = true;
            btnCheck.IsEnabled = false;
            btnNext.IsEnabled = false;
            btnNewGame.IsEnabled = true;
        }

        /// <summary>
        /// Displays statistics and the current question.
        /// </summary>
        private void Display()
        {
            DisplayPossibleAnswers();
        }

        /// <summary>
        /// Displays the question and possible answers.
        /// </summary>
        private void DisplayPossibleAnswers()
        {
            radA.Content = SelectedQuestion.PossibleSolutions[0].ToString("N0");
            radB.Content = SelectedQuestion.PossibleSolutions[1].ToString("N0");
            radC.Content = SelectedQuestion.PossibleSolutions[2].ToString("N0");
            radD.Content = SelectedQuestion.PossibleSolutions[3].ToString("N0");
        }

        #endregion Display-Manipulation Methods

        #region Question Results

        /// <summary>
        /// Check whether the selected radio button is the correct answer.
        /// </summary>
        /// <param name="radChecked">Which radio button is checked</param>
        private void CheckCorrect(int radChecked)
        {
            if (QuestionList[QuestionIndex].Solution == QuestionList[QuestionIndex].PossibleSolutions[radChecked])
                Correct();
            else
                Incorrect();
        }

        /// <summary>
        /// You got the question correct.
        /// </summary>
        private void Correct()
        {
            QuestionDone = true;
            Score += 10;
            btnCheck.IsEnabled = false;
            if (QuestionIndex != QuestionList.Count - 1)
                btnNext.IsEnabled = true;
            else
            {
                btnNewGame.IsEnabled = true;
                GameState.CurrentPlayer.TotalWins += 1;
                CheckAllAchievements();
            }

            Comment = "Good job!";
        }

        /// <summary>
        /// You got the question incorrect.
        /// </summary>
        private void Incorrect()
        {
            AttemptsRemaining--;
            Score -= 5;
            if (AttemptsRemaining > 0)
                lblComment.Text = "Incorrect. Try again.";
            else
            {
                lblComment.Text = "Incorrect. You lose!";
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
            QuestionIndex++;
            SelectedQuestion = QuestionList[QuestionIndex];
            lblQuestion.DataContext = SelectedQuestion;
            QuestionDone = false;
            Display();
            Comment = "";
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
            if (!QuestionDone)
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
            ClearChecked();
        }

        private void windowQuestion_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefToMainWindow.Show();
            GameState.SavePlayer();
        }

        #endregion Window-Manipulation Methods
    }
}