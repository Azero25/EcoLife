using EcoLife.Model.Context;
using EcoLife.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Model.Repository
{
    public class HistoryRepository
    {
        // declare private object connection
        private SQLiteConnection _conn;

        // constructor
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
    }
}
