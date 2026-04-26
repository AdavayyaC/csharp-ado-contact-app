using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Data.Sqlite;

namespace csharp_ado_contact
{
    /// <summary>
    /// FormBase — Core form with UI, validation, search, favorites, groups, activity logging
    /// </summary>
    public partial class FormBase : Form
    {
        public List<Contact> contactList = null;
        public static string connectionString = string.Empty;

        public string STATUS
        {
            set { toolStripStatusLabelStatus.Text = value; }
        }

        protected FormBase()
        {
            InitializeComponent();
            contactList = new List<Contact>();
            connectionString = ConfigurationManager.AppSettings["connectionString"];
        }

        private void FormBase_Load(object sender, EventArgs e)
        {
            EnsureDatabaseExists();
            LoadData();
            STATUS = "✅ Contact list loaded successfully.";
            ShowWelcomeMessage();
        }

        private void ShowWelcomeMessage()
        {
            int count = contactList.Count;
            int favCount = contactList.Count(c => c.IsFavorite);
            MessageBox.Show(
                $"Welcome to Contact Manager!\n\n" +
                $"📋 {count} contact(s) in database  |  ⭐ {favCount} favorite(s)\n\n" +
                $"Features:\n" +
                $"  • Add / Update / Delete contacts\n" +
                $"  • Organize with Groups (Friend, Family, Work...)\n" +
                $"  • Mark favorites with ⭐ (shown at top)\n" +
                $"  • Search by name, phone, or email\n" +
                $"  • Full Activity Log of all changes\n" +
                $"  • Phone validation (10 digits only)\n\n" +
                $"Happy managing! 🎉",
                "📖 Contact Manager",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ============================================================
        // DATABASE SETUP
        // ============================================================

        private void EnsureDatabaseExists()
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();

                    // Create Contacts table
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Contacts (
                            ContactId    INTEGER PRIMARY KEY AUTOINCREMENT,
                            FirstName    TEXT NOT NULL,
                            LastName     TEXT NOT NULL DEFAULT '',
                            Email        TEXT NOT NULL DEFAULT '',
                            PhoneNumber  TEXT NOT NULL,
                            Address      TEXT NOT NULL DEFAULT '',
                            WebAddress   TEXT NOT NULL DEFAULT '',
                            Notes        TEXT NOT NULL DEFAULT '',
                            Category     TEXT NOT NULL DEFAULT 'General',
                            IsFavorite   INTEGER NOT NULL DEFAULT 0
                        )";
                    cmd.ExecuteNonQuery();

                    // Create ActivityLog table
                    var logCmd = conn.CreateCommand();
                    logCmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS ActivityLog (
                            LogId       INTEGER PRIMARY KEY AUTOINCREMENT,
                            Action      TEXT NOT NULL,
                            ContactName TEXT NOT NULL,
                            Details     TEXT NOT NULL DEFAULT '',
                            Timestamp   TEXT NOT NULL
                        )";
                    logCmd.ExecuteNonQuery();

                    // Insert sample data if empty
                    var countCmd = conn.CreateCommand();
                    countCmd.CommandText = "SELECT COUNT(*) FROM Contacts";
                    long count = (long)countCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        var insertCmd = conn.CreateCommand();
                        insertCmd.CommandText = @"
                            INSERT INTO Contacts (FirstName, LastName, Email, PhoneNumber, Address, WebAddress, Notes, Category, IsFavorite)
                            VALUES 
                                ('Rahul', 'Kumar', 'rahul@example.com', '9876543210', 'Bangalore, Karnataka', 'www.rahul.dev', 'Classmate', 'College', 1),
                                ('Priya', 'Sharma', 'priya@example.com', '8765432109', 'Mumbai, Maharashtra', 'www.priya.in', 'Project Partner', 'College', 0),
                                ('Amit', 'Patil', 'amit@example.com', '7654321098', 'Pune, Maharashtra', 'www.amit.tech', 'Senior Developer', 'Work', 1),
                                ('Sneha', 'Reddy', 'sneha@example.com', '6543210987', 'Hyderabad, Telangana', '', 'Best friend', 'Friend', 0),
                                ('Vikram', 'Singh', 'vikram@example.com', '5432109876', 'Delhi', '', 'Uncle', 'Family', 0)";
                        insertCmd.ExecuteNonQuery();

                        LogActivity("SYSTEM", "App Initialized", "Sample contacts loaded");
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create database: " + ex.Message,
                    "Database Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // ACTIVITY LOG
        // ============================================================

        /// <summary>
        /// Logs an activity to the ActivityLog table with a timestamp.
        /// Called on every Add, Update, Delete, and Favorite toggle.
        /// </summary>
        protected void LogActivity(string action, string contactName, string details = "")
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO ActivityLog (Action, ContactName, Details, Timestamp) VALUES (@Action, @ContactName, @Details, @Timestamp)";
                    cmd.Parameters.AddWithValue("@Action", action);
                    cmd.Parameters.AddWithValue("@ContactName", contactName);
                    cmd.Parameters.AddWithValue("@Details", details);
                    cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch { /* Don't crash the app if logging fails */ }
        }

        /// <summary>
        /// Opens the Activity Log viewer form
        /// </summary>
        private void buttonViewLogs_Click(object sender, EventArgs e)
        {
            var logForm = new FormActivityLog();
            logForm.ShowDialog();
        }

        // ============================================================
        // VIRTUAL METHODS (overridden by FormConnected / FormDisconnected)
        // ============================================================

        protected virtual void LoadData()
        {
            // Sort: favorites first, then alphabetically
            contactList.Sort((a, b) =>
            {
                if (a.IsFavorite != b.IsFavorite)
                    return b.IsFavorite.CompareTo(a.IsFavorite); // favorites first
                return a.FullName.CompareTo(b.FullName);
            });
            FillContactList();
            UpdateContactCount();
        }

        protected virtual Contact GetContactById(int contactId) { return null; }

        protected virtual void AddContact(Contact newContact)
        {
            contactList.Clear();
            LoadData();
        }

        protected virtual void UpdateContact(Contact updatedContact)
        {
            contactList.Clear();
            LoadData();
        }

        protected virtual void DeleteContactById(int contactId)
        {
            contactList.Clear();
            LoadData();
        }

        protected virtual void ToggleFavorite(int contactId, bool isFavorite)
        {
            contactList.Clear();
            LoadData();
        }

        protected virtual bool CheckDuplicate(string firstName, string phoneNumber, int excludeId = -1)
        {
            return false;
        }

        protected void FillContactList()
        {
            listBoxContactList.DataSource = null;
            listBoxContactList.DataSource = contactList;
            listBoxContactList.DisplayMember = "FullName";
            listBoxContactList.ValueMember = "contactId";
            UpdateContactCount();
        }

        private void UpdateContactCount()
        {
            int favs = contactList.Count(c => c.IsFavorite);
            labelContactCount.Text = $"{contactList.Count} contact(s) | ⭐ {favs}";
        }

        // ============================================================
        // VALIDATION
        // ============================================================

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
            {
                MessageBox.Show("⚠️ First Name is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxPhoneNumber.Text))
            {
                MessageBox.Show("⚠️ Phone Number is required!\n\nPlease enter a 10-digit phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPhoneNumber.Focus();
                return false;
            }

            string phone = textBoxPhoneNumber.Text.Trim();
            if (phone.Length != 10 || !phone.All(char.IsDigit))
            {
                MessageBox.Show($"⚠️ Phone Number must be exactly 10 digits!\n\nYou entered: \"{phone}\" ({phone.Length} chars)\nExample: 9876543210", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPhoneNumber.Focus();
                textBoxPhoneNumber.SelectAll();
                return false;
            }

            return true;
        }

        private void textBoxPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                STATUS = "⚠️ Phone number accepts only digits (0-9)";
            }
        }

        // ============================================================
        // BUTTON HANDLERS
        // ============================================================

        private void buttonNew_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string firstName = textBoxFirstName.Text.Trim();
            string phone = textBoxPhoneNumber.Text.Trim();

            // DUPLICATE CHECK
            if (CheckDuplicate(firstName, phone))
            {
                DialogResult dupResult = MessageBox.Show(
                    $"⚠️ A contact with the same name \"{firstName}\" or phone \"{phone}\" already exists!\n\nDo you still want to add this contact?",
                    "Duplicate Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dupResult == DialogResult.No) return;
            }

            var newContact = new Contact
            {
                FirstName = firstName,
                LastName = textBoxLastName.Text.Trim(),
                Email = textBoxEmail.Text.Trim(),
                PhoneNumber = phone,
                Address = textBoxAddress.Text.Trim(),
                WebAddress = textBoxWebAddress.Text.Trim(),
                Notes = textBoxNotes.Text.Trim(),
                Category = comboBoxCategory.SelectedItem?.ToString() ?? "General",
                IsFavorite = false
            };
            AddContact(newContact);
            LogActivity("ADD", newContact.FirstName + " " + newContact.LastName,
                $"Phone: {newContact.PhoneNumber}, Group: {newContact.Category}");
            ResetInput();
            STATUS = $"✅ Contact '{newContact.FirstName} {newContact.LastName}' added!";
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (listBoxContactList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a contact to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateInput()) return;

            var selected = listBoxContactList.SelectedItem as Contact;
            if (selected == null) return;

            var updatedContact = new Contact
            {
                contactId = selected.contactId,
                FirstName = textBoxFirstName.Text.Trim(),
                LastName = textBoxLastName.Text.Trim(),
                Email = textBoxEmail.Text.Trim(),
                PhoneNumber = textBoxPhoneNumber.Text.Trim(),
                Address = textBoxAddress.Text.Trim(),
                WebAddress = textBoxWebAddress.Text.Trim(),
                Notes = textBoxNotes.Text.Trim(),
                Category = comboBoxCategory.SelectedItem?.ToString() ?? "General",
                IsFavorite = selected.IsFavorite
            };
            UpdateContact(updatedContact);
            LogActivity("UPDATE", updatedContact.FirstName + " " + updatedContact.LastName,
                $"Phone: {updatedContact.PhoneNumber}, Group: {updatedContact.Category}");
            STATUS = $"✅ Contact '{updatedContact.FirstName} {updatedContact.LastName}' updated!";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxContactList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a contact to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selected = listBoxContactList.SelectedItem as Contact;
            if (selected == null) return;

            string contactName = selected.FirstName + " " + selected.LastName;

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete this contact?\n\n" +
                $"  👤 Name: {contactName}\n" +
                $"  📱 Phone: {selected.PhoneNumber}\n" +
                $"  📂 Group: {selected.Category}\n\n" +
                $"This action cannot be undone.",
                "🗑 Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                DeleteContactById(selected.contactId);
                LogActivity("DELETE", contactName, $"Phone: {selected.PhoneNumber}, Group: {selected.Category}");
                ResetInput();
                STATUS = $"🗑 Contact '{contactName}' deleted.";
            }
        }

        /// <summary>
        /// Toggle favorite status for the selected contact
        /// </summary>
        private void buttonFavorite_Click(object sender, EventArgs e)
        {
            if (listBoxContactList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a contact to favorite.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selected = listBoxContactList.SelectedItem as Contact;
            if (selected == null) return;

            bool newFavStatus = !selected.IsFavorite;
            ToggleFavorite(selected.contactId, newFavStatus);

            string name = selected.FirstName + " " + selected.LastName;
            string action = newFavStatus ? "FAVORITE" : "UNFAVORITE";
            LogActivity(action, name, newFavStatus ? "Marked as favorite" : "Removed from favorites");
            STATUS = newFavStatus ? $"⭐ '{name}' added to favorites!" : $"'{name}' removed from favorites.";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ResetInput();
            listBoxContactList.ClearSelected();
            STATUS = "Form cleared.";
        }

        private void ResetInput()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxEmail.Clear();
            textBoxPhoneNumber.Clear();
            textBoxWebAddress.Clear();
            textBoxAddress.Clear();
            textBoxNotes.Clear();
            comboBoxCategory.SelectedIndex = 0;
        }

        // ============================================================
        // SEARCH
        // ============================================================

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string query = textBoxSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(query))
            {
                listBoxContactList.DataSource = null;
                listBoxContactList.DataSource = contactList;
            }
            else
            {
                var filtered = contactList
                    .Where(c => c.FullName.ToLower().Contains(query) ||
                                c.PhoneNumber.Contains(query) ||
                                c.Email.ToLower().Contains(query) ||
                                c.Category.ToLower().Contains(query))
                    .ToList();
                listBoxContactList.DataSource = null;
                listBoxContactList.DataSource = filtered;
            }
            listBoxContactList.DisplayMember = "FullName";
            listBoxContactList.ValueMember = "contactId";
            UpdateContactCount();
        }

        private void listBoxContactList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxContactList.SelectedItems.Count == 0) return;
            var selectedContact = listBoxContactList.SelectedItem as Contact;
            if (selectedContact == null) return;

            Contact fullContact = GetContactById(selectedContact.contactId);
            if (fullContact == null) return;

            textBoxFirstName.Text = fullContact.FirstName;
            textBoxLastName.Text = fullContact.LastName;
            textBoxEmail.Text = fullContact.Email;
            textBoxPhoneNumber.Text = fullContact.PhoneNumber;
            textBoxWebAddress.Text = fullContact.WebAddress;
            textBoxAddress.Text = fullContact.Address;
            textBoxNotes.Text = fullContact.Notes;

            // Set category dropdown
            int catIndex = comboBoxCategory.Items.IndexOf(fullContact.Category);
            comboBoxCategory.SelectedIndex = catIndex >= 0 ? catIndex : 0;

            string favText = fullContact.IsFavorite ? "⭐" : "☆";
            STATUS = $"📋 Viewing: {fullContact.FirstName} {fullContact.LastName} {favText} [{fullContact.Category}]";
        }
    }
}
