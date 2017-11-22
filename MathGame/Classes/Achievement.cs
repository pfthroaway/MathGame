using System;
using System.ComponentModel;

namespace MathGame.Classes
{
    public class Achievement : INotifyPropertyChanged, IEquatable<Achievement>
    {
        private string _name, _description, _type;
        private int _points;

        #region Properties

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public int Points
        {
            get => _points;
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
            get => _type;
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

        protected void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data Binding

        #region Override Operators

        private static bool Equals(Achievement left, Achievement right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Description == right.Description && left.Type == right.Type && left.Points == right.Points;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Achievement);

        public bool Equals(Achievement other) => Equals(this, other);

        public static bool operator ==(Achievement left, Achievement right) => Equals(left, right);

        public static bool operator !=(Achievement left, Achievement right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => Name;

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of Achievement.</summary>
        public Achievement()
        {
        }

        /// <summary>Initializes an instance of Achievement by assigning Properties.</summary>
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

        /// <summary>Replaces this instance of Achievement with another Achievement.</summary>
        /// <param name="other"></param>
        public Achievement(Achievement other) : this(other.Name, other.Description, other.Points, other.Type)
        {
        }

        #endregion Constructors
    }
}