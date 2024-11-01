namespace DailyExpenses.Application.UseCases.ExpenseTypes.Queries;

using Microsoft.EntityFrameworkCore;
using DailyExpenses.Application.UseCases.ExpenseTypes.Models;

public record GetExpenseTypeQuery(int Id) : IRequest<ExpenseTypeModel>;

internal class GetExpenseTypeQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetExpenseTypeQuery, ExpenseTypeModel>
{
    public async Task<ExpenseTypeModel> Handle(GetExpenseTypeQuery request, CancellationToken cancellationToken)
    {
        var expenseType = await dbContext.ExpenseTypes
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(ExpenseType), request.Id);

        return mapper.Map<ExpenseTypeModel>(expenseType);
    }
}
