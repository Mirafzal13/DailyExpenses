using DailyExpenses.Api.Models.Expense;
using DailyExpenses.Application.UseCases.Expenses.Commands;
using DailyExpenses.Application.UseCases.Expenses.Models;
using DailyExpenses.Application.UseCases.Expenses.Queries;

namespace DailyExpenses.Api.Controllers
{
    [Route("api/expense")]
    [ApiController]
    public class ExpenseController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// GetExpenses
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedList<ExpenseModel>>> GetExpenses([FromQuery] GetExpensesQuery request)
        {
            return await sender.Send(request);
        }

        /// <summary>
        /// GetExpense
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseModel>> GetExpense([FromRoute] int id)
        {
            return await sender.Send(new GetExpenseQuery(id));
        }

        /// <summary>
        /// CreateExpense
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseCommand request)
        {
            await sender.Send(request);

            return Ok();
        }

        /// <summary>
        /// UpdateExpense
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense([FromRoute] int id, [FromBody] UpdateExpenseRequest request)
        {
            await sender.Send(new UpdateExpenseCommand(
                id,
                request.Amount,
                request.Comment));

            return Ok();
        }

        /// <summary>
        /// DeleteExpense
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense([FromRoute] int id)
        {
            await sender.Send(new DeleteExpenseCommand(id));

            return Ok();
        }
    }
}
