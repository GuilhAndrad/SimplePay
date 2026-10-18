namespace SimplePay.Domain.Entities;

public abstract class EntityBase
{
    public int Id { get; set; }
    public bool Active { get; set; } = true;
}
