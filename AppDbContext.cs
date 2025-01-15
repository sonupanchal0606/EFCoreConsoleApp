using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfileEFCoreConsole
{
    public class AppDbContext : DbContext
    {
        // DbSets for Entities
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure PostgreSQL Connection
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=UserProfileEFCoreDemo;Username=postgres;Password=12345678");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-One Relationship between User and Profile
            // User -> Profile (One-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Ensures profile is deleted with user

            // One-to-Many Relationship between User and Posts
            // // User -> Posts (One-to-Many)
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Ensures posts are deleted with user

            // Many-to-Many Relationship between User and Role
            // User -> UserRoles (Many-to-Many through bridge table)
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // Composite Key

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                 .OnDelete(DeleteBehavior.Cascade); // Ensures UserRoles are deleted with user

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

/*            modelBuilder.Entity<User>()
                .Property(u => u.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Post>()
                .Property(p => p.RowVersion)
                .IsRowVersion();*/

        }
    }
}