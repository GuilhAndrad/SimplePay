namespace SimplePay.Communication.Requests;

public class TransferRequest
{
    public decimal Value { get; set; }
    public int Payer { get; set; }
    public int Payee { get; set; }
}