using Microsoft.EntityFrameworkCore;
using UsersTask.Domain;

namespace UsersTask.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleLink> UserRoleLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Role>().HasKey(x => x.Id);

            // many to many relationship
            modelBuilder.Entity<UserRoleLink>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRoleLink>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserRoleLinks)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<UserRoleLink>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.UserRoleLinks)
                .HasForeignKey(bc => bc.RoleId);
        }
    }
}
