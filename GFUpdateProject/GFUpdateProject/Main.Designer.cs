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
            SuspendLayout();
            // 
            // updateBT
            // 
            updateBT.Location = new Point(540, 309);
            updateBT.Name = "updateBT";
            updateBT.Size = new Size(135, 62);
            updateBT.TabIndex = 0;
            updateBT.Text = "Update";
            updateBT.UseVisualStyleBackColor = true;
            updateBT.Click += this.updateBT_ClickAsync;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(updateBT);
            Name = "Main";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button updateBT;
    }
}
