using Extensions;
using MathGame.Classes;
using MathGame.Classes.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MathGame.Views
{
    /// <summary>Interaction logic for QuestionPage.xaml</summary>
    public partial class QuestionPage : INotifyPropertyChanged
    {
        private List<Question> _questionList = new List<Question>();
        private int _questionIndex, _attemptsRemaining, _score;
        private string _comment;
        private Operation _gameType;
        private Difficulty _difficulty;
        private Question _selectedQuestion = new Question();

        #region Modifying Properties

        /// <summary>Has the current Question been completed?</summary>
        public bool QuestionDone { get; set; }

        /// <summary>Which game type is currently being played?</summary>
        public Operation GameType
        {
            get => _gameType;
            set { _gameType = value; NotifyPropertyChanged(nameof(GameType)); }
        }

        /// <summary>What is the difficulty level of the current game type?</summary>
        public Difficulty Difficulty
        {
            get => _difficulty;
            set { _difficulty = value; NotifyPropertyChanged(nameof(Difficulty)); }
        }

        /// <summary>How much score has the current Player achieved this game?</summary>
        public int Score
        {
            get => _score;
            set { _score = value; NotifyPropertyChanged(nameof(Score), nameof(ScoreToString)); }
        }

        /// <summary>How many attempts does the current Player have remaining?</summary>
        public int AttemptsRemaining
        {
            get => _attemptsRemaining;
            set { _attemptsRemaining = value; NotifyPropertyChanged(nameof(AttemptsRemaining), nameof(AttemptsRemainingToString)); }
        }

        /// <summary>What is the index of the current Question in the List?</summary>
        public int QuestionIndex
        {
            get => _questionIndex;
            set { _questionIndex = value; NotifyPropertyChanged(nameof(QuestionsRemaining)); }
        }

        /// <summary>How many Questions does the current Player have left to complete?</summary>
        public string QuestionsRemaining => (QuestionList.Count - QuestionIndex - 1).ToString("N0");

        /// <summary>List of all Questions this game.</summary>
        internal List<Question> QuestionList
        {
            get => _questionList;
            set { _questionList = value; NotifyPropertyChanged(nameof(QuestionList)); }
        }

        /// <summary>Comment after a Question has been completed.</summary>
        public string Comment
        {
            get => _comment;
            set { _comment = value; NotifyPropertyChanged(nameof(Comment)); }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>How many attempts does the current Player have remaining? Formatted</summary>
        public string AttemptsRemainingToString => AttemptsRemaining.ToString("N0");

        /// <summary>How much score has the current Player achieved this game? Formatted</summary>
        public string ScoreToString => Score.ToString("N0");

        #endregion Helper Properties

        #region INotifyPropertyChanged Members

        /// <summary>The event that is raised when a property that calls the NotifyPropertyChanged method is changed.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Raises the PropertyChanged event alerting the WPF Framework to update the UI.</summary>
        /// <param name="propertyNames">The names of the properties to update in the UI.</param>
        protected void NotifyPropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        /// <summary>Raises the PropertyChanged event alerting the WPF Framework to update the UI.</summary>
        /// <param name="propertyName">The optional name of the property to update in the UI. If this is left blank, the name will be taken from the calling member via the CallerMemberName attribute.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Binds information to controls.</summary>
        private void BindLabels()
        {
            DataContext = this;
            lblQuestion.DataContext = _selectedQuestion;
        }

        #endregion INotifyPropertyChanged Members

        #region Achievement-Manipulation

        /// <summary>Checks all Achievements.</summary>
        private void CheckAllAchievements()
        {
            CheckGeneralAchievements();
            CheckAchievements();
        }

        /// <summary>Checks general Achievements.</summary>
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

        /// <summary>Checks Achievements for this game type.</summary>
        private void CheckAchievements()
        {
            switch (GameType)
            {
                case Operation.Addition:
                    switch (Difficulty)
                    {
                        case Difficulty.Easy:
                            GameState.CurrentPlayer.EasyAdditionWins++;
                            if (GameState.CurrentPlayer.EasyAdditionWins == 5)
                                EarnAchievement("5 Easy Addition Wins");
                            else if (GameState.CurrentPlayer.EasyAdditionWins == 20)
                                EarnAchievement("20 Easy Addition Wins");

                            break;

                        case Difficulty.Medium:
                            GameState.CurrentPlayer.MediumAdditionWins++;
                            if (GameState.CurrentPlayer.MediumAdditionWins == 5)
                                EarnAchievement("5 Medium Addition Wins");
                            else if (GameState.CurrentPlayer.MediumAdditionWins == 20)
                                EarnAchievement("20 Medium Addition Wins");
                            break;

                        case Difficulty.Hard:
                            GameState.CurrentPlayer.HardAdditionWins++;
                            if (GameState.CurrentPlayer.MediumAdditionWins == 5)
                                EarnAchievement("5 Medium Addition Wins");
                            else if (GameState.CurrentPlayer.MediumAdditionWins == 20)
                                EarnAchievement("20 Medium Addition Wins");

                            if (AttemptsRemaining == 3)
                                EarnAchievement("Perfect Game Hard Addition");
                            break;

                        case Difficulty.VeryHard:
                            GameState.CurrentPlayer.VeryHardAdditionWins++;
                            if (GameState.CurrentPlayer.VeryHardAdditionWins == 5)
                                EarnAchievement("5 Very Hard Addition Wins");
                            else if (GameState.CurrentPlayer.VeryHardAdditionWins == 20)
                                EarnAchievement("20 Very Hard Addition Wins");

                            if (AttemptsRemaining == 2)
                                EarnAchievement("Perfect Game Very Hard Addition");
                            break;
                    }
                    break;

                case Operation.Subtraction:
                    switch (Difficulty)
                    {
                        case Difficulty.Easy:
                            if (GameState.CurrentPlayer.EasySubtractionWins == 5)
                                EarnAchievement("5 Easy Subtraction Wins");
                            else if (GameState.CurrentPlayer.EasySubtractionWins == 20)
                                EarnAchievement("20 Easy Subtraction Wins");

                            break;

                        case Difficulty.Medium:
                            if (GameState.CurrentPlayer.MediumSubtractionWins == 5)
                                EarnAchievement("5 Medium Subtraction Wins");
                            else if (GameState.CurrentPlayer.MediumSubtractionWins == 20)
                                EarnAchievement("20 Medium Subtraction Wins");
                            break;

                        case Difficulty.Hard:
                            GameState.CurrentPlayer.HardAdditionWins++;
                            if (GameState.CurrentPlayer.HardSubtractionWins == 5)
                                EarnAchievement("5 Hard Subtraction Wins");
                            else if (GameState.CurrentPlayer.HardSubtractionWins == 20)
                                EarnAchievement("20 Hard Subtraction Wins");

                            if (AttemptsRemaining == 3)
                                EarnAchievement("Perfect Game Hard Subtraction");
                            break;

                        case Difficulty.VeryHard:
                            GameState.CurrentPlayer.VeryHardSubtractionWins++;
                            if (GameState.CurrentPlayer.VeryHardSubtractionWins == 5)
                                EarnAchievement("5 Very Hard Subtraction Wins");
                            else if (GameState.CurrentPlayer.VeryHardSubtractionWins == 20)
                                EarnAchievement("20 Very Hard Subtraction Wins");

                            if (AttemptsRemaining == 2)
                                EarnAchievement("Perfect Game Very Hard Subtraction");
                            break;
                    }
                    break;

                case Operation.Multiplication:
                    switch (Difficulty)
                    {
                        case Difficulty.Easy:
                            if (GameState.CurrentPlayer.EasyMultiplicationWins == 5)
                                EarnAchievement("5 Easy Multiplication Wins");
                            else if (GameState.CurrentPlayer.EasyMultiplicationWins == 20)
                                EarnAchievement("20 Easy Multiplication Wins");

                            break;

                        case Difficulty.Medium:
                            if (GameState.CurrentPlayer.MediumMultiplicationWins == 5)
                                EarnAchievement("5 Medium Multiplication Wins");
                            else if (GameState.CurrentPlayer.MediumMultiplicationWins == 20)
                                EarnAchievement("20 Medium Multiplication Wins");
                            break;

                        case Difficulty.Hard:
                            if (GameState.CurrentPlayer.HardMultiplicationWins == 5)
                                EarnAchievement("5 Hard Multiplication Wins");
                            else if (GameState.CurrentPlayer.HardMultiplicationWins == 20)
                                EarnAchievement("20 Hard Multiplication Wins");

                            if (AttemptsRemaining == 3)
                                EarnAchievement("Perfect Game Hard Multiplication");
                            break;

                        case Difficulty.VeryHard:
                            GameState.CurrentPlayer.VeryHardMultiplicationWins++;
                            if (GameState.CurrentPlayer.VeryHardMultiplicationWins == 5)
                                EarnAchievement("5 Very Hard Multiplication Wins");
                            else if (GameState.CurrentPlayer.VeryHardMultiplicationWins == 20)
                                EarnAchievement("20 Very Hard Multiplication Wins");

                            if (AttemptsRemaining == 2)
                                EarnAchievement("Perfect Game Very Hard Multiplication");
                            break;
                    }
                    break;

                case Operation.Division:
                    switch (Difficulty)
                    {
                        case Difficulty.Easy:
                            if (GameState.CurrentPlayer.EasyDivisionWins == 5)
                                EarnAchievement("5 Easy Division Wins");
                            else if (GameState.CurrentPlayer.EasyDivisionWins == 20)
                                EarnAchievement("20 Easy Division Wins");

                            break;

                        case Difficulty.Medium:
                            if (GameState.CurrentPlayer.MediumDivisionWins == 5)
                                EarnAchievement("5 Medium Division Wins");
                            else if (GameState.CurrentPlayer.MediumDivisionWins == 20)
                                EarnAchievement("20 Medium Division Wins");
                            break;

                        case Difficulty.Hard:
                            if (GameState.CurrentPlayer.HardDivisionWins == 5)
                                EarnAchievement("5 Hard Division Wins");
                            else if (GameState.CurrentPlayer.HardDivisionWins == 20)
                                EarnAchievement("20 Hard Division Wins");

                            if (AttemptsRemaining == 3)
                                EarnAchievement("Perfect Game Hard Division");
                            break;

                        case Difficulty.VeryHard:
                            GameState.CurrentPlayer.VeryHardDivisionWins++;
                            if (GameState.CurrentPlayer.VeryHardDivisionWins == 5)
                                EarnAchievement("5 Very Hard Division Wins");
                            else if (GameState.CurrentPlayer.VeryHardDivisionWins == 20)
                                EarnAchievement("20 Very Hard Division Wins");

                            if (AttemptsRemaining == 2)
                                EarnAchievement("Perfect Game Very Hard Division");
                            break;
                    }
                    break;
            }
        }

        /// <summary>Earns the Player an achievement.</summary>
        /// <param name="achievementName"></param>
        private void EarnAchievement(string achievementName)
        {
            Achievement newAchievement = GameState.AllAchievements.Find(ach => ach.Name == achievementName);
            if (!GameState.CurrentPlayer.UnlockedAchievements.Contains(newAchievement))
            {
                GameState.CurrentPlayer.UnlockedAchievements.Add(newAchievement);
                GameState.EarnAchievement(newAchievement);
            }
        }

        #endregion Achievement-Manipulation

        /// <summary>Loads the game.</summary>
        /// <param name="mathType">What math operation to use</param>
        /// <param name="difficulty">Difficulty of game</param>
        internal void LoadGame(Operation mathType, Difficulty difficulty)
        {
            GameType = mathType;
            Difficulty = difficulty;
            switch (GameType)
            {
                case Operation.Addition:
                    switch (Difficulty)
                    {
                        case Difficulty.Practice:
                            Addition(30, 100);
                            break;

                        case Difficulty.Easy:
                            Addition(10, 10);
                            break;

                        case Difficulty.Medium:
                            Addition(20, 15);
                            break;

                        case Difficulty.Hard:
                            Addition(30, 20);
                            break;

                        case Difficulty.VeryHard:
                            Addition(100, 25);
                            break;
                    }
                    break;

                case Operation.Subtraction:
                    switch (Difficulty)
                    {
                        case Difficulty.Practice:
                            Subtraction(30, 100);
                            break;

                        case Difficulty.Easy:
                            Subtraction(12, 10);
                            break;

                        case Difficulty.Medium:
                            Subtraction(20, 15);
                            break;

                        case Difficulty.Hard:
                            Subtraction(30, 20);
                            break;

                        case Difficulty.VeryHard:
                            Subtraction(100, 25);
                            break;
                    }
                    break;

                case Operation.Multiplication:
                    switch (Difficulty)
                    {
                        case Difficulty.Practice:
                            Multiplication(15, 100);
                            break;

                        case Difficulty.Easy:
                            Multiplication(5, 10);
                            break;

                        case Difficulty.Medium:
                            Multiplication(10, 15);
                            break;

                        case Difficulty.Hard:
                            Multiplication(15, 20);
                            break;

                        case Difficulty.VeryHard:
                            Multiplication(30, 25);
                            break;
                    }
                    break;

                case Operation.Division:
                    switch (Difficulty)
                    {
                        case Difficulty.Practice:
                            Division(50, 100);
                            break;

                        case Difficulty.Easy:
                            Division(20, 10);
                            break;

                        case Difficulty.Medium:
                            Division(40, 15);
                            break;

                        case Difficulty.Hard:
                            Division(60, 20);
                            break;

                        case Difficulty.VeryHard:
                            Division(100, 25);
                            break;
                    }
                    break;
            }

            switch (Difficulty)
            {
                case Difficulty.Practice:
                    AttemptsRemaining = 100;
                    break;

                case Difficulty.Easy:
                    AttemptsRemaining = 6;
                    break;

                case Difficulty.Medium:
                    AttemptsRemaining = 4;
                    break;

                case Difficulty.Hard:
                    AttemptsRemaining = 3;
                    break;

                case Difficulty.VeryHard:
                    AttemptsRemaining = 2;
                    break;
            }

            _selectedQuestion = QuestionList[QuestionIndex];
            BindLabels();
            Display();
        }

        #region Math Methods

        /// <summary>Creates a list of Addition questions.</summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        private void Addition(int high, int questions)
        {
            for (int i = 0; i < questions; i++)
            {
                int int1 = Functions.GenerateRandomNumber(high / 2, high);
                int int2 = Functions.GenerateRandomNumber(high / 2, high);
                int correctAnswer = int1 + int2;
                List<int> possibleAnswers = new List<int> { correctAnswer };
                for (int j = 0; j < 3; j++)
                {
                    int intNext = Functions.GenerateRandomNumber(high, high * 2);
                    while (possibleAnswers.Contains(intNext))
                        intNext = Functions.GenerateRandomNumber(high, high * 2);
                    possibleAnswers.Add(intNext);
                }

                possibleAnswers.Shuffle();
                QuestionList.Add(new Question(int1, int2, " + ", correctAnswer, possibleAnswers));
            }
        }

        /// <summary>Creates a list of Subtraction questions.</summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        private void Subtraction(int high, int questions)
        {
            for (int i = 0; i < questions; i++)
            {
                int int1 = Functions.GenerateRandomNumber(high / 2, high);
                int int2 = Functions.GenerateRandomNumber(high / 4, high / 2);

                int correctAnswer = int1 - int2;
                List<int> possibleAnswers = new List<int> { correctAnswer };
                for (int j = 0; j < 3; j++)
                {
                    int intNext = Functions.GenerateRandomNumber(high / 4, high);
                    while (possibleAnswers.Contains(intNext))
                        intNext = Functions.GenerateRandomNumber(high / 4, high);
                    possibleAnswers.Add(intNext);
                }

                possibleAnswers.Shuffle();
                QuestionList.Add(new Question(int1, int2, " - ", correctAnswer, possibleAnswers));
            }
        }

        /// <summary>Creates a list of Multiplication questions.</summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        private void Multiplication(int high, int questions)
        {
            for (int i = 0; i < questions; i++)
            {
                int int1 = Functions.GenerateRandomNumber(high / 4, high);
                int int2 = Functions.GenerateRandomNumber(high / 4, high);

                int correctAnswer = int1 * int2;
                List<int> possibleAnswers = new List<int> { correctAnswer };
                for (int j = 0; j < 3; j++)
                {
                    int intNext = Functions.GenerateRandomNumber(high / 4, high * high);
                    while (possibleAnswers.Contains(intNext))
                        intNext = Functions.GenerateRandomNumber(high / 4, high * high);
                    possibleAnswers.Add(intNext);
                }

                possibleAnswers.Shuffle();
                QuestionList.Add(new Question(int1, int2, " * ", correctAnswer, possibleAnswers));
            }
        }

        /// <summary>Creates a list of Division questions.</summary>
        /// <param name="high">Highest value for this question set</param>
        /// <param name="questions">Number of questions for this question set</param>
        private void Division(int high, int questions)
        {
            for (int i = 0; i < questions; i++)
            {
                int int1 = Functions.GenerateRandomNumber(high / 2, high);
                int int2 = Functions.GenerateRandomNumber(2, int1 - 2);

                while (int1 % int2 != 0 && int1 != int2)
                {
                    int1 = Functions.GenerateRandomNumber(high / 2, high);
                    int2 = Functions.GenerateRandomNumber(2, int1 - 2);
                }
                int correctAnswer = int1 / int2;
                List<int> possibleAnswers = new List<int> { correctAnswer };
                for (int j = 0; j < 3; j++)
                {
                    int intNextAnswer = Functions.GenerateRandomNumber(2, correctAnswer * 4);
                    while (possibleAnswers.Contains(intNextAnswer))
                        intNextAnswer = Functions.GenerateRandomNumber(2, correctAnswer * 4);
                    possibleAnswers.Add(intNextAnswer);
                }

                possibleAnswers.Shuffle();
                QuestionList.Add(new Question(int1, int2, " / ", correctAnswer, possibleAnswers));
            }
        }

        #endregion Math Methods

        #region Display-Manipulation Methods

        /// <summary>Clear all radio checked.</summary>
        private void ClearChecked()
        {
            radA.IsChecked = false;
            radB.IsChecked = false;
            radC.IsChecked = false;
            radD.IsChecked = false;
        }

        /// <summary>Disable all the buttons.</summary>
        private void DisableButtions()
        {
            QuestionDone = true;
            BtnCheck.IsEnabled = false;
            BtnNext.IsEnabled = false;
            BtnNewGame.IsEnabled = true;
        }

        /// <summary>Displays statistics and the current question.</summary>
        private void Display() => DisplayPossibleAnswers();

        /// <summary>Displays the question and possible answers.</summary>
        private void DisplayPossibleAnswers()
        {
            radA.Content = _selectedQuestion.PossibleSolutions[0].ToString("N0");
            radB.Content = _selectedQuestion.PossibleSolutions[1].ToString("N0");
            radC.Content = _selectedQuestion.PossibleSolutions[2].ToString("N0");
            radD.Content = _selectedQuestion.PossibleSolutions[3].ToString("N0");
        }

        #endregion Display-Manipulation Methods

        #region Question Results

        /// <summary>Checks whether the selected radio button is the correct answer.</summary>
        /// <param name="radChecked">Which radio button is checked?</param>
        private void CheckCorrect(int radChecked)
        {
            if (QuestionList[QuestionIndex].Solution == QuestionList[QuestionIndex].PossibleSolutions[radChecked])
                Correct();
            else
                Incorrect();
        }

        /// <summary>The question was answered correctly.</summary>
        private void Correct()
        {
            QuestionDone = true;
            Score += 10;
            BtnCheck.IsEnabled = false;
            if (QuestionIndex != QuestionList.Count - 1)
                BtnNext.IsEnabled = true;
            else
            {
                BtnNewGame.IsEnabled = true;
                GameState.CurrentPlayer.TotalWins++;
                CheckAllAchievements();
            }

            Comment = "Good job!";
        }

        /// <summary>The question was answered incorrectly.</summary>
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

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
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

        private void BtnNewGame_Click(object sender, RoutedEventArgs e) => ClosePage();

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            QuestionIndex++;
            _selectedQuestion = QuestionList[QuestionIndex];
            lblQuestion.DataContext = _selectedQuestion;
            QuestionDone = false;
            Display();
            Comment = "";
            BtnNext.IsEnabled = false;
            ClearChecked();
        }

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        private void ClosePage()
        {
            GameState.SavePlayer();
            GameState.GoBack();
        }

        /// <summary>If one of the Radio buttons has its Checked state changed</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!QuestionDone)
            {
                BtnCheck.IsEnabled = radA.IsChecked == true || radB.IsChecked == true || radC.IsChecked == true || radD.IsChecked == true;
            }
        }

        public QuestionPage()
        {
            InitializeComponent();
            ClearChecked();
        }

        #endregion Page-Manipulation Methods
    }
}