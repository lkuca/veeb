using Microsoft.EntityFrameworkCore;
using veeb.Models;

namespace veeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<Toode> Tooded { get; set; }
        public DbSet<Kasutaja> kasutajad {  get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}