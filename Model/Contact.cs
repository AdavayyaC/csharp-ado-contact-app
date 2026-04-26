namespace csharp_ado_contact
{
    /// <summary>
    /// Represents a single contact entry in the database.
    /// This is a POCO (Plain Old CLR Object) — a simple data container.
    /// </summary>
    public class Contact
    {
        public int contactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// Read-only computed property that combines FirstName and LastName.
        /// Shows a star ⭐ prefix if the contact is a favorite.
        /// </summary>
        public string FullName
        {
            get
            {
                string star = IsFavorite ? "⭐ " : "";
                string cat = !string.IsNullOrEmpty(Category) ? $" [{Category}]" : "";
                return $"{star}{FirstName} {LastName}{cat}";
            }
        }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string WebAddress { get; set; }
        public string Notes { get; set; }

        /// <summary>
        /// Contact category/group: Friend, Family, Work, College, Other
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Whether this contact is marked as a favorite (shown at top of list with ⭐)
        /// </summary>
        public bool IsFavorite { get; set; }
    }
}
