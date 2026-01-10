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
    public class BadgeRepository
    {
        // declare private object connection
        private SQLiteConnection _conn;

        public BadgeRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        private bool EnsureAdmin(User user)
        {
            if (user.Role == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Badge> GetAllBadge(User admin)
        {
            if (EnsureAdmin(admin) == true)
            {

                List<Badge> listBadge = new List<Badge>();

                try
                {
                    // declare sql command
                    string sql = @"SELECT name_badge, file_path, created_at 
                               FROM badge ORDER by name_badge";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                    {
                        using (SQLiteDataReader dtr = cmd.ExecuteReader())
                        {
                            while (dtr.Read())
                            {
                                Badge badge = new Badge();
                                badge.Name_Badge = dtr["name_badge"].ToString();
                                badge.File_Path = dtr["file_path"].ToString();
                                badge.CreatedAt = dtr.GetDateTime(3);

                                listBadge.Add(badge);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
                }

                return listBadge;
            }
            else
            {
                return null;
            }
        }

        public List<Badge> ReadByNamaBadge(string nama)
        {
            List<Badge> listBadge = new List<Badge>();

            try
            {
                // declare sql command
                string sql = @"SELECT name_badge, file_path, created_at
                               WHERE name_challenge LIKE @nama
                               FROM badge ORDER by name_challenge";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@nama", string.Format("%{0}%", nama));

                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Badge badge = new Badge();
                            badge.Name_Badge = dtr["name_badge"].ToString();
                            badge.File_Path = dtr["file_path"].ToString();
                            badge.CreatedAt = dtr.GetDateTime(3);

                            listBadge.Add(badge);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByName error: {0}", ex.Message);
            }

            return listBadge;
        }

        public void CreateBadge(Badge badge, User admin)
        {
            if (EnsureAdmin(admin) == true)
            {
                string sql = @"INSERT INTO badge (name_badge, file_path) " +
                        "VALUES (@name_badge, @file_path)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@name_badge", badge.Name_Badge);
                    cmd.Parameters.AddWithValue("@file_path", badge.File_Path);

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

        public void UpdateBadge(Badge badge, User admin)
        {
            if (EnsureAdmin(admin) == true)
            {
                string sql = @"UPDATE badge SET name_badge=@name_badge, file_path=@file_path
                             where id_badge=@id_badge";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@name_badge", badge.Name_Badge);
                    cmd.Parameters.AddWithValue("@file_path", badge.File_Path);
                    cmd.Parameters.AddWithValue("@id_badge", badge.Id_Badge);

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

        public void DeleteBadge(Badge badge, User admin)
        {
            if (EnsureAdmin(admin) == true)
            {
                string sql = @"DELETE FROM badge
                             where id_badge=@id_badge";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@id_badge", badge.Id_Badge);

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
