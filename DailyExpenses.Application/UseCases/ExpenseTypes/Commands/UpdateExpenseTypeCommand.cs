namespace DailyExpenses.Application.UseCases.ExpenseTypes.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateExpenseTypeCommand(
    int Id,
    string Type) : IRequest;

internal class UpdateExpenseTypeCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<UpdateExpenseTypeCommand>
{
    public async Task Handle(UpdateExpenseTypeCommand request, CancellationToken cancellationToken)
    {
        var expenseType = await GetExpenseTypeAsync(request.Id)
            ?? throw new NotFoundException(nameof(ExpenseType), request.Id);

        mapper.Map(request, expenseType);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<ExpenseType?> GetExpenseTypeAsync(int id)
    {
        return dbContext.ExpenseTypes
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
