namespace DailyExpenses.Application.UseCases.ExpenseTypes.Validators;

using DailyExpenses.Application.UseCases.ExpenseTypes.Commands;

public class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseTypeCommand>
{
    public UpdateExpenseCommandValidator()
    {
        RuleFor(x => x.Type).NotEmpty();
    }
}
