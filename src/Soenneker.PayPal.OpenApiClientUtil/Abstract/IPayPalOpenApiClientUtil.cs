using Soenneker.PayPal.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.PayPal.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a cached PayPal REST API client backed by the configured HTTP provider.
/// </summary>
public interface IPayPalOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached PayPal client, creating it on the first call.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The configured PayPal client.</returns>
    ValueTask<PayPalOpenApiClient> Get(CancellationToken cancellationToken = default);
}
