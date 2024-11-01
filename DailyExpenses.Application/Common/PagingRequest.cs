namespace DailyExpenses.Application.Common;

public record PagingRequest
{
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 10;
}
