using System.Collections.Generic;
using System.ComponentModel;

namespace MathGame.Classes
{
    internal class Question : INotifyPropertyChanged
    {
        private int _int1, _int2, _solution;
        private string _operation;
        private List<int> _possibleSolutions = new List<int>();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>First Integer</summary>
        public int Int1
        {
            get => _int1;
            set { _int1 = value; OnPropertyChanged("Int1"); OnPropertyChanged("QuestionToString"); }
        }

        /// <summary>Second Integer</summary>
        public int Int2
        {
            get => _int2;
            set { _int2 = value; OnPropertyChanged("Int2"); OnPropertyChanged("QuestionToString"); }
        }

        /// <summary>Mathematical Operation being performed in this Question.</summary>
        public string Operation
        {
            get => _operation;
            set { _operation = value; OnPropertyChanged("Operation"); }
        }

        /// <summary>Correct answer to the Question.</summary>
        public int Solution
        {
            get => _solution;
            set { _solution = value; OnPropertyChanged("Int2"); }
        }

        /// <summary>List of possible solutions for this Question that the Player can choose from.</summary>
        public List<int> PossibleSolutions
        {
            get => _possibleSolutions;
            set { _possibleSolutions = value; OnPropertyChanged("PossibleSolutions"); }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Formatted Question.</summary>
        public string QuestionToString => Int1 + Operation + Int2;

        #endregion Helper Properties

        #region Constructors

        /// <summary>Initializes a default instance of Question.</summary>
        internal Question()
        {
        }

        /// <summary>Initializes an instance of Question by assigning Properties.</summary>
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

        /// <summary>Replaces this instance of Question with another instance.</summary>
        /// <param name="other">Instance to replace this instance</param>
        public Question(Question other) : this(other.Int1, other.Int2, other.Operation, other.Solution, other.PossibleSolutions)
        {
        }

        #endregion Constructors
    }
}