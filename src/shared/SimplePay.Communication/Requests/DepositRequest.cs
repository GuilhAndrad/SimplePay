namespace SimplePay.Communication.Requests;

public class DepositRequest
{
    public int UserId { get; set; }
    public decimal Value { get; set; }
}
