namespace SimplePay.Communication.Responses;

public class ErrorResponse
{
    public List<string> Messages { get; set; }

    public ErrorResponse(string message) => Messages = [message];

    public ErrorResponse(List<string> messages) => Messages = messages;
}