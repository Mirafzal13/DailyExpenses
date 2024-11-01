namespace DailyExpenses.Api.Models.Expense
{
    public class UpdateExpenseRequest
    {
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
    }
}
