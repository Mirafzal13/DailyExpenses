namespace DailyExpenses.Domain.Common;

using System.ComponentModel.DataAnnotations;

public abstract class Entity : IEntity
{
    [Key]
    public int Id { get; set; }
}
