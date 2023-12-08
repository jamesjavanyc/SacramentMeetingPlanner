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
        public DbSet<Activity> Activities { get; set; }
    }
}
