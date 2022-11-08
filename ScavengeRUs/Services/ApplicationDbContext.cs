using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScavengeRUs.Models.Entities;

namespace ScavengeRUs.Data
{
    /// <summary>
    /// This is the interface to connects to the database
    /// 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Location> Location { get; set; }
        public DbSet<Hunt> Hunts => Set<Hunt>();
        public DbSet<AccessCode> AccessCodes => Set<AccessCode>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.AccessCode)
                .WithMany(a => a.Users)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AccessCode>()
                .HasMany(a => a.Users)
                .WithOne(a => a.AccessCode)
                .OnDelete(DeleteBehavior.Cascade);            
            builder.Entity<AccessCode>()
                .HasOne(a => a.Hunt)
                .WithMany(a => a.AccessCodes)
                .HasForeignKey(a => a.HuntId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Hunt>()
                .HasMany(a => a.Players)
                .WithOne(a => a.Hunt)

                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Hunt>()
                .HasMany(a => a.AccessCodes)
                .WithOne(a => a.Hunt)
                .HasForeignKey(a => a.HuntId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            
        }
    }
}