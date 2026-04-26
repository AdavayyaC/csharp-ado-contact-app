using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Data.Sqlite;

namespace csharp_ado_contact
{
    /// <summary>
    /// Activity Log viewer — shows all logged actions in a DataGridView.
    /// Demonstrates: Reading data with DataTable, DataGridView binding, 
    /// aggregate queries (COUNT, GROUP BY), and a separate WinForm.
    /// </summary>
    public class FormActivityLog : Form
    {
        private DataGridView dataGridView;
        private Label labelTitle;
        private Label labelSummary;
        private Button buttonRefresh;
        private Button buttonClearLogs;
        private Panel panelTop;

        public FormActivityLog()
        {
            InitializeUI();
            LoadLogs();
        }

        private void InitializeUI()
        {
            this.Text = "📋 Activity Log";
            this.Size = new System.Drawing.Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            // Top panel
            panelTop = new Panel();
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 80;
            panelTop.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);

            labelTitle = new Label();
            labelTitle.Text = "📋 Activity Log";
            labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            labelTitle.ForeColor = System.Drawing.Color.White;
            labelTitle.Location = new System.Drawing.Point(15, 8);
            labelTitle.AutoSize = true;
            panelTop.Controls.Add(labelTitle);

            labelSummary = new Label();
            labelSummary.Text = "Loading...";
            labelSummary.Font = new System.Drawing.Font("Segoe UI", 9F);
            labelSummary.ForeColor = System.Drawing.Color.FromArgb(189, 195, 199);
            labelSummary.Location = new System.Drawing.Point(18, 42);
            labelSummary.AutoSize = true;
            panelTop.Controls.Add(labelSummary);

            // Buttons on top-right
            buttonRefresh = new Button();
            buttonRefresh.Text = "🔄 Refresh";
            buttonRefresh.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            buttonRefresh.ForeColor = System.Drawing.Color.White;
            buttonRefresh.FlatStyle = FlatStyle.Flat;
            buttonRefresh.FlatAppearance.BorderSize = 0;
            buttonRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonRefresh.Size = new System.Drawing.Size(90, 30);
            buttonRefresh.Location = new System.Drawing.Point(480, 10);
            buttonRefresh.Cursor = Cursors.Hand;
            buttonRefresh.Click += (s, e) => LoadLogs();
            panelTop.Controls.Add(buttonRefresh);

            buttonClearLogs = new Button();
            buttonClearLogs.Text = "🗑 Clear All";
            buttonClearLogs.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            buttonClearLogs.ForeColor = System.Drawing.Color.White;
            buttonClearLogs.FlatStyle = FlatStyle.Flat;
            buttonClearLogs.FlatAppearance.BorderSize = 0;
            buttonClearLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonClearLogs.Size = new System.Drawing.Size(90, 30);
            buttonClearLogs.Location = new System.Drawing.Point(580, 10);
            buttonClearLogs.Cursor = Cursors.Hand;
            buttonClearLogs.Click += ButtonClearLogs_Click;
            panelTop.Controls.Add(buttonClearLogs);

            this.Controls.Add(panelTop);

            // DataGridView
            dataGridView = new DataGridView();
            dataGridView.Location = new System.Drawing.Point(10, 90);
            dataGridView.Size = new System.Drawing.Size(665, 360);
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;
            dataGridView.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.Controls.Add(dataGridView);
        }

        /// <summary>
        /// Loads all activity logs from the database into the DataGridView
        /// </summary>
        private void LoadLogs()
        {
            try
            {
                string connStr = ConfigurationManager.AppSettings["connectionString"];
                using (var conn = new SqliteConnection(connStr))
                {
                    conn.Open();

                    // Load logs (newest first)
                    var dt = new DataTable();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT LogId AS '#', Action, ContactName AS 'Contact', Details, Timestamp AS 'Date/Time' FROM ActivityLog ORDER BY LogId DESC";
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    dataGridView.DataSource = dt;

                    // Set column widths
                    if (dataGridView.Columns.Count > 0)
                    {
                        dataGridView.Columns["#"].Width = 40;
                        dataGridView.Columns["Action"].Width = 90;
                        dataGridView.Columns["Contact"].Width = 130;
                        dataGridView.Columns["Details"].Width = 230;
                        dataGridView.Columns["Date/Time"].Width = 140;
                    }

                    // Color-code the action column
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        string action = row.Cells["Action"].Value?.ToString() ?? "";
                        if (action == "ADD")
                            row.Cells["Action"].Style.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
                        else if (action == "DELETE")
                            row.Cells["Action"].Style.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
                        else if (action == "UPDATE")
                            row.Cells["Action"].Style.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
                        else if (action == "FAVORITE")
                            row.Cells["Action"].Style.ForeColor = System.Drawing.Color.FromArgb(243, 156, 18);
                    }

                    // Summary stats
                    var statsCmd = conn.CreateCommand();
                    statsCmd.CommandText = @"SELECT 
                        COUNT(*) as Total,
                        SUM(CASE WHEN Action='ADD' THEN 1 ELSE 0 END) as Adds,
                        SUM(CASE WHEN Action='UPDATE' THEN 1 ELSE 0 END) as Updates,
                        SUM(CASE WHEN Action='DELETE' THEN 1 ELSE 0 END) as Deletes
                        FROM ActivityLog";
                    var statsReader = statsCmd.ExecuteReader();
                    if (statsReader.Read())
                    {
                        long total = statsReader.GetInt64(0);
                        long adds = statsReader.IsDBNull(1) ? 0 : statsReader.GetInt64(1);
                        long updates = statsReader.IsDBNull(2) ? 0 : statsReader.GetInt64(2);
                        long deletes = statsReader.IsDBNull(3) ? 0 : statsReader.GetInt64(3);
                        labelSummary.Text = $"Total: {total} actions  |  ➕ {adds} adds  |  ✏️ {updates} updates  |  🗑 {deletes} deletes";
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading logs: " + ex.Message);
            }
        }

        /// <summary>
        /// Clears all activity logs from the database
        /// </summary>
        private void ButtonClearLogs_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear ALL activity logs?\n\nThis cannot be undone.",
                "Clear Activity Log", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string connStr = ConfigurationManager.AppSettings["connectionString"];
                    using (var conn = new SqliteConnection(connStr))
                    {
                        conn.Open();
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = "DELETE FROM ActivityLog";
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    LoadLogs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error clearing logs: " + ex.Message);
                }
            }
        }
    }
}
