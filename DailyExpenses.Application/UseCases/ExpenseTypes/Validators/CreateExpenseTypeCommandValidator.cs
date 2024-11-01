namespace DailyExpenses.Application.UseCases.ExpenseTypes.Validators;

using DailyExpenses.Application.UseCases.ExpenseTypes.Commands;

public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseTypeCommand>
{
    public CreateExpenseCommandValidator()
    {
        RuleFor(x => x.Type).NotEmpty();
    }
}
