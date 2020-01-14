using Extensions;
using Extensions.DatabaseHelp;
using Extensions.DataTypeHelpers;
using Extensions.Encryption;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MathGame.Classes.Database
{
    /// <summary>Represents database interactions using SQLiteHelper.</summary>
    internal class SQLiteDatabaseInteraction : IDatabaseInteraction
    {
        private const string _DATABASENAME = "MathGame.sqlite";
        private readonly string _con = $"Data Source = {UsersDatabaseLocation}; foreign keys = TRUE; Version = 3;";
        private static readonly string UsersDatabaseLocation = Path.Combine(AppData.Location, _DATABASENAME);

        #region Database Interaction

        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        public void VerifyDatabaseIntegrity() => Functions.VerifyFileIntegrity(
            Assembly.GetExecutingAssembly().GetManifestResourceStream($"MathGame.{_DATABASENAME}"), _DATABASENAME, AppData.Location);

        #endregion Database Interaction

        #region Achievement Management

        /// <summary>Loads all Achievements.</summary>
        /// <returns>All Achievements</returns>
        public async Task<List<Achievement>> LoadAchievements()
        {
            DataSet ds = await SQLiteHelper.FillDataSet(_con, "SELECT * FROM Achievements");

            List<Achievement> allAchievements = new List<Achievement>();
            if (ds.Tables[0].Rows.Count > 0)
                allAchievements.AddRange(from DataRow dr in ds.Tables[0].Rows select new Achievement(dr["Name"].ToString(), dr["Description"].ToString(), Int32Helper.Parse(dr["Points"]), dr["Type"].ToString()));

            return allAchievements;
        }

        #endregion Achievement Management

        #region Player Management

        /// <summary>Checks if a name can be added to the database.</summary>
        /// <param name="name">Name to be checked</param>
        /// <returns>True if name can be added</returns>
        public async Task<bool> CheckNewPlayerName(string name)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "SELECT * FROM Players WHERE [Username] = @name" };
            cmd.Parameters.AddWithValue("@name", name);

            DataSet ds = await SQLiteHelper.FillDataSet(_con, cmd);

            return ds.Tables[0].Rows.Count == 0;
        }

        /// <summary>Loads a Player.</summary>
        /// <param name="username">Name of Player</param>
        /// <returns>Player</returns>
        public async Task<Player> LoadPlayer(string username)
        {
            Player loadPlayer = new Player();
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "SELECT * FROM Players WHERE [Username] = @name" };
            cmd.Parameters.AddWithValue("@name", username);
            DataSet ds = await SQLiteHelper.FillDataSet(_con, cmd);
            DataRow dr = ds.Tables[0].Rows[0];
            loadPlayer.Name = username;
            loadPlayer.Password = dr["Password"].ToString();
            loadPlayer.TotalWins = Int32Helper.Parse(dr["TotalWins"]);
            loadPlayer.EasyAdditionWins = Int32Helper.Parse(dr["EasyAdditionWins"]);
            loadPlayer.MediumAdditionWins = Int32Helper.Parse(dr["MediumAdditionWins"]);
            loadPlayer.HardAdditionWins = Int32Helper.Parse(dr["HardAdditionWins"]);
            loadPlayer.VeryHardAdditionWins = Int32Helper.Parse(dr["VeryHardAdditionWins"]);
            loadPlayer.EasySubtractionWins = Int32Helper.Parse(dr["EasySubtractionWins"]);
            loadPlayer.MediumSubtractionWins = Int32Helper.Parse(dr["MediumSubtractionWins"]);
            loadPlayer.HardSubtractionWins = Int32Helper.Parse(dr["HardSubtractionWins"]);
            loadPlayer.VeryHardSubtractionWins = Int32Helper.Parse(dr["VeryHardSubtractionWins"]);
            loadPlayer.EasyMultiplicationWins = Int32Helper.Parse(dr["EasyMultiplicationWins"]);
            loadPlayer.MediumMultiplicationWins = Int32Helper.Parse(dr["MediumMultiplicationWins"]);
            loadPlayer.HardMultiplicationWins = Int32Helper.Parse(dr["HardMultiplicationWins"]);
            loadPlayer.VeryHardMultiplicationWins = Int32Helper.Parse(dr["VeryHardMultiplicationWins"]);
            loadPlayer.EasyDivisionWins = Int32Helper.Parse(dr["EasyDivisionWins"]);
            loadPlayer.MediumDivisionWins = Int32Helper.Parse(dr["MediumDivisionWins"]);
            loadPlayer.HardDivisionWins = Int32Helper.Parse(dr["HardDivisionWins"]);
            loadPlayer.VeryHardDivisionWins = Int32Helper.Parse(dr["VeryHardDivisionWins"]);

            if (dr["AchievementsUnlocked"].ToString().Length > 0)
                loadPlayer.LoadAchievements(dr["AchievementsUnlocked"].ToString());

            return loadPlayer;
        }

        /// <summary>Adds a new Player to the database.</summary>
        /// <param name="newPlayer">Player to be added</param>
        /// <returns>True if successful</returns>
        public async Task<bool> NewPlayer(Player newPlayer)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "INSERT INTO Players([Username], [Password],[AchievementsUnlocked], [TotalWins], [EasyAdditionWins], [MediumAdditionWins], [HardAdditionWins], [VeryHardAdditionWins], [EasySubtractionWins], [MediumSubtractionWins], [HardSubtractionWins], [VeryHardSubtractionWins], [EasyMultiplicationWins], [MediumMultiplicationWins], [HardMultiplicationWins], [VeryHardMultiplicationWins], [EasyDivisionWins], [MediumDivisionWins], [HardDivisionWins], [VeryHardDivisionWins])VALUES(@username, @hashedPassword, @achievementsUnlocked, @totalWins, @easyAdditionWins, @mediumAdditionWins, @hardAdditionWins, @veryHardAdditionWins, @easySubtractionWins, @mediumSubtractionWins, @hardSubtractionWins, @veryHardSubtractionWins, @easyMultiplicationWins, @mediumMultiplicationWins, @hardMultiplicationWins, @veryHardMultiplicationWins, @easyDivisionWins, @mediumDivisionWins, @hardDivisionWins, @veryHardDivisionWins)" };
            cmd.Parameters.AddWithValue("@username", newPlayer.Name);
            cmd.Parameters.AddWithValue("@hashedPassword", newPlayer.Password);
            cmd.Parameters.AddWithValue("@achievementsUnlocked", newPlayer.UnlockedAchievementsToString);
            cmd.Parameters.AddWithValue("@totalWins", newPlayer.TotalWins);
            cmd.Parameters.AddWithValue("@easyAdditionWins", newPlayer.EasyAdditionWins);
            cmd.Parameters.AddWithValue("@mediumAdditionWins", newPlayer.MediumAdditionWins);
            cmd.Parameters.AddWithValue("@hardAdditionWins", newPlayer.HardAdditionWins);
            cmd.Parameters.AddWithValue("@veryHardAdditionWins", newPlayer.VeryHardAdditionWins);
            cmd.Parameters.AddWithValue("@easySubtractionWins", newPlayer.EasySubtractionWins);
            cmd.Parameters.AddWithValue("@mediumSubtractionWins", newPlayer.MediumSubtractionWins);
            cmd.Parameters.AddWithValue("@hardSubtractionWins", newPlayer.HardSubtractionWins);
            cmd.Parameters.AddWithValue("@veryHardSubtractionWins", newPlayer.VeryHardSubtractionWins);
            cmd.Parameters.AddWithValue("@easyMultiplicationWins", newPlayer.EasyMultiplicationWins);
            cmd.Parameters.AddWithValue("@mediumMultiplicationWins", newPlayer.MediumMultiplicationWins);
            cmd.Parameters.AddWithValue("@hardMultiplicationWins", newPlayer.HardMultiplicationWins);
            cmd.Parameters.AddWithValue("@veryHardMultiplicationWins", newPlayer.VeryHardMultiplicationWins);
            cmd.Parameters.AddWithValue("@easyDivisionWins", newPlayer.EasyDivisionWins);
            cmd.Parameters.AddWithValue("@mediumDivisionWins", newPlayer.MediumDivisionWins);
            cmd.Parameters.AddWithValue("@hardDivisionWins", newPlayer.HardDivisionWins);
            cmd.Parameters.AddWithValue("@veryHardDivisionWins", newPlayer.VeryHardDivisionWins);

            return await SQLiteHelper.ExecuteCommand(_con, cmd);
        }

        /// <summary>Saves the current Player.</summary>
        /// <param name="savePlayer">Player to be saved</param>
        public async Task<bool> SavePlayer(Player savePlayer)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "UPDATE Players SET [AchievementsUnlocked] = @achievementsUnlocked, [TotalWins] = @totalWins, [EasyAdditionWins] = @easyAdditionWins, [MediumAdditionWins] = @mediumAdditionWins, [HardAdditionWins] = @hardAdditionWins, [EasySubtractionWins] = @easySubtractionWins, [MediumSubtractionWins] = @mediumSubtractionWins, [HardSubtractionWins] = @hardSubtractionWins, [EasyMultiplicationWins] = @easyMultiplicationWins, [MediumMultiplicationWins] = @mediumMultiplicationWins, [HardMultiplicationWins] = @hardMultiplicationWins, [EasyDivisionWins] = @easyDivisionWins, [MediumDivisionWins] = @mediumDivisionWins, [HardDivisionWins] = @hardDivisionWins WHERE [Username] = @username" };

            cmd.Parameters.AddWithValue("@achievementsUnlocked", savePlayer.UnlockedAchievementsToString);
            cmd.Parameters.AddWithValue("@totalWins", savePlayer.TotalWins);
            cmd.Parameters.AddWithValue("@easyAdditionWins", savePlayer.EasyAdditionWins);
            cmd.Parameters.AddWithValue("@mediumAdditionWins", savePlayer.MediumAdditionWins);
            cmd.Parameters.AddWithValue("@hardAdditionWins", savePlayer.HardAdditionWins);
            cmd.Parameters.AddWithValue("@easySubtractionWins", savePlayer.EasySubtractionWins);
            cmd.Parameters.AddWithValue("@mediumSubtractionWins", savePlayer.MediumSubtractionWins);
            cmd.Parameters.AddWithValue("@hardSubtractionWins", savePlayer.HardSubtractionWins);
            cmd.Parameters.AddWithValue("@easyMultiplicationWins", savePlayer.EasyMultiplicationWins);
            cmd.Parameters.AddWithValue("@mediumMultiplicationWins", savePlayer.MediumMultiplicationWins);
            cmd.Parameters.AddWithValue("@hardMultiplicationWins", savePlayer.HardMultiplicationWins);
            cmd.Parameters.AddWithValue("@easyDivisionWins", savePlayer.EasyDivisionWins);
            cmd.Parameters.AddWithValue("@mediumDivisionWins", savePlayer.MediumDivisionWins);
            cmd.Parameters.AddWithValue("@hardDivisionWins", savePlayer.HardDivisionWins);
            cmd.Parameters.AddWithValue("@username", savePlayer.Name);

            return await SQLiteHelper.ExecuteCommand(_con, cmd);
        }

        /// <summary>Verifies the password of an attempted login.</summary>
        /// <param name="username">User attempting to login</param>
        /// <param name="password">Plaintext password</param>
        /// <returns>True if passwords match</returns>
        public async Task<bool> VerifyPassword(string username, string password)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "SELECT [Password] FROM Players WHERE [Username] = @name" };
            cmd.Parameters.AddWithValue("@name", username);

            DataSet ds = await SQLiteHelper.FillDataSet(_con, cmd);
            return ds.Tables[0].Rows.Count > 0 ? PBKDF2.ValidatePassword(password, ds.Tables[0].Rows[0]["Password"].ToString()) : false;
        }

        #endregion Player Management
    }
}