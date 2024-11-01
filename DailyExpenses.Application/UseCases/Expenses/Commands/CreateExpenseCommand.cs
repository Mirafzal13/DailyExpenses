namespace DailyExpenses.Application.UseCases.Expenses.Commands;

public record CreateExpenseCommand(string ExpenseTypeId,
    decimal Amount,
    string? Comment) : IRequest;

internal class CreateExpenseCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<CreateExpenseCommand>
{
    public async Task Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = mapper.Map<Expense>(request);

        await dbContext.Expenses.AddAsync(expense, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
