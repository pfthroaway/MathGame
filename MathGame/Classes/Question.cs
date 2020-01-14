using System.Collections.Generic;

namespace MathGame.Classes
{
    internal class Question : BaseINPC
    {
        private int _int1, _int2, _solution;
        private string _operation;
        private List<int> _possibleSolutions = new List<int>();

        #region Modifying Properties

        /// <summary>First Integer</summary>
        public int Int1
        {
            get => _int1;
            set { _int1 = value; NotifyPropertyChanged(nameof(Int1), nameof(QuestionToString)); }
        }

        /// <summary>Second Integer</summary>
        public int Int2
        {
            get => _int2;
            set { _int2 = value; NotifyPropertyChanged(nameof(Int2), nameof(QuestionToString)); }
        }

        /// <summary>Mathematical Operation being performed in this <see cref="Question"/>.</summary>
        public string Operation
        {
            get => _operation;
            set { _operation = value; NotifyPropertyChanged(nameof(Operation)); }
        }

        /// <summary>Correct answer to the <see cref="Question"/>.</summary>
        public int Solution
        {
            get => _solution;
            set { _solution = value; NotifyPropertyChanged(nameof(Solution)); }
        }

        /// <summary>List of possible solutions for this <see cref="Question"/> that the <see cref="Player"/> can choose from.</summary>
        public List<int> PossibleSolutions
        {
            get => _possibleSolutions;
            set { _possibleSolutions = value; NotifyPropertyChanged(nameof(PossibleSolutions)); }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Formatted <see cref="Question"/>.</summary>
        public string QuestionToString => Int1 + Operation + Int2;

        #endregion Helper Properties

        #region Constructors

        /// <summary>Initializes a default instance of <see cref="Question"/>.</summary>
        internal Question()
        {
        }

        /// <summary>Initializes an instance of <see cref="Question"/> by assigning Properties.</summary>
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

        /// <summary>Replaces this instance of <see cref="Question"/> with another instance.</summary>
        /// <param name="other">Instance of <see cref="Question"/> to replace this instance</param>
        public Question(Question other) : this(other.Int1, other.Int2, other.Operation, other.Solution, other.PossibleSolutions)
        {
        }

        #endregion Constructors
    }
}