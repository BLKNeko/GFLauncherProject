namespace GFUpdateProject
{
    partial class Main
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
            updateBT = new Button();
            FullPB = new ProgressBar();
            FilePB = new ProgressBar();
            GameFolderTB = new TextBox();
            GameFolderBT = new Button();
            ServerAddressTB = new TextBox();
            ManifestVersionTB = new TextBox();
            BGFrameDWPB = new PictureBox();
            BGFrameUPPB = new PictureBox();
            BGCenterPB = new PictureBox();
            FullPBCust = new CustomProgressBar();
            FilePBCust = new CustomProgressBar();
            ManifestDownloadBT = new Button();
            IpLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)BGFrameDWPB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGFrameUPPB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGCenterPB).BeginInit();
            SuspendLayout();
            // 
            // updateBT
            // 
            updateBT.Font = new Font("HanWangYenHeavy", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            updateBT.Location = new Point(653, 292);
            updateBT.Name = "updateBT";
            updateBT.Size = new Size(135, 62);
            updateBT.TabIndex = 0;
            updateBT.Text = "Update";
            updateBT.UseVisualStyleBackColor = true;
            updateBT.Click += updateBT_ClickAsync;
            // 
            // FullPB
            // 
            FullPB.BackColor = Color.Turquoise;
            FullPB.Location = new Point(12, 418);
            FullPB.Name = "FullPB";
            FullPB.Size = new Size(776, 11);
            FullPB.Style = ProgressBarStyle.Continuous;
            FullPB.TabIndex = 1;
            // 
            // FilePB
            // 
            FilePB.BackColor = Color.LightCoral;
            FilePB.Location = new Point(12, 401);
            FilePB.Name = "FilePB";
            FilePB.Size = new Size(776, 11);
            FilePB.TabIndex = 2;
            // 
            // GameFolderTB
            // 
            GameFolderTB.Font = new Font("HanWangYenHeavy", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            GameFolderTB.Location = new Point(12, 12);
            GameFolderTB.Name = "GameFolderTB";
            GameFolderTB.ReadOnly = true;
            GameFolderTB.Size = new Size(687, 21);
            GameFolderTB.TabIndex = 3;
            // 
            // GameFolderBT
            // 
            GameFolderBT.Font = new Font("HanWangYenHeavy", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            GameFolderBT.Location = new Point(705, 9);
            GameFolderBT.Name = "GameFolderBT";
            GameFolderBT.Size = new Size(83, 39);
            GameFolderBT.TabIndex = 4;
            GameFolderBT.Text = "Game Folder";
            GameFolderBT.UseVisualStyleBackColor = true;
            GameFolderBT.Click += GameFolderBT_Click;
            // 
            // ServerAddressTB
            // 
            ServerAddressTB.Font = new Font("HanWangYenHeavy", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            ServerAddressTB.Location = new Point(12, 59);
            ServerAddressTB.Name = "ServerAddressTB";
            ServerAddressTB.Size = new Size(198, 26);
            ServerAddressTB.TabIndex = 5;
            ServerAddressTB.TextChanged += ServerAddressTB_TextChanged;
            // 
            // ManifestVersionTB
            // 
            ManifestVersionTB.Font = new Font("HanWangYenHeavy", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            ManifestVersionTB.Location = new Point(624, 59);
            ManifestVersionTB.Name = "ManifestVersionTB";
            ManifestVersionTB.ReadOnly = true;
            ManifestVersionTB.Size = new Size(75, 23);
            ManifestVersionTB.TabIndex = 6;
            // 
            // BGFrameDWPB
            // 
            BGFrameDWPB.Image = Properties.Resources.KaslowFrameRZ;
            BGFrameDWPB.Location = new Point(0, 360);
            BGFrameDWPB.Name = "BGFrameDWPB";
            BGFrameDWPB.Size = new Size(800, 100);
            BGFrameDWPB.SizeMode = PictureBoxSizeMode.StretchImage;
            BGFrameDWPB.TabIndex = 7;
            BGFrameDWPB.TabStop = false;
            // 
            // BGFrameUPPB
            // 
            BGFrameUPPB.Image = Properties.Resources.KaslowFrameRZUPDW;
            BGFrameUPPB.Location = new Point(0, 0);
            BGFrameUPPB.Name = "BGFrameUPPB";
            BGFrameUPPB.Size = new Size(800, 100);
            BGFrameUPPB.SizeMode = PictureBoxSizeMode.StretchImage;
            BGFrameUPPB.TabIndex = 8;
            BGFrameUPPB.TabStop = false;
            // 
            // BGCenterPB
            // 
            BGCenterPB.Image = Properties.Resources.KaslowRZ;
            BGCenterPB.Location = new Point(0, 97);
            BGCenterPB.Name = "BGCenterPB";
            BGCenterPB.Size = new Size(800, 265);
            BGCenterPB.SizeMode = PictureBoxSizeMode.StretchImage;
            BGCenterPB.TabIndex = 9;
            BGCenterPB.TabStop = false;
            // 
            // FullPBCust
            // 
            FullPBCust.EndColor = Color.FromArgb(200, 0, 249, 255);
            FullPBCust.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            FullPBCust.Location = new Point(12, 420);
            FullPBCust.Maximum = 10;
            FullPBCust.Name = "FullPBCust";
            FullPBCust.Size = new Size(776, 20);
            FullPBCust.StartColor = Color.FromArgb(250, 33, 97, 204);
            FullPBCust.TabIndex = 1;
            FullPBCust.Value = 10;
            // 
            // FilePBCust
            // 
            FilePBCust.EndColor = Color.FromArgb(250, 255, 82, 82);
            FilePBCust.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            FilePBCust.Location = new Point(12, 390);
            FilePBCust.Maximum = 10;
            FilePBCust.Name = "FilePBCust";
            FilePBCust.Size = new Size(776, 20);
            FilePBCust.StartColor = Color.FromArgb(250, 232, 90, 24);
            FilePBCust.TabIndex = 0;
            FilePBCust.Value = 10;
            // 
            // ManifestDownloadBT
            // 
            ManifestDownloadBT.Font = new Font("HanWangYenHeavy", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            ManifestDownloadBT.Location = new Point(705, 54);
            ManifestDownloadBT.Name = "ManifestDownloadBT";
            ManifestDownloadBT.Size = new Size(83, 37);
            ManifestDownloadBT.TabIndex = 10;
            ManifestDownloadBT.Text = "Manifest Download";
            ManifestDownloadBT.UseVisualStyleBackColor = true;
            ManifestDownloadBT.Click += ManifestDownloadBT_Click;
            // 
            // IpLabel
            // 
            IpLabel.AutoSize = true;
            IpLabel.BackColor = Color.Transparent;
            IpLabel.Font = new Font("HanWangYenHeavy", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            IpLabel.Location = new Point(216, 64);
            IpLabel.Name = "IpLabel";
            IpLabel.Size = new Size(134, 15);
            IpLabel.TabIndex = 11;
            IpLabel.Text = "Server Address";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(IpLabel);
            Controls.Add(ManifestDownloadBT);
            Controls.Add(FilePBCust);
            Controls.Add(FullPBCust);
            Controls.Add(ManifestVersionTB);
            Controls.Add(ServerAddressTB);
            Controls.Add(GameFolderBT);
            Controls.Add(GameFolderTB);
            Controls.Add(updateBT);
            Controls.Add(BGFrameUPPB);
            Controls.Add(BGCenterPB);
            Controls.Add(BGFrameDWPB);
            Name = "Main";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)BGFrameDWPB).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGFrameUPPB).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGCenterPB).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button updateBT;
        private ProgressBar FullPB;
        private ProgressBar FilePB;
        private CustomProgressBar FullPBCust;
        private CustomProgressBar FilePBCust;
        private TextBox GameFolderTB;
        private Button GameFolderBT;
        private TextBox ServerAddressTB;
        private TextBox ManifestVersionTB;
        private PictureBox BGFrameDWPB;
        private PictureBox BGFrameUPPB;
        private PictureBox BGCenterPB;
        private Button ManifestDownloadBT;
        private Label IpLabel;
    }
}
