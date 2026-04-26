namespace csharp_ado_contact
{
    partial class FormBase
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // === Create all controls ===
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelAppTitle = new System.Windows.Forms.Label();
            this.labelAppSubtitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxContacts = new System.Windows.Forms.GroupBox();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.listBoxContactList = new System.Windows.Forms.ListBox();
            this.labelContactCount = new System.Windows.Forms.Label();
            this.groupBoxInformation = new System.Windows.Forms.GroupBox();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonFavorite = new System.Windows.Forms.Button();
            this.buttonViewLogs = new System.Windows.Forms.Button();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.labelAddress = new System.Windows.Forms.Label();
            this.textBoxWebAddress = new System.Windows.Forms.TextBox();
            this.labelWebAddress = new System.Windows.Forms.Label();
            this.textBoxPhoneNumber = new System.Windows.Forms.TextBox();
            this.labelPhoneNumber = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.labelLastName = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.labelRequiredHint = new System.Windows.Forms.Label();
            this.statusStripStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();

            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBoxContacts.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.groupBoxInformation.SuspendLayout();
            this.statusStripStatus.SuspendLayout();
            this.SuspendLayout();

            // ===========================
            // panelHeader (Top Banner)
            // ===========================
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.panelHeader.Controls.Add(this.labelAppTitle);
            this.panelHeader.Controls.Add(this.labelAppSubtitle);
            this.panelHeader.Controls.Add(this.buttonViewLogs);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(750, 60);

            // labelAppTitle
            this.labelAppTitle.AutoSize = true;
            this.labelAppTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelAppTitle.ForeColor = System.Drawing.Color.White;
            this.labelAppTitle.Location = new System.Drawing.Point(15, 6);
            this.labelAppTitle.Text = "\U0001F4D6 Contact Manager";

            // labelAppSubtitle
            this.labelAppSubtitle.AutoSize = true;
            this.labelAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelAppSubtitle.ForeColor = System.Drawing.Color.FromArgb(214, 234, 248);
            this.labelAppSubtitle.Location = new System.Drawing.Point(18, 38);
            this.labelAppSubtitle.Text = "ADO.NET + WinForms | Groups \u2022 Favorites \u2022 Activity Log";

            // buttonViewLogs (top-right)
            this.buttonViewLogs.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.buttonViewLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonViewLogs.FlatAppearance.BorderSize = 0;
            this.buttonViewLogs.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.buttonViewLogs.ForeColor = System.Drawing.Color.White;
            this.buttonViewLogs.Location = new System.Drawing.Point(620, 15);
            this.buttonViewLogs.Size = new System.Drawing.Size(115, 30);
            this.buttonViewLogs.Text = "\U0001F4CB Activity Log";
            this.buttonViewLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonViewLogs.Click += new System.EventHandler(this.buttonViewLogs_Click);

            // ===========================
            // panelMain (Body)
            // ===========================
            this.panelMain.Controls.Add(this.groupBoxContacts);
            this.panelMain.Controls.Add(this.groupBoxInformation);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Padding = new System.Windows.Forms.Padding(8);
            this.panelMain.Size = new System.Drawing.Size(750, 410);

            // ===========================
            // groupBoxContacts (Left Panel)
            // ===========================
            this.groupBoxContacts.Controls.Add(this.listBoxContactList);
            this.groupBoxContacts.Controls.Add(this.labelContactCount);
            this.groupBoxContacts.Controls.Add(this.panelSearch);
            this.groupBoxContacts.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxContacts.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.groupBoxContacts.Location = new System.Drawing.Point(10, 5);
            this.groupBoxContacts.Size = new System.Drawing.Size(250, 390);
            this.groupBoxContacts.TabStop = false;
            this.groupBoxContacts.Text = " Contacts ";

            // panelSearch
            this.panelSearch.Controls.Add(this.textBoxSearch);
            this.panelSearch.Controls.Add(this.labelSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(3, 19);
            this.panelSearch.Size = new System.Drawing.Size(244, 30);

            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.labelSearch.ForeColor = System.Drawing.Color.Gray;
            this.labelSearch.Location = new System.Drawing.Point(2, 7);
            this.labelSearch.Text = "\U0001F50D";

            this.textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.textBoxSearch.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.textBoxSearch.Location = new System.Drawing.Point(22, 4);
            this.textBoxSearch.Size = new System.Drawing.Size(218, 23);
            this.textBoxSearch.PlaceholderText = "Search contacts...";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);

            // listBoxContactList
            this.listBoxContactList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxContactList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.listBoxContactList.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.listBoxContactList.FormattingEnabled = true;
            this.listBoxContactList.Location = new System.Drawing.Point(3, 52);
            this.listBoxContactList.Size = new System.Drawing.Size(244, 315);
            this.listBoxContactList.IntegralHeight = false;
            this.listBoxContactList.HorizontalScrollbar = true;
            this.listBoxContactList.TabIndex = 0;
            this.listBoxContactList.SelectedIndexChanged += new System.EventHandler(this.listBoxContactList_SelectedIndexChanged);

            // labelContactCount
            this.labelContactCount.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.labelContactCount.ForeColor = System.Drawing.Color.Gray;
            this.labelContactCount.Location = new System.Drawing.Point(3, 370);
            this.labelContactCount.Size = new System.Drawing.Size(244, 15);
            this.labelContactCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelContactCount.Text = "0 contacts";

            // ===========================
            // groupBoxInformation (Right Panel)
            // ===========================
            this.groupBoxInformation.Controls.Add(this.labelRequiredHint);
            this.groupBoxInformation.Controls.Add(this.buttonNew);
            this.groupBoxInformation.Controls.Add(this.buttonUpdate);
            this.groupBoxInformation.Controls.Add(this.buttonDelete);
            this.groupBoxInformation.Controls.Add(this.buttonClear);
            this.groupBoxInformation.Controls.Add(this.buttonFavorite);
            this.groupBoxInformation.Controls.Add(this.textBoxNotes);
            this.groupBoxInformation.Controls.Add(this.labelNotes);
            this.groupBoxInformation.Controls.Add(this.textBoxAddress);
            this.groupBoxInformation.Controls.Add(this.labelAddress);
            this.groupBoxInformation.Controls.Add(this.textBoxWebAddress);
            this.groupBoxInformation.Controls.Add(this.labelWebAddress);
            this.groupBoxInformation.Controls.Add(this.textBoxPhoneNumber);
            this.groupBoxInformation.Controls.Add(this.labelPhoneNumber);
            this.groupBoxInformation.Controls.Add(this.textBoxEmail);
            this.groupBoxInformation.Controls.Add(this.labelEmail);
            this.groupBoxInformation.Controls.Add(this.textBoxLastName);
            this.groupBoxInformation.Controls.Add(this.labelLastName);
            this.groupBoxInformation.Controls.Add(this.textBoxFirstName);
            this.groupBoxInformation.Controls.Add(this.labelFirstName);
            this.groupBoxInformation.Controls.Add(this.comboBoxCategory);
            this.groupBoxInformation.Controls.Add(this.labelCategory);
            this.groupBoxInformation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxInformation.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.groupBoxInformation.Location = new System.Drawing.Point(270, 5);
            this.groupBoxInformation.Size = new System.Drawing.Size(465, 390);
            this.groupBoxInformation.TabStop = false;
            this.groupBoxInformation.Text = " Contact Information ";

            var labelFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            var labelColor = System.Drawing.Color.FromArgb(80, 80, 80);
            var inputFont = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);

            // === Required hint ===
            this.labelRequiredHint.AutoSize = true;
            this.labelRequiredHint.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);
            this.labelRequiredHint.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.labelRequiredHint.Location = new System.Drawing.Point(10, 20);
            this.labelRequiredHint.Text = "* Required fields";

            // Row 1: First Name
            this.labelFirstName.AutoSize = true; this.labelFirstName.Font = labelFont;
            this.labelFirstName.ForeColor = labelColor; this.labelFirstName.Location = new System.Drawing.Point(10, 45);
            this.labelFirstName.Text = "First Name *";
            this.textBoxFirstName.Font = inputFont; this.textBoxFirstName.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.textBoxFirstName.Location = new System.Drawing.Point(120, 42); this.textBoxFirstName.MaxLength = 40;
            this.textBoxFirstName.Size = new System.Drawing.Size(330, 25); this.textBoxFirstName.TabIndex = 0;
            this.textBoxFirstName.PlaceholderText = "Enter first name";

            // Row 2: Last Name
            this.labelLastName.AutoSize = true; this.labelLastName.Font = labelFont;
            this.labelLastName.ForeColor = labelColor; this.labelLastName.Location = new System.Drawing.Point(10, 75);
            this.labelLastName.Text = "Last Name";
            this.textBoxLastName.Font = inputFont; this.textBoxLastName.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.textBoxLastName.Location = new System.Drawing.Point(120, 72); this.textBoxLastName.MaxLength = 40;
            this.textBoxLastName.Size = new System.Drawing.Size(330, 25); this.textBoxLastName.TabIndex = 1;
            this.textBoxLastName.PlaceholderText = "Enter last name";

            // Row 3: Email
            this.labelEmail.AutoSize = true; this.labelEmail.Font = labelFont;
            this.labelEmail.ForeColor = labelColor; this.labelEmail.Location = new System.Drawing.Point(10, 105);
            this.labelEmail.Text = "E-mail";
            this.textBoxEmail.Font = inputFont; this.textBoxEmail.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.textBoxEmail.Location = new System.Drawing.Point(120, 102); this.textBoxEmail.MaxLength = 50;
            this.textBoxEmail.Size = new System.Drawing.Size(330, 25); this.textBoxEmail.TabIndex = 2;
            this.textBoxEmail.PlaceholderText = "example@email.com";

            // Row 4: Phone Number
            this.labelPhoneNumber.AutoSize = true; this.labelPhoneNumber.Font = labelFont;
            this.labelPhoneNumber.ForeColor = labelColor; this.labelPhoneNumber.Location = new System.Drawing.Point(10, 135);
            this.labelPhoneNumber.Text = "Phone No. *";
            this.textBoxPhoneNumber.Font = inputFont; this.textBoxPhoneNumber.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.textBoxPhoneNumber.Location = new System.Drawing.Point(120, 132); this.textBoxPhoneNumber.MaxLength = 10;
            this.textBoxPhoneNumber.Size = new System.Drawing.Size(330, 25); this.textBoxPhoneNumber.TabIndex = 3;
            this.textBoxPhoneNumber.PlaceholderText = "10-digit number";
            this.textBoxPhoneNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPhoneNumber_KeyPress);

            // Row 5: Category
            this.labelCategory.AutoSize = true; this.labelCategory.Font = labelFont;
            this.labelCategory.ForeColor = labelColor; this.labelCategory.Location = new System.Drawing.Point(10, 165);
            this.labelCategory.Text = "Group";
            this.comboBoxCategory.Font = inputFont; this.comboBoxCategory.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.Location = new System.Drawing.Point(120, 162);
            this.comboBoxCategory.Size = new System.Drawing.Size(330, 25); this.comboBoxCategory.TabIndex = 4;
            this.comboBoxCategory.Items.AddRange(new object[] { "General", "Friend", "Family", "Work", "College", "Other" });
            this.comboBoxCategory.SelectedIndex = 0;

            // Row 6: Web Address
            this.labelWebAddress.AutoSize = true; this.labelWebAddress.Font = labelFont;
            this.labelWebAddress.ForeColor = labelColor; this.labelWebAddress.Location = new System.Drawing.Point(10, 197);
            this.labelWebAddress.Text = "Web Address";
            this.textBoxWebAddress.Font = inputFont; this.textBoxWebAddress.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.textBoxWebAddress.Location = new System.Drawing.Point(120, 194); this.textBoxWebAddress.MaxLength = 50;
            this.textBoxWebAddress.Size = new System.Drawing.Size(330, 25); this.textBoxWebAddress.TabIndex = 5;
            this.textBoxWebAddress.PlaceholderText = "www.example.com";

            // Row 7: Address
            this.labelAddress.AutoSize = true; this.labelAddress.Font = labelFont;
            this.labelAddress.ForeColor = labelColor; this.labelAddress.Location = new System.Drawing.Point(10, 227);
            this.labelAddress.Text = "Address";
            this.textBoxAddress.Font = inputFont; this.textBoxAddress.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.textBoxAddress.Location = new System.Drawing.Point(120, 224); this.textBoxAddress.MaxLength = 80;
            this.textBoxAddress.Multiline = true; this.textBoxAddress.Size = new System.Drawing.Size(330, 35);
            this.textBoxAddress.TabIndex = 6; this.textBoxAddress.PlaceholderText = "City, State";

            // Row 8: Notes
            this.labelNotes.AutoSize = true; this.labelNotes.Font = labelFont;
            this.labelNotes.ForeColor = labelColor; this.labelNotes.Location = new System.Drawing.Point(10, 267);
            this.labelNotes.Text = "Notes";
            this.textBoxNotes.Font = inputFont; this.textBoxNotes.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.textBoxNotes.Location = new System.Drawing.Point(120, 264); this.textBoxNotes.MaxLength = 80;
            this.textBoxNotes.Multiline = true; this.textBoxNotes.Size = new System.Drawing.Size(330, 35);
            this.textBoxNotes.TabIndex = 7; this.textBoxNotes.PlaceholderText = "Additional notes";

            // === Buttons Row ===
            var btnFont = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            int btnY = 310;

            // buttonNew (Green)
            this.buttonNew.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.buttonNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNew.FlatAppearance.BorderSize = 0;
            this.buttonNew.Font = btnFont; this.buttonNew.ForeColor = System.Drawing.Color.White;
            this.buttonNew.Location = new System.Drawing.Point(10, btnY);
            this.buttonNew.Size = new System.Drawing.Size(72, 30); this.buttonNew.TabIndex = 8;
            this.buttonNew.Text = "\u2795 Add"; this.buttonNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);

            // buttonUpdate (Blue)
            this.buttonUpdate.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.buttonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpdate.FlatAppearance.BorderSize = 0;
            this.buttonUpdate.Font = btnFont; this.buttonUpdate.ForeColor = System.Drawing.Color.White;
            this.buttonUpdate.Location = new System.Drawing.Point(88, btnY);
            this.buttonUpdate.Size = new System.Drawing.Size(80, 30); this.buttonUpdate.TabIndex = 9;
            this.buttonUpdate.Text = "\u270F Update"; this.buttonUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);

            // buttonDelete (Red)
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.Font = btnFont; this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(174, btnY);
            this.buttonDelete.Size = new System.Drawing.Size(72, 30); this.buttonDelete.TabIndex = 10;
            this.buttonDelete.Text = "\U0001F5D1 Delete"; this.buttonDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);

            // buttonFavorite (Orange/Gold)
            this.buttonFavorite.BackColor = System.Drawing.Color.FromArgb(243, 156, 18);
            this.buttonFavorite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFavorite.FlatAppearance.BorderSize = 0;
            this.buttonFavorite.Font = btnFont; this.buttonFavorite.ForeColor = System.Drawing.Color.White;
            this.buttonFavorite.Location = new System.Drawing.Point(252, btnY);
            this.buttonFavorite.Size = new System.Drawing.Size(60, 30); this.buttonFavorite.TabIndex = 11;
            this.buttonFavorite.Text = "\u2B50 Fav"; this.buttonFavorite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFavorite.Click += new System.EventHandler(this.buttonFavorite_Click);

            // buttonClear (Gray)
            this.buttonClear.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClear.FlatAppearance.BorderSize = 0;
            this.buttonClear.Font = btnFont; this.buttonClear.ForeColor = System.Drawing.Color.White;
            this.buttonClear.Location = new System.Drawing.Point(318, btnY);
            this.buttonClear.Size = new System.Drawing.Size(60, 30); this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Clear"; this.buttonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);

            // === Tooltips for buttons ===
            var toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.buttonFavorite, "Toggle favorite for selected contact");
            toolTip.SetToolTip(this.buttonViewLogs, "View all activity history");

            // ===========================
            // statusStripStatus
            // ===========================
            this.statusStripStatus.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.statusStripStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolStripStatusLabelStatus });
            this.statusStripStatus.Location = new System.Drawing.Point(0, 470);
            this.statusStripStatus.Size = new System.Drawing.Size(750, 22);
            this.statusStripStatus.SizingGrip = false;

            this.toolStripStatusLabelStatus.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.toolStripStatusLabelStatus.Text = "Ready";

            // ===========================
            // FormBase
            // ===========================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(750, 492);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.statusStripStatus);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contact Manager - ADO.NET";
            this.Load += new System.EventHandler(this.FormBase_Load);

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.groupBoxContacts.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.groupBoxInformation.ResumeLayout(false);
            this.groupBoxInformation.PerformLayout();
            this.statusStripStatus.ResumeLayout(false);
            this.statusStripStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelAppTitle;
        private System.Windows.Forms.Label labelAppSubtitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBoxContacts;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.GroupBox groupBoxInformation;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelPhoneNumber;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Label labelWebAddress;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.Label labelRequiredHint;
        private System.Windows.Forms.Label labelContactCount;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonFavorite;
        private System.Windows.Forms.Button buttonViewLogs;
        private System.Windows.Forms.StatusStrip statusStripStatus;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.TextBox textBoxPhoneNumber;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.TextBox textBoxWebAddress;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.ListBox listBoxContactList;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
    }
}
