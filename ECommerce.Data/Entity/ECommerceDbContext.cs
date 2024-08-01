using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAcces.Entity
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\LocalDB;Database=ECommerceDB;Trusted_Connection=True;");
            }
        }
    }
}
