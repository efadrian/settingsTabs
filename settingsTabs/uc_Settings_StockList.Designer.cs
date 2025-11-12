namespace WinSettingsTabs
{
    partial class uc_Settings_StockList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gridStock = new DataGridView();
            stkName = new Label();
            stkDesc = new Label();
            ((System.ComponentModel.ISupportInitialize)gridStock).BeginInit();
            SuspendLayout();
            // 
            // gridStock
            // 
            gridStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridStock.Location = new Point(3, 3);
            gridStock.Name = "gridStock";
            gridStock.Size = new Size(694, 300);
            gridStock.TabIndex = 3;
            // 
            // stkName
            // 
            stkName.AutoSize = true;
            stkName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            stkName.Location = new Point(3, 316);
            stkName.Name = "stkName";
            stkName.Size = new Size(12, 15);
            stkName.TabIndex = 4;
            stkName.Text = "-";
            // 
            // stkDesc
            // 
            stkDesc.AutoSize = true;
            stkDesc.Location = new Point(3, 342);
            stkDesc.Name = "stkDesc";
            stkDesc.Size = new Size(12, 15);
            stkDesc.TabIndex = 5;
            stkDesc.Text = "-";
            // 
            // uc_Settings_StockList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(stkDesc);
            Controls.Add(stkName);
            Controls.Add(gridStock);
            Name = "uc_Settings_StockList";
            Size = new Size(700, 400);
            ((System.ComponentModel.ISupportInitialize)gridStock).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView gridStock;
        private Label stkName;
        private Label stkDesc;
    }
}
