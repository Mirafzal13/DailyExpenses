namespace DailyExpenses.Application.UseCases.ExpenseTypes.Queries;

using DailyExpenses.Application.UseCases.ExpenseTypes.Models;
using Microsoft.EntityFrameworkCore;

public record GetExpenseTypesQuery() : PagingRequest, IRequest<PagedList<ExpenseTypeModel>>;

internal sealed class GetExpenseTypesQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetExpenseTypesQuery, PagedList<ExpenseTypeModel>>
{
    public async Task<PagedList<ExpenseTypeModel>> Handle(GetExpenseTypesQuery request, CancellationToken cancellationToken)
    {
        var result = dbContext.ExpenseTypes.Skip(request.Skip).Take(request.Limit).AsQueryable();
        var count = dbContext.ExpenseTypes.Count();

        var expenseType = await result.ProjectTo<ExpenseTypeModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new PagedList<ExpenseTypeModel>(expenseType, count);
    }
}

