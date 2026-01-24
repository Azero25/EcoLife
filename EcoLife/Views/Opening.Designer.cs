namespace EcoLife.Views
{
    partial class Opening
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Opening));
            this.iconOpening = new Guna.UI2.WinForms.Guna2PictureBox();
            this.ecoLife = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.loginBtn = new Guna.UI2.WinForms.Guna2Button();
            this.registerBtn = new Guna.UI2.WinForms.Guna2Button();
            this.carousel1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.carousel2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.carousel3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.iconOpening)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carousel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carousel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carousel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // iconOpening
            // 
            this.iconOpening.Image = ((System.Drawing.Image)(resources.GetObject("iconOpening.Image")));
            this.iconOpening.ImageRotate = 0F;
            this.iconOpening.Location = new System.Drawing.Point(50, 35);
            this.iconOpening.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.iconOpening.Name = "iconOpening";
            this.iconOpening.Size = new System.Drawing.Size(82, 85);
            this.iconOpening.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconOpening.TabIndex = 0;
            this.iconOpening.TabStop = false;
            // 
            // ecoLife
            // 
            this.ecoLife.BackColor = System.Drawing.Color.Transparent;
            this.ecoLife.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ecoLife.ForeColor = System.Drawing.Color.White;
            this.ecoLife.Location = new System.Drawing.Point(154, 60);
            this.ecoLife.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ecoLife.Name = "ecoLife";
            this.ecoLife.Size = new System.Drawing.Size(89, 42);
            this.ecoLife.TabIndex = 1;
            this.ecoLife.Text = "Eco Life";
            // 
            // loginBtn
            // 
            this.loginBtn.BorderColor = System.Drawing.Color.White;
            this.loginBtn.BorderRadius = 3;
            this.loginBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.loginBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.loginBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.loginBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.loginBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(75)))), ((int)(((byte)(60)))));
            this.loginBtn.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold);
            this.loginBtn.ForeColor = System.Drawing.Color.Aquamarine;
            this.loginBtn.Location = new System.Drawing.Point(848, 60);
            this.loginBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(150, 46);
            this.loginBtn.TabIndex = 2;
            this.loginBtn.Text = "Login";
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // registerBtn
            // 
            this.registerBtn.BorderColor = System.Drawing.Color.White;
            this.registerBtn.BorderRadius = 3;
            this.registerBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.registerBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.registerBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.registerBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.registerBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(90)))), ((int)(((byte)(60)))));
            this.registerBtn.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold);
            this.registerBtn.ForeColor = System.Drawing.Color.Aquamarine;
            this.registerBtn.Location = new System.Drawing.Point(1006, 60);
            this.registerBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(150, 46);
            this.registerBtn.TabIndex = 3;
            this.registerBtn.Text = "Register";
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // carousel1
            // 
            this.carousel1.BorderRadius = 10;
            this.carousel1.Image = ((System.Drawing.Image)(resources.GetObject("carousel1.Image")));
            this.carousel1.ImageRotate = 0F;
            this.carousel1.Location = new System.Drawing.Point(154, 155);
            this.carousel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.carousel1.Name = "carousel1";
            this.carousel1.Size = new System.Drawing.Size(900, 246);
            this.carousel1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.carousel1.TabIndex = 4;
            this.carousel1.TabStop = false;
            // 
            // carousel2
            // 
            this.carousel2.BorderRadius = 10;
            this.carousel2.Image = ((System.Drawing.Image)(resources.GetObject("carousel2.Image")));
            this.carousel2.ImageRotate = 0F;
            this.carousel2.Location = new System.Drawing.Point(154, 155);
            this.carousel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.carousel2.Name = "carousel2";
            this.carousel2.Size = new System.Drawing.Size(900, 246);
            this.carousel2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.carousel2.TabIndex = 5;
            this.carousel2.TabStop = false;
            // 
            // carousel3
            // 
            this.carousel3.BorderRadius = 10;
            this.carousel3.Image = ((System.Drawing.Image)(resources.GetObject("carousel3.Image")));
            this.carousel3.ImageRotate = 0F;
            this.carousel3.Location = new System.Drawing.Point(154, 155);
            this.carousel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.carousel3.Name = "carousel3";
            this.carousel3.Size = new System.Drawing.Size(900, 246);
            this.carousel3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.carousel3.TabIndex = 6;
            this.carousel3.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureLogo
            // 
            this.pictureLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureLogo.Image")));
            this.pictureLogo.ImageRotate = 0F;
            this.pictureLogo.Location = new System.Drawing.Point(196, 489);
            this.pictureLogo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(180, 185);
            this.pictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureLogo.TabIndex = 8;
            this.pictureLogo.TabStop = false;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(432, 505);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(619, 38);
            this.guna2HtmlLabel1.TabIndex = 9;
            this.guna2HtmlLabel1.Text = "Eco Life adalah sebuah aplikasi challenge harian untuk anda.";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(432, 538);
            this.guna2HtmlLabel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(606, 38);
            this.guna2HtmlLabel2.TabIndex = 10;
            this.guna2HtmlLabel2.Text = "Tujuan dari aplikasi kami adalah untuk menyehatkan setiap";
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(432, 571);
            this.guna2HtmlLabel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(598, 38);
            this.guna2HtmlLabel3.TabIndex = 11;
            this.guna2HtmlLabel3.Text = "orang dengan beraktivitas setiap hari. Setiap aktivitas akan";
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(432, 605);
            this.guna2HtmlLabel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(203, 38);
            this.guna2HtmlLabel4.TabIndex = 12;
            this.guna2HtmlLabel4.Text = "mendapatkan poin.";
            // 
            // Opening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(56)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(1200, 754);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Controls.Add(this.guna2HtmlLabel3);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.pictureLogo);
            this.Controls.Add(this.carousel3);
            this.Controls.Add(this.carousel2);
            this.Controls.Add(this.carousel1);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.ecoLife);
            this.Controls.Add(this.iconOpening);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Opening";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eco Life";
            ((System.ComponentModel.ISupportInitialize)(this.iconOpening)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carousel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carousel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carousel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox iconOpening;
        private Guna.UI2.WinForms.Guna2HtmlLabel ecoLife;
        private Guna.UI2.WinForms.Guna2Button loginBtn;
        private Guna.UI2.WinForms.Guna2Button registerBtn;
        private Guna.UI2.WinForms.Guna2PictureBox carousel1;
        private Guna.UI2.WinForms.Guna2PictureBox carousel2;
        private Guna.UI2.WinForms.Guna2PictureBox carousel3;
        private System.Windows.Forms.Timer timer1;
        private Guna.UI2.WinForms.Guna2PictureBox pictureLogo;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
    }
}