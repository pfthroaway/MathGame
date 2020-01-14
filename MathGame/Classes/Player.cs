using System.Collections.Generic;

namespace MathGame.Classes
{
    /// <summary>Represents a <see cref="Player"/> of the game.</summary>
    internal class Player : BaseINPC
    {
        private string _name;
        private int _totalWins, _easyAdditionWins, _mediumAdditionWins, _hardAdditionWins, _veryHardAdditionWins, _easySubtractionWins, _mediumSubtractionWins, _hardSubtractionWins, _veryHardSubtractionWins, _easyMultiplicationWins, _mediumMultiplicationWins, _hardMultiplicationWins, _veryHardMultiplicationWins, _easyDivisionWins, _mediumDivisionWins, _hardDivisionWins, _veryHardDivisionWins;
        private List<Achievement> _unlockedAchievements = new List<Achievement>();

        #region Properties

        /// <summary>Name of the <see cref="Player"/>.</summary>
        public string Name
        {
            get => _name;
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        /// <summary>The <see cref="Player"/>'s hashed password.</summary>
        public string Password { get; set; }

        /// <summary>The <see cref="Player"/>'s total wins.</summary>
        public int TotalWins
        {
            get => _totalWins;
            set { _totalWins = value; NotifyPropertyChanged(nameof(TotalWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Easy Addition wins.</summary>
        public int EasyAdditionWins
        {
            get => _easyAdditionWins;
            set { _easyAdditionWins = value; NotifyPropertyChanged(nameof(EasyAdditionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Medium Addition wins.</summary>
        public int MediumAdditionWins
        {
            get => _mediumAdditionWins;
            set { _mediumAdditionWins = value; NotifyPropertyChanged(nameof(MediumAdditionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Hard Addition wins.</summary>
        public int HardAdditionWins
        {
            get => _hardAdditionWins;
            set { _hardAdditionWins = value; NotifyPropertyChanged(nameof(HardAdditionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Very Hard Addition wins.</summary>
        public int VeryHardAdditionWins
        {
            get => _veryHardAdditionWins;
            set { _veryHardAdditionWins = value; NotifyPropertyChanged(nameof(VeryHardAdditionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Easy Subtraction wins.</summary>
        public int EasySubtractionWins
        {
            get => _easySubtractionWins;
            set { _easySubtractionWins = value; NotifyPropertyChanged(nameof(EasySubtractionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Medium Subtraction wins.</summary>
        public int MediumSubtractionWins
        {
            get => _mediumSubtractionWins;
            set { _mediumSubtractionWins = value; NotifyPropertyChanged(nameof(MediumSubtractionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Hard Subtraction wins.</summary>
        public int HardSubtractionWins
        {
            get => _hardSubtractionWins;
            set { _hardSubtractionWins = value; NotifyPropertyChanged(nameof(HardSubtractionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Very Hard Subtraction wins.</summary>
        public int VeryHardSubtractionWins
        {
            get => _veryHardSubtractionWins;
            set { _veryHardSubtractionWins = value; NotifyPropertyChanged(nameof(VeryHardSubtractionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Easy Multiplication wins.</summary>
        public int EasyMultiplicationWins
        {
            get => _easyMultiplicationWins;
            set { _easyMultiplicationWins = value; NotifyPropertyChanged(nameof(EasyMultiplicationWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Medium Multiplication wins.</summary>
        public int MediumMultiplicationWins
        {
            get => _mediumMultiplicationWins;
            set { _mediumMultiplicationWins = value; NotifyPropertyChanged(nameof(MediumMultiplicationWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Hard Multiplication wins.</summary>
        public int HardMultiplicationWins
        {
            get => _hardMultiplicationWins;
            set { _hardMultiplicationWins = value; NotifyPropertyChanged(nameof(HardMultiplicationWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Very Hard Multiplication wins.</summary>
        public int VeryHardMultiplicationWins
        {
            get => _veryHardMultiplicationWins;
            set { _veryHardMultiplicationWins = value; NotifyPropertyChanged(nameof(VeryHardMultiplicationWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Easy Division wins.</summary>
        public int EasyDivisionWins
        {
            get => _easyDivisionWins;
            set { _easyDivisionWins = value; NotifyPropertyChanged(nameof(EasyDivisionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Medium Division wins.</summary>
        public int MediumDivisionWins
        {
            get => _mediumDivisionWins;
            set { _mediumDivisionWins = value; NotifyPropertyChanged(nameof(MediumDivisionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Hard Division wins.</summary>
        public int HardDivisionWins
        {
            get => _hardDivisionWins;
            set { _hardDivisionWins = value; NotifyPropertyChanged(nameof(HardDivisionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s total Very Hard Division wins.</summary>
        public int VeryHardDivisionWins
        {
            get => _veryHardDivisionWins;
            set { _veryHardDivisionWins = value; NotifyPropertyChanged(nameof(VeryHardDivisionWins)); }
        }

        /// <summary>The <see cref="Player"/>'s unlocked achievements.</summary>
        internal List<Achievement> UnlockedAchievements
        {
            get => _unlockedAchievements;
            set { _unlockedAchievements = value; NotifyPropertyChanged(nameof(UnlockedAchievements)); }
        }

        /// <summary>The <see cref="Player"/>'s unlocked achievements, formatted.</summary>
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
            foreach (string str in achievements.Split(','))
                UnlockedAchievements.Add(GameState.AllAchievements.Find(ach => ach.Name == str.Trim()));
        }

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