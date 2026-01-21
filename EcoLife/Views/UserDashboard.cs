using EcoLife.Controller;
using EcoLife.Model.Entity;
using System;
using EcoLife.Model.Repository;
using EcoLife.Model.Context;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace EcoLife.Views
{
    public partial class UserDashboard : Form
    {
        private User currentUser;
        private List<Challenge> activeChallenges;
        private List<int> completedChallengeIds = new List<int>();
        private UserController userController;
        private ChallengeRepository challengeRepo;
        private BadgeController badgeController;
        private PictureBox pbUserBadge;
        private Label lblUserTitle;

        public UserDashboard(User user)
        {
            InitializeComponent();
            this.btnFinishCh1.Click += new System.EventHandler(this.btnFinishCh1_Click);
            this.btnFinishCh2.Click += new System.EventHandler(this.btnFinishCh2_Click);
            this.btnFinishCh3.Click += new System.EventHandler(this.btnFinishCh3_Click);
            this.currentUser = user;
            DbContext context = new DbContext();
            this.challengeRepo = new ChallengeRepository(context);
            this.badgeController = new BadgeController();

            this.Load += UserDashboard_Load;
            this.userController = new UserController();

            InitializeBadgeAndTitle();
        }

        private void InitializeBadgeAndTitle()
        {
            pbUserBadge = this.Controls.Find("pictureBox1", true).FirstOrDefault() as PictureBox;

            if (pbUserBadge == null)
            {
                pbUserBadge = this.Controls.Find("pbBadge", true).FirstOrDefault() as PictureBox;
            }

            if (pbUserBadge == null)
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Panel || ctrl is GroupBox)
                    {
                        pbUserBadge = FindPictureBoxInControl(ctrl);
                        if (pbUserBadge != null) break;
                    }
                }
            }

            if (pbUserBadge != null)
            {
                pbUserBadge.SizeMode = PictureBoxSizeMode.Zoom;
                pbUserBadge.BackColor = Color.White;
                MakePictureBoxCircular(pbUserBadge);
                CreateTitleLabel();
            }
        }

        private void CreateTitleLabel()
        {
            if (pbUserBadge == null) return;

            lblUserTitle = new Label();
            lblUserTitle.Name = "lblUserTitle";
            lblUserTitle.AutoSize = false;
            lblUserTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblUserTitle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblUserTitle.ForeColor = Color.White;

            lblUserTitle.Location = new Point(
                pbUserBadge.Location.X - 20,
                pbUserBadge.Location.Y + pbUserBadge.Height + 5
            );
            lblUserTitle.Size = new Size(pbUserBadge.Width + 40, 25);
            lblUserTitle.Text = "Newbie";

            if (pbUserBadge.Parent != null)
            {
                pbUserBadge.Parent.Controls.Add(lblUserTitle);
                lblUserTitle.BringToFront();
            }
            else
            {
                this.Controls.Add(lblUserTitle);
            }
        }

        private PictureBox FindPictureBoxInControl(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is PictureBox pb)
                {
                    if (pb.BackColor == Color.White || pb.Image == null)
                    {
                        return pb;
                    }
                }

                if (ctrl.HasChildren)
                {
                    PictureBox found = FindPictureBoxInControl(ctrl);
                    if (found != null) return found;
                }
            }
            return null;
        }

        private void MakePictureBoxCircular(PictureBox pb)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pb.Width, pb.Height);
            Region region = new Region(gp);
            pb.Region = region;
        }

        // Update badge dan title berdasarkan level (sinkron dengan sistem badge)
        private void UpdateBadgeAndTitle(int level)
        {
            if (pbUserBadge == null) return;

            try
            {
                List<Badge> allBadges = badgeController.GetAllBadges();
                Badge currentBadge = null;
                string title = "";

                if (level <= 1)
                {
                    currentBadge = allBadges.FirstOrDefault(b =>
                        b.NameBadge.ToLower().Contains("no one") ||
                        b.NameBadge.ToLower().Contains("starter"));
                    title = "🌱 No One";
                }
                else if (level < 10)
                {
                    currentBadge = allBadges.FirstOrDefault(b =>
                        b.NameBadge.ToLower().Contains("bronze") ||
                        b.NameBadge.ToLower().Contains("newbie"));
                    title = "🥉 Eco Newbie";
                }
                else if (level < 25)
                {
                    currentBadge = allBadges.FirstOrDefault(b =>
                        b.NameBadge.ToLower().Contains("silver"));
                    title = "🌿 Eco Explorer";
                }
                else if (level < 50)
                {
                    currentBadge = allBadges.FirstOrDefault(b =>
                        b.NameBadge.ToLower().Contains("gold"));
                    title = "⭐ Eco Warrior";
                }
                else if (level < 100)
                {
                    currentBadge = allBadges.FirstOrDefault(b =>
                        b.NameBadge.ToLower().Contains("platinum"));
                    title = "🏆 Eco Champion";
                }
                else
                {
                    currentBadge = allBadges.FirstOrDefault(b =>
                        b.NameBadge.ToLower().Contains("diamond"));
                    title = "💎 Eco Legend";
                }

                if (currentBadge != null &&
                    !string.IsNullOrEmpty(currentBadge.FilePath) &&
                    System.IO.File.Exists(currentBadge.FilePath))
                {
                    if (pbUserBadge.Image != null)
                    {
                        pbUserBadge.Image.Dispose();
                    }

                    pbUserBadge.Image = Image.FromFile(currentBadge.FilePath);
                    pbUserBadge.BackColor = Color.White;
                }
                else
                {
                    pbUserBadge.Image = null;
                    pbUserBadge.BackColor = Color.FromArgb(220, 220, 220);
                }

                if (lblUserTitle != null)
                {
                    lblUserTitle.Text = title;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateBadgeAndTitle ERROR: " + ex.Message);
            }
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

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            if (currentUser == null) return;

            LoadUserInfo();
            LoadChallenge();
            LoadHistory();
        }

        private void LoadUserInfo()
        {
            lblHelloUser.Text = "Hello, " + currentUser.Name;

            int score = currentUser.TotalScore;
            lblScore.Text = score.ToString();

            int level = GetLevelFromScore(score);
            lblLevel.Text = level.ToString();

            SetProgressBar(score, level);
            UpdateBadgeAndTitle(level);
        }

        // Konversi score ke level (sinkron dengan badge: 1, 10, 25, 50, 100)
        private int GetLevelFromScore(int score)
        {
            if (score < 1000) return 1;
            else if (score < 3000) return 10;
            else if (score < 6000) return 25;
            else if (score < 10000) return 50;
            else return 100;
        }

        // Progress bar menampilkan total score dengan range sesuai level
        private void SetProgressBar(int score, int level)
        {
            int min = 0;
            int max = 1000;

            if (level == 1)
            {
                min = 0;
                max = 1000;
            }
            else if (level == 10)
            {
                min = 1000;
                max = 3000;
            }
            else if (level == 25)
            {
                min = 3000;
                max = 6000;
            }
            else if (level == 50)
            {
                min = 6000;
                max = 10000;
            }
            else if (level == 100)
            {
                min = 10000;
                max = 15000;
            }

            pbLevel.Minimum = min;
            pbLevel.Maximum = max;
            pbLevel.Value = score;

            if (pbLevel.Value < pbLevel.Minimum)
                pbLevel.Value = pbLevel.Minimum;

            if (pbLevel.Value > pbLevel.Maximum)
                pbLevel.Value = pbLevel.Maximum;

            System.Diagnostics.Debug.WriteLine($"[ProgressBar] Score:{score} | Level:{level} | Min:{min} | Max:{max} | Value:{pbLevel.Value}");
        }

        // Load 3 challenge berikutnya yang belum diselesaikan
        private void LoadChallenge()
        {
            try
            {
                DateTime now = DateTime.Now;
                List<Challenge> allChallenges = challengeRepo.ReadByTimeDateChallenge(now);

                activeChallenges = allChallenges
                    .Where(c => !completedChallengeIds.Contains(c.IdChallenge))
                    .Take(3)
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"Total challenges: {allChallenges.Count}, Available: {activeChallenges.Count}");

                ResetChallengeSlots();

                if (activeChallenges.Count >= 1)
                {
                    lblChallengeName1.Text = activeChallenges[0].NameChallenge;
                    lblChallengeDesc1.Text = activeChallenges[0].DecsChallenge;
                    btnFinishCh1.Tag = activeChallenges[0];
                    btnFinishCh1.Enabled = true;
                    btnFinishCh1.Text = "Done";
                }

                if (activeChallenges.Count >= 2)
                {
                    lblChallengeName2.Text = activeChallenges[1].NameChallenge;
                    lblChallengeDesc2.Text = activeChallenges[1].DecsChallenge;
                    btnFinishCh2.Tag = activeChallenges[1];
                    btnFinishCh2.Enabled = true;
                    btnFinishCh2.Text = "Done";
                }

                if (activeChallenges.Count >= 3)
                {
                    lblChallengeName3.Text = activeChallenges[2].NameChallenge;
                    lblChallengeDesc3.Text = activeChallenges[2].DecsChallenge;
                    btnFinishCh3.Tag = activeChallenges[2];
                    btnFinishCh3.Enabled = true;
                    btnFinishCh3.Text = "Done";
                }

                if (activeChallenges.Count == 0)
                {
                    MessageBox.Show("Semua challenge telah diselesaikan! 🎉", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat load challenge: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadChallenge ERROR: {ex.Message}");
            }
        }

        // Reset semua slot challenge ke default
        private void ResetChallengeSlots()
        {
            lblChallengeName1.Text = "Tidak ada challenge";
            lblChallengeDesc1.Text = "Challenge belum tersedia";
            btnFinishCh1.Enabled = false;
            btnFinishCh1.Tag = null;
            btnFinishCh1.Text = "Done";

            lblChallengeName2.Text = "Tidak ada challenge";
            lblChallengeDesc2.Text = "Challenge belum tersedia";
            btnFinishCh2.Enabled = false;
            btnFinishCh2.Tag = null;
            btnFinishCh2.Text = "Done";

            lblChallengeName3.Text = "Tidak ada challenge";
            lblChallengeDesc3.Text = "Challenge belum tersedia";
            btnFinishCh3.Enabled = false;
            btnFinishCh3.Tag = null;
            btnFinishCh3.Text = "Done";
        }

        private void LoadHistory()
        {
            lbHistory.Items.Clear();
        }

        // Proses penyelesaian challenge dan refresh list
        private void FinishChallenge(Guna2Button btn, Guna2HtmlLabel name, Guna2HtmlLabel desc)
        {
            if (btn.Tag == null)
            {
                MessageBox.Show("Challenge tidak ditemukan!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Challenge ch = (Challenge)btn.Tag;

            DialogResult dialogResult = MessageBox.Show(
                $"Apakah Anda yakin telah menyelesaikan challenge '{ch.NameChallenge}'?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.No)
            {
                return;
            }

            int oldScore = currentUser.TotalScore;
            int oldLevel = GetLevelFromScore(oldScore);

            currentUser.TotalScore += ch.PointChallenge;
            int newLevel = GetLevelFromScore(currentUser.TotalScore);

            bool success = userController.UpdateUserScore(currentUser.IdUser, currentUser.TotalScore);

            if (!success)
            {
                MessageBox.Show("Gagal menyimpan skor ke database!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                currentUser.TotalScore = oldScore;
                return;
            }

            completedChallengeIds.Add(ch.IdChallenge);

            lblScore.Text = currentUser.TotalScore.ToString();
            lblLevel.Text = newLevel.ToString();
            SetProgressBar(currentUser.TotalScore, newLevel);

            if (newLevel > oldLevel)
            {
                UpdateBadgeAndTitle(newLevel);

                MessageBox.Show(
                    $"🎉 LEVEL UP! 🎉\n\n" +
                    $"Selamat! Anda naik ke Level {newLevel}!\n" +
                    $"Badge dan Title baru telah di-unlock!\n\n" +
                    $"Total Score: {currentUser.TotalScore}",
                    "Level Up!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    $"Selamat! Anda mendapatkan {ch.PointChallenge} poin!\n" +
                    $"Total Score: {currentUser.TotalScore}",
                    "Challenge Completed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            lbHistory.Items.Insert(0,
                DateTime.Now.ToString("dd/MM HH:mm") + " - Complete " + ch.NameChallenge + " +" + ch.PointChallenge);

            LoadChallenge();
        }

        private void btnFinishCh1_Click(object sender, EventArgs e)
        {
            FinishChallenge(btnFinishCh1, lblChallengeName1, lblChallengeDesc1);
        }

        private void btnFinishCh2_Click(object sender, EventArgs e)
        {
            FinishChallenge(btnFinishCh2, lblChallengeName2, lblChallengeDesc2);
        }

        private void btnFinishCh3_Click(object sender, EventArgs e)
        {
            FinishChallenge(btnFinishCh3, lblChallengeName3, lblChallengeDesc3);
        }
    }
}