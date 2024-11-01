using DailyExpenses.Api.Models.ExpenseType;
using DailyExpenses.Application.UseCases.ExpenseTypes.Commands;
using DailyExpenses.Application.UseCases.ExpenseTypes.Models;
using DailyExpenses.Application.UseCases.ExpenseTypes.Queries;

namespace DailyExpenses.Api.Controllers
{
    [Route("api/expense-type")]
    [ApiController]
    public class ExpenseTypeController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// GetExpenseTypes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedList<ExpenseTypeModel>>> GetExpenseTypes([FromQuery] GetExpenseTypesQuery request)
        {
            return await sender.Send(request);
        }

        /// <summary>
        /// GetExpenseType
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseTypeModel>> GetExpenseType([FromRoute] int id)
        {
            return await sender.Send(new GetExpenseTypeQuery(id));
        }

        /// <summary>
        /// CreateExpenseType
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateExpenseType([FromBody] CreateExpenseTypeCommand request)
        {
            await sender.Send(request);

            return Ok();
        }

        /// <summary>
        /// UpdateExpenseType
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpenseType([FromRoute] int id, [FromBody] UpdateExpenseTypeRequest request)
        {
            await sender.Send(new UpdateExpenseTypeCommand(
                id,
                request.Type));

            return Ok();
        }

        /// <summary>
        /// DeleteExpenseType
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseType([FromRoute] int id)
        {
            await sender.Send(new DeleteExpenseTypeCommand(id));

            return Ok();
        }
    }
}
