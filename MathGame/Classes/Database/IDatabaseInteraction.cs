using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathGame.Classes.Database
{
    /// <summary>Represents all required database interactions regardless of implementation.</summary>
    internal interface IDatabaseInteraction
    {
        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        /// <returns>Returns true once the database has been validated</returns>
        void VerifyDatabaseIntegrity();

        #region Load

        /// <summary>Loads all Achievements.</summary>
        /// <returns>All Achievements</returns>
        Task<List<Achievement>> LoadAchievements();

        #endregion Load

        #region Player Management

        /// <summary>Checks if a name can be added to the database.</summary>
        /// <param name="name">Name to be checked</param>
        /// <returns>True if name can be added</returns>
        Task<bool> CheckNewPlayerName(string name);

        /// <summary>Loads a Player.</summary>
        /// <param name="username">Name of Player</param>
        /// <returns>Player</returns>
        Task<Player> LoadPlayer(string username);

        /// <summary>Adds a new Player to the database.</summary>
        /// <param name="newPlayer">Player to be added</param>
        /// <returns>True if successful</returns>
        Task<bool> NewPlayer(Player newPlayer);

        /// <summary>Saves the current Player.</summary>
        /// <param name="savePlayer">Player to be saved</param>
        Task<bool> SavePlayer(Player savePlayer);

        /// <summary>Verifies the password of an attempted login.</summary>
        /// <param name="username">User attempting to login</param>
        /// <param name="password">Plaintext password</param>
        /// <returns>True if passwords match</returns>
        Task<bool> VerifyPassword(string username, string password);

        #endregion Player Management
    }
}