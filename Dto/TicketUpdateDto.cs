using TicketingSystemAPI.Models;

namespace TicketingSystemAPI.DTOs
{
    public class TicketUpdateDto
    {
        public TicketStatus? Status { get; set; }
        public Guid? AssignedToUserId { get; set; }
    }
}