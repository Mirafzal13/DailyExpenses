namespace DailyExpenses.Application.UseCases.ExpenseTypes.Mappers;

using DailyExpenses.Application.UseCases.ExpenseTypes.Commands;
using DailyExpenses.Application.UseCases.ExpenseTypes.Models;

public class ExpenseMappingProfile : Profile
{
    public ExpenseMappingProfile()
    {
        CreateMap<ExpenseType, ExpenseTypeModel>();
        CreateMap<CreateExpenseTypeCommand, ExpenseType>();
        CreateMap<UpdateExpenseTypeCommand, ExpenseType>();
    }
}
