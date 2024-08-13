using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAcces.Entity
{
    // Entity Framework Core'un DbContext sınıfından türetilmiş ECommerceDbContext sınıfı
    public class ECommerceDbContext : DbContext
    {
        // DbContext'e options nesnesini geçiren yapıcı metot
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }

        // Users tablosunu temsil eden DbSet
        public DbSet<User> Users { get; set; }

        // Veritabanı konfigürasyonunu yapılandırmak için override edilen metod
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Eğer DbContext options zaten yapılandırılmışsa
            if (optionsBuilder.IsConfigured)
            {
                // SQL Server ile bağlantı dizesini yapılandırır
                optionsBuilder.UseSqlServer("Server=(localdb)\\LocalDB;Database=ECommerceDB;Trusted_Connection=True;");
            }
        }
    }
}
