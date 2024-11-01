namespace SIDailyExpensesGN.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DailyExpenses.Domain.Entities;

public class ExpenseTypeConfiguration : IEntityTypeConfiguration<ExpenseType>
{
    public virtual void Configure(EntityTypeBuilder<ExpenseType> builder)
    {
        builder.Property(n => n.Type)
            .IsRequired();
    }
}
