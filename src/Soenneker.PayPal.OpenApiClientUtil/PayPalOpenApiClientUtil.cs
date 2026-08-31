using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.PayPal.HttpClients.Abstract;
using Soenneker.PayPal.OpenApiClient;
using Soenneker.PayPal.OpenApiClientUtil.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.PayPal.OpenApiClientUtil;

public sealed class PayPalOpenApiClientUtil : IPayPalOpenApiClientUtil
{
    private readonly AsyncSingleton<PayPalOpenApiClient> _client;

    public PayPalOpenApiClientUtil(IPayPalOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<PayPalOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            if (httpClient.BaseAddress is not null)
                requestAdapter.BaseUrl = httpClient.BaseAddress.ToString().TrimEnd('/');

            return new PayPalOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<PayPalOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
