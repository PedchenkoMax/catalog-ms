namespace Catalog.Tests.IntegrationTests.API;

public class BrandsIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient client;
    public BrandsIntegrationTests(TestingWebAppFactory<Program> factory)
        => client = factory.CreateClient();

    [Fact]
    public async Task GetBrandsAsync_ReturnsOkResultWithData()
    {
        var response = await client.GetAsync("/api/brands");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }    
}
