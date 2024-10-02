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
            ((System.ComponentModel.ISupportInitialize)BGFrameDWPB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGFrameUPPB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGCenterPB).BeginInit();
            SuspendLayout();
            // 
            // updateBT
            // 
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
            GameFolderTB.Location = new Point(12, 12);
            GameFolderTB.Name = "GameFolderTB";
            GameFolderTB.ReadOnly = true;
            GameFolderTB.Size = new Size(687, 23);
            GameFolderTB.TabIndex = 3;
            // 
            // GameFolderBT
            // 
            GameFolderBT.Font = new Font("HanWangYenHeavy", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            GameFolderBT.Location = new Point(713, 9);
            GameFolderBT.Name = "GameFolderBT";
            GameFolderBT.Size = new Size(75, 30);
            GameFolderBT.TabIndex = 4;
            GameFolderBT.Text = "Folder";
            GameFolderBT.UseVisualStyleBackColor = true;
            GameFolderBT.Click += GameFolderBT_Click;
            // 
            // ServerAddressTB
            // 
            ServerAddressTB.Location = new Point(12, 41);
            ServerAddressTB.Name = "ServerAddressTB";
            ServerAddressTB.Size = new Size(198, 23);
            ServerAddressTB.TabIndex = 5;
            // 
            // ManifestVersionTB
            // 
            ManifestVersionTB.Location = new Point(624, 41);
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
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}
