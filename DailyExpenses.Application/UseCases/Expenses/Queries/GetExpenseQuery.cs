namespace DailyExpenses.Application.UseCases.Expenses.Queries;

using Microsoft.EntityFrameworkCore;
using DailyExpenses.Application.UseCases.Expenses.Models;

public record GetExpenseQuery(int Id) : IRequest<ExpenseModel>;

internal class GetExpenseQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetExpenseQuery, ExpenseModel>
{
    public async Task<ExpenseModel> Handle(GetExpenseQuery request, CancellationToken cancellationToken)
    {
        var expense = await dbContext.Expenses
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Expense), request.Id);

        return mapper.Map<ExpenseModel>(expense);
    }
}
