using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VEA.Core.Domain;
using VEA.Core.Domain.Common;
using VEA.Tests.UnitTests.Services;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace VEA.Tests.UnitTests.Fixtures;

public class Fixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
    {
        services.RegisterCoreServices();

        services.AddTransient<ICurrentTime, StubCurrentTime>();
    }

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
    {
        yield return new();
    }

    protected override ValueTask DisposeAsyncCore()
        => new();
}