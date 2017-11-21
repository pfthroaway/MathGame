using Extensions;
using Extensions.DatabaseHelp;
using Extensions.DataTypeHelpers;
using Extensions.Encryption;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MathGame.Classes.Database
{
    /// <summary>Represents database interactions using SQLite.</summary>
    internal class SQLiteDatabaseInteraction : IDatabaseInteraction
    {
        // ReSharper disable once InconsistentNaming
        private const string _DATABASENAME = "MathGame.sqlite";

        private readonly string _con = $"Data Source = {_DATABASENAME}; foreign keys = TRUE; Version = 3;";

        #region Database Interaction

        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        public void VerifyDatabaseIntegrity() => Functions.VerifyFileIntegrity(
            Assembly.GetExecutingAssembly().GetManifestResourceStream($"MathGame.{_DATABASENAME}"), _DATABASENAME);

        #endregion Database Interaction

        #region Achievement Management

        /// <summary>Loads all Achievements.</summary>
        /// <returns>All Achievements</returns>
        public async Task<List<Achievement>> LoadAchievements()
        {
            DataSet ds = await SQLite.FillDataSet(_con, "SELECT * FROM Achievements");

            List<Achievement> allAchievements = new List<Achievement>();
            if (ds.Tables[0].Rows.Count > 0)
                allAchievements.AddRange(from DataRow dr in ds.Tables[0].Rows select new Achievement(dr["AchievementName"].ToString(), dr["AchievementDescription"].ToString(), Int32Helper.Parse(dr["AchievementPoints"]), dr["AchievementType"].ToString()));

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

            DataSet ds = await SQLite.FillDataSet(_con, cmd);

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
            DataSet ds = await SQLite.FillDataSet(_con, cmd);
            DataRow dr = ds.Tables[0].Rows[0];
            loadPlayer.Name = username;
            loadPlayer.Password = dr["Password"].ToString();
            loadPlayer.TotalWins = Int32Helper.Parse(dr["TotalWins"].ToString());
            loadPlayer.EasyAdditionWins = Int32Helper.Parse(dr["EasyAdditionWins"].ToString());
            loadPlayer.MediumAdditionWins = Int32Helper.Parse(dr["MediumAdditionWins"].ToString());
            loadPlayer.HardAdditionWins = Int32Helper.Parse(dr["HardAdditionWins"].ToString());
            loadPlayer.EasySubtractionWins = Int32Helper.Parse(dr["EasySubtractionWins"].ToString());
            loadPlayer.MediumSubtractionWins = Int32Helper.Parse(dr["MediumSubtractionWins"].ToString());
            loadPlayer.HardSubtractionWins = Int32Helper.Parse(dr["HardSubtractionWins"].ToString());
            loadPlayer.EasyMultiplicationWins = Int32Helper.Parse(dr["EasyMultiplicationWins"].ToString());
            loadPlayer.MediumMultiplicationWins = Int32Helper.Parse(dr["MediumMultiplicationWins"].ToString());
            loadPlayer.HardMultiplicationWins = Int32Helper.Parse(dr["HardMultiplicationWins"].ToString());
            loadPlayer.EasyDivisionWins = Int32Helper.Parse(dr["EasyDivisionWins"].ToString());
            loadPlayer.MediumDivisionWins = Int32Helper.Parse(dr["MediumDivisionWins"].ToString());
            loadPlayer.HardDivisionWins = Int32Helper.Parse(dr["HardDivisionWins"].ToString());

            if (dr["AchievementsUnlocked"].ToString().Length > 0)
                loadPlayer.LoadAchievements(dr["AchievementsUnlocked"].ToString());

            return loadPlayer;
        }

        /// <summary>Adds a new Player to the database.</summary>
        /// <param name="newPlayer">Player to be added</param>
        /// <returns>True if successful</returns>
        public async Task<bool> NewPlayer(Player newPlayer)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "INSERT INTO Players([Username], [Password],[AchievementsUnlocked], [TotalWins], [EasyAdditionWins], [MediumAdditionWins], [HardAdditionWins], [EasySubtractionWins], [MediumSubtractionWins], [HardSubtractionWins], [EasyMultiplicationWins], [MediumMultiplicationWins], [HardMultiplicationWins], [EasyDivisionWins], [MediumDivisionWins], [HardDivisionWins])VALUES(@username, @hashedPassword, @achievementsUnlocked, @totalWins, @easyAdditionWins, @mediumAdditionWins, @hardAdditionWins, @easySubtractionWins, @mediumSubtractionWins, @hardSubtractionWins, @easyMultiplicationWins, @mediumMultiplicationWins, @hardMultiplicationWins, @easyDivisionWins, @mediumDivisionWins, @hardDivisionWins)" };
            cmd.Parameters.AddWithValue("@username", newPlayer.Name);
            cmd.Parameters.AddWithValue("@hashedPassword", newPlayer.Password);
            cmd.Parameters.AddWithValue("@achievementsUnlocked", newPlayer.UnlockedAchievementsToString);
            cmd.Parameters.AddWithValue("@totalWins", newPlayer.TotalWins);
            cmd.Parameters.AddWithValue("@easyAdditionWins", newPlayer.EasyAdditionWins);
            cmd.Parameters.AddWithValue("@mediumAdditionWins", newPlayer.MediumAdditionWins);
            cmd.Parameters.AddWithValue("@hardAdditionWins", newPlayer.HardAdditionWins);
            cmd.Parameters.AddWithValue("@easySubtractionWins", newPlayer.EasySubtractionWins);
            cmd.Parameters.AddWithValue("@mediumSubtractionWins", newPlayer.MediumSubtractionWins);
            cmd.Parameters.AddWithValue("@hardSubtractionWins", newPlayer.HardSubtractionWins);
            cmd.Parameters.AddWithValue("@easyMultiplicationWins", newPlayer.EasyMultiplicationWins);
            cmd.Parameters.AddWithValue("@mediumMultiplicationWins", newPlayer.MediumMultiplicationWins);
            cmd.Parameters.AddWithValue("@hardMultiplicationWins", newPlayer.HardMultiplicationWins);
            cmd.Parameters.AddWithValue("@easyDivisionWins", newPlayer.EasyDivisionWins);
            cmd.Parameters.AddWithValue("@mediumDivisionWins", newPlayer.MediumDivisionWins);
            cmd.Parameters.AddWithValue("@hardDivisionWins", newPlayer.HardDivisionWins);

            return await SQLite.ExecuteCommand(_con, cmd);
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

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        /// <summary>Verifies the password of an attempted login.</summary>
        /// <param name="username">User attempting to login</param>
        /// <param name="password">Plaintext password</param>
        /// <returns>True if passwords match</returns>
        public async Task<bool> VerifyPassword(string username, string password)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "SELECT [Password] FROM Players WHERE [Username] = @name" };
            cmd.Parameters.AddWithValue("@name", username);

            DataSet ds = await SQLite.FillDataSet(_con, cmd);
            return ds.Tables[0].Rows.Count > 0 ? PBKDF2.ValidatePassword(password, ds.Tables[0].Rows[0]["Password"].ToString()) : false;
        }

        #endregion Player Management
    }
}