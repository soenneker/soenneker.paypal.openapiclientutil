using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.PayPal.HttpClients.Abstract;
using Soenneker.PayPal.OpenApiClientUtil.Abstract;
using Soenneker.PayPal.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.PayPal.OpenApiClientUtil;

///<inheritdoc cref="IPayPalOpenApiClientUtil"/>
public sealed class PayPalOpenApiClientUtil : IPayPalOpenApiClientUtil
{
    private readonly AsyncSingleton<PayPalOpenApiClient> _client;

    public PayPalOpenApiClientUtil(IPayPalOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<PayPalOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("PayPal:ApiKey");
            string authHeaderValueTemplate = configuration["PayPal:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

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
