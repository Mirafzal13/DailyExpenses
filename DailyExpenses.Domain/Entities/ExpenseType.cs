using DailyExpenses.Domain.Common;

namespace DailyExpenses.Domain.Entities;

public class ExpenseType : AuditableEntity, IDeletable
{
    public required string Type { get; set; }

    public bool IsDeleted { get; set; }
}
