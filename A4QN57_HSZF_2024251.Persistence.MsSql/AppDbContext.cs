using A4QN57_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.Extensions.Logging;
namespace A4QN57_HSZF_2024251.Persistence.MsSql
{
    //connection to the database
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Env.Load();
            bool isProductionMode = Environment.GetEnvironmentVariable("ENVIRONMENT") == "Production";
            if (isProductionMode)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Plurasight");
            }
            else
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Plurasight")
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information );
            }
        }
        //generate or interact with the tables Courses - Users - Licenses
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<License> Licenses { get; set; }

    }
}
