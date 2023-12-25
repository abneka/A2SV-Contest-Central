using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppDBContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
        
    }
}
