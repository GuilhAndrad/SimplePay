namespace SimplePay.Domain.Entities;

public class Transfer : EntityBase
{
    public int PayerId { get; set; }
    public int PayeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}
