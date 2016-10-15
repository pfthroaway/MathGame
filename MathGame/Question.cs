using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame_WPF
{
    internal class Question
    {
        private int _int1;
        private int _int2;
        private int _solution;
        private List<int> _possibleSolutions = new List<int>();

        internal int Int1
        {
            get { return _int1; }
            set { _int1 = value; }
        }

        internal int Int2
        {
            get { return _int2; }
            set { _int2 = value; }
        }

        internal int Solution
        {
            get { return _solution; }
            set { _solution = value; }
        }

        internal List<int> PossibleSolutions
        {
            get { return _possibleSolutions; }
            set { _possibleSolutions = value; }
        }

        internal Question()
        {
        }

        internal Question(int num1, int num2, int solution, List<int> solutionList)
        {
            Int1 = num1;
            Int2 = num2;
            Solution = solution;
            PossibleSolutions = solutionList;
        }
    }
}