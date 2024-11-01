namespace DailyExpenses.Application.UseCases.Expenses.Validators;

using DailyExpenses.Application.UseCases.Expenses.Commands;

public class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommand>
{
    public UpdateExpenseCommandValidator()
    {
        RuleFor(x => x.Amount).NotEmpty();
    }
}
