using Soenneker.PayPal.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.PayPal.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class PayPalOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IPayPalOpenApiClientUtil _openapiclientutil;

    public PayPalOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IPayPalOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
