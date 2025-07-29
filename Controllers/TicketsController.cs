using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TicketingSystemAPI.Data;
using TicketingSystemAPI.Models;
using System;
using System.Security.Claims;
using TicketingSystemAPI.DTOs;
using OfflineTicketingSystem.DTOs;

namespace TicketingSystemAPI.Controllers
{
    [Route("tickets")]
    [ApiController]
    [Authorize] 
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<Ticket>> CreateTicket([FromBody] TicketCreateDto ticketDto)
        {
            var userId= Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                Title = ticketDto.Title,
                Description = ticketDto.Description,
                Priority = ticketDto.Priority,
                CreatedByUserId = userId,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                Status = TicketStatus.Open
            }; 
            
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        [HttpGet("my")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetMyTickets()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var tickets = await _context.Tickets
                .Include(t => t.CreatedByUser)
                .Include(t => t.AssignedToUser)
                .Where(t => t.CreatedByUserId == Guid.Parse(userId))
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return Ok(tickets);
        }
         

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] TicketUpdateDto ticketDto)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            if (ticketDto.Status.HasValue)
                ticket.Status = ticketDto.Status.Value;

            if (ticketDto.AssignedToUserId.HasValue)
                ticket.AssignedToUserId = ticketDto.AssignedToUserId.Value;

            ticket.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("stats")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TicketStatsDto>> GetTicketStats()
        {
            var stats = new TicketStatsDto
            {
                Open = await _context.Tickets.CountAsync(t => t.Status == TicketStatus.Open),
                InProgress = await _context.Tickets.CountAsync(t => t.Status == TicketStatus.InProgress),
                Closed = await _context.Tickets.CountAsync(t => t.Status == TicketStatus.Closed)
            };

            return Ok(stats);
        }

        [HttpGet("{id}")]
        public IActionResult GetTicket(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());
            var ticket = _context.Tickets.Include(t => t.CreatedByUser).FirstOrDefault(t => t.Id == id && (t.CreatedByUserId == userId || t.AssignedToUserId == userId));
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}