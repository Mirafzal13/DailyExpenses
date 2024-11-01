namespace DailyExpenses.Application.UseCases.ExpenseTypes.Commands;

public record CreateExpenseTypeCommand(string Type) : IRequest;

internal class CreateExpenseTypeCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<CreateExpenseTypeCommand>
{
    public async Task Handle(CreateExpenseTypeCommand request, CancellationToken cancellationToken)
    {
        var expenseType = mapper.Map<ExpenseType>(request);

        await dbContext.ExpenseTypes.AddAsync(expenseType, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
