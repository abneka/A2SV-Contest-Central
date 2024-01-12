using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Seed;

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
        public DbSet<UserTypeEntity> UserTypeEntity { get; set; }
        
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

            modelBuilder.Entity<LocationEntity>()
                .HasIndex(u => u.Id)
                .IsUnique();
            
            // User Entity
            // User Entity with Group
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(u => u.GroupId);

            // User Entity with UserType
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.UserType)
                .WithMany(ut => ut.Users)
                .HasForeignKey(u => u.UserTypeId);

            // UserQuestionResult Entity
            // UserQuestionResult Entity with Question
            modelBuilder.Entity<UserQuestionResultEntity>()
                .HasOne(u => u.Question)
                .WithMany(u => u.UserQuestionResults)
                .HasForeignKey(u => u.QuestionId);

            // UserQuestionResult Entity with User
            modelBuilder.Entity<UserQuestionResultEntity>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserQuestionResults)
                .HasForeignKey(u => u.UserId);
            
            // Question Entity
            // Question Entity with Contest
            modelBuilder.Entity<QuestionEntity>()
                .HasOne(q => q.Contest)
                .WithMany(q => q.Questions)
                .HasForeignKey(q => q.ContestId);
            
            // Question Entity with UserQuestionResult
            modelBuilder.Entity<QuestionEntity>()
                .HasMany(q => q.UserQuestionResults)
                .WithOne(u => u.Question)
                .HasForeignKey(u => u.QuestionId);
            
            // UserContestResult Entity
            // UserContestResult Entity with User
            modelBuilder.Entity<UserContestResultEntity>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserContestResults)
                .HasForeignKey(u => u.UserId);
            
            // UserContestResult Entity with Contest
            modelBuilder.Entity<UserContestResultEntity>()
                .HasOne(u => u.Contest)
                .WithMany(u => u.UserContestResults)
                .HasForeignKey(u => u.ContestId);
            
            // Contest Entity
            // Contest Entity with Questions
            modelBuilder.Entity<ContestEntity>()
                .HasMany(c => c.Questions)
                .WithOne(q => q.Contest)
                .HasForeignKey(q => q.ContestId);
            
            // Contest Entity with UserContestResult
            modelBuilder.Entity<ContestEntity>()
                .HasMany(c => c.UserContestResults)
                .WithOne(u => u.Contest)
                .HasForeignKey(u => u.ContestId);
            
            // Contest Entity with UserContestResult
            modelBuilder.Entity<ContestEntity>()
                .HasMany(c => c.TeamContestResults)
                .WithOne(u => u.Contest)
                .HasForeignKey(u => u.ContestId);
            
            // Contest Entity with Group
            modelBuilder.Entity<ContestEntity>()
                .HasMany(c => c.ContestGroups)
                .WithOne(c => c.Contest)
                .HasForeignKey(c => c.ContestId);
            
            // Group Entity
            // Group Entity with Location
            modelBuilder.Entity<GroupEntity>()
                .HasOne(g => g.Location)
                .WithMany(l => l.Groups)
                .HasForeignKey(g => g.LocationId);
            
            // ContestGroup Entity
            // ContestGroup with Contest
            modelBuilder.Entity<ContestGroupEntity>()
                .HasKey(cg => new { cg.ContestId, cg.GroupId });
            
            modelBuilder.Entity<ContestGroupEntity>()
                .HasOne(cgm => cgm.Contest)
                .WithMany(c => c.ContestGroups)
                .HasForeignKey(cgm => cgm.ContestId);

            // ContestGroup with Group
            modelBuilder.Entity<ContestGroupEntity>()
                .HasOne(cgm => cgm.Group)
                .WithMany(g => g.Contests)
                .HasForeignKey(cgm => cgm.GroupId);
            
            // Seed Database
            SeedData.Seed(modelBuilder);
        }
        
    }
}
