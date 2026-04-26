using System;
using Microsoft.Data.Sqlite;

namespace csharp_ado_contact
{
    /// <summary>
    /// FormConnected — CONNECTED ADO.NET Mode (SQLite)
    /// Now supports: Category, IsFavorite, Duplicate Check, and all CRUD
    /// </summary>
    class FormConnected : FormBase
    {
        protected override void LoadData()
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT ContactId, FirstName, LastName, Email, PhoneNumber, Address, WebAddress, Notes, Category, IsFavorite FROM Contacts";

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        contactList.Add(new Contact
                        {
                            contactId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Address = reader.GetString(5),
                            WebAddress = reader.GetString(6),
                            Notes = reader.GetString(7),
                            Category = reader.GetString(8),
                            IsFavorite = reader.GetInt32(9) == 1
                        });
                    }
                    conn.Close();
                }
                catch (Exception ex) { STATUS = ex.Message; }
            }
            base.LoadData();
        }

        protected override Contact GetContactById(int contactId)
        {
            Contact tmpContact = new Contact();
            using (var conn = new SqliteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT ContactId, FirstName, LastName, Email, PhoneNumber, Address, WebAddress, Notes, Category, IsFavorite FROM Contacts WHERE ContactId = @Id";
                    cmd.Parameters.AddWithValue("@Id", contactId);

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tmpContact.contactId = reader.GetInt32(0);
                        tmpContact.FirstName = reader.GetString(1);
                        tmpContact.LastName = reader.GetString(2);
                        tmpContact.Email = reader.GetString(3);
                        tmpContact.PhoneNumber = reader.GetString(4);
                        tmpContact.Address = reader.GetString(5);
                        tmpContact.WebAddress = reader.GetString(6);
                        tmpContact.Notes = reader.GetString(7);
                        tmpContact.Category = reader.GetString(8);
                        tmpContact.IsFavorite = reader.GetInt32(9) == 1;
                    }
                    conn.Close();
                }
                catch (Exception ex) { STATUS = ex.Message; }
            }
            return tmpContact;
        }

        protected override void AddContact(Contact c)
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"INSERT INTO Contacts(FirstName, LastName, Email, PhoneNumber, Address, WebAddress, Notes, Category, IsFavorite) 
                                        VALUES(@FirstName, @LastName, @Email, @Phone, @Address, @Web, @Notes, @Category, @IsFav)";
                    cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", c.LastName);
                    cmd.Parameters.AddWithValue("@Email", c.Email);
                    cmd.Parameters.AddWithValue("@Phone", c.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", c.Address);
                    cmd.Parameters.AddWithValue("@Web", c.WebAddress);
                    cmd.Parameters.AddWithValue("@Notes", c.Notes);
                    cmd.Parameters.AddWithValue("@Category", c.Category);
                    cmd.Parameters.AddWithValue("@IsFav", c.IsFavorite ? 1 : 0);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex) { STATUS = ex.Message; }
            }
            base.AddContact(c);
        }

        protected override void UpdateContact(Contact c)
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"UPDATE Contacts SET FirstName=@FirstName, LastName=@LastName, Email=@Email, 
                                        PhoneNumber=@Phone, Address=@Address, WebAddress=@Web, Notes=@Notes, 
                                        Category=@Category, IsFavorite=@IsFav WHERE ContactId=@Id";
                    cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", c.LastName);
                    cmd.Parameters.AddWithValue("@Email", c.Email);
                    cmd.Parameters.AddWithValue("@Phone", c.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", c.Address);
                    cmd.Parameters.AddWithValue("@Web", c.WebAddress);
                    cmd.Parameters.AddWithValue("@Notes", c.Notes);
                    cmd.Parameters.AddWithValue("@Category", c.Category);
                    cmd.Parameters.AddWithValue("@IsFav", c.IsFavorite ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Id", c.contactId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex) { STATUS = ex.Message; }
            }
            base.UpdateContact(c);
        }

        protected override void DeleteContactById(int contactId)
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM Contacts WHERE ContactId = @Id";
                    cmd.Parameters.AddWithValue("@Id", contactId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex) { STATUS = ex.Message; }
            }
            base.DeleteContactById(contactId);
        }

        /// <summary>
        /// Toggles the IsFavorite flag in the database
        /// </summary>
        protected override void ToggleFavorite(int contactId, bool isFavorite)
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "UPDATE Contacts SET IsFavorite = @IsFav WHERE ContactId = @Id";
                    cmd.Parameters.AddWithValue("@IsFav", isFavorite ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Id", contactId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex) { STATUS = ex.Message; }
            }
            base.ToggleFavorite(contactId, isFavorite);
        }

        /// <summary>
        /// Checks if a contact with the same first name or phone number already exists.
        /// Used for duplicate detection before adding.
        /// </summary>
        protected override bool CheckDuplicate(string firstName, string phoneNumber, int excludeId = -1)
        {
            bool found = false;
            using (var conn = new SqliteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"SELECT COUNT(*) FROM Contacts 
                                        WHERE (LOWER(FirstName) = LOWER(@Name) OR PhoneNumber = @Phone) 
                                        AND ContactId != @ExcludeId";
                    cmd.Parameters.AddWithValue("@Name", firstName);
                    cmd.Parameters.AddWithValue("@Phone", phoneNumber);
                    cmd.Parameters.AddWithValue("@ExcludeId", excludeId);
                    long count = (long)cmd.ExecuteScalar();
                    found = count > 0;
                    conn.Close();
                }
                catch { }
            }
            return found;
        }
    }
}
