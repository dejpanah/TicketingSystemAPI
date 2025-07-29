using System.ComponentModel.DataAnnotations;
using TicketingSystemAPI.Models;

namespace TicketingSystemAPI.DTOs
{
    public class TicketCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    }
}