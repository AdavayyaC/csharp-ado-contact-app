using System;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.Sqlite;

namespace csharp_ado_contact
{
    /// <summary>
    /// FormDisconnected — DISCONNECTED ADO.NET Mode (SQLite)
    /// Now supports: Category, IsFavorite, Duplicate Check
    /// </summary>
    class FormDisconnected : FormBase
    {
        DataTable dataTable = null;
        SqliteConnection persistentConnection = null;

        protected override void LoadData()
        {
            if (dataTable == null)
            {
                dataTable = new DataTable();
                try
                {
                    persistentConnection = new SqliteConnection(connectionString);
                    persistentConnection.Open();
                    var cmd = persistentConnection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Contacts";
                    using (var reader = cmd.ExecuteReader()) { dataTable.Load(reader); }
                }
                catch (Exception ex) { STATUS = ex.Message; }
            }

            foreach (DataRow row in dataTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    contactList.Add(new Contact
                    {
                        contactId = Convert.ToInt32(row["ContactId"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        Email = row["Email"].ToString(),
                        PhoneNumber = row["PhoneNumber"].ToString(),
                        Address = row["Address"].ToString(),
                        WebAddress = row["WebAddress"].ToString(),
                        Notes = row["Notes"].ToString(),
                        Category = row["Category"].ToString(),
                        IsFavorite = Convert.ToInt32(row["IsFavorite"]) == 1
                    });
                }
            }
            base.LoadData();
        }

        protected override Contact GetContactById(int contactId)
        {
            var rows = dataTable.Select("ContactId=" + contactId);
            if (rows.Length == 0) return new Contact();
            return new Contact
            {
                contactId = Convert.ToInt32(rows[0]["ContactId"]),
                FirstName = rows[0]["FirstName"].ToString(),
                LastName = rows[0]["LastName"].ToString(),
                Email = rows[0]["Email"].ToString(),
                PhoneNumber = rows[0]["PhoneNumber"].ToString(),
                Address = rows[0]["Address"].ToString(),
                WebAddress = rows[0]["WebAddress"].ToString(),
                Notes = rows[0]["Notes"].ToString(),
                Category = rows[0]["Category"].ToString(),
                IsFavorite = Convert.ToInt32(rows[0]["IsFavorite"]) == 1
            };
        }

        protected override void AddContact(Contact c)
        {
            var row = dataTable.NewRow();
            row["FirstName"] = c.FirstName; row["LastName"] = c.LastName;
            row["Email"] = c.Email; row["PhoneNumber"] = c.PhoneNumber;
            row["Address"] = c.Address; row["WebAddress"] = c.WebAddress;
            row["Notes"] = c.Notes; row["Category"] = c.Category;
            row["IsFavorite"] = c.IsFavorite ? 1 : 0;
            dataTable.Rows.Add(row);
            STATUS = "Contact added (saves on close).";
            base.AddContact(c);
        }

        protected override void UpdateContact(Contact c)
        {
            var rows = dataTable.Select("ContactId=" + c.contactId);
            if (rows.Length > 0)
            {
                rows[0]["FirstName"] = c.FirstName; rows[0]["LastName"] = c.LastName;
                rows[0]["Email"] = c.Email; rows[0]["PhoneNumber"] = c.PhoneNumber;
                rows[0]["Address"] = c.Address; rows[0]["WebAddress"] = c.WebAddress;
                rows[0]["Notes"] = c.Notes; rows[0]["Category"] = c.Category;
                rows[0]["IsFavorite"] = c.IsFavorite ? 1 : 0;
            }
            STATUS = "Contact updated (saves on close).";
            base.UpdateContact(c);
        }

        protected override void DeleteContactById(int contactId)
        {
            foreach (DataRow row in dataTable.Select("ContactId=" + contactId)) { row.Delete(); }
            STATUS = "Contact deleted (saves on close).";
            base.DeleteContactById(contactId);
        }

        protected override void ToggleFavorite(int contactId, bool isFavorite)
        {
            var rows = dataTable.Select("ContactId=" + contactId);
            if (rows.Length > 0) { rows[0]["IsFavorite"] = isFavorite ? 1 : 0; }
            base.ToggleFavorite(contactId, isFavorite);
        }

        protected override bool CheckDuplicate(string firstName, string phoneNumber, int excludeId = -1)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;
                int id = Convert.ToInt32(row["ContactId"]);
                if (id == excludeId) continue;
                if (row["FirstName"].ToString().Equals(firstName, StringComparison.OrdinalIgnoreCase) ||
                    row["PhoneNumber"].ToString() == phoneNumber)
                    return true;
            }
            return false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (persistentConnection != null && dataTable != null)
            {
                try
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            var cmd = persistentConnection.CreateCommand();
                            cmd.CommandText = @"INSERT INTO Contacts(FirstName,LastName,Email,PhoneNumber,Address,WebAddress,Notes,Category,IsFavorite) 
                                                VALUES(@a,@b,@c,@d,@e,@f,@g,@h,@i)";
                            cmd.Parameters.AddWithValue("@a", row["FirstName"]);
                            cmd.Parameters.AddWithValue("@b", row["LastName"]);
                            cmd.Parameters.AddWithValue("@c", row["Email"]);
                            cmd.Parameters.AddWithValue("@d", row["PhoneNumber"]);
                            cmd.Parameters.AddWithValue("@e", row["Address"]);
                            cmd.Parameters.AddWithValue("@f", row["WebAddress"]);
                            cmd.Parameters.AddWithValue("@g", row["Notes"]);
                            cmd.Parameters.AddWithValue("@h", row["Category"]);
                            cmd.Parameters.AddWithValue("@i", row["IsFavorite"]);
                            cmd.ExecuteNonQuery();
                        }
                        else if (row.RowState == DataRowState.Modified)
                        {
                            var cmd = persistentConnection.CreateCommand();
                            cmd.CommandText = @"UPDATE Contacts SET FirstName=@a,LastName=@b,Email=@c,PhoneNumber=@d,
                                                Address=@e,WebAddress=@f,Notes=@g,Category=@h,IsFavorite=@i WHERE ContactId=@id";
                            cmd.Parameters.AddWithValue("@a", row["FirstName"]);
                            cmd.Parameters.AddWithValue("@b", row["LastName"]);
                            cmd.Parameters.AddWithValue("@c", row["Email"]);
                            cmd.Parameters.AddWithValue("@d", row["PhoneNumber"]);
                            cmd.Parameters.AddWithValue("@e", row["Address"]);
                            cmd.Parameters.AddWithValue("@f", row["WebAddress"]);
                            cmd.Parameters.AddWithValue("@g", row["Notes"]);
                            cmd.Parameters.AddWithValue("@h", row["Category"]);
                            cmd.Parameters.AddWithValue("@i", row["IsFavorite"]);
                            cmd.Parameters.AddWithValue("@id", row["ContactId"]);
                            cmd.ExecuteNonQuery();
                        }
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            var cmd = persistentConnection.CreateCommand();
                            cmd.CommandText = "DELETE FROM Contacts WHERE ContactId=@id";
                            cmd.Parameters.AddWithValue("@id", row["ContactId", DataRowVersion.Original]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    persistentConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show("Error saving: " + ex.Message); }
            }
            base.OnClosing(e);
        }
    }
}
