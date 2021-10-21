using eStolutionTech.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStolutionTech.Data.EF
{
    public class eTechDbContext : DbContext
    {
        public eTechDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeOffRequest> TimeOffRequests { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<TimeOffType> TimeOffTypes { get; set; }
    }
}
