namespace DailyExpenses.Application.UseCases.ExpenseTypes.Models;

public class ExpenseTypeModel
{
    public int Id { get; set; }

    public required string Type { get; set; }
}
