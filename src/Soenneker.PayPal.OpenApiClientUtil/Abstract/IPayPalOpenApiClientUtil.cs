using Soenneker.PayPal.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.PayPal.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IPayPalOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<PayPalOpenApiClient> Get(CancellationToken cancellationToken = default);
}
