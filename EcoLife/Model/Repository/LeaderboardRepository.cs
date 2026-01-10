using EcoLife.Model.Context;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Model.Repository
{
    public class LeaderboardRepository
    {
        // declare private object connection
        private SQLiteConnection _conn;

        public LeaderboardRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        // Insert point per user
        public void InsertPoint(int idUser, int point)
        {
            string sql = @"
                INSERT INTO leaderboard (id_user, point)
                VALUES (@idUser, @point)
            ";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@idUser", idUser);
                cmd.Parameters.AddWithValue("@point", point);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }
        }

        // Get All Leaderboard
        public List<(string Name, int totalPoint)> GetAllLeaderboard()
        {
            var result = new List<(string, int)>();

            try
            {
                // declare sql command
                string sql = @"
                    SELECT 
                        u.name,
                        COALESCE(SUM(l.point), 0) AS total_point
                    FROM users u
                    LEFT JOIN leaderboard l ON u.id_user = l.id_user
                    GROUP BY u.id_user, u.name
                    ORDER BY total_point DESC
                ";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            result.Add((
                                dtr.GetString(0),
                                dtr.GetInt32(1)
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }

            return result;
        }

        // Get Point Total By User
        public int GetTotalPointUser(int idUser)
        {
            int result = 0;

            string sql = @"
                SELECT COALESCE(SUM(point), 0)
                FROM leaderboard
                WHERE id_user = @idUser
            ";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@idUser", idUser);

                try
                {
                    result = System.Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Get Point User error: {0}", ex.Message);
                }
            }
            return result;
        }

        // Delete Point By User
        public void DeleteByUser(int idUser)
        {
            string sql = @"DELETE FROM leaderboard WHERE id_user = @idUser";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@idUser", idUser);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }
        }
    }
}
