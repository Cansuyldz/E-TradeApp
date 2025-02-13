using Microsoft.EntityFrameworkCore;

namespace MVC_E_ticaretWeb.Models
{
    public class DataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=MVC_E-ticaret;Trusted_Connection=True;TrustServerCertificate=true");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }  
        public DbSet<Guest> Guests { get; set; }  
        public DbSet<Category> Categories { get; set; }  

    }
}
