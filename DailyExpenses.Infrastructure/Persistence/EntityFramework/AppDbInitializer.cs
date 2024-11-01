namespace DailyExpenses.Infrastructure.Persistence.EntityFramework;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DailyExpenses.Domain.Entities;

public class AppDbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.EnsureCreated();
            if (!context.Expenses.Any())
            {
                context.ExpenseTypes.AddRange(new List<ExpenseType>()
                {
                    new ExpenseType()
                    {
                        Type = "Oziq-ovqat"
                    },
                    new ExpenseType()
                    {
                        Type = "Transport"
                    },
                    new ExpenseType()
                    {
                        Type = "Mobil aloqa"
                    },
                    new ExpenseType()
                    {
                        Type = "Internet"
                    },
                    new ExpenseType()
                    {
                        Type = "O'yin-kulgi"
                    },
                });
                context.SaveChanges();
            }
        }
    }
}
