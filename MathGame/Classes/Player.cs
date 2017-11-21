using System.Collections.Generic;
using System.ComponentModel;

namespace MathGame.Classes
{
    internal class Player : INotifyPropertyChanged
    {
        private string _name, _password;
        private int _totalWins, _easyAdditionWins, _mediumAdditionWins, _hardAdditionWins, _easySubtractionWins, _mediumSubtractionWins, _hardSubtractionWins, _easyMultiplicationWins, _mediumMultiplicationWins, _hardMultiplicationWins, _easyDivisionWins, _mediumDivisionWins, _hardDivisionWins;
        private List<Achievement> _unlockedAchievements = new List<Achievement>();

        #region Properties

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged("Name"); }
        }

        public int TotalWins
        {
            get => _totalWins;
            set { _totalWins = value; OnPropertyChanged("TotalWins"); }
        }

        public int EasyAdditionWins
        {
            get => _easyAdditionWins;
            set { _easyAdditionWins = value; OnPropertyChanged("EasyAdditionWins"); }
        }

        public int MediumAdditionWins
        {
            get => _mediumAdditionWins;
            set { _mediumAdditionWins = value; OnPropertyChanged("MediumAdditionWins"); }
        }

        public int HardAdditionWins
        {
            get => _hardAdditionWins;
            set { _hardAdditionWins = value; OnPropertyChanged("HardAdditionWins"); }
        }

        public int EasySubtractionWins
        {
            get => _easySubtractionWins;
            set { _easySubtractionWins = value; OnPropertyChanged("EasySubtractionWins"); }
        }

        public int MediumSubtractionWins
        {
            get => _mediumSubtractionWins;
            set { _mediumSubtractionWins = value; OnPropertyChanged("MediumSubtractionWins"); }
        }

        public int HardSubtractionWins
        {
            get => _hardSubtractionWins;
            set { _hardSubtractionWins = value; OnPropertyChanged("HardSubtractionWins"); }
        }

        public int EasyMultiplicationWins
        {
            get => _easyMultiplicationWins;
            set { _easyMultiplicationWins = value; OnPropertyChanged("EasyMultiplicationWins"); }
        }

        public int MediumMultiplicationWins
        {
            get => _mediumMultiplicationWins;
            set { _mediumMultiplicationWins = value; OnPropertyChanged("MediumMultiplicationWins"); }
        }

        public int HardMultiplicationWins
        {
            get => _hardMultiplicationWins;
            set { _hardMultiplicationWins = value; OnPropertyChanged("HardMultiplicationWins"); }
        }

        public int EasyDivisionWins
        {
            get => _easyDivisionWins;
            set { _easyDivisionWins = value; OnPropertyChanged("EasyDivisionWins"); }
        }

        public int MediumDivisionWins
        {
            get => _mediumDivisionWins;
            set { _mediumDivisionWins = value; OnPropertyChanged("MediumDivisionWins"); }
        }

        public int HardDivisionWins
        {
            get => _hardDivisionWins;
            set { _hardDivisionWins = value; OnPropertyChanged("HardDivisionWins"); }
        }

        internal List<Achievement> UnlockedAchievements
        {
            get => _unlockedAchievements;
            set { _unlockedAchievements = value; OnPropertyChanged("UnlockedAchievements"); }
        }

        public string UnlockedAchievementsToString
        {
            get
            {
                string[] arrUnlockedAchievements = new string[UnlockedAchievements.Count];
                for (int i = 0; i < UnlockedAchievements.Count; i++)
                    arrUnlockedAchievements[i] = UnlockedAchievements[i].Name;

                return string.Join(",", arrUnlockedAchievements);
            }
        }

        #endregion Properties

        /// <summary>Loads all Achievements for a player from a string.</summary>
        /// <param name="achievements">String list of achievements</param>
        internal void LoadAchievements(string achievements)
        {
            string[] arrAchievements = achievements.Split(',');
            foreach (string str in arrAchievements)
                UnlockedAchievements.Add(GameState.AllAchievements.Find(ach => ach.Name == str.Trim()));
        }

        #region Data Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data Binding

        #region Constructors

        /// <summary>Initializes a default instance of Player.</summary>
        public Player()
        {
        }

        /// <summary>Initializes an instace of Player by assigning Properties.</summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="totalWins"></param>
        /// <param name="easyAdditionWins"></param>
        /// <param name="mediumAdditionWins"></param>
        /// <param name="hardAdditionWins"></param>
        /// <param name="easySubtractionWins"></param>
        /// <param name="mediumSubtractionWins"></param>
        /// <param name="hardSubtractionWins"></param>
        /// <param name="easyMultiplicationWins"></param>
        /// <param name="mediumMultiplicationWins"></param>
        /// <param name="hardMultiplicationWins"></param>
        /// <param name="easyDivisionWins"></param>
        /// <param name="mediumDivisionWins"></param>
        /// <param name="hardDivisionWins"></param>
        /// <param name="unlockedAchievements"></param>
        public Player(string name, string password, int totalWins, int easyAdditionWins, int mediumAdditionWins, int hardAdditionWins, int easySubtractionWins, int mediumSubtractionWins, int hardSubtractionWins, int easyMultiplicationWins, int mediumMultiplicationWins, int hardMultiplicationWins, int easyDivisionWins, int mediumDivisionWins, int hardDivisionWins, List<Achievement> unlockedAchievements)
        {
            Name = name;
            Password = password;
            TotalWins = totalWins;
            EasyAdditionWins = easyAdditionWins;
            MediumAdditionWins = mediumAdditionWins;
            HardAdditionWins = hardAdditionWins;
            EasySubtractionWins = easySubtractionWins;
            MediumSubtractionWins = mediumSubtractionWins;
            HardSubtractionWins = hardSubtractionWins;
            EasyMultiplicationWins = easyMultiplicationWins;
            MediumMultiplicationWins = mediumMultiplicationWins;
            HardMultiplicationWins = hardMultiplicationWins;
            EasyDivisionWins = easyDivisionWins;
            MediumDivisionWins = mediumDivisionWins;
            HardDivisionWins = hardDivisionWins;
            UnlockedAchievements = unlockedAchievements;
        }

        /// <summary>Replaces this instance of Player with another instance.</summary>
        /// <param name="other">Instance to replace this instance</param>
        public Player(Player other) : this(other.Name, other.Password, other.TotalWins, other.EasyAdditionWins, other.MediumAdditionWins, other.HardAdditionWins, other.EasySubtractionWins, other.MediumSubtractionWins, other.HardSubtractionWins, other.EasyMultiplicationWins, other.MediumMultiplicationWins, other.HardMultiplicationWins, other.EasyDivisionWins, other.MediumDivisionWins, other.HardDivisionWins, other.UnlockedAchievements)
        {
        }

        #endregion Constructors
    }
}