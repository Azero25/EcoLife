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
    public class ChallengeRepository
    {
        // declare private object connection
        private SQLiteConnection _conn;

        public ChallengeRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        private bool EnsureAdmin(User user)
        {
            if (user != null && user.Role == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Challenge> GetAllChallenge()
        {
            List<Challenge> listChallenge = new List<Challenge>();

            try
            {
                string sql = @"SELECT id_challenge, name_challenge, desc_challenge,
                              point_challenge, created_at
                       FROM challenge
                       ORDER BY name_challenge";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                using (SQLiteDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        Challenge challenge = new Challenge
                        {
                            IdChallenge = Convert.ToInt32(dtr["id_challenge"]),
                            NameChallenge = dtr["name_challenge"].ToString(),
                            DecsChallenge = dtr["desc_challenge"].ToString(),
                            PointChallenge = Convert.ToInt32(dtr["point_challenge"]),
                            CreatedAt = Convert.ToDateTime(dtr["created_at"])
                        };

                        listChallenge.Add(challenge);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal mengambil data challenge", ex);
            }

            return listChallenge;
        }

        public List<Challenge> ReadByNamaChallenge(string nama)
        {
            List<Challenge> listChallenge = new List<Challenge>();

            try
            {
                // declare sql command
                string sql = @"SELECT name_challenge, desc_challenge, point_challenge, created_at
                             FROM challenge
                             WHERE name_challenge LIKE @nama
                             ORDER BY name_challenge";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@nama", string.Format("%{0}%", nama));

                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Challenge challenge = new Challenge();
                            challenge.NameChallenge = dtr["name_challenge"].ToString();
                            challenge.DecsChallenge = dtr["desc_challenge"].ToString();
                            challenge.PointChallenge = Convert.ToInt32(dtr["point_challenge"]);
                            challenge.CreatedAt = Convert.ToDateTime(dtr["created_at"]);

                            listChallenge.Add(challenge);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByName error: {0}", ex.Message);
            }

            return listChallenge;
        }

        public List<Challenge> ReadByTimeDateChallenge(DateTime dateTime)
        {
            List<Challenge> challenges = new List<Challenge>();

            try             {
                string sql = @"SELECT id_challenge, name_challenge, desc_challenge,
                              point_challenge, created_at
                       FROM challenge
                       WHERE DATE(created_at) = DATE(@dateTime)
                       ORDER BY name_challenge";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@dateTime", dateTime);
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Challenge challenge = new Challenge
                            {
                                IdChallenge = Convert.ToInt32(dtr["id_challenge"]),
                                NameChallenge = dtr["name_challenge"].ToString(),
                                DecsChallenge = dtr["desc_challenge"].ToString(),
                                PointChallenge = Convert.ToInt32(dtr["point_challenge"]),
                                CreatedAt = Convert.ToDateTime(dtr["created_at"])
                            };
                            challenges.Add(challenge);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByTime error: {0}", ex.Message);
            }

            return challenges;
        }

        public void CreateChallenge(Challenge challenge, User admin)
        {
            if (EnsureAdmin(admin) == true)
            {
                string sql = @"INSERT INTO challenge (name_challenge, desc_challenge, point_challenge) " +
                        "VALUES (@name_challenge, @desc_challenge, @point_challenge)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@name_challenge", challenge.NameChallenge);
                    cmd.Parameters.AddWithValue("@desc_challenge", challenge.DecsChallenge);
                    cmd.Parameters.AddWithValue("@point_challenge", challenge.PointChallenge);

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

        public void UpdateChallenge(Challenge challenge, User admin)
        {
            if (EnsureAdmin(admin) == true)
            {
                string sql = @"UPDATE challenge SET name_challenge=@name_challenge, desc_challenge=@desc_challenge,
                             point_challenge=@point_challenge where id_challenge=@id_challenge";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@name_challenge", challenge.NameChallenge);
                    cmd.Parameters.AddWithValue("@desc_challenge", challenge.DecsChallenge);
                    cmd.Parameters.AddWithValue("@point_challenge", challenge.PointChallenge);
                    cmd.Parameters.AddWithValue("@id_challenge", challenge.IdChallenge);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.Print("Update error: {0}", ex.Message);
                    }
                }
            }
        }

        public void DeleteChallenge(int IdChallenge, User admin)
        {
            if (EnsureAdmin(admin) == true)
            {
                string sql = @"DELETE FROM challenge
                   WHERE id_challenge = @id_challenge";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@id_challenge", IdChallenge);

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
}
