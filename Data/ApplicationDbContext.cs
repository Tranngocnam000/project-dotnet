using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phone.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Phone.Data.Interfaces;

namespace Phone.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        IEnumerable<EntityEntry> modified = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
        foreach (EntityEntry item in modified)
        {
            if (item.Entity is IDateTracking changedOrAddedItem)
            {
                if (item.State == EntityState.Added)
                {
                    changedOrAddedItem.CreateAt = DateTime.Now;
                    changedOrAddedItem.UpdatedAt = DateTime.Now;
                }
                else
                {
                    changedOrAddedItem.UpdatedAt = DateTime.Now;
                }
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Mobile> Phones { get; set; }
     public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasMany(e => e.Mobiles)
        .WithOne(e => e.Category)
        .HasForeignKey(e => e.CategoryId)
        .HasPrincipalKey(e => e.Id);
    }
}
