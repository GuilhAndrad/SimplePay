namespace SimplePay.Domain.ExternalServices;

public interface IAuthorizerService
{
    Task<bool> Authorize(CancellationToken cancellationToken);
}