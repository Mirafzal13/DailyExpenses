namespace DailyExpenses.Application.UseCases.Expenses.Mappers;

using DailyExpenses.Application.UseCases.Expenses.Commands;
using DailyExpenses.Application.UseCases.Expenses.Models;

public class ExpenseMappingProfile : Profile
{
    public ExpenseMappingProfile()
    {
        CreateMap<Expense, ExpenseModel>();
        CreateMap<CreateExpenseCommand, Expense>();
        CreateMap<UpdateExpenseCommand, Expense>();
    }
}
