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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<CartProduct>()
            //.HasOne(cp => cp.Cart)
            //.WithMany(c => c.CartProducts)
            //.HasForeignKey(cp => cp.CartId);

            //modelBuilder.Entity<CartProduct>()
            //    .HasOne(cp => cp.Product)
            //    .WithMany(p => p.CartProducts)
            //    .HasForeignKey(cp => cp.ProductId);


            // Product - Category (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // CartProduct - Product (Many-to-One)
            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(cp => cp.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Sepette ürün varken, ürün silinemez.

            // CartProduct - Cart (Many-to-One)
            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Cart)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(cp => cp.CartId)
                .OnDelete(DeleteBehavior.Cascade); // Sepet silinirse, CartProducts da silinir.

            // Product İçin Ek Ayarlar
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Stock)
                .IsRequired();

            // Category İçin Ek Ayarlar
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            // NotMapped olan IsFavorite’i Fluent API'de belirtmeye gerek yok.
        }

        private void DataSeeder()
        {

            this.Products.AddRange(new List<Product>() {
                new Product() {
                    Name = "Monitör",
                    Image = "images/bilgisayar.jpg",
                    Price = 15000,
                    Stock = 156
                },
                new Product(){
                    Name = "Televizyon",
                    Image = "images/televizyon.jpg",
                    Price = 26000,
                    Stock = 203
                },
                new Product(){
                    Name = "Dizüstü Bilgisayar",
                    Image = "images/pc.jpg",
                    Price = 32000,
                    Stock = 25
                },
                new Product(){
                    Name = "Yazıcı",
                    Image = "images/yazıcı.jpg",
                    Price = 12500,
                    Stock = 159
                },
                new Product(){
                    Name = "Mouse",
                    Image = "images/mouse.png",
                    Price = 1560,
                    Stock = 12
                },
                new Product(){
                    Name = "Mouse pad",
                    Image = "images/mouse.png",
                    Price = 15689,
                    Stock = 156
                },
                new Product(){
                    Name = "Kulaklık",
                    Image = "images/kulaklık.png",
                    Price = 12000,
                    Stock = 156
                }
            });
            this.SaveChanges();
        }
    }
}
