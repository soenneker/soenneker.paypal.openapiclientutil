using Soenneker.PayPal.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.PayPal.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class PayPalOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IPayPalOpenApiClientUtil _openapiclientutil;

    public PayPalOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IPayPalOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
