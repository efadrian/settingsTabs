using System.Text.Json;
using WinSettingsTabs.Model;
using System.IO;
using System.ComponentModel;
using System.Net.Mail;

namespace WinSettingsTabs
{
    public partial class uc_Settings_EmailData : UserControl
    {
        private const string SampleJsonFile = "sample.json";

        private CheckBox? readOnlyCk;
        private BindingList<EmailData>? emailDataList;
        
        public uc_Settings_EmailData()
        {
            InitializeComponent();
            LoadEmailData();
            ConfigureDataGridView();
        }

        private void LoadEmailData()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SampleJsonFile);
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Sample JSON file not found.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string jsonData = File.ReadAllText(filePath);
                var emailList = JsonSerializer.Deserialize<List<EmailData>>(jsonData);

                if (emailList == null || emailList.Count == 0)
                {
                    MessageBox.Show("No email data found in the file.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate each email
                foreach (var email in emailList)
                {
                    if (string.IsNullOrWhiteSpace(email.Email) || !IsValidEmail(email.Email))
                    {
                        MessageBox.Show($"Invalid email address: {email.Email}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                emailDataList = new BindingList<EmailData>(emailList);
                
                if (dataGridView != null)
                {
                    dataGridView.DataSource = emailDataList;
                }
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Error deserializing JSON: {ex.Message}", "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dataGridView != null)
            {
                dataGridView.Location = new Point(18, 0);
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void InitializeComponent()
        {
            dataGridView = new DataGridView();
            addRowBtn = new Button();
            readOnlyCk = new CheckBox();
            ((ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(3, 3);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(642, 333);
            dataGridView.TabIndex = 0;
            // 
            // addRowBtn
            // 
            addRowBtn.Enabled = false;
            addRowBtn.Location = new Point(551, 342);
            addRowBtn.Name = "addRowBtn";
            addRowBtn.Size = new Size(94, 23);
            addRowBtn.TabIndex = 1;
            addRowBtn.Text = "Add Row";
            addRowBtn.UseVisualStyleBackColor = true;
            addRowBtn.Click += addRowBtn_Click;
            // 
            // readOnlyCk
            // 
            readOnlyCk.AutoSize = true;
            readOnlyCk.Checked = true;
            readOnlyCk.CheckState = CheckState.Checked;
            readOnlyCk.Location = new Point(3, 346);
            readOnlyCk.Name = "readOnlyCk";
            readOnlyCk.Size = new Size(123, 19);
            readOnlyCk.TabIndex = 2;
            readOnlyCk.Text = "DataGrid readOnly";
            readOnlyCk.UseVisualStyleBackColor = true;
            readOnlyCk.CheckedChanged += this.readOnlyCk_CheckedChanged;
            // 
            // uc_Settings_EmailData
            // 
            Controls.Add(readOnlyCk);
            Controls.Add(addRowBtn);
            Controls.Add(dataGridView);
            Name = "uc_Settings_EmailData";
            Size = new Size(648, 378);
            ((ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private void readOnlyCk_CheckedChanged(object? sender, EventArgs e)
        {
            if (dataGridView != null && readOnlyCk != null && addRowBtn != null)
            {
                dataGridView.ReadOnly = readOnlyCk.Checked;
                addRowBtn.Enabled = !readOnlyCk.Checked;
            }
        }

        private DataGridView? dataGridView;
        private Button? addRowBtn;

        private void addRowBtn_Click(object? sender, EventArgs e)
        {
            try
            {
                emailDataList?.Add(new EmailData());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding new row: {ex.Message}", "Add Row Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}