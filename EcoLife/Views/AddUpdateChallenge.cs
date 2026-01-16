using EcoLife.Controller;
using EcoLife.Model.Entity;
using EcoLife.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLife.Views
{
    public partial class AddUpdateChallenge : Form
    {
        // declare event handler type data create update challenge
        public delegate void CreateUpdateChallengeEventHandler(Challenge chl);
        // declare event create
        public event CreateUpdateChallengeEventHandler onCreateChallenge;
        // declare event update
        public event CreateUpdateChallengeEventHandler onUpdateChallenge;
        // declare controller object
        private ChallengeController challengeController;
        // declare variable to check status
        private bool isNewData = true;
        // declare field to store challenge
        private Challenge challenge;
        // declare field to store admin dashboard
        private User user;

        public AddUpdateChallenge()
        {
            InitializeComponent();
        }

        // Contructor entry new data
        public AddUpdateChallenge(string title, ChallengeController controller, User user)
        {
            InitializeComponent();

            this.user = user;
            this.Text = title;
            this.challengeController = controller;
            titleAddUpdateChallenge.Text = title;
        }

        // Contructor entry update data
        public AddUpdateChallenge(string title, ChallengeController controller, Challenge challenge, User user)
        {
            InitializeComponent();

            this.user = user;
            this.Text = title;
            this.challengeController = controller;
            this.challenge = challenge;
            this.isNewData = false;
            titleAddUpdateChallenge.Text = title;

            txtNameChallenge.Text = challenge.NameChallenge;
            txtDescChallenge.Text = challenge.DecsChallenge;
            txtPointChallenge.Text = challenge.PointChallenge.ToString();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (isNewData) challenge = new Challenge();

            challenge.NameChallenge = txtNameChallenge.Text.Trim();
            challenge.DecsChallenge = txtDescChallenge.Text.Trim();
            challenge.PointChallenge = int.Parse(txtPointChallenge.Text.Trim());

            if (isNewData)
            {
                challengeController.CreateChallenge(challenge, user);
                onCreateChallenge(challenge);

                MessageBox.Show("Challenge berhasil ditambahkan!", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                AdminDashboard form = new AdminDashboard(user);
                form.FormClosed += (s, args) => this.Close();
                form.Show();
            } else
            {
                challengeController.UpdateChallenge(challenge, user);
                onUpdateChallenge(challenge);

                MessageBox.Show("Challenge berhasil diupdate!", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.Hide();
                AdminDashboard form = new AdminDashboard(user);
                form.FormClosed += (s, args) => this.Close();
                form.Show();
            }
        }

        private void btnBackChallenge_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard form = new AdminDashboard(user);
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }
    }
}
