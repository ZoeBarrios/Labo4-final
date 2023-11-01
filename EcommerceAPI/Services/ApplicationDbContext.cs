using Azure;
using EcommerceAPI.Models.Category;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models.Role;
using EcommerceAPI.Models.User;
using EcommerceAPI.Models.Publication;
using EcommerceAPI.Models.Purchase;
using EcommerceAPI.Models.UserFavorite;
using EcommerceAPI.Models.Comment;

namespace EcommerceAPI.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
     
        public DbSet<Role> Roles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Publication> Publications { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<UserFavorite> UserFavorites { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<PurchasePublication> PurchasePublications { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique(unique: true);
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" },
                new Role { Id = 3, Name = "Moderator" }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1,Name="Electrónica"},
                new Category { Id = 2,Name="Electrodomésticos"},
                new Category { Id=3,Name="Moda"},
                new Category { Id = 4, Name = "Hogar y Jardín" },
                new Category { Id = 5, Name = "Deportes y Fitness" },
                new Category { Id = 6, Name = "Belleza y Cuidado Personal" },
                new Category { Id = 7, Name = "Juguetes y Niños" },
                new Category { Id = 8, Name = "Libros,Música y Películas" },
                new Category { Id = 9, Name = "Coleccionables" }

            );

            modelBuilder.Entity<Purchase>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<UserFavorite>()
            .HasKey(uf => new { uf.UserId, uf.PublicationId });


            modelBuilder.Entity<UserFavorite>()
            .HasOne(p => p.Publication)
            .WithMany()
            .HasForeignKey(p => p.PublicationId)
            .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<User>().HasMany(e => e.Roles).WithMany().UsingEntity<RoleUsers>(
                l => l.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId),
                r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserId)
            );


            modelBuilder.Entity<Purchase>().HasMany(e => e.Publications).WithMany().UsingEntity<PurchasePublication>(
                l => l.HasOne<Publication>().WithMany().HasForeignKey(e => e.PublicationId),
                r => r.HasOne<Purchase>().WithMany().HasForeignKey(e => e.PurchaseId)
            );




        }
    }
}
