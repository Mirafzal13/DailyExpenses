namespace DailyExpenses.Application.UseCases.ExpenseTypes.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteExpenseTypeCommand(int Id) : IRequest;

internal class DeleteExpenseTypeCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteExpenseTypeCommand>
{
    public async Task Handle(DeleteExpenseTypeCommand request, CancellationToken cancellationToken)
    {
        var expenseType = await dbContext.ExpenseTypes
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (expenseType == 0)
        {
            throw new NotFoundException(nameof(ExpenseType), request.Id);
        }
    }
}
