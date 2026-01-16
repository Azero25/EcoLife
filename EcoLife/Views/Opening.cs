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
    public partial class Opening : Form
    {
        public Opening()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (carousel1.Visible == true)
            {
                carousel1.Visible = false;
                carousel2.Visible = true;
            }
            else if (carousel2.Visible == true)
            {
                carousel2.Visible = false;
                carousel3.Visible = true;
            }
            else if (carousel3.Visible == true)
            {
                carousel3.Visible = false;
                carousel1.Visible = true;
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm form = new LoginForm();
            form.FormClosed += (s, args) => this.Close();
            form.Show();

        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm form = new RegisterForm();
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }
    }
}
