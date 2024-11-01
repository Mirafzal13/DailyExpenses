namespace DailyExpenses.Application.UseCases.Expenses.Queries;

using DailyExpenses.Application.UseCases.Expenses.Models;
using Microsoft.EntityFrameworkCore;

public record GetExpensesQuery(int? ExpenseTypeId) : PagingRequest, IRequest<PagedList<ExpenseModel>>;

internal sealed class GetExpensesQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetExpensesQuery, PagedList<ExpenseModel>>
{
    public async Task<PagedList<ExpenseModel>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        var result = dbContext.Expenses.Where(x => (!request.ExpenseTypeId.HasValue || x.ExpenseTypeId == request.ExpenseTypeId))
            .Skip(request.Skip)
            .Take(request.Limit)
            .AsQueryable();

        var count = dbContext.Expenses.Where(x => (!request.ExpenseTypeId.HasValue || x.ExpenseTypeId == request.ExpenseTypeId)).Count();

        var expense = await result.ProjectTo<ExpenseModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new PagedList<ExpenseModel>(expense, count);
    }
}

