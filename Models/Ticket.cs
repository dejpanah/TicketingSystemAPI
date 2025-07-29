using System.ComponentModel.DataAnnotations;

namespace TicketingSystemAPI.Models
{
    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public TicketStatus Status { get; set; } = TicketStatus.Open;

        public TicketPriority Priority { get; set; } = TicketPriority.Medium;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }

        public Guid? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }
    }

    public enum TicketStatus
    {
        Open,
        InProgress,
        Closed
    }

    public enum TicketPriority
    {
        Low,
        Medium,
        High
    }
}
