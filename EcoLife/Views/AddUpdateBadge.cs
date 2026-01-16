using EcoLife.Controller;
using EcoLife.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLife.Views
{
    public partial class AddUpdateBadge : Form
    {
        // declare event handler type data create update challenge
        public delegate void CreateUpdateBadgeEventHandler(Badge bg);
        // declare event create
        public event CreateUpdateBadgeEventHandler onCreateBadge;
        // declare event update
        public event CreateUpdateBadgeEventHandler onUpdateBadge;
        // declare controller object
        private BadgeController badgeController;
        // declare variable to check status
        private bool isNewData = true;
        // declare field to store challenge
        private Badge badge;
        // declare field to store admin dashboard
        private User user;
        // declare variable to store file path
        private string filePath;

        public AddUpdateBadge()
        {
            InitializeComponent();
        }

        public AddUpdateBadge(string title, BadgeController controller, User user)
        {
            InitializeComponent();
            this.user = user;
            this.Text = title;
            this.badgeController = controller;
            titleAddUpdateBadge.Text = title;
        }

        public AddUpdateBadge(string title, BadgeController controller, Badge badge, User user)
        {
            InitializeComponent();
            this.user = user;
            this.Text = title;
            this.badgeController = controller;
            this.badge = badge;
            this.isNewData = false;
            titleAddUpdateBadge.Text = title;

            txtNameBadge.Text = badge.NameBadge;
            if (badge.FilePath != null) { 
                picBadge.Image = Image.FromFile(badge.FilePath);
                picBadge.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private string SaveBadgeImage(string sourcePath)
        {
            string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
            string folder = Path.Combine(projectDir, "Storage", "Badge");
            
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName = Path.GetFileName(sourcePath);
            string destPath = Path.Combine(folder, fileName);
            File.Copy(sourcePath, destPath, true);

            return destPath;
        }

        private void btnPilih_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.png;*.jpeg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                picBadge.Image = Image.FromFile(filePath);
                picBadge.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtNameBadge.Text))
            {
                MessageBox.Show("Nama Badge harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(isNewData && filePath == null)
            {
                MessageBox.Show("Nama Badge harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string savedPath;

            if(filePath != null)
            {
                savedPath = SaveBadgeImage(filePath);
            } else
            {
                savedPath = badge.FilePath;
            }

            Badge newBadge = new Badge
            {
                NameBadge = txtNameBadge.Text.Trim(),
                FilePath = savedPath
            };

            if(isNewData)
            {
                badgeController.CreateBadge(newBadge, user);
                onCreateBadge(newBadge);
                MessageBox.Show("Badge berhasil ditambahkan!", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                AdminDashboard form = new AdminDashboard(user);
                form.FormClosed += (s, args) => this.Close();
                form.Show();
            }
            else
            {
                newBadge.IdBadge = badge.IdBadge;
                badgeController.UpdateBadge(newBadge, user);
                onUpdateBadge(newBadge);
                MessageBox.Show("Badge berhasil diupdate!", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                AdminDashboard form = new AdminDashboard(user);
                form.FormClosed += (s, args) => this.Close();
                form.Show();
            }
        }

        private void btnBackBadge_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard form = new AdminDashboard(user);
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }
    }
}
