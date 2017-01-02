﻿using Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows;

namespace MathGame
{
    internal class GameState
    {
        internal const string _DBPROVIDERANDSOURCE = "Data Source = MathGame.sqlite;Version=3";
        internal static Player CurrentPlayer = new Player();
        internal static List<Achievement> AllAchievements = new List<Achievement>();

        internal static async void LoadAll()
        {
            SQLiteConnection con = new SQLiteConnection();
            SQLiteDataAdapter da;
            DataSet ds = new DataSet();
            con.ConnectionString = _DBPROVIDERANDSOURCE;

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    string sql = "SELECT * FROM Achievements";
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, "Achievements");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Achievement newAchievement = new Achievement(ds.Tables[0].Rows[i]["AchievementName"].ToString(), ds.Tables[0].Rows[i]["AchievementDescription"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["AchievementPoints"]), ds.Tables[0].Rows[i]["AchievementType"].ToString());

                            AllAchievements.Add(newAchievement);
                        }
                    }
                }
                catch (Exception ex)
                {
                    new Notification(ex.Message, "Error Filling DataSet", NotificationButtons.OK).ShowDialog();
                }
                finally { con.Close(); }
            });
        }

        /// <summary>
        /// Saves the current Player.
        /// </summary>
        internal static async void SavePlayer()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection con = new SQLiteConnection { ConnectionString = _DBPROVIDERANDSOURCE };
            string sql = "UPDATE Players SET [AchievementsUnlocked] = @achievementsUnlocked,[TotalWins] = @totalWins,[EasyAdditionWins] = @easyAdditionWins,[MediumAdditionWins] = @mediumAdditionWins,[HardAdditionWins] = @hardAdditionWins,[EasySubtractionWins] = @easySubtractionWins,[MediumSubtractionWins] = @mediumSubtractionWins,[HardSubtractionWins] = @hardSubtractionWins,[EasyMultiplicationWins] = @easyMultiplicationWins,[MediumMultiplicationWins] = @mediumMultiplicationWins,[HardMultiplicationWins] = @hardMultiplicationWins,[EasyDivisionWins] = @easyDivisionWins,[MediumDivisionWins] = @mediumDivisionWins,[HardDivisionWins] = @hardDivisionWins WHERE [Username] = @username";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@achievementsUnlocked", CurrentPlayer.UnlockedAchievementsToString);
            cmd.Parameters.AddWithValue("@totalWins", CurrentPlayer.TotalWins);
            cmd.Parameters.AddWithValue("@easyAdditionWins", CurrentPlayer.EasyAdditionWins);
            cmd.Parameters.AddWithValue("@mediumAdditionWins", CurrentPlayer.MediumAdditionWins);
            cmd.Parameters.AddWithValue("@hardAdditionWins", CurrentPlayer.HardAdditionWins);
            cmd.Parameters.AddWithValue("@easySubtractionWins", CurrentPlayer.EasySubtractionWins);
            cmd.Parameters.AddWithValue("@mediumSubtractionWins", CurrentPlayer.MediumSubtractionWins);
            cmd.Parameters.AddWithValue("@hardSubtractionWins", CurrentPlayer.HardSubtractionWins);
            cmd.Parameters.AddWithValue("@easyMultiplicationWins", CurrentPlayer.EasyMultiplicationWins);
            cmd.Parameters.AddWithValue("@mediumMultiplicationWins", CurrentPlayer.MediumMultiplicationWins);
            cmd.Parameters.AddWithValue("@hardMultiplicationWins", CurrentPlayer.HardMultiplicationWins);
            cmd.Parameters.AddWithValue("@easyDivisionWins", CurrentPlayer.EasyDivisionWins);
            cmd.Parameters.AddWithValue("@mediumDivisionWins", CurrentPlayer.MediumDivisionWins);
            cmd.Parameters.AddWithValue("@hardDivisionWins", CurrentPlayer.HardDivisionWins);
            cmd.Parameters.AddWithValue("@username", CurrentPlayer.Name);

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    new Notification(ex.Message, "Error Saving Player", NotificationButtons.OK).ShowDialog();
                }
                finally { con.Close(); }
            });
        }
    }
}