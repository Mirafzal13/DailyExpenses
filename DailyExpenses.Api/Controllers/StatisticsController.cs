using DailyExpenses.Application.UseCases.Expenses.Models;
using DailyExpenses.Application.UseCases.Statistics.Queries;

namespace DailyExpenses.Api.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// GetMonthlyStatistics
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedList<ExpenseModel>>> GetMonthlyStatistics([FromQuery] GetMonthlyStatisticsQuery request)
        {
            return await sender.Send(request);
        }
    }
}
