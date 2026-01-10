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

        // Creating user (Register user)
        public int CreateUser(User user)
        {
            string sql = @"INSERT INTO users (name, email, password, role)
                           VALUES (@name, @email, @password, @role)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@role", user.Role);

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

        // Login User
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

                    return new User
                    {
                        Id_User = Convert.ToInt32(reader["id_user"]),
                        Name = reader["name"].ToString(),
                        Email = reader["email"].ToString(),
                        Role = reader["role"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["created_at"])
                    };
                }
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string sql = @"SELECT id_user, name, email, role, created_at
                           FROM users ORDER BY name";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        users.Add(new User
                        {
                            Id_User = Convert.ToInt32(rdr["id_user"]),
                            Name = rdr["name"].ToString(),
                            Email = rdr["email"].ToString(),
                            Role = rdr["role"].ToString(),
                            CreatedAt = Convert.ToDateTime(rdr["created_at"])
                        });
                    }
                }
            }
            return users;
        }

        // Get data user by name
        public User GetUserByName(string name)
        {
            string sql = @"SELECT id_user, name, email, role, created_at
                           FROM users WHERE name = @name";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", name);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return new User
                    {
                        Id_User = Convert.ToInt32(reader["id_user"]),
                        Name = reader["name"].ToString(),
                        Email = reader["email"].ToString(),
                        Role = reader["role"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["created_at"])
                    };
                }
            }
        }

        // Updating user
        public int UpdateUser(User user)
        {
            string sql = @"UPDATE users 
                           SET name = @name,
                               email = @email,
                               password = @password,
                               role = @role
                           WHERE id_user = @id";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@role", user.Role);
                cmd.Parameters.AddWithValue("@id", user.Id_User);

                return cmd.ExecuteNonQuery();
            }
        }

        // Deleting user
        public void DeleteUser(User user)
        {
            string sql = @"DELETE FROM users WHERE id_user = @id";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id", user.Id_User);

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
