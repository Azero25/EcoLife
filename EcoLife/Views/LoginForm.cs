using EcoLife.Controller;
using EcoLife.Model.Entity;
using EcoLife.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLife.Views
{
    public partial class LoginForm : Form
    {
        private string storagePath = Path.GetFullPath(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Storage")
        );

        private string eyeOpenPath;
        private string eyeClosedPath;

        private bool passwordVisible = false;

        public LoginForm()
        {
            InitializeComponent();
            
            eyeOpenPath = Path.Combine(storagePath, "show.png");
            eyeClosedPath = Path.Combine(storagePath, "hide.png");

            txtPasswordLog.UseSystemPasswordChar = true;
        }

        private void linkLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm form = new RegisterForm();
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserController controller = new UserController();
            User user = controller.LoginUser(txtEmailLog.Text, txtPasswordLog.Text);

            if (user == null) return;

            if (string.IsNullOrEmpty(user.Role))
            {
                MessageBox.Show("Role user tidak ditemukan!");
                return;
            }

            this.Hide();

            if (user.Role == "admin")
            {
                AdminDashboard adminDashboard = new AdminDashboard(user);
                adminDashboard.FormClosed += (s, args) => this.Close();
                adminDashboard.Show();
            }
            else
            {
                UserDashboard userDashboard = new UserDashboard(user);
                userDashboard.FormClosed += (s, args) => this.Close();
                userDashboard.Show();
            }
        }

        private void showHidePasswordLog_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            txtPasswordLog.UseSystemPasswordChar = !passwordVisible;

            string path = passwordVisible ? eyeClosedPath : eyeOpenPath;
            if(File.Exists(path))
            {
                // Dispose gambar lama supaya tidak memory leak
                if (showHidePasswordLog.Image != null)
                    showHidePasswordLog.Image.Dispose();

                showHidePasswordLog.Image = Image.FromFile(path);
            }
        }

        private void btnBackLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Opening opening = new Opening();
            opening.FormClosed += (s, args) => this.Close();
            opening.Show();
        }
    }
}
