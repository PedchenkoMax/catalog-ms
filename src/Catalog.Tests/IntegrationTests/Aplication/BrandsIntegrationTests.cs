using System.Net;
using System.Net.Http.Json;
using Catalog.API.DTO;
using Catalog.Tests.UnitTests;
using Xunit;

namespace Catalog.Tests.IntegrationTests.Aplication;

public class BrandsIntegrationTests : IClassFixture<TestingWebAppFactory>
{
    private readonly HttpClient client;
    public BrandsIntegrationTests(TestingWebAppFactory factory)
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

    [Fact]
    public async Task GetBrandByIdAsync_WithExistBrandId_ReturnOkResult() 
    {
        var existBrandId = FakeData.BrandApple;
        var response = await client.GetAsync($"/api/v1/brands/{existBrandId}");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBrandByIdAsync_WithNotExistBrandId_ReturnNotFound() 
    {
        var notExistBrandId = Guid.NewGuid();
        var response = await client.GetAsync($"/api/v1/brands/{notExistBrandId}");        

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetBrandByIdAsync_WithEmptyBrandId_ReturnBadRequest() 
    {
        var emptyBrandId = Guid.Empty;
        var response = await client.GetAsync($"/api/v1/brands/{emptyBrandId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public async Task GetBrandByIdAsync_WithAppleBrandId_ReturnAppleBrand() 
    {
        var appleBrandId = FakeData.BrandApple;
        var response = await client.GetAsync($"/api/v1/brands/{appleBrandId}");
        
        var brand = await response.Content.ReadFromJsonAsync<Brand>();

        Assert.IsType<Brand>(brand);
        Assert.Equal("Apple", brand.Name);
    }
}