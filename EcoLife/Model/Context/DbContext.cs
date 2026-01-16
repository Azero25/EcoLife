using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Model.Context
{
    public class DbContext : IDisposable
    {
        // Declare private variable connection
        private SQLiteConnection _conn;

        // Declare property connection
        public SQLiteConnection Conn
        {
            get { return _conn ?? (_conn = GetOpenConnection()); }
        }

        // Function / Method to connection database
        private SQLiteConnection GetOpenConnection()
        {
            SQLiteConnection conn = null;

            try
            {
                string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
                string dbPath = Path.Combine(projectDir, "Database");
                string dbName = Path.Combine(dbPath, "DbEcoLife.db");

                if (!Directory.Exists(dbPath))
                {
                    Directory.CreateDirectory(dbPath);
                }
                string connectionString = string.Format("Data Source={0};Version=3", dbName);

                conn = new SQLiteConnection(connectionString);
                conn.Open();

                // Create tables if database is new
                CreateTables(conn);

                // Insert user admin if not exists
                InsertAdminUserIfNotExists(conn);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Open Connection Error: {0}", ex.Message);
            }

            return conn;
        }

        // Creating tables
        private void CreateTables(SQLiteConnection conn)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                // Table user
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS users (
                    id_user INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    email TEXT UNIQUE NOT NULL,
                    password TEXT NOT NULL,
                    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                    role TEXT CHECK(role IN('admin', 'user')) NOT NULL
                ); ";
                cmd.ExecuteNonQuery();

                // Table badge
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS badge (
                    id_badge INTEGER PRIMARY KEY AUTOINCREMENT,
                    name_badge TEXT NOT NULL,
                    file_path TEXT NOT NULL,
                    created_at DATETIME DEFAULT CURRENT_TIMESTAMP
                ); ";
                cmd.ExecuteNonQuery();

                // Table challenge
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS challenge (
                    id_challenge INTEGER PRIMARY KEY AUTOINCREMENT,
                    name_challenge TEXT NOT NULL,
                    desc_challenge TEXT NULL,
                    point_challenge INTEGER NOT NULL,
                    created_at DATETIME DEFAULT CURRENT_TIMESTAMP
                ); ";
                cmd.ExecuteNonQuery();

                // Table history
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS history (
                    id_history INTEGER PRIMARY KEY AUTOINCREMENT,
                    id_user INTEGER NOT NULL,
                    id_challenge INTEGER NOT NULL,
                    name_history TEXT NOT NULL,
                    desc_history TEXT NULL,
                    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (id_user) REFERENCES users(id_user) ON DELETE CASCADE ON UPDATE CASCADE,
                    FOREIGN KEY (id_challenge) REFERENCES challenge(id_challenge) ON DELETE CASCADE ON UPDATE CASCADE
                ); ";
                cmd.ExecuteNonQuery();

                // Table leaderboard
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS leaderboard (
                    id_leaderboard INTEGER PRIMARY KEY AUTOINCREMENT,
                    id_user INTEGER NOT NULL,
                    point INTEGER NOT NULL,
                    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (id_user) REFERENCES users(id_user) ON DELETE CASCADE ON UPDATE CASCADE
                ); ";
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertAdminUserIfNotExists(SQLiteConnection conn)
        {
            string checkAdminSql = "SELECT COUNT(1) FROM users WHERE email='admin@gmail.com'";
            using (SQLiteCommand cmd = new SQLiteCommand(checkAdminSql, conn))
            {
                long count = (long)cmd.ExecuteScalar();
                if (count == 0)
                {
                    // Insert admin karena belum ada
                    string insertAdmin = @"INSERT INTO users (name, email, password, role) 
                                   VALUES (@name, @email, @password, @role)";
                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertAdmin, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@name", "Admin");
                        insertCmd.Parameters.AddWithValue("@email", "admin@gmail.com");
                        insertCmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword("admin123"));
                        insertCmd.Parameters.AddWithValue("@role", "admin");
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Function / Method to delete database object from memory when not use
        public void Dispose()
        {
            if (_conn != null)
            {
                try
                {
                    if (_conn.State != ConnectionState.Closed) _conn.Close();
                }
                finally
                {
                    _conn.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
