namespace WinSettingsTabs
{
    partial class Form1
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
            treeView = new TreeView();
            mainPanel = new Panel();
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.Location = new Point(12, 12);
            treeView.Name = "treeView";
            treeView.Size = new Size(160, 381);
            treeView.TabIndex = 0;
            // 
            // mainPanel
            // 
            mainPanel.Location = new Point(178, 12);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(720, 381);
            mainPanel.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(910, 405);
            Controls.Add(mainPanel);
            Controls.Add(treeView);
            Name = "Form1";
            Text = "settingsTabs  v.01";
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeView;
        private Panel mainPanel;
    }
}
