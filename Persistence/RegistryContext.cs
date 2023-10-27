using FamilySync.Registry.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilySync.Registry.Persistence;

public class RegistryContext : DbContext
{
    public DbSet<Family> Families { get; set; }
    public DbSet<FamilyMember> Members { get; set; }

    public RegistryContext(DbContextOptions<RegistryContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Family>()
            .HasMany(x => x.FamilyMembers)
            .WithOne(x => x.Family)
            .HasForeignKey(x => x.FamilyId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FamilyMember>()
            .HasKey(x => new { x.MemberId, x.FamilyId });
    }
}