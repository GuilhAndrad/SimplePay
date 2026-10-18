namespace SimplePay.Communication.Responses;

public class TransferResponse
{
    public int Id { get; set; }
    public int Payer { get; set; }
    public int Payee { get; set; }
    public decimal Value { get; set; }
    public DateTime CreatedAt { get; set; }
}
