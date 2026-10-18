using Microsoft.Extensions.Logging;
using SimplePay.Domain.ExternalServices;
using System.Net.Http.Json;

namespace SimplePay.Infrastructure.ExternalServices;

internal sealed class AuthorizerService(HttpClient httpClient, ILogger<AuthorizerService> logger) : IAuthorizerService
{
    private const string AuthorizeUrl = "https://util.devi.tools/api/v2/authorize";

    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<AuthorizerService> _logger = logger;

    public async Task<bool> Authorize(CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync(AuthorizeUrl, cancellationToken);

            if (response.IsSuccessStatusCode == false)
            {
                _logger.LogWarning("Authorizer service returned {StatusCode}. Denying transfer.", response.StatusCode);

                return false;
            }

            var body = await response.Content.ReadFromJsonAsync<AuthorizerResponse>(cancellationToken);

            return body?.Data?.Authorization ?? false;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to reach the authorizer service. Denying transfer.");

            return false;
        }
    }

    private sealed class AuthorizerResponse
    {
        public AuthorizerData? Data { get; set; }
    }

    private sealed class AuthorizerData
    {
        public bool Authorization { get; set; }
    }
}
