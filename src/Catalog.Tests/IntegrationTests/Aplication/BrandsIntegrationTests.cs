namespace Catalog.Tests.IntegrationTests.Aplication;

public class BrandsIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient client;
    public BrandsIntegrationTests(TestingWebAppFactory<Program> factory)
        => client = factory.CreateClient();

    [Fact]
    public async Task GetBrandsAsync_WithData_ReturnOkResult()
    {
        var response = await client.GetAsync("/api/v1/brands");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBrandsAsync_WithData_ReturnApplicationJsonUtf8()
    {
        var response = await client.GetAsync("/api/v1/brands");

        Assert.Equal("application/json; charset=utf-8; ver=1", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task GetBrandsAsync_WithData_ReturnAllBrands()
    {
        var response = await client.GetAsync("/api/v1/brands");
        response.EnsureSuccessStatusCode();

        var brands = await response.Content.ReadFromJsonAsync<List<Brand>>();

        Assert.Equal(4, brands.Count);
        Assert.Collection(brands,
            item => Assert.Equal("Lenovo", item.Name),
            item => Assert.Equal("Samsung", item.Name),
            item => Assert.Equal("Apple", item.Name),
            item => Assert.Equal("Lg", item.Name));
    }
}