using Azure;
using EcommerceAPI.Models.Category;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models.Role;
using EcommerceAPI.Models.User;
using EcommerceAPI.Models.Publication;

namespace EcommerceAPI.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
     
        public DbSet<Role> Roles { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Publication> Publication { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique(unique: true);
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

            modelBuilder.Entity<User>().HasMany(e => e.Roles).WithMany().UsingEntity<RoleUsers>(
                l => l.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId),
                r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserId)
            );

            modelBuilder.Entity<User>().HasMany(e => e.Publications).WithMany().UsingEntity<PublicationsBySeller>(
                l => l.HasOne<Publication>().WithMany().HasForeignKey(e => e.PublicationId),
                r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserId)
             );
        }
    }
}
