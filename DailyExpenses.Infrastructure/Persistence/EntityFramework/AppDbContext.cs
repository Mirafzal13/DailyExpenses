namespace DailyExpenses.Infrastructure.Persistence.EntityFramework;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using DailyExpenses.Application.Common;
using DailyExpenses.Domain.Common;
using DailyExpenses.Domain.Entities;
using DailyExpenses.Infrastructure.Persistence.EntityFramework.Extensions;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseType> ExpenseTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.SetSoftDeleteFilter();
    }

    public override int SaveChanges()
    {
        SetAuditableEntity();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditableEntity();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditableEntity()
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTimeOffset.Now;
                entry.Entity.UpdatedAt = DateTimeOffset.Now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTimeOffset.Now;
            }
        }
    }
}
