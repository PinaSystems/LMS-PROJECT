using Microsoft.EntityFrameworkCore;
using OAWA.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace OAWA.Data
{
    public class DataContext : IdentityDbContext<User,Role, long, 
    IdentityUserClaim<long>,UserRole, IdentityUserLogin<long>,
     IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<LearnerChoice> LearnerChoices { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<WebsiteVisitor> WebsiteVisitors { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>  {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId});  

            userRole.HasOne(ur => ur.Role)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

            userRole.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

            });
        }

    }
}