namespace GFUpdateProject
{
    partial class GFLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GFLogin));
            btnExecutar = new Button();
            txtLogin = new TextBox();
            txtSenha = new TextBox();
            pictureBox1 = new PictureBox();
            ManualLogin = new Button();
            SVPassCB = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnExecutar
            // 
            btnExecutar.BackColor = Color.DarkOrange;
            btnExecutar.FlatAppearance.BorderColor = Color.Chocolate;
            btnExecutar.FlatAppearance.BorderSize = 3;
            btnExecutar.FlatStyle = FlatStyle.Flat;
            btnExecutar.Font = new Font("HanWangYenHeavy", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 136);
            btnExecutar.ForeColor = SystemColors.ControlText;
            btnExecutar.Location = new Point(43, 342);
            btnExecutar.Name = "btnExecutar";
            btnExecutar.Size = new Size(285, 54);
            btnExecutar.TabIndex = 0;
            btnExecutar.Text = "Auto Login";
            btnExecutar.UseVisualStyleBackColor = false;
            btnExecutar.Click += btnExecutar_Click;
            // 
            // txtLogin
            // 
            txtLogin.BackColor = Color.Bisque;
            txtLogin.Font = new Font("HanWangYenHeavy", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            txtLogin.Location = new Point(43, 224);
            txtLogin.Name = "txtLogin";
            txtLogin.PlaceholderText = "Login";
            txtLogin.Size = new Size(285, 22);
            txtLogin.TabIndex = 1;
            txtLogin.TextAlign = HorizontalAlignment.Center;
            // 
            // txtSenha
            // 
            txtSenha.BackColor = Color.Bisque;
            txtSenha.Font = new Font("HanWangYenHeavy", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            txtSenha.Location = new Point(43, 269);
            txtSenha.Name = "txtSenha";
            txtSenha.PlaceholderText = "Password";
            txtSenha.Size = new Size(285, 22);
            txtSenha.TabIndex = 2;
            txtSenha.TextAlign = HorizontalAlignment.Center;
            txtSenha.UseSystemPasswordChar = true;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.GFLogoNBG;
            pictureBox1.Location = new Point(12, 1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(361, 206);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // ManualLogin
            // 
            ManualLogin.BackColor = Color.Bisque;
            ManualLogin.Font = new Font("HanWangYenHeavy", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            ManualLogin.Location = new Point(43, 402);
            ManualLogin.Name = "ManualLogin";
            ManualLogin.Size = new Size(285, 26);
            ManualLogin.TabIndex = 4;
            ManualLogin.Text = "In-Game Login";
            ManualLogin.UseVisualStyleBackColor = false;
            ManualLogin.Click += ManualLogin_Click;
            // 
            // SVPassCB
            // 
            SVPassCB.AutoSize = true;
            SVPassCB.BackColor = Color.Transparent;
            SVPassCB.Font = new Font("HanWangYenHeavy", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            SVPassCB.ForeColor = Color.White;
            SVPassCB.Location = new Point(43, 297);
            SVPassCB.Name = "SVPassCB";
            SVPassCB.Size = new Size(124, 16);
            SVPassCB.TabIndex = 5;
            SVPassCB.Text = "Save Password";
            SVPassCB.UseVisualStyleBackColor = false;
            // 
            // GFLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.GFWP;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(385, 450);
            Controls.Add(SVPassCB);
            Controls.Add(ManualLogin);
            Controls.Add(pictureBox1);
            Controls.Add(txtSenha);
            Controls.Add(txtLogin);
            Controls.Add(btnExecutar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "GFLogin";
            Text = "GFLogin";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnExecutar;
        private TextBox txtLogin;
        private TextBox txtSenha;
        private PictureBox pictureBox1;
        private Button ManualLogin;
        private CheckBox SVPassCB;
    }
}
