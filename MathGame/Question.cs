using System.Collections.Generic;
using System.ComponentModel;

namespace MathGame
{
    internal class Question : INotifyPropertyChanged
    {
        private int _int1, _int2, _solution;
        private string _operation = "";
        private List<int> _possibleSolutions = new List<int>();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        public int Int1
        {
            get { return _int1; }
            set { _int1 = value; OnPropertyChanged("Int1"); OnPropertyChanged("QuestionToString"); }
        }

        public int Int2
        {
            get { return _int2; }
            set { _int2 = value; OnPropertyChanged("Int2"); OnPropertyChanged("QuestionToString"); }
        }

        public string QuestionToString
        {
            get { return Int1 + Operation + Int2; }
        }

        public string Operation
        {
            get { return _operation; }
            set { _operation = value; OnPropertyChanged("Operation"); }
        }

        public int Solution
        {
            get { return _solution; }
            set { _solution = value; OnPropertyChanged("Int2"); }
        }

        public List<int> PossibleSolutions
        {
            get { return _possibleSolutions; }
            set { _possibleSolutions = value; OnPropertyChanged("PossibleSolutions"); }
        }

        #endregion Properties

        /// <summary>
        /// Initializes a default instance of Question.
        /// </summary>
        internal Question()
        {
        }

        /// <summary>
        /// Initializes an instance of Question by assigning Properties.
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        /// <param name="operation">Math operation</param>
        /// <param name="solution">Correct solution</param>
        /// <param name="solutionList">Possible solutions</param>
        internal Question(int num1, int num2, string operation, int solution, List<int> solutionList)
        {
            Int1 = num1;
            Int2 = num2;
            Operation = operation;
            Solution = solution;
            PossibleSolutions = solutionList;
        }

        /// <summary>
        /// Replaces this instance of Question with another instance.
        /// </summary>
        /// <param name="otherQuestion">Instance to replace this instance</param>
        public Question(Question otherQuestion)
        {
            Int1 = otherQuestion.Int1;
            Int2 = otherQuestion.Int2;
            Operation = otherQuestion.Operation;
            Solution = otherQuestion.Solution;
            PossibleSolutions = otherQuestion.PossibleSolutions;
        }
    }
}