using DummyProjectApi.BusinessModel.Model;
using Microsoft.EntityFrameworkCore;

namespace DummyProjectApi.DataContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Registration> EmployeeBasicInformation { get; set; }
        public DbSet<UserType> TypeOfUser { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
