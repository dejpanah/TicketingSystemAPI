namespace TicketingSystemAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Hashed password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// "Employee" or "Admin"
        /// </summary>
        public string Role { get; set; }
    }
}
