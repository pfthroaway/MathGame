using System.ComponentModel;

namespace MathGame
{
    internal class Achievement : INotifyPropertyChanged
    {
        private string _name, _description, _type;
        private int _points;

        #region Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public int Points
        {
            get { return _points; }
            set { _points = value; OnPropertyChanged("Points"); OnPropertyChanged("PointsToString"); }
        }

        public string PointsToString
        {
            get
            {
                if (Points > 0)
                    return "Points: " + Points.ToString("N0");
                else
                    return "";
            }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); OnPropertyChanged("TypeWithText"); }
        }

        public string TypeWithText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Type))
                    return "Type: " + Type;
                else
                    return "";
            }
        }

        #endregion Properties

        #region Data Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data Binding

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Achievement.
        /// </summary>
        public Achievement()
        {
        }

        /// <summary>
        /// Initializes an instance of Achievement by assigning Properties.
        /// </summary>
        /// <param name="name">Name of Achievement</param>
        /// <param name="description">Description of Achievement</param>
        /// <param name="points">Amount of Achievement Points</param>
        /// <param name="type">Type of Achievement</param>
        public Achievement(string name, string description, int points, string type)
        {
            Name = name;
            Description = description;
            Points = points;
            Type = type;
        }

        /// <summary>
        /// Replaces this instance of Achievement with another Achievement.
        /// </summary>
        /// <param name="otherAchievement"></param>
        public Achievement(Achievement otherAchievement)
        {
            Name = otherAchievement.Name;
            Description = otherAchievement.Description;
            Points = otherAchievement.Points;
            Type = otherAchievement.Type;
        }

        #endregion Constructors
    }
}