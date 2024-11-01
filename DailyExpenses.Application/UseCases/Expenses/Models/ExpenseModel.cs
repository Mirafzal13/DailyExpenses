namespace DailyExpenses.Application.UseCases.Expenses.Models;

public class ExpenseModel
{
    public int Id { get; set; }

    public int ExpenseTypeId { get; set; }

    public decimal Amount { get; set; }

    public string? Comment { get; set; }
}
