namespace DailyExpenses.Application.Common;

using DailyExpenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public interface IAppDbContext : IDbContext
{
    DbSet<Expense> Expenses { get; set; }
    DbSet<ExpenseType> ExpenseTypes { get; set; }
}
