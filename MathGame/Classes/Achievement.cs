using System;

namespace MathGame.Classes
{
    /// <summary>Represents an <see cref="Achievement"/> that the <see cref="Player"/> can earn.</summary>
    internal class Achievement : BaseINPC
    {
        private string _name, _description, _type;
        private int _points;

        #region Properties

        /// <summary>Name of the <see cref="Achievement"/>.</summary>
        public string Name
        {
            get => _name;
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        /// <summary>Description of the <see cref="Achievement"/>.</summary>
        public string Description
        {
            get => _description;
            set { _description = value; NotifyPropertyChanged(nameof(Description)); }
        }

        /// <summary>Points value of the <see cref="Achievement"/>.</summary>
        public int Points
        {
            get => _points;
            set { _points = value; NotifyPropertyChanged(nameof(Points), nameof(PointsToString)); }
        }

        /// <summary>Points value of the <see cref="Achievement"/>, formatted with preceding text.</summary>
        public string PointsToString => Points > 0 ? $"Points: {Points:N0}" : "";

        public string Type
        {
            get => _type;
            set { _type = value; NotifyPropertyChanged(nameof(Type), nameof(TypeWithText)); }
        }

        public string TypeWithText => !string.IsNullOrWhiteSpace(Type) ? $"Type: {Type}" : "";

        #endregion Properties

        #region Override Operators

        private static bool Equals(Achievement left, Achievement right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
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

        /// <summary>Initializes a default instance of <see cref="Achievement"/>.</summary>
        public Achievement()
        {
        }

        /// <summary>Initializes an instance of <see cref="Achievement"/> by assigning Properties.</summary>
        /// <param name="name">Name of <see cref="Achievement"/></param>
        /// <param name="description">Description of <see cref="Achievement"/></param>
        /// <param name="points">Amount of <see cref="Achievement"/> Points</param>
        /// <param name="type">Type of <see cref="Achievement"/></param>
        public Achievement(string name, string description, int points, string type)
        {
            Name = name;
            Description = description;
            Points = points;
            Type = type;
        }

        /// <summary>Replaces this instance of <see cref="Achievement"/> with another <see cref="Achievement"/>.</summary>
        /// <param name="other">Instance of <see cref="Achievement"/> to replace this instance</param>
        public Achievement(Achievement other) : this(other.Name, other.Description, other.Points, other.Type)
        {
        }

        #endregion Constructors
    }
}