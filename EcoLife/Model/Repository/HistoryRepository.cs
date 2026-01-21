using EcoLife.Model.Context;
using EcoLife.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Model.Repository
{
    public class HistoryRepository
    {
        private SQLiteConnection _conn;
        public HistoryRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        public List<History> GetAllHistoryUser(User user)
        {

            List<History> listHistory = new List<History>();

            try
            {
                // declare sql command
                string sql = @"
                    SELECT 
                        h.id_history,
                        c.name_challenge,
                        c.desc_challenge,
                        h.created_at
                    FROM history h
                    JOIN challenge c ON h.id_challenge = c.id_challenge
                    WHERE h.id_user = @idUser
                    ORDER BY h.created_at DESC
                ";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@idUser", user.IdUser);

                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            History history = new History();
                            history.IdHistory = dtr.GetInt32(0);
                            history.NameHistory = dtr["name_history"].ToString();
                            history.DecsHistory = dtr.IsDBNull(2) ? null : dtr["desc_history"].ToString();
                            history.CreatedAt = dtr.GetDateTime(3);

                            listHistory.Add(history);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }

            return listHistory;
        }

        public void CreateHistory(User user, Challenge challenge)
        {
            string sql = @"
                INSERT INTO history (id_user, id_challenge, name_history, desc_history)
                SELECT 
                    @idUser,
                    c.id_challenge,
                    c.name_challenge,
                    c.desc_challenge
                FROM challenge c
                WHERE c.id_challenge = @idChallenge
            ";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@idUser", user.IdUser);
                cmd.Parameters.AddWithValue("@idChallenge", challenge.IdChallenge);

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

        public History GetHistoryById(int idHistory)
        {
            History history = null;
            string sql = @"
                SELECT 
                    id_history,
                    id_user,
                    name_history,
                    desc_history,
                    created_at
                FROM history
                WHERE id_history = @idHistory
            ";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@idHistory", idHistory);
                using (SQLiteDataReader dtr = cmd.ExecuteReader())
                {
                    if (dtr.Read())
                    {
                        history = new History();
                        history.IdHistory = dtr.GetInt32(0);
                        history.IdUser = dtr.GetInt32(1);
                        history.NameHistory = dtr["name_history"].ToString();
                        history.DecsHistory = dtr.IsDBNull(3) ? null : dtr["desc_history"].ToString();
                        history.CreatedAt = dtr.GetDateTime(4);
                    }
                }
            }
            return history;
        }

        public List<History> GetHistoryByActivityType(int userId, string activityType)
        {
            List<History> histories = new List<History>();

            string sql = @"
                SELECT 
                    id_history,
                    id_user,
                    name_history,
                    desc_history,
                    created_at
                FROM history
                WHERE id_user = @idUser AND name_history = @activityType
                ORDER BY created_at DESC
            ";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@idUser", userId);
                cmd.Parameters.AddWithValue("@activityType", activityType ?? string.Empty);

                try
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            History history = new History();

                            history.IdHistory = dtr.IsDBNull(0) ? 0 : dtr.GetInt32(0);
                            history.IdUser = dtr.IsDBNull(1) ? 0 : dtr.GetInt32(1);
                            history.NameHistory = dtr.IsDBNull(2) ? null : dtr.GetString(2);
                            history.DecsHistory = dtr.IsDBNull(3) ? null : dtr.GetString(3);
                            history.CreatedAt = dtr.IsDBNull(4) ? DateTime.MinValue : dtr.GetDateTime(4);

                            histories.Add(history);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("GetHistoryByActivityType error: {0}", ex.Message);
                }
            }

            return histories;
        }

        public History GetMostRecentActivity(int userId)
        {
            History history = null;

            string sql = @"
                SELECT 
                    id_history,
                    id_user,
                    name_history,
                    desc_history,
                    created_at
                FROM history
                WHERE id_user = @idUser
                ORDER BY created_at DESC
                LIMIT 1
            ";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@idUser", userId);

                try
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            history = MapToHistory(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("GetMostRecentActivity error: {0}", ex.Message);
                }
            }

            return history;
        }

        private History MapToHistory(SQLiteDataReader reader)
        {
            History history = new History();
            history.IdHistory = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            history.IdUser = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
            history.NameHistory = reader.IsDBNull(2) ? null : reader.GetString(2);
            history.DecsHistory = reader.IsDBNull(3) ? null : reader.GetString(3);

            if (reader.IsDBNull(4))
            {
                history.CreatedAt = DateTime.MinValue;
            }
            else
            {
                try
                {
                    history.CreatedAt = reader.GetDateTime(4);
                }
                catch
                {
                    object val = reader.GetValue(4);
                    history.CreatedAt = val == null ? DateTime.MinValue : Convert.ToDateTime(val);
                }
            }

            return history;
        }

        public bool Update(History history)
        {
            if (history == null)
                return false;

            bool result = false;

            string sql = @"
                UPDATE history
                SET
                    name_history = @NameHistory,
                    desc_history = @DescHistory,
                    created_at = @CreatedAt
                WHERE id_history = @IdHistory
            ";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdHistory", history.IdHistory);
                cmd.Parameters.AddWithValue("@NameHistory", history.NameHistory ?? string.Empty);

                if (history.DecsHistory != null)
                    cmd.Parameters.AddWithValue("@DescHistory", history.DecsHistory);
                else
                    cmd.Parameters.AddWithValue("@DescHistory", DBNull.Value);

                if (history.CreatedAt == DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@CreatedAt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@CreatedAt", history.CreatedAt);

                try
                {
                    result = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Update error: {0}", ex.Message);
                }
            }

            return result;
        }

        public bool Delete(int historyId)
        {
            bool result = false;

            string sql = "DELETE FROM history WHERE id_history = @IdHistory";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdHistory", historyId);

                try
                {
                    result = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print($"Error deleting history: {ex.Message}");
                }
            }

            return result;
        }

        public int GetTotalPointsEarned(int userId)
        {
            int totalPoints = 0;

            string sql = "SELECT IFNULL(SUM(points), 0) FROM history WHERE id_user = @IdUser";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdUser", userId);

                try
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalPoints = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print($"Error getting total points: {ex.Message}");
                }
            }

            return totalPoints;
        }


        public int GetHistoryCount(int userId)
        {
            int count = 0;

            string sql = "SELECT COUNT(*) FROM history WHERE id_user = @IdUser";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdUser", userId);

                try
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        count = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print($"Error getting history count: {ex.Message}");
                }
            }

            return count;
        }

       
        public Dictionary<string, int> GetActivityCountByType(int userId)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

           
            string sql = @"SELECT name_history AS ActivityType, COUNT(*) as Count 
                          FROM history 
                          WHERE id_user = @IdUser 
                          GROUP BY name_history";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdUser", userId);

                try
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string activityType = reader["ActivityType"] != null ? reader["ActivityType"].ToString() : string.Empty;
                            int count = 0;
                            object cVal = reader["Count"];
                            if (cVal != null && cVal != DBNull.Value)
                            {
                                count = Convert.ToInt32(cVal);
                            }
                            counts[activityType] = count;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print($"Error getting activity count by type: {ex.Message}");
                }
            }

            return counts;
        }


    }
}