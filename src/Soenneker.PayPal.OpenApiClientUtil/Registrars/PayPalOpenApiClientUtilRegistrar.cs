using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.PayPal.HttpClients.Registrars;
using Soenneker.PayPal.OpenApiClientUtil.Abstract;

namespace Soenneker.PayPal.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class PayPalOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="PayPalOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddPayPalOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddPayPalOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IPayPalOpenApiClientUtil, PayPalOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="PayPalOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddPayPalOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddPayPalOpenApiHttpClientAsSingleton()
                .TryAddScoped<IPayPalOpenApiClientUtil, PayPalOpenApiClientUtil>();

        return services;
    }
}
