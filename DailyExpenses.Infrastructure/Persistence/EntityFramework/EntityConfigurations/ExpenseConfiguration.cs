namespace DailyExpenses.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DailyExpenses.Domain.Entities;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public virtual void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.Property(n => n.ExpenseTypeId)
            .IsRequired();
        builder.Property(n => n.Amount)
            .IsRequired();
    }
}
