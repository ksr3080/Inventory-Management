using System.Net.Http.Headers;

namespace HotelManagement.Handler;

/// <summary>
/// DelegatingHandler that automatically injects the JWT Bearer token into outgoing API requests.
/// </summary>
public class AuthHeaderHandler : DelegatingHandler
{
    private readonly AuthTokenHolder _tokenHolder;

    public AuthHeaderHandler(AuthTokenHolder tokenHolder)
    {
        _tokenHolder = tokenHolder;
    }

    public AuthHeaderHandler(AuthTokenHolder tokenHolder, HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
        _tokenHolder = tokenHolder;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_tokenHolder.Token) && request.Headers.Authorization == null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenHolder.Token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
