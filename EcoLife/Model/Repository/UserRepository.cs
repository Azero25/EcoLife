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
    public class UserRepository
    {
        private SQLiteConnection _conn;

        public UserRepository(DbContext context)
        {
            _conn = context.Conn;
        }


        public int CreateUser(User user)
        {
            string sql = @"INSERT INTO users (name, email, password, role, total_score)
                   VALUES (@name, @email, @password, @role, @total_score)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@role", user.Role);
                cmd.Parameters.AddWithValue("@total_score", 0); // ✅ Set default score = 0

                return cmd.ExecuteNonQuery();
            }
        }

        // Checking email if exists in database
        public bool IsEmailExist(string email)
        {
            string sql = @"SELECT COUNT(1) FROM users WHERE email = @email";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@email", email);
                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }


        public User Login(string email, string password)
        {
            string sql = @"SELECT * FROM users WHERE email = @email LIMIT 1";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@email", email);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    string hashPassword = reader["password"].ToString();

                    // verifikasi password
                    if (!BCrypt.Net.BCrypt.Verify(password, hashPassword))
                        return null;

                    var user = new User
                    {
                        IdUser = Convert.ToInt32(reader["id_user"]),
                        Name = reader["name"].ToString(),
                        Email = reader["email"].ToString(),
                        Role = reader["role"].ToString(),
                        // ✅ TAMBAHKAN INI!
                        TotalScore = reader["total_score"] != DBNull.Value
                            ? Convert.ToInt32(reader["total_score"])
                            : 0,
                        CreatedAt = Convert.ToDateTime(reader["created_at"])
                    };

                    System.Diagnostics.Debug.WriteLine($"[Login] User {user.Name} logged in with score: {user.TotalScore}");

                    return user;
                }
            }
        }


        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string sql = @"SELECT id_user, name, email, role, total_score, created_at
                   FROM users ORDER BY name";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        users.Add(new User
                        {
                            IdUser = Convert.ToInt32(rdr["id_user"]),
                            Name = rdr["name"].ToString(),
                            Email = rdr["email"].ToString(),
                            Role = rdr["role"].ToString(),
                            TotalScore = rdr["total_score"] != DBNull.Value
                                ? Convert.ToInt32(rdr["total_score"])
                                : 0,
                            CreatedAt = Convert.ToDateTime(rdr["created_at"])
                        });
                    }
                }
            }
            return users;
        }



        public User GetUserById(int id)
        {
            User user = null;
            string sql = "SELECT * FROM users WHERE id_user = @id";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            IdUser = Convert.ToInt32(reader["id_user"]),
                            Name = reader["name"].ToString(),
                            Email = reader["email"].ToString(),
                            Password = reader["password"].ToString(),
                            Role = reader["role"].ToString(),
                            TotalScore = reader["total_score"] != DBNull.Value
                                ? Convert.ToInt32(reader["total_score"])
                                : 0,
                            CreatedAt = reader["created_at"] != DBNull.Value
                                ? Convert.ToDateTime(reader["created_at"])
                                : DateTime.Now
                        };

                        System.Diagnostics.Debug.WriteLine($"[GetUserById] Loaded user {id} with score: {user.TotalScore}");
                    }
                }
            }
            return user;
        }


        public List<User> GetUserByName(string name)
        {
            List<User> users = new List<User>();
            string sql = "SELECT * FROM users WHERE name LIKE @name";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            IdUser = Convert.ToInt32(reader["id_user"]),
                            Name = reader["name"].ToString(),
                            Email = reader["email"].ToString(),
                            Password = reader["password"].ToString(),
                            Role = reader["role"].ToString(),
                            // ✅ TAMBAHKAN INI!
                            TotalScore = reader["total_score"] != DBNull.Value
                                ? Convert.ToInt32(reader["total_score"])
                                : 0,
                            CreatedAt = reader["created_at"] != DBNull.Value
                                ? Convert.ToDateTime(reader["created_at"])
                                : DateTime.Now
                        });
                    }
                }
            }

            return users;
        }

        // Get password by id
        public string GetPasswordById(int id)
        {
            string password = null;
            string sql = "SELECT password FROM users WHERE id_user = @id";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        password = reader["password"].ToString();
                    }
                }
            }
            return password;
        }


        // Updating user
        public int UpdateUser(User user)
        {
            if (user.IdUser <= 0)
                throw new Exception("ID user tidak valid.");

            int result;
            bool updatePassword = !string.IsNullOrWhiteSpace(user.Password);

            string sql = $@"UPDATE users 
                    SET name = @name, 
                        email = @email,
                        role = @role,
                        {(updatePassword ? ", password = @password" : "")}
                    WHERE id_user = @id";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@role", user.Role);
                cmd.Parameters.AddWithValue("@id", user.IdUser);

                if (updatePassword)
                    cmd.Parameters.AddWithValue("@password", user.Password);

                result = cmd.ExecuteNonQuery();
            }

            if (result == 0)
                throw new Exception("UPDATE gagal — data tidak ditemukan");

            return result;
        }

        // Deleting user
        public void DeleteUser(User user)
        {
            string sql = @"DELETE FROM users WHERE id_user = @id";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id", user.IdUser);

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

        

        public int UpdateUserScore(int userId, int newScore)
        {
            try
            {
                string sql = "UPDATE users SET total_score = @total_score WHERE id_user = @id_user";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@total_score", newScore);
                    cmd.Parameters.AddWithValue("@id_user", userId);

                    int result = cmd.ExecuteNonQuery();

                    System.Diagnostics.Debug.WriteLine($"Update score: userId={userId}, newScore={newScore}, rows affected={result}");

                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("UpdateUserScore error: {0}", ex.Message);
                throw new Exception("Gagal mengupdate skor user", ex);
            }
        }



    }
}
