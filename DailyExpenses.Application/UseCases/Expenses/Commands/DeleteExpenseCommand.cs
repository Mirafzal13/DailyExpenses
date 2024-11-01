namespace DailyExpenses.Application.UseCases.Expenses.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteExpenseCommand(int Id) : IRequest;

internal class DeleteExpenseCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteExpenseCommand>
{
    public async Task Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await dbContext.Expenses
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (expense == 0)
        {
            throw new NotFoundException(nameof(Expense), request.Id);
        }
    }
}
