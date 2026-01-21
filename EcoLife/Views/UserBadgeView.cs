using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EcoLife.Controller;
using EcoLife.Model.Entity;

namespace EcoLife.Views
{
    public partial class UserBadgeView : Form
    {
        private BadgeController _badgeController;
        private User _currentUser;
        private Badge _selectedBadge;
        private PictureBox _userBadgePictureBox; 

        public UserBadgeView(User user, PictureBox userBadgePictureBox)
        {
            InitializeCustomComponents();
            _badgeController = new BadgeController();
            _currentUser = user;
            _userBadgePictureBox = userBadgePictureBox;
            LoadBadges();
        }

        private void InitializeCustomComponents()
        {
            this.SuspendLayout();

            // Form settings
            this.ClientSize = new Size(800, 600);
            this.Text = "Pilih Badge Anda";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(20, 60, 60);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "Pilih Badge Anda";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Size = new Size(300, 40);
            this.Controls.Add(lblTitle);

            // Search Panel
            Panel searchPanel = new Panel();
            searchPanel.Location = new Point(20, 70);
            searchPanel.Size = new Size(760, 50);
            searchPanel.BackColor = Color.FromArgb(30, 80, 80);

            Label lblSearch = new Label();
            lblSearch.Text = "Cari Badge:";
            lblSearch.ForeColor = Color.White;
            lblSearch.Location = new Point(10, 15);
            lblSearch.Size = new Size(100, 20);
            searchPanel.Controls.Add(lblSearch);

            TextBox txtSearch = new TextBox();
            txtSearch.Name = "txtSearch";
            txtSearch.Location = new Point(120, 12);
            txtSearch.Size = new Size(300, 25);
            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.TextChanged += TxtSearch_TextChanged;
            searchPanel.Controls.Add(txtSearch);

            Button btnClearSearch = new Button();
            btnClearSearch.Text = "Clear";
            btnClearSearch.Location = new Point(430, 10);
            btnClearSearch.Size = new Size(80, 30);
            btnClearSearch.BackColor = Color.FromArgb(100, 150, 150);
            btnClearSearch.ForeColor = Color.White;
            btnClearSearch.FlatStyle = FlatStyle.Flat;
            btnClearSearch.Click += (s, e) => {
                txtSearch.Text = "";
                LoadBadges();
            };
            searchPanel.Controls.Add(btnClearSearch);

            this.Controls.Add(searchPanel);

            // FlowLayoutPanel for badges
            FlowLayoutPanel flowPanel = new FlowLayoutPanel();
            flowPanel.Name = "flowPanelBadges";
            flowPanel.Location = new Point(20, 130);
            flowPanel.Size = new Size(760, 380);
            flowPanel.AutoScroll = true;
            flowPanel.BackColor = Color.FromArgb(30, 80, 80);
            flowPanel.Padding = new Padding(10);
            this.Controls.Add(flowPanel);

            // Button Panel
            Panel buttonPanel = new Panel();
            buttonPanel.Location = new Point(20, 520);
            buttonPanel.Size = new Size(760, 50);
            buttonPanel.BackColor = Color.FromArgb(20, 60, 60);

            Button btnSelect = new Button();
            btnSelect.Name = "btnSelect";
            btnSelect.Text = "Pilih Badge";
            btnSelect.Location = new Point(530, 10);
            btnSelect.Size = new Size(120, 35);
            btnSelect.BackColor = Color.FromArgb(80, 180, 180);
            btnSelect.ForeColor = Color.White;
            btnSelect.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSelect.FlatStyle = FlatStyle.Flat;
            btnSelect.Enabled = false;
            btnSelect.Click += BtnSelect_Click;
            buttonPanel.Controls.Add(btnSelect);

            Button btnCancel = new Button();
            btnCancel.Text = "Batal";
            btnCancel.Location = new Point(660, 10);
            btnCancel.Size = new Size(90, 35);
            btnCancel.BackColor = Color.FromArgb(180, 80, 80);
            btnCancel.ForeColor = Color.White;
            btnCancel.Font = new Font("Segoe UI", 10);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += (s, e) => this.Close();
            buttonPanel.Controls.Add(btnCancel);

            this.Controls.Add(buttonPanel);

            this.ResumeLayout(false);
        }

        private void LoadBadges(string searchTerm = "")
        {
            FlowLayoutPanel flowPanel = this.Controls.Find("flowPanelBadges", true)[0] as FlowLayoutPanel;
            flowPanel.Controls.Clear();

            List<Badge> badges;

            if (string.IsNullOrEmpty(searchTerm))
            {
                badges = _badgeController.GetAllBadges();
            }
            else
            {
                badges = _badgeController.SearchBadges(searchTerm);
            }

            if (badges.Count == 0)
            {
                Label lblNoData = new Label();
                lblNoData.Text = "Tidak ada badge tersedia";
                lblNoData.ForeColor = Color.White;
                lblNoData.Font = new Font("Segoe UI", 12);
                lblNoData.AutoSize = true;
                lblNoData.Padding = new Padding(20);
                flowPanel.Controls.Add(lblNoData);
                return;
            }

            foreach (Badge badge in badges)
            {
                Panel badgePanel = CreateBadgePanel(badge);
                flowPanel.Controls.Add(badgePanel);
            }
        }

        private Panel CreateBadgePanel(Badge badge)
        {
            Panel panel = new Panel();
            panel.Size = new Size(160, 200);
            panel.BackColor = Color.FromArgb(40, 100, 100);
            panel.Margin = new Padding(10);
            panel.Tag = badge;
            panel.Cursor = Cursors.Hand;

            // Badge PictureBox
            PictureBox pbBadge = new PictureBox();
            pbBadge.Size = new Size(120, 120);
            pbBadge.Location = new Point(20, 20);
            pbBadge.SizeMode = PictureBoxSizeMode.Zoom;
            pbBadge.BackColor = Color.White;

            // Load image
            try
            {
                if (!string.IsNullOrEmpty(badge.FilePath) && System.IO.File.Exists(badge.FilePath))
                {
                    pbBadge.Image = Image.FromFile(badge.FilePath);
                }
                else
                {
                    // Default image jika file tidak ada
                    pbBadge.BackColor = Color.LightGray;
                }
            }
            catch
            {
                pbBadge.BackColor = Color.LightGray;
            }

            // Make PictureBox circular
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pbBadge.Width, pbBadge.Height);
            Region region = new Region(gp);
            pbBadge.Region = region;

            panel.Controls.Add(pbBadge);

            // Badge Name Label
            Label lblName = new Label();
            lblName.Text = badge.NameBadge;
            lblName.ForeColor = Color.White;
            lblName.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            lblName.Location = new Point(5, 150);
            lblName.Size = new Size(150, 40);
            panel.Controls.Add(lblName);

            // Click event untuk select badge
            panel.Click += (s, e) => SelectBadge(panel, badge);
            pbBadge.Click += (s, e) => SelectBadge(panel, badge);
            lblName.Click += (s, e) => SelectBadge(panel, badge);

            return panel;
        }

        private void SelectBadge(Panel panel, Badge badge)
        {
            // Reset all panels
            FlowLayoutPanel flowPanel = this.Controls.Find("flowPanelBadges", true)[0] as FlowLayoutPanel;
            foreach (Control ctrl in flowPanel.Controls)
            {
                if (ctrl is Panel p)
                {
                    p.BackColor = Color.FromArgb(40, 100, 100);
                    p.BorderStyle = BorderStyle.None;
                }
            }

            // Highlight selected panel
            panel.BackColor = Color.FromArgb(80, 180, 180);
            panel.BorderStyle = BorderStyle.FixedSingle;

            _selectedBadge = badge;

            // Enable select button
            Button btnSelect = this.Controls.Find("btnSelect", true)[0] as Button;
            btnSelect.Enabled = true;
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (_selectedBadge != null)
            {
    
                try
                {
                    if (!string.IsNullOrEmpty(_selectedBadge.FilePath) &&
                        System.IO.File.Exists(_selectedBadge.FilePath))
                    {
            
                        if (_userBadgePictureBox.Image != null)
                        {
                            _userBadgePictureBox.Image.Dispose();
                        }

                        _userBadgePictureBox.Image = Image.FromFile(_selectedBadge.FilePath);
                        _userBadgePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

         
                        System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                        gp.AddEllipse(0, 0, _userBadgePictureBox.Width, _userBadgePictureBox.Height);
                        Region region = new Region(gp);
                        _userBadgePictureBox.Region = region;

                        MessageBox.Show("Badge berhasil dipilih!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("File badge tidak ditemukan!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat badge: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            TextBox txtSearch = sender as TextBox;
            LoadBadges(txtSearch.Text);
        }
    }
}