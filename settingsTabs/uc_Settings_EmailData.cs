using System.Text.Json;
using WinSettingsTabs.Model;
using System.ComponentModel;
using System.Net.Mail;

namespace WinSettingsTabs
{
    public partial class uc_Settings_EmailData : UserControl
    {
        private const string SampleJsonFile = "email_list.json";

        private CheckBox? readOnlyCk;
        private TextBox? searchGrid;
        private Label? label1;
        private RadioButton? rbAll;
        private RadioButton? rbEmails;
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
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory+"/Data/", SampleJsonFile);
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

                foreach (var email in emailList)
                {
                    if (string.IsNullOrWhiteSpace(email.Email) || !IsValidEmail(email.Email))
                    {
                        MessageBox.Show($"Invalid email address: {email.Email}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                emailDataList = new BindingList<EmailData>(emailList);
                UpdateDataGridView(); 
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
            if (gridEmail != null)
            {
                gridEmail.Location = new Point(18, 0);
                gridEmail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            gridEmail = new DataGridView();
            addRowBtn = new Button();
            readOnlyCk = new CheckBox();
            searchGrid = new TextBox();
            label1 = new Label();
            rbAll = new RadioButton();
            rbEmails = new RadioButton();
            ((ISupportInitialize)gridEmail).BeginInit();
            SuspendLayout();
            // 
            // gridEmail
            // 
            gridEmail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridEmail.Location = new Point(3, 0);
            gridEmail.Name = "gridEmail";
            gridEmail.Size = new Size(694, 340);
            gridEmail.TabIndex = 0;
            // 
            // addRowBtn
            // 
            addRowBtn.Enabled = false;
            addRowBtn.Location = new Point(601, 352);
            addRowBtn.Name = "addRowBtn";
            addRowBtn.Size = new Size(96, 23);
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
            readOnlyCk.Location = new Point(469, 355);
            readOnlyCk.Name = "readOnlyCk";
            readOnlyCk.Size = new Size(126, 19);
            readOnlyCk.TabIndex = 2;
            readOnlyCk.Text = "DataGrid ReadOnly";
            readOnlyCk.UseVisualStyleBackColor = true;
            readOnlyCk.CheckedChanged += readOnlyCk_CheckedChanged;
            // 
            // searchGrid
            // 
            searchGrid.Location = new Point(61, 356);
            searchGrid.Name = "searchGrid";
            searchGrid.Size = new Size(162, 23);
            searchGrid.TabIndex = 3;
            searchGrid.TextChanged += searchGrid_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 360);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 4;
            label1.Text = "Search";
            // 
            // rbAll
            // 
            rbAll.AutoSize = true;
            rbAll.Checked = true;
            rbAll.Location = new Point(246, 356);
            rbAll.Name = "rbAll";
            rbAll.Size = new Size(71, 19);
            rbAll.TabIndex = 11;
            rbAll.TabStop = true;
            rbAll.Text = "Show All";
            rbAll.UseVisualStyleBackColor = true;
            rbAll.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // rbEmails
            // 
            rbEmails.AutoSize = true;
            rbEmails.Location = new Point(323, 356);
            rbEmails.Name = "rbEmails";
            rbEmails.Size = new Size(106, 19);
            rbEmails.TabIndex = 10;
            rbEmails.Text = "Group By Email";
            rbEmails.UseVisualStyleBackColor = true;
            rbEmails.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // uc_Settings_EmailData
            // 
            Controls.Add(rbAll);
            Controls.Add(rbEmails);
            Controls.Add(label1);
            Controls.Add(searchGrid);
            Controls.Add(readOnlyCk);
            Controls.Add(addRowBtn);
            Controls.Add(gridEmail);
            Name = "uc_Settings_EmailData";
            Size = new Size(700, 400);
            ((ISupportInitialize)gridEmail).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private void readOnlyCk_CheckedChanged(object? sender, EventArgs e)
        {
            if (gridEmail != null && readOnlyCk != null && addRowBtn != null)
            {
                gridEmail.ReadOnly = readOnlyCk.Checked;
                addRowBtn.Enabled = !readOnlyCk.Checked;
            }
        }

        private DataGridView? gridEmail;
        private Button? addRowBtn;

        private void addRowBtn_Click(object? sender, EventArgs e)
        {
            try
            {
                emailDataList?.Add(new EmailData());
                UpdateDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding new row: {ex.Message}", "Add Row Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void searchGrid_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            if (gridEmail == null || emailDataList == null)
                return;

            try
            {
                var filteredData = GetFilteredData();

                if (rbEmails != null && rbEmails.Checked)
                {
                    var groupedData = GetGroupedData(filteredData);
                    gridEmail.DataSource = new BindingList<EmailData>(groupedData);
                }
                else
                {
                    gridEmail.DataSource = new BindingList<EmailData>(filteredData.ToList());
                }

                // Configure NoOfEmails column visibility and size
                ConfigureNoOfEmailsColumn();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating data view: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IEnumerable<EmailData> GetFilteredData()
        {
            IEnumerable<EmailData> sourceList = emailDataList;

            // Apply search filter if applicable
            string searchText = searchGrid?.Text?.Trim() ?? string.Empty;
            if (searchText.Length >= 2)
            {
                sourceList = sourceList.Where(ed =>
                    (ed.Email?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (ed.Title?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                );
            }

            return sourceList;
        }

        private List<EmailData> GetGroupedData(IEnumerable<EmailData> data)
        {
            return data
                .Where(ed => !string.IsNullOrEmpty(ed.Email))
                .GroupBy(ed => ed.Email)
                .Select(g => new EmailData
                {
                    Email = g.Key,
                    Title = g.FirstOrDefault()?.Title, // Use the first title as representative
                    Body = g.FirstOrDefault()?.Body,   // Use the first body as representative
                    NoOfEmails = g.Count()
                })
                .ToList();
        }

        private void ConfigureNoOfEmailsColumn()
        {
            if (gridEmail != null)
            {
                gridEmail.Columns["NoOfEmails"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                gridEmail.Columns["NoOfEmails"].Width = 75;
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }
    }
}