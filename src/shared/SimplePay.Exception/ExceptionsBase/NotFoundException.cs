using System.Net;

namespace SimplePay.Exception.ExceptionsBase;

public class NotFoundException(string message) : SimplePayException
{
    public override List<string> GetErrorMessages() => [message];
    public override int StatusCode() => (int)HttpStatusCode.NotFound;
}