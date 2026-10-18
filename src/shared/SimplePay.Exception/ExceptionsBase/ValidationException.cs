using System.Net;

namespace SimplePay.Exception.ExceptionsBase;

public class ValidationException : SimplePayException
{
    private readonly List<string> _errorMessages;

    public ValidationException(List<string> errorMessages) => _errorMessages = errorMessages;

    public override List<string> GetErrorMessages() => _errorMessages;
    public override int StatusCode() => (int)HttpStatusCode.BadRequest;
}
