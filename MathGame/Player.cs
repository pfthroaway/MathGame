using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MathGame
{
    internal class Player : INotifyPropertyChanged
    {
        private string _name = "";
        private string _password = "";
        private int _totalWins = 0;
        private int _easyAdditionWins = 0;
        private int _mediumAdditionWins = 0;
        private int _hardAdditionWins = 0;
        private int _easySubtractionWins = 0;
        private int _mediumSubtractionWins = 0;
        private int _hardSubtractionWins = 0;
        private int _easyMultiplicationWins = 0;
        private int _mediumMultiplicationWins = 0;
        private int _hardMultiplicationWins = 0;
        private int _easyDivisionWins = 0;
        private int _mediumDivisionWins = 0;
        private int _hardDivisionWins = 0;
        private List<Achievement> _unlockedAchievements = new List<Achievement>();

        #region Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Name"); }
        }

        public int TotalWins
        {
            get { return _totalWins; }
            set { _totalWins = value; OnPropertyChanged("TotalWins"); }
        }

        public int EasyAdditionWins
        {
            get { return _easyAdditionWins; }
            set { _easyAdditionWins = value; OnPropertyChanged("EasyAdditionWins"); }
        }

        public int MediumAdditionWins
        {
            get { return _mediumAdditionWins; }
            set { _mediumAdditionWins = value; OnPropertyChanged("MediumAdditionWins"); }
        }

        public int HardAdditionWins
        {
            get { return _hardAdditionWins; }
            set { _hardAdditionWins = value; OnPropertyChanged("HardAdditionWins"); }
        }

        public int EasySubtractionWins
        {
            get { return _easySubtractionWins; }
            set { _easySubtractionWins = value; OnPropertyChanged("EasySubtractionWins"); }
        }

        public int MediumSubtractionWins
        {
            get { return _mediumSubtractionWins; }
            set { _mediumSubtractionWins = value; OnPropertyChanged("MediumSubtractionWins"); }
        }

        public int HardSubtractionWins
        {
            get { return _hardSubtractionWins; }
            set { _hardSubtractionWins = value; OnPropertyChanged("HardSubtractionWins"); }
        }

        public int EasyMultiplicationWins
        {
            get { return _easyMultiplicationWins; }
            set { _easyMultiplicationWins = value; OnPropertyChanged("EasyMultiplicationWins"); }
        }

        public int MediumMultiplicationWins
        {
            get { return _mediumMultiplicationWins; }
            set { _mediumMultiplicationWins = value; OnPropertyChanged("MediumMultiplicationWins"); }
        }

        public int HardMultiplicationWins
        {
            get { return _hardMultiplicationWins; }
            set { _hardMultiplicationWins = value; OnPropertyChanged("HardMultiplicationWins"); }
        }

        public int EasyDivisionWins
        {
            get { return _easyDivisionWins; }
            set { _easyDivisionWins = value; OnPropertyChanged("EasyDivisionWins"); }
        }

        public int MediumDivisionWins
        {
            get { return _mediumDivisionWins; }
            set { _mediumDivisionWins = value; OnPropertyChanged("MediumDivisionWins"); }
        }

        public int HardDivisionWins
        {
            get { return _hardDivisionWins; }
            set { _hardDivisionWins = value; OnPropertyChanged("HardDivisionWins"); }
        }

        internal List<Achievement> UnlockedAchievements
        {
            get { return _unlockedAchievements; }
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

        #region Data Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data Binding

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Player.
        /// </summary>
        public Player()
        {
        }

        /// <summary>
        /// Initializes an instace of Player by assigning Properties.
        /// </summary>
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

        /// <summary>
        /// Replaces this instance of Player with another instance.
        /// </summary>
        /// <param name="otherPlayer">Instance to replace this instance</param>
        public Player(Player otherPlayer)
        {
            Name = otherPlayer.Name;
            Password = otherPlayer.Password;
            TotalWins = otherPlayer.TotalWins;
            EasyAdditionWins = otherPlayer.EasyAdditionWins;
            MediumAdditionWins = otherPlayer.MediumAdditionWins;
            HardAdditionWins = otherPlayer.HardAdditionWins;
            EasySubtractionWins = otherPlayer.EasySubtractionWins;
            MediumSubtractionWins = otherPlayer.MediumSubtractionWins;
            HardSubtractionWins = otherPlayer.HardSubtractionWins;
            EasyMultiplicationWins = otherPlayer.EasyMultiplicationWins;
            MediumMultiplicationWins = otherPlayer.MediumMultiplicationWins;
            HardMultiplicationWins = otherPlayer.HardMultiplicationWins;
            EasyDivisionWins = otherPlayer.EasyDivisionWins;
            MediumDivisionWins = otherPlayer.MediumDivisionWins;
            HardDivisionWins = otherPlayer.HardDivisionWins;
            UnlockedAchievements = otherPlayer.UnlockedAchievements;
        }

        #endregion Constructors
    }
}