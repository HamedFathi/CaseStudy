using FluentAssertions;

namespace CaseStudy.IntegrationTests;

public class CaseStudyIntegrationTest : WebIntegrationTestBase
{
    public CaseStudyIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public void ShouldSeedDataSuccessfully()
    {
        var vendors = DbContext.Vendors.ToList();
        var count = vendors.Count;

        count.Should().Be(3);
    }
}