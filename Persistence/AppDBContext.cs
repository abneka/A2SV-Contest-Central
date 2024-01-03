using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppDBContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<QuestionEntity> Questions { get; set; }
        public DbSet<UserQuestionResultEntity> UserQuestionResults { get; set; }
        public DbSet<TeamQuestionResultEntity> TeamQuestionResults { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<ContestEntity> Contests { get; set; }
        public DbSet<ContestGroupEntity> ContestGroups { get; set; }
        public DbSet<TeamContestResultEntity> TeamContestResults { get; set; }
        public DbSet<UserContestResultEntity> UserContestResults { get; set; }
        
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

            modelBuilder.Entity<UserContestResultEntity>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserContestResults)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserQuestionResultEntity>()
                .HasOne(u => u.Question)
                .WithMany(u => u.UserQuestionResults)
                .HasForeignKey(u => u.QuestionId);

            modelBuilder.Entity<UserQuestionResultEntity>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserQuestionResults)
                .HasForeignKey(u => u.UserId);
            
            // ContestGroup Entity
            modelBuilder.Entity<ContestGroupEntity>()
                .HasKey(cg => new { cg.ContestId, cg.GroupId });
            
            modelBuilder.Entity<ContestGroupEntity>()
                .HasOne(cgm => cgm.Contest)
                .WithMany(c => c.ContestGroups)
                .HasForeignKey(cgm => cgm.ContestId);

            modelBuilder.Entity<ContestGroupEntity>()
                .HasOne(cgm => cgm.Group)
                .WithMany(g => g.Contests)
                .HasForeignKey(cgm => cgm.GroupId);
        }
        
    }
}
