using EcoLife.Controller;
using EcoLife.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EcoLife.Views
{
    public partial class AddUpdateUser : Form
    {
        // declare event handler type data create update challenge
        public delegate void CreateUpdateUserEventHandler(User usr);
        // declare event create
        public event CreateUpdateUserEventHandler onCreateUser;
        // declare event update
        public event CreateUpdateUserEventHandler onUpdateUser;
        // declare controller object
        private UserController userController;
        // declare variable to check status
        private bool isNewData = true;
        // declare field to store user
        private User user;
        // declare field to store admin dashboard
        private User admin;

        private void LoadRole()
        {
            comboRoleUser.Items.Clear();
            comboRoleUser.Items.Add("admin");
            comboRoleUser.Items.Add("user");
            comboRoleUser.SelectedIndex = 1;
        }

        public AddUpdateUser()
        {
            InitializeComponent();

            comboRoleUser.Size = new System.Drawing.Size(225, 25);
        }

        // constructor entry new data
        public AddUpdateUser(string title, UserController controller, User admin)
        {
            InitializeComponent();
            this.admin = admin;
            this.Text = title;
            this.userController = controller;
            titleAddUpdateUser.Text = title;
            LoadRole();
        }

        // constructor entry update data
        public AddUpdateUser(string title, UserController controller, User user, User admin)
        {
            InitializeComponent();
            this.admin = admin;
            this.Text = title;
            this.userController = controller;
            this.user = user;
            this.isNewData = false;
            LoadRole();

            titleAddUpdateUser.Text = title;
            txtNameUser.Text = user.Name;
            txtEmailUser.Text = user.Email;
            comboRoleUser.Text = user.Role;

            txtPasswordUser.PlaceholderText = "Kosongkan jika tidak diubah";
        }

        private void btnTambahUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNameUser.Text) ||
                string.IsNullOrEmpty(txtEmailUser.Text) ||
                comboRoleUser.SelectedItem == null)
                {
                    MessageBox.Show("Semua data wajib diisi!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isNewData && string.IsNullOrEmpty(txtPasswordUser.Text))
                {
                    MessageBox.Show("Password wajib diisi!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isNewData) user = new User();

                if (isNewData)
                {
                    user.Name = txtNameUser.Text.Trim();
                    user.Email = txtEmailUser.Text.Trim();
                    user.Role = comboRoleUser.SelectedItem.ToString();
                    user.Password = txtPasswordUser.Text.Trim();

                    int result = userController.RegisterUser(user);
                    if (result > 0)
                    {
                        onCreateUser(user);
                        MessageBox.Show("User berhasil ditambahkan!", "Informasi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        AdminDashboard form = new AdminDashboard(user);
                        form.FormClosed += (s, args) => this.Close();
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("User gagal ditambahkan!", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    User existingUser = userController.GetUserById(user.IdUser);

                    user.Name = txtNameUser.Text.Trim();
                    user.Email = txtEmailUser.Text.Trim();
                    user.Role = comboRoleUser.SelectedItem.ToString();

                    if (!string.IsNullOrEmpty(txtPasswordUser.Text))
                    {
                        user.Password = txtPasswordUser.Text.Trim();
                    }
                    else
                    {
                        user.Password = existingUser.Password;
                    }

                    int result = userController.UpdateUser(user);
                    if (result > 0)
                    {
                        onUpdateUser(user);
                        MessageBox.Show("User berhasil diubah!", "Informasi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        AdminDashboard form = new AdminDashboard(user);
                        form.FormClosed += (s, args) => this.Close();
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("User gagal diubah!", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackUser_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard form = new AdminDashboard(user);
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }
    }
}
