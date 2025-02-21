using partyForm2.Data;
using Microsoft.EntityFrameworkCore;

namespace partyForm2.Database
{
    public class EmployeeDbContext: DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
