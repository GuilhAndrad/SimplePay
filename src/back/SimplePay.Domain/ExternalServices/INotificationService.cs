namespace SimplePay.Domain.ExternalServices;

public interface INotificationService
{
    Task Notify(string recipient, string message, CancellationToken cancellationToken);
}