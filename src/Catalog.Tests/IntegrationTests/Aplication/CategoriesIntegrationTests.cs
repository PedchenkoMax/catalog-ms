namespace Catalog.Tests.IntegrationTests.Aplication;

public class CategoriesIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient client;
    public CategoriesIntegrationTests(TestingWebAppFactory<Program> factory)
        => client = factory.CreateClient();

    [Fact]
    public async Task GetCategoriesAsync_ReturnsOkResultWithData()
    {
        var response = await client.GetAsync("/api/categories");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCategoriesAsync_ReturnApplicationJsonUtf8()
    {
        var response = await client.GetAsync("/api/categories");

        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}
