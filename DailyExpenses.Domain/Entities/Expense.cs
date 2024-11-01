using DailyExpenses.Domain.Common;

namespace DailyExpenses.Domain.Entities;

public class Expense : AuditableEntity, IDeletable
{
    public required decimal Amount { get; set; }

    public string? Comment { get; set; }

    public bool IsDeleted { get; set; }

    public int ExpenseTypeId { get; set; }
    public virtual ExpenseType ExpenseType { get; set; }
}
