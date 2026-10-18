using System.Net;

namespace SimplePay.Exception.ExceptionsBase;

public class ConcurrencyConflictException : SimplePayException
{
    public override List<string> GetErrorMessages() => [MessagesExceptionResource.CONCURRENCY_CONFLICT_VALIDATION];
    public override int StatusCode() => (int)HttpStatusCode.Conflict;
}