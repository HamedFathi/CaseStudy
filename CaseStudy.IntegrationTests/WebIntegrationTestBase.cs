using CaseStudy.Infrastructure;
using HamedStack.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace CaseStudy.IntegrationTests;

public abstract class WebIntegrationTestBase : IClassFixture<IntegrationTestWebAppFactory>
{
    public HttpClient HttpClient { get; }
    protected ICommandQueryDispatcher Dispatcher { get; }
    protected CaseStudyContext DbContext { get; }

    protected WebIntegrationTestBase(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Dispatcher = scope.ServiceProvider.GetRequiredService<ICommandQueryDispatcher>();
        DbContext = scope.ServiceProvider.GetRequiredService<CaseStudyContext>();
        HttpClient = factory.CreateClient();
    }
}