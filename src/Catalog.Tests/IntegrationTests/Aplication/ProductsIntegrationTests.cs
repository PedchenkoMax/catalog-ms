namespace Catalog.Tests.IntegrationTests.Aplication;

public class ProductsIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient client;
    public ProductsIntegrationTests(TestingWebAppFactory<Program> factory)
        => client = factory.CreateClient();

    [Fact]
    public async Task GetBrandsAsync_ReturnsOkResultWithData()
    {
        var response = await client.GetAsync("/api/products");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
