using Azure;
using Microsoft.EntityFrameworkCore;
using UsersApi.Models.Role;
using UsersApi.Models.User;

namespace UsersApi.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
     
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique(unique: true);
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" },
                new Role { Id = 3, Name = "Moderator" }
            );

            modelBuilder.Entity<User>().HasMany(e => e.Roles).WithMany().UsingEntity<RoleUsers>(
                l => l.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId),
                r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserId)
            );
        }
    }
}
