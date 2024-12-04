using Microsoft.EntityFrameworkCore;
using veeb.Models;

namespace veeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Kasutaja> Kasutajad { get; set; } // Таблица пользователей
        public DbSet<Cart> Carts { get; set; } // Таблица корзин
        public DbSet<Toode> Tooted { get; set; } // Таблица товаров

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка отношений Kasutaja -> Cart
            modelBuilder.Entity<Kasutaja>()
                .HasOne(k => k.Cart)
                .WithOne(c => c.Kasutaja)
                .HasForeignKey<Kasutaja>(k => k.CartId)
                .OnDelete(DeleteBehavior.SetNull);

            // Настройка отношений Cart -> Toode
            modelBuilder.Entity<Toode>()
         .HasOne(t => t.Cart)
         .WithMany(c => c.Tooted)
         .HasForeignKey(t => t.CartId)
         .IsRequired(false);
        }
    }
}