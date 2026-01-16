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

namespace EcoLife.Views
{
    public partial class UserDashboard : Form
    {
        private User currentUser;

        private UserController userController;

        public UserDashboard(User user)
        {
            InitializeComponent();
            this.currentUser = user;

            this.userController = new UserController();
        }

        private void btnAccountUser_Click(object sender, EventArgs e)
        {
            currentUser = userController.GetUserById(currentUser.IdUser);

            if (currentUser == null || currentUser.IdUser <= 0)
            {
                MessageBox.Show("ID User tidak ditemukan!");
                return;
            }

            this.Hide();
            using (UserProfile form = new UserProfile(userController, currentUser))
            {
                form.ShowDialog();
            }

            currentUser = userController.GetUserById(currentUser.IdUser);

            this.Show();
        }

        private void btnLogoutUser_Click(object sender, EventArgs e)
        {
            userController.Logout();
        }
    }
}
