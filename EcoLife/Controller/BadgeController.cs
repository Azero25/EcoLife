using EcoLife.Model.Context;
using EcoLife.Model.Entity;
using EcoLife.Model.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Controller
{
    public class BadgeController
    {
        private BadgeRepository _badgeRepo;

        private void DeleteImageFile(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Delete image error: " + ex.Message);
            }
        }

        public List<Badge> GetAllBadges()
        {
            List<Badge> badges = new List<Badge>();
            using (DbContext context = new DbContext())
            {
                _badgeRepo = new BadgeRepository(context);
                badges = _badgeRepo.GetAllBadge();
            }
            
            return badges;
        }

        public List<Badge> SearchBadges(string name)
        {
            List<Badge> badges = new List<Badge>();
            using (DbContext context = new DbContext())
            {
                _badgeRepo = new BadgeRepository(context);
                badges = _badgeRepo.ReadByNamaBadge(name);
            }
            
            return badges;
        }

        public void CreateBadge(Badge badge, User user)
        {
            if (user != null && user.Role == "admin")
            {
                if (string.IsNullOrEmpty(badge.NameBadge))
                {
                    System.Windows.Forms.MessageBox.Show("Nama Badge harus diisi !!!", "Peringatan",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
                if (string.IsNullOrEmpty(badge.FilePath))
                {
                    System.Windows.Forms.MessageBox.Show("File Path harus diisi !!!", "Peringatan",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
                using (DbContext context = new DbContext())
                {
                    _badgeRepo = new BadgeRepository(context);
                    _badgeRepo.CreateBadge(badge, user);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Hanya admin yang dapat menambahkan badge baru.", "Akses Ditolak",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void UpdateBadge(Badge badge, User user)
        {
            if (user != null && user.Role == "admin")
            {
                if (string.IsNullOrEmpty(badge.NameBadge))
                {
                    System.Windows.Forms.MessageBox.Show("Nama Badge harus diisi !!!", "Peringatan",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
                if (string.IsNullOrEmpty(badge.FilePath))
                {
                    System.Windows.Forms.MessageBox.Show("File Path harus diisi !!!", "Peringatan",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
                using (DbContext context = new DbContext())
                {
                    _badgeRepo = new BadgeRepository(context);
                    _badgeRepo.UpdateBadge(badge, user);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Hanya admin yang dapat memperbarui badge.", "Akses Ditolak",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void DeleteBadge(Badge badge, User user)
        {
            if (user != null && user.Role == "admin")
            {
                using (DbContext context = new DbContext())
                {
                    _badgeRepo = new BadgeRepository(context);
                    _badgeRepo.DeleteBadge(badge.IdBadge, user);
                    DeleteImageFile(badge.FilePath);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Hanya admin yang dapat menghapus badge.", "Akses Ditolak",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
