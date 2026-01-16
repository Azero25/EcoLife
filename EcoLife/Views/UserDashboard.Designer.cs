namespace EcoLife.Views
{
    partial class UserDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserDashboard));
            this.iconEco = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnAccountUser = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnLogoutUser = new Guna.UI2.WinForms.Guna2ImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.iconEco)).BeginInit();
            this.SuspendLayout();
            // 
            // iconEco
            // 
            this.iconEco.Image = ((System.Drawing.Image)(resources.GetObject("iconEco.Image")));
            this.iconEco.ImageRotate = 0F;
            this.iconEco.Location = new System.Drawing.Point(12, 12);
            this.iconEco.Name = "iconEco";
            this.iconEco.Size = new System.Drawing.Size(40, 40);
            this.iconEco.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconEco.TabIndex = 2;
            this.iconEco.TabStop = false;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(59, 22);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(52, 25);
            this.guna2HtmlLabel1.TabIndex = 3;
            this.guna2HtmlLabel1.Text = "Eco Life";
            // 
            // btnAccountUser
            // 
            this.btnAccountUser.CheckedState.ImageSize = new System.Drawing.Size(25, 25);
            this.btnAccountUser.HoverState.ImageSize = new System.Drawing.Size(25, 25);
            this.btnAccountUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAccountUser.Image")));
            this.btnAccountUser.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnAccountUser.ImageRotate = 0F;
            this.btnAccountUser.ImageSize = new System.Drawing.Size(25, 25);
            this.btnAccountUser.Location = new System.Drawing.Point(687, 22);
            this.btnAccountUser.Name = "btnAccountUser";
            this.btnAccountUser.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.btnAccountUser.Size = new System.Drawing.Size(25, 25);
            this.btnAccountUser.TabIndex = 4;
            this.btnAccountUser.Click += new System.EventHandler(this.btnAccountUser_Click);
            // 
            // btnLogoutUser
            // 
            this.btnLogoutUser.CheckedState.ImageSize = new System.Drawing.Size(25, 25);
            this.btnLogoutUser.HoverState.ImageSize = new System.Drawing.Size(25, 25);
            this.btnLogoutUser.Image = ((System.Drawing.Image)(resources.GetObject("btnLogoutUser.Image")));
            this.btnLogoutUser.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnLogoutUser.ImageRotate = 0F;
            this.btnLogoutUser.ImageSize = new System.Drawing.Size(25, 25);
            this.btnLogoutUser.Location = new System.Drawing.Point(728, 22);
            this.btnLogoutUser.Name = "btnLogoutUser";
            this.btnLogoutUser.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.btnLogoutUser.Size = new System.Drawing.Size(25, 25);
            this.btnLogoutUser.TabIndex = 5;
            this.btnLogoutUser.Click += new System.EventHandler(this.btnLogoutUser_Click);
            // 
            // UserDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(60)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogoutUser);
            this.Controls.Add(this.btnAccountUser);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.iconEco);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserDashboard";
            ((System.ComponentModel.ISupportInitialize)(this.iconEco)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox iconEco;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2ImageButton btnAccountUser;
        private Guna.UI2.WinForms.Guna2ImageButton btnLogoutUser;
    }
}