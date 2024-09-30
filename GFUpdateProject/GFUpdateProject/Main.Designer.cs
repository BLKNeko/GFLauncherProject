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
            SuspendLayout();
            // 
            // updateBT
            // 
            updateBT.Location = new Point(347, 181);
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
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(FilePB);
            Controls.Add(FullPB);
            Controls.Add(updateBT);
            Name = "Main";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button updateBT;
        private ProgressBar FullPB;
        private ProgressBar FilePB;
    }
}
