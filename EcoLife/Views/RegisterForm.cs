using EcoLife.Controller;
using EcoLife.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLife.Views
{
    public partial class RegisterForm : Form
    {
        private UserController userController;
        private string storagePath = Path.GetFullPath(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Storage")
        );

        private string eyeOpenPath;
        private string eyeClosedPath;

        private bool passwordVisible = false;

        public RegisterForm()
        {
            InitializeComponent();
            
            eyeOpenPath = Path.Combine(storagePath, "show.png");
            eyeClosedPath = Path.Combine(storagePath, "hide.png");

            txtPasswordReg.UseSystemPasswordChar = true;
        }

        private void linkRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm form = new LoginForm();
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (btnRegister.Enabled)
            {
                User user = new User();
                user.Name = txtNameReg.Text.Trim();
                user.Email = txtEmailReg.Text.Trim();
                user.Password = txtPasswordReg.Text.Trim();

                userController = new UserController();
                userController.RegisterUser(user);

                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
        }

        private void showHidePasswordReg_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            txtPasswordReg.UseSystemPasswordChar = !passwordVisible;

            string path = passwordVisible ? eyeClosedPath : eyeOpenPath;
            if (File.Exists(path))
            {
                // Dispose gambar lama supaya tidak memory leak
                if (showHidePasswordReg.Image != null)
                    showHidePasswordReg.Image.Dispose();

                showHidePasswordReg.Image = Image.FromFile(path);
            }
        }

        private void btnBackRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Opening opening = new Opening();
            opening.FormClosed += (s, args) => this.Close();
            opening.Show();
        }
    }
}
