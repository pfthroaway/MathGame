using System.Collections.Generic;
using System.ComponentModel;

namespace MathGame.Classes
{
    internal class Player : INotifyPropertyChanged
    {
        private string _name, _password;
        private int _totalWins, _easyAdditionWins, _mediumAdditionWins, _hardAdditionWins, _veryHardAdditionWins, _easySubtractionWins, _mediumSubtractionWins, _hardSubtractionWins, _veryHardSubtractionWins, _easyMultiplicationWins, _mediumMultiplicationWins, _hardMultiplicationWins, _veryHardMultiplicationWins, _easyDivisionWins, _mediumDivisionWins, _hardDivisionWins, _veryHardDivisionWins;
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

        public int VeryHardAdditionWins
        {
            get => _veryHardAdditionWins;
            set { _veryHardAdditionWins = value; OnPropertyChanged("VeryHardAdditionWins"); }
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

        public int VeryHardSubtractionWins
        {
            get => _veryHardSubtractionWins;
            set { _veryHardSubtractionWins = value; OnPropertyChanged("VeryHardSubtractionWins"); }
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

        public int VeryHardMultiplicationWins
        {
            get => _veryHardMultiplicationWins;
            set { _veryHardMultiplicationWins = value; OnPropertyChanged("VeryHardMultiplicationWins"); }
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

        public int VeryHardDivisionWins
        {
            get => _veryHardDivisionWins;
            set { _veryHardDivisionWins = value; OnPropertyChanged("VeryHardDivisionWins"); }
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
        public Player(string name, string password, int totalWins, int easyAdditionWins, int mediumAdditionWins, int hardAdditionWins, int veryHardAdditionWins, int easySubtractionWins, int mediumSubtractionWins, int hardSubtractionWins, int veryHardSubtractionWins, int easyMultiplicationWins, int mediumMultiplicationWins, int hardMultiplicationWins, int veryHardMultiplicationWins, int easyDivisionWins, int mediumDivisionWins, int hardDivisionWins, int veryHardDivisionWins, List<Achievement> unlockedAchievements)
        {
            Name = name;
            Password = password;
            TotalWins = totalWins;
            EasyAdditionWins = easyAdditionWins;
            MediumAdditionWins = mediumAdditionWins;
            HardAdditionWins = hardAdditionWins;
            VeryHardAdditionWins = veryHardAdditionWins;
            EasySubtractionWins = easySubtractionWins;
            MediumSubtractionWins = mediumSubtractionWins;
            HardSubtractionWins = hardSubtractionWins;
            VeryHardSubtractionWins = veryHardSubtractionWins;
            EasyMultiplicationWins = easyMultiplicationWins;
            MediumMultiplicationWins = mediumMultiplicationWins;
            HardMultiplicationWins = hardMultiplicationWins;
            VeryHardMultiplicationWins = veryHardMultiplicationWins;
            EasyDivisionWins = easyDivisionWins;
            MediumDivisionWins = mediumDivisionWins;
            HardDivisionWins = hardDivisionWins;
            VeryHardDivisionWins = veryHardDivisionWins;
            UnlockedAchievements = unlockedAchievements;
        }

        /// <summary>Replaces this instance of Player with another instance.</summary>
        /// <param name="other">Instance to replace this instance</param>
        public Player(Player other) : this(other.Name, other.Password, other.TotalWins, other.EasyAdditionWins, other.MediumAdditionWins, other.HardAdditionWins, other.VeryHardAdditionWins, other.EasySubtractionWins, other.MediumSubtractionWins, other.HardSubtractionWins, other.VeryHardSubtractionWins, other.EasyMultiplicationWins, other.MediumMultiplicationWins, other.HardMultiplicationWins, other.VeryHardMultiplicationWins, other.EasyDivisionWins, other.MediumDivisionWins, other.HardDivisionWins, other.VeryHardDivisionWins, other.UnlockedAchievements)
        {
        }

        #endregion Constructors
    }
}