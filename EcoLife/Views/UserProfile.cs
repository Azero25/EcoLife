using EcoLife.Controller;
using EcoLife.Model.Entity;
using Guna.UI2.AnimatorNS;
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
    public partial class UserProfile : Form
    {
        // declare controller object
        private UserController _controller;
        // declare field to store user
        private User dtUser;

        public UserProfile()
        {
            InitializeComponent();
        }

        // constructor update profile
        public UserProfile(UserController controller, User user)
        {
            InitializeComponent();
            this._controller = controller;
            this.dtUser = user;

            txtNameProfileUser.Text = dtUser.Name;
            txtEmailProfileUser.Text = dtUser.Email;

            txtPasswordProfileUser.PlaceholderText = "Kosongkan jika tidak diubah";
        }

        private void btnProfileUser_Click(object sender, EventArgs e)
        {
            try
            {

                dtUser.Name = txtNameProfileUser.Text.Trim();
                dtUser.Email = txtEmailProfileUser.Text.Trim();

                dtUser.Password = string.IsNullOrWhiteSpace(txtPasswordProfileUser.Text)
                                  ? null
                                  : txtPasswordProfileUser.Text.Trim();

                int result = _controller.UpdateUser(dtUser);

                if (result > 0)
                {
                    MessageBox.Show("Profile berhasil diperbarui!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackProfileUser_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
