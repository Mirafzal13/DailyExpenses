namespace DailyExpenses.Application.UseCases.Expenses.Validators;

using DailyExpenses.Application.UseCases.Expenses.Commands;

public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
    {
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.ExpenseTypeId).NotEmpty();
    }
}
