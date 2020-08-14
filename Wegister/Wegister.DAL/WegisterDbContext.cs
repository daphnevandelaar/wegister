using Microsoft.EntityFrameworkCore;
using Wegister.Models;

namespace Wegister.DAL
{
    public class WegisterDbContext : DbContext
    {
        public WegisterDbContext(DbContextOptions<WegisterDbContext> options) 
            : base(options)
        {  }

        public DbSet<HourRegistration> HourRegistrations { get; set; }
        public DbSet<Workweek> Workweeks { get; set; }
        public DbSet<Employer> Employers { get; set; }
    }
}
