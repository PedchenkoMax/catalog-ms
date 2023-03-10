using System.Net;
using System.Net.Http.Json;
using Catalog.API.DTO;
using Catalog.Tests.UnitTests;
using Xunit;

namespace Catalog.Tests.IntegrationTests.Aplication;

public class BrandsIntegrationTests : IClassFixture<ApplicationFixture>
{
    private const string BrandEndpoint = "api/v1/brands";
    
    private readonly HttpClient client;
    public BrandsIntegrationTests(ApplicationFixture applicationFixture)
        => client = applicationFixture.Factory.CreateClient();

    [Fact]
    public async Task GetBrandsAsync_WithData_ReturnOkResult()
    {
        var response = await client.GetAsync(BrandEndpoint);
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBrandsAsync_WithData_ReturnApplicationJsonUtf8()
    {
        var response = await client.GetAsync(BrandEndpoint);

        Assert.Equal("application/json; charset=utf-8; ver=1", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task GetBrandsAsync_WithData_ReturnAllBrands()
    {
        var response = await client.GetAsync(BrandEndpoint);
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
        var response = await client.GetAsync($"{BrandEndpoint}/{existBrandId}");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBrandByIdAsync_WithNotExistBrandId_ReturnNotFound() 
    {
        var notExistBrandId = Guid.NewGuid();
        var response = await client.GetAsync($"{BrandEndpoint}/{notExistBrandId}");        

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetBrandByIdAsync_WithEmptyBrandId_ReturnBadRequest() 
    {
        var emptyBrandId = Guid.Empty;
        var response = await client.GetAsync($"{BrandEndpoint}/{emptyBrandId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public async Task GetBrandByIdAsync_WithAppleBrandId_ReturnAppleBrand() 
    {
        var appleBrandId = FakeData.BrandApple;
        var response = await client.GetAsync($"{BrandEndpoint}/{appleBrandId}");
        
        var brand = await response.Content.ReadFromJsonAsync<Brand>();

        Assert.IsType<Brand>(brand);
        Assert.Equal("Apple", brand.Name);
    }
}