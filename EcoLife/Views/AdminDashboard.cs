using EcoLife.Controller;
using EcoLife.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLife.Views
{
    public partial class AdminDashboard : Form
    {
        private User currentUser;

        private UserController userController;
        private ChallengeController challengeController;
        private BadgeController badgeController;

        private List<User> listOfUser = new List<User>();
        private List<Challenge> listOfChallenge = new List<Challenge>();
        private List<Badge> listOfBadge = new List<Badge>();

        public AdminDashboard(User user)
        {
            InitializeComponent();
            this.challengeController = new ChallengeController();
            this.badgeController = new BadgeController();
            this.userController = new UserController();
            this.currentUser = user;

            InitializeChallenge();
            InitializeBadge();
            InitializeUser();

            LoadDataChallenge();
            LoadDataBadge();
            LoadDataUser();
        }

        private void InitializeChallenge()
        {
            lvwChallenge.View = View.Details;
            lvwChallenge.FullRowSelect = true;
            lvwChallenge.GridLines = true;

            lvwChallenge.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwChallenge.Columns.Add("Nama Challenge", 150, HorizontalAlignment.Center);
            lvwChallenge.Columns.Add("Deskripsi", 230, HorizontalAlignment.Center);
            lvwChallenge.Columns.Add("Point", 50, HorizontalAlignment.Center);
            lvwChallenge.Columns.Add("Tanggal dibuat", 100, HorizontalAlignment.Center);
        }

        private void InitializeBadge()
        {
            lvwBadge.View = View.Details;
            lvwBadge.FullRowSelect = true;
            lvwBadge.GridLines = true;

            lvwBadge.Columns.Add("No.", 50, HorizontalAlignment.Center);
            lvwBadge.Columns.Add("Nama Badge", 150, HorizontalAlignment.Center);
            lvwBadge.Columns.Add("File Path", 220, HorizontalAlignment.Center);
            lvwBadge.Columns.Add("Tanggal dibuat", 150, HorizontalAlignment.Center);


        }

        private void InitializeUser()
        {
            lvwUser.View = View.Details;
            lvwUser.FullRowSelect = true;
            lvwUser.GridLines = true;
            
            lvwUser.Columns.Add("No.", 50, HorizontalAlignment.Center);
            lvwUser.Columns.Add("Nama User", 150, HorizontalAlignment.Center);
            lvwUser.Columns.Add("Email", 175, HorizontalAlignment.Center);
            lvwUser.Columns.Add("Role", 50, HorizontalAlignment.Center);
            lvwUser.Columns.Add("Tanggal Bergabung", 150, HorizontalAlignment.Center);
        }

        private void LoadDataChallenge()
        {
            lvwChallenge.Items.Clear();
            challengeController = new ChallengeController();
            listOfChallenge = challengeController.GetAllChallenge();

            foreach(var chl in listOfChallenge)
            {
                int noUrut = lvwChallenge.Items.Count + 1;
                ListViewItem item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(chl.NameChallenge);
                item.SubItems.Add(chl.DecsChallenge);
                item.SubItems.Add(chl.PointChallenge.ToString());
                item.SubItems.Add(chl.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                
                lvwChallenge.Items.Add(item);
            }
        }

        private void LoadDataBadge()
        {
            lvwBadge.Items.Clear();
            badgeController = new BadgeController();
            listOfBadge = badgeController.GetAllBadges();
            foreach(var bg in listOfBadge)
            {
                int noUrut = lvwBadge.Items.Count + 1;
                ListViewItem item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(bg.NameBadge);
                item.SubItems.Add(bg.FilePath);
                item.SubItems.Add(bg.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                
                lvwBadge.Items.Add(item);
            }
        }

        private void LoadDataUser()
        {
            lvwUser.Items.Clear();
            userController = new UserController();
            listOfUser = userController.ReadAllUser();

            // Read all data
            foreach(var usr in listOfUser)
            {
                int noUrut = lvwUser.Items.Count + 1;
                ListViewItem item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(usr.Name);
                item.SubItems.Add(usr.Email);
                item.SubItems.Add(usr.Role);
                item.SubItems.Add(usr.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                lvwUser.Items.Add(item);
            }
        }

        private void onCreateChallenge(Challenge chl)
        {
            listOfChallenge.Add(chl);
            int noUrut = lvwChallenge.Items.Count + 1;
            
            ListViewItem item = new ListViewItem(noUrut.ToString());
            item.SubItems.Add(chl.NameChallenge);
            item.SubItems.Add(chl.DecsChallenge);
            item.SubItems.Add(chl.PointChallenge.ToString());

            lvwChallenge.Items.Add(item);
        }

        private void onUpdateChallenge(Challenge chl)
        {
            int index = listOfChallenge.FindIndex(c => c.IdChallenge == chl.IdChallenge);
            if(index != -1)
            {
                listOfChallenge[index] = chl;
                ListViewItem item = lvwChallenge.Items[index];
                item.SubItems[1].Text = chl.NameChallenge;
                item.SubItems[2].Text = chl.DecsChallenge;
                item.SubItems[3].Text = chl.PointChallenge.ToString();
            }
        }

        private void onCreateBadge(Badge bg)
        {
            listOfBadge.Add(bg);
            int noUrut = lvwBadge.Items.Count + 1;

            ListViewItem item = new ListViewItem(noUrut.ToString());
            item.SubItems.Add(bg.NameBadge);
            item.SubItems.Add(bg.FilePath);
            
            lvwBadge.Items.Add(item);
        }

        private void onUpdateBadge(Badge bg)
        {
            int index = listOfBadge.FindIndex(b => b.IdBadge == bg.IdBadge);
            if(index != -1)
            {
                listOfBadge[index] = bg;
                ListViewItem item = lvwBadge.Items[index];
                item.SubItems[1].Text = bg.NameBadge;
                item.SubItems[2].Text = bg.FilePath;
            }
        }

        private void onCreateUser(User usr)
        {
            listOfUser.Add(usr);
            int noUrut = lvwUser.Items.Count + 1;

            ListViewItem item = new ListViewItem(noUrut.ToString());
            item.SubItems.Add(usr.Name);
            item.SubItems.Add(usr.Email);
            item.SubItems.Add(usr.Role);

            lvwUser.Items.Add(item);
        }

        private void onUpdateUser(User usr)
        {
            int index = listOfUser.FindIndex(u => u.IdUser == usr.IdUser);
            if(index != -1)
            {
                listOfUser[index] = usr;
                ListViewItem item = lvwUser.Items[index];
                item.SubItems[1].Text = usr.Name;
                item.SubItems[2].Text = usr.Email;
                item.SubItems[3].Text = usr.Role;
            }
        }

        private void btnTambahChallenge_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddUpdateChallenge form = new AddUpdateChallenge("Tambah Challenge", challengeController, currentUser);
            form.onCreateChallenge += onCreateChallenge;
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }

        private void btnUpdateChallenge_Click(object sender, EventArgs e)
        {
            if(lvwChallenge.SelectedItems.Count > 0)
            {
                this.Hide();
                Challenge challenge = listOfChallenge[lvwChallenge.SelectedIndices[0]];
                AddUpdateChallenge form = new AddUpdateChallenge("Update Challenge", this.challengeController, challenge, currentUser);
                form.onUpdateChallenge += onUpdateChallenge;
                form.FormClosed += (s, args) => this.Close();

                form.Show();
            } else
            {
                MessageBox.Show("Pilih challenge yang ingin diupdate!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDeleteChallenge_Click(object sender, EventArgs e)
        {
            if(lvwChallenge.SelectedItems.Count > 0)
            {
                var confirmResult = MessageBox.Show("Apakah anda yakin ingin menghapus challenge ini?", "Konfirmasi Hapus Challenge",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(confirmResult == DialogResult.Yes)
                {
                    Challenge challenge = listOfChallenge[lvwChallenge.SelectedIndices[0]];
                    try
                    {
                        challengeController.DeleteChallenge(challenge.IdChallenge, currentUser);

                        listOfChallenge.RemoveAt(lvwChallenge.SelectedIndices[0]);
                        lvwChallenge.Items.RemoveAt(lvwChallenge.SelectedIndices[0]);

                        MessageBox.Show("Challenge berhasil dihapus!", "Informasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadDataChallenge();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus challenge:\n" + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            } else
            {
                MessageBox.Show("Pilih challenge yang ingin dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCariChallenge_Click(object sender, EventArgs e)
        {
            lvwChallenge.Items.Clear();

            listOfChallenge = challengeController.SearchChallenge(txtCariChallenge.Text);

            foreach (var chl in listOfChallenge)
            {
                int noUrut = lvwChallenge.Items.Count + 1;
                ListViewItem item = new ListViewItem(noUrut.ToString());

                item.SubItems.Add(chl.NameChallenge);
                item.SubItems.Add(chl.DecsChallenge);
                item.SubItems.Add(chl.PointChallenge.ToString());
                item.SubItems.Add(chl.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));

                lvwChallenge.Items.Add(item);
            }
        }

        private void btnTambahBadge_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddUpdateBadge form = new AddUpdateBadge("Tambah Badge", badgeController, currentUser);
            form.onCreateBadge += onCreateBadge;
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }

        private void btnUpdateBadge_Click(object sender, EventArgs e)
        {
            if(lvwBadge.SelectedItems.Count > 0)
            {
                this.Hide();
                Badge badge = listOfBadge[lvwBadge.SelectedIndices[0]];
                AddUpdateBadge form = new AddUpdateBadge("Update Badge", this.badgeController, badge, currentUser);
                form.onUpdateBadge += onUpdateBadge;
                form.FormClosed += (s, args) => this.Close();
                form.Show();
            } else
            {
                MessageBox.Show("Pilih badge yang ingin diupdate!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDeleteBadge_Click(object sender, EventArgs e)
        {
            if(lvwBadge.SelectedItems.Count > 0)
            {
                var confirmResult = MessageBox.Show("Apakah anda yakin ingin menghapus badge ini?", "Konfirmasi Hapus Badge",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(confirmResult == DialogResult.Yes)
                {
                    Badge badge = listOfBadge[lvwBadge.SelectedIndices[0]];
                    try
                    {
                        badgeController.DeleteBadge(badge, currentUser);
                        listOfBadge.RemoveAt(lvwBadge.SelectedIndices[0]);
                        lvwBadge.Items.RemoveAt(lvwBadge.SelectedIndices[0]);
                        MessageBox.Show("Badge berhasil dihapus!", "Informasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataBadge();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus badge:\n" + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } else
            {
                MessageBox.Show("Pilih badge yang ingin dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCariBadge_Click(object sender, EventArgs e)
        {
            lvwBadge.Items.Clear();
            listOfBadge = badgeController.SearchBadges(txtCariBadge.Text);
            foreach (var bg in listOfBadge)
            {
                int noUrut = lvwBadge.Items.Count + 1;
                ListViewItem item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(bg.NameBadge);
                item.SubItems.Add(bg.FilePath);
                item.SubItems.Add(bg.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                lvwBadge.Items.Add(item);
            }
        }

        private void btnTambahUser_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddUpdateUser form = new AddUpdateUser("Tambah User", userController, currentUser);
            form.onCreateUser += onCreateUser;
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if(lvwUser.Items.Count > 0)
            {
                this.Hide();
                User user = listOfUser[lvwUser.SelectedIndices[0]];
                AddUpdateUser form = new AddUpdateUser("Update User", this.userController, user, currentUser);
                form.onUpdateUser += onUpdateUser;
                form.FormClosed += (s, args) => this.Close();
                form.Show();
            } else
            {
                MessageBox.Show("Pilih user yang ingin diupdate!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if(lvwUser.Items.Count > 0)
            {
                var confirmResult = MessageBox.Show("Apakah anda yakin ingin menghapus user ini?", "Konfirmasi Hapus User",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(confirmResult == DialogResult.Yes)
                {
                    User user = listOfUser[lvwUser.SelectedIndices[0]];
                    try
                    {
                        userController.DeleteUser(user);
                        
                        listOfUser.RemoveAt(lvwUser.SelectedIndices[0]);
                        lvwUser.Items.RemoveAt(lvwUser.SelectedIndices[0]);
                        MessageBox.Show("User berhasil dihapus!", "Informasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataUser();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus user:\n" + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } else
            {
                MessageBox.Show("Pilih user yang ingin dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCariUser_Click(object sender, EventArgs e)
        {
            lvwUser.Items.Clear();
            listOfUser = userController.GetUserByName(txtCariUser.Text);
            foreach (var usr in listOfUser)
            {
                int noUrut = lvwUser.Items.Count + 1;
                ListViewItem item = new ListViewItem(noUrut.ToString());
                
                item.SubItems.Add(usr.Name);
                item.SubItems.Add(usr.Email);
                item.SubItems.Add(usr.Role);
                item.SubItems.Add(usr.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                
                lvwUser.Items.Add(item);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            userController.Logout();
        }
    }
}
