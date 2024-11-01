namespace DailyExpenses.Application.UseCases.Expenses.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateExpenseCommand(
    int Id,
    decimal Amount,
    string? Comment) : IRequest;

internal class UpdateExpenseCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<UpdateExpenseCommand>
{
    public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await GetExpenseAsync(request.Id)
            ?? throw new NotFoundException(nameof(Expense), request.Id);

        mapper.Map(request, expense);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<Expense?> GetExpenseAsync(int id)
    {
        return dbContext.Expenses
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
