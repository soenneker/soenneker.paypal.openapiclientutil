[![](https://img.shields.io/nuget/v/soenneker.paypal.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.paypal.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.paypal.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.paypal.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.paypal.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.paypal.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.paypal.openapiclientutil/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.paypal.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.PayPal.OpenApiClientUtil

Provides a configured PayPal REST API client and reuses it for the lifetime of the registered service.

## Installation

```bash
dotnet add package Soenneker.PayPal.OpenApiClientUtil
```

## Configuration

```json
{
  "PayPal": {
    "AccessToken": "your-oauth-access-token",
    "ClientBaseUrl": "https://api-m.sandbox.paypal.com"
  }
}
```

Use `https://api-m.paypal.com` for live requests. This package consumes an access token; it does not exchange client credentials or refresh expired tokens.

## Usage

```csharp
using Soenneker.PayPal.OpenApiClientUtil.Abstract;
using Soenneker.PayPal.OpenApiClientUtil.Registrars;

services.AddPayPalOpenApiClientUtilAsSingleton();

IPayPalOpenApiClientUtil payPal = serviceProvider
    .GetRequiredService<IPayPalOpenApiClientUtil>();

var client = await payPal.Get(cancellationToken);
var eventTypes = await client.Notifications_webhooks_v1.V1.Notifications
    .WebhooksEventTypes
    .WithUrl("https://api-m.sandbox.paypal.com/v1/notifications/webhooks-event-types")
    .GetAsync(cancellationToken: cancellationToken);
```

The merged schema namespaces request paths by source document, so use `WithUrl(...)` with the real PayPal endpoint.

Use `AddPayPalOpenApiClientUtilAsScoped()` when each application scope should have its own generated client wrapper. The underlying authenticated HTTP provider remains shared and is disposed by the service container at shutdown.
