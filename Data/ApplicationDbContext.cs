using Microsoft.EntityFrameworkCore;
using TicketingSystemAPI.Models;

namespace TicketingSystemAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Ticket>().Property(t => t.Status).HasConversion<string>();
            modelBuilder.Entity<Ticket>().Property(t => t.Priority).HasConversion<string>();
        }
    }
}