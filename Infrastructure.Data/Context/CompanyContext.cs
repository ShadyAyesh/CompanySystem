using CompanySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Infrastructure.Context
{
    public class CompanyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Server=.;Database=clickins;Trusted_Connection=True;";
            optionsBuilder
                .UseSqlServer(connectionString);
        }
    }
}