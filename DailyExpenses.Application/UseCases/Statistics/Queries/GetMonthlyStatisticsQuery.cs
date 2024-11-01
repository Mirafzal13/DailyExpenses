using DailyExpenses.Application.UseCases.Expenses.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyExpenses.Application.UseCases.Statistics.Queries
{
    public record GetMonthlyStatisticsQuery (int? ExpenseTypeId,
        int Year,
        int Month) : PagingRequest, IRequest<PagedList<ExpenseModel>>;

    internal sealed class GetMonthlyStatisticsQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetMonthlyStatisticsQuery, PagedList<ExpenseModel>>
    {
        public async Task<PagedList<ExpenseModel>> Handle(GetMonthlyStatisticsQuery request, CancellationToken cancellationToken)
        {
            DateTime startDate = new DateTime(request.Year, request.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            var result = dbContext.Expenses.Where(x => (!request.ExpenseTypeId.HasValue || x.ExpenseTypeId == request.ExpenseTypeId)
                && (x.CreatedAt >= startDate && x.CreatedAt <= endDate))
                .Skip(request.Skip)
                .Take(request.Limit)
                .AsQueryable();

            var count = dbContext.Expenses.Where(x => (!request.ExpenseTypeId.HasValue || x.ExpenseTypeId == request.ExpenseTypeId)
                && (x.CreatedAt >= startDate && x.CreatedAt <= endDate)).Count();

            var expense = await result.ProjectTo<ExpenseModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return new PagedList<ExpenseModel>(expense, count);
        }
    }
}
