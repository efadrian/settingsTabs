
using System.Text.Json;
using WinSettingsTabs.Model;
using System.ComponentModel;

namespace WinSettingsTabs
{
    public partial class uc_Settings_StockList : UserControl
    {
        private const string SampleJsonFile = "stocklist.json";
        private BindingList<StockData>? stockDataList;

        public uc_Settings_StockList()
        {
            InitializeComponent();
            LoadStockData();
            ConfigureDataGridView();

            // wire selection changed so we can show description and formatted price
            if (gridStock != null)
            {
                gridStock.SelectionChanged += GridStock_SelectionChanged;
            }
        }

        private void LoadStockData()
        {
            try
            {
                // prefer the host form's dataPath if available (Form1 exposes public dataPath)
                string baseDataPath = (this.FindForm() as Form1)?.dataPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                string filePath = Path.Combine(baseDataPath, SampleJsonFile);
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Sample JSON file not found.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string jsonData = File.ReadAllText(filePath);
                var stockList = JsonSerializer.Deserialize<List<StockData>>(jsonData);

                if (stockList == null || stockList.Count == 0)
                {
                    MessageBox.Show("No stock data found in the file.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                stockDataList = new BindingList<StockData>(stockList);
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
            if (gridStock != null)
            {
                gridStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridStock.AllowUserToAddRows = false;
                gridStock.ReadOnly = true;
            }
        }

        private void UpdateDataGridView()
        {
            if (gridStock == null || stockDataList == null)
                return;

            try
            {
                gridStock.DataSource = stockDataList;

                // adjust column for price if present and format it
                var priceCol = gridStock.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => string.Equals(c.DataPropertyName, "StockPrice", StringComparison.OrdinalIgnoreCase) || string.Equals(c.Name, "StockPrice", StringComparison.OrdinalIgnoreCase));
                if (priceCol != null)
                {
                    priceCol.Width = 95;
                    priceCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // hide description from column list if you prefer and only show via label
                var descCol = gridStock.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => string.Equals(c.DataPropertyName, "StockDescription", StringComparison.OrdinalIgnoreCase));
                if (descCol != null)
                {
                    // keep it visible but narrow; alternatively set Visible = false;
                    descCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating data view: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridStock_SelectionChanged(object? sender, EventArgs e)
        {
            if (gridStock == null || stockDataList == null)
                return;

            try
            {
                if (gridStock.CurrentRow == null)
                {
                    stkName.Text = "-";
                    return;
                }

                var item = gridStock.CurrentRow.DataBoundItem as StockData;

                //
                stkName.Text = string.IsNullOrWhiteSpace(item?.StockName) ? "" : item.StockName;
                stkDesc.Text = string.IsNullOrWhiteSpace(item?.StockDescription) ? "" : item.StockDescription;

                if (!string.IsNullOrWhiteSpace(item?.StockPrice) && decimal.TryParse(item.StockPrice, out var priceVal))
                {
                    var priceText = priceVal.ToString("C2");
                    var priceCell = gridStock.CurrentRow.Cells.Cast<DataGridViewCell>().FirstOrDefault(c => string.Equals(c.OwningColumn.DataPropertyName, "StockPrice", StringComparison.OrdinalIgnoreCase));
                    if (priceCell != null)
                    {
                        priceCell.Value = priceText;
                    }
                }
                else
                {
                    var priceCell = gridStock.CurrentRow.Cells.Cast<DataGridViewCell>().FirstOrDefault(c => string.Equals(c.OwningColumn.DataPropertyName, "StockPrice", StringComparison.OrdinalIgnoreCase));
                    if (priceCell != null && (priceCell.Value == null || string.IsNullOrWhiteSpace(priceCell.Value.ToString())))
                        priceCell.Value = "N/A";
                }
            }
            catch
            {
            }
        }

        //  StockPrice adjust to 95px
        // use dataPath from settingsForm to load both json files
    }
}
