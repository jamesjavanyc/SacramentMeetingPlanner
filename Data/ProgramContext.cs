using SacramentMeetingPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace SacramentMeetingPlanner.Data
{
    public class ProgramContext : DbContext
    {
        public ProgramContext(DbContextOptions<ProgramContext> options) : base(options) 
        {

        }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<SacramentMeetingPlanner.Models.Talk> Talk { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meeting>()
                .HasMany(m => m.Talks)
                .WithOne(t => t.Meeting)
                .HasForeignKey(t => t.MeetingId)
                .HasPrincipalKey(t => t.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
