namespace SimplePay.Domain.Entities;

public class Wallet : EntityBase
{
    public int UserId { get; set; }
    public decimal Balance { get; set; }
    public int Version { get; set; }
}
