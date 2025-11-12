using System.Reflection;

namespace WinSettingsTabs
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Type>? settingsControls;
        public string dataPath = $"{AppDomain.CurrentDomain.BaseDirectory}/Data/";

        public Form1()
        {
            InitializeComponent();
            LoadSettingsControls();
            treeView.AfterSelect += TreeView_AfterSelect;
            LoadSettingsIntoTreeView();
        }

        private void LoadSettingsControls()
        {
            settingsControls = new Dictionary<string, Type>();
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(UserControl)) && t.Name.StartsWith("uc_Settings"))
                .ToList();
            foreach (var type in types)
            {
                // remove prefix
                string displayName = type.Name.Substring(3); 
                if (!settingsControls.ContainsKey(displayName))
                {
                    settingsControls[displayName] = type;
                }
            }
        }

        private void LoadSettingsIntoTreeView()
        {
            foreach (var key in settingsControls.Keys.OrderBy(k => k))
            {
                treeView.Nodes.Add(key);
            }
        }

        private void TreeView_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            mainPanel.Controls.Clear();
            if (settingsControls.TryGetValue(e.Node.Text, out var type))
            {
                try
                {
                    UserControl uc = (UserControl)Activator.CreateInstance(type);
                    uc.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(uc);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
