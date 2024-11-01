namespace DailyExpenses.Application.Common;

public interface IDbContext
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
