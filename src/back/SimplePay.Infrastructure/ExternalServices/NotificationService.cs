using Microsoft.Extensions.Logging;
using SimplePay.Domain.ExternalServices;
using System.Net.Http.Json;

namespace SimplePay.Infrastructure.ExternalServices;

internal sealed class NotificationService(HttpClient httpClient, ILogger<NotificationService> logger) : INotificationService
{
    private const string NotifyUrl = "https://util.devi.tools/api/v1/notify";

    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<NotificationService> _logger = logger;

    public async Task Notify(string recipient, string message, CancellationToken cancellationToken)
    {
        var payload = new { recipient, message };

        var response = await _httpClient.PostAsJsonAsync(NotifyUrl, payload, cancellationToken);

        if (response.IsSuccessStatusCode == false)
            _logger.LogWarning("Notification service returned {StatusCode} for {Recipient}.", response.StatusCode, recipient);
    }
}
