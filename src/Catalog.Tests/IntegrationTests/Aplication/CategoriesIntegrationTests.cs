namespace Catalog.Tests.IntegrationTests.Aplication;

public class CategoriesIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient client;
    public CategoriesIntegrationTests(TestingWebAppFactory<Program> factory)
        => client = factory.CreateClient();

    [Fact]
    public async Task GetCategoriesAsync_WithData_ReturnOkResult()
    {
        var response = await client.GetAsync("/api/v1/categories");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCategoriesAsync_WithData_ReturnApplicationJsonUtf8()
    {
        var response = await client.GetAsync("/api/v1/categories");

        Assert.Equal("application/json; charset=utf-8; ver=1", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task GetBrandsAsync_WithData_ReturnAllBrands()
    {
        var response = await client.GetAsync("/api/v1/categories");
        response.EnsureSuccessStatusCode();

        var categories = await response.Content.ReadFromJsonAsync<List<Category>>();

        Assert.Equal(3, categories.Count);
        Assert.Collection(categories,
            item => Assert.Equal("Phone", item.Name),
            item => Assert.Equal("TV", item.Name),
            item => Assert.Equal("Notebook", item.Name));          
    }
}