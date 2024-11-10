using CrudApiSession3.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApiSession3.Data
{
    public class ApplicationDbContext:DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
