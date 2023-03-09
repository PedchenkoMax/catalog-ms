using System.Net;
using System.Net.Http.Json;
using Catalog.API.DTO;
using Catalog.Tests.UnitTests;
using Xunit;

namespace Catalog.Tests.IntegrationTests.Aplication;

public class ProductsIntegrationTests : IClassFixture<TestingWebAppFactory>
{
    private const string ProductEndpoint = "api/v1/products";
    
    private readonly HttpClient client;
    public ProductsIntegrationTests(TestingWebAppFactory factory)
        => client = factory.CreateClient();

    [Fact]
    public async Task ProductByIdAsync_WithExistsProductId_ReturnOkResult()
    {
        var existProductId = FakeData.Phone1;        
        var response = await client.GetAsync($"{ProductEndpoint}/{existProductId}");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WithExistsProuctId_ReturnApplicationJsonUtf8()
    {
        var existProductId = FakeData.Phone1;
        var response = await client.GetAsync($"{ProductEndpoint}/{existProductId}");

        Assert.Equal("application/json; charset=utf-8; ver=1", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task ProductByIdAsync_WithNotExistsProductId_ReturnNotFound()
    {
        var notExistProductId = Guid.NewGuid();
        var response = await client.GetAsync($"{ProductEndpoint}/{notExistProductId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WithEmptyProductId_ReturnBadRequest()
    {
        var emptyProductId = Guid.Empty;
        var response = await client.GetAsync($"{ProductEndpoint}/{emptyProductId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WithIphoneProductId_ReturnIphoneProduct() {
        var iphoneProductId = FakeData.Phone1;
        var response = await client.GetAsync($"{ProductEndpoint}/{iphoneProductId}");

        var product = await response.Content.ReadFromJsonAsync<Product>();

        Assert.IsType<Product>(product);
        Assert.Equal("Apple iPhone 12 Pro Max", product.Name);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnOkResult()
    {        
        var response = await client.GetAsync($"{ProductEndpoint}/");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8; ver=1", response.Content.Headers.ContentType.ToString());       
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnDefaultPaginedTenProducts()
    {
        var response = await client.GetAsync($"{ProductEndpoint}/");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.IsType<List<Product>>(products);
        Assert.Equal(10, products.Count);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnFivePaginedProducts()
    {
        var response = await client.GetAsync($"{ProductEndpoint}/?PageIndex=0&PageSize=5");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.IsType<List<Product>>(products);
        Assert.Equal(5, products.Count);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnAllProducts()
    {
        var response = await client.GetAsync($"{ProductEndpoint}/?PageIndex=0&PageSize=30");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.IsType<List<Product>>(products);
        Assert.Equal(28, products.Count);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytId_ReturnFilteredProducts()
    {
        var existCategorytId = FakeData.CategoryPhone;
        var response = await client.GetAsync($"{ProductEndpoint}?CategoryId={existCategorytId}");

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(5, products.Count);
        Assert.Collection(products,
            item => Assert.Equal("Samsung Galaxy Z Flip", item.Name),
            item => Assert.Equal("Samsung Galaxy Note 20", item.Name),
            item => Assert.Equal("Samsung Galaxy S21", item.Name),
            item => Assert.Equal("Apple iPhone 12 Mini", item.Name),
            item => Assert.Equal("Apple iPhone 12 Pro Max", item.Name));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithNotExistCategorytId_ReturnNotFound()
    {
        var notExistCategorytId = Guid.NewGuid();
        var response = await client.GetAsync($"api/products?CategoryId={notExistCategorytId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandId_ReturnFilteredProducts()
    {
        var existCategorytId = FakeData.CategoryPhone;
        var existBrandId = FakeData.BrandApple;

        var response = await client.GetAsync($"{ProductEndpoint}?CategoryId={existCategorytId}&BrandIds={existBrandId}");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(2, products.Count);
        Assert.Collection(products,
            item => Assert.Equal("Apple iPhone 12 Mini", item.Name),
            item => Assert.Equal("Apple iPhone 12 Pro Max", item.Name));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdAndMinPrice_ReturnFilteredProducts()
    {
        var existCategorytId = FakeData.CategoryPhone;
        var existBrandId = FakeData.BrandApple;
        var minPrice = 700.0;

        var response = await client.GetAsync($"{ProductEndpoint}?CategoryId={existCategorytId}&BrandIds={existBrandId}&MinPrice={minPrice}");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(1, products.Count);
        Assert.Collection(products, item => Assert.Equal("Apple iPhone 12 Pro Max", item.Name));
        Assert.Collection(products, item => Assert.Equal(1099.99M, item.FullPrice));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdAndMaxPrice_ReturnFilteredProducts()
    {
        var existCategorytId = FakeData.CategoryPhone;
        var existBrandId = FakeData.BrandApple;
        var maxPrice = 1000.0;

        var response = await client.GetAsync($"{ProductEndpoint}?CategoryId={existCategorytId}&BrandIds={existBrandId}&MaxPrice={maxPrice}");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(1, products.Count);
        Assert.Collection(products, item => Assert.Equal("Apple iPhone 12 Mini", item.Name));
        Assert.Collection(products, item => Assert.Equal(699.99M, item.FullPrice));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdOrderByPrice_ReturnFilteredProducts()
    {
        var existCategorytId = FakeData.CategoryPhone;
        var existBrandId = FakeData.BrandApple;

        var response = await client.GetAsync($"{ProductEndpoint}?CategoryId={existCategorytId}&BrandIds={existBrandId}&OrderBy=FullPrice&IsDesc=false");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(2, products.Count);
        Assert.Equal("Apple iPhone 12 Mini", products.First().Name);
        Assert.Equal("Apple iPhone 12 Pro Max", products.Last().Name);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdOrderByPriceDesc_ReturnFilteredProducts()
    {
        var existCategorytId = FakeData.CategoryPhone;
        var existBrandId = FakeData.BrandApple;

        var response = await client.GetAsync($"{ProductEndpoint}?CategoryId={existCategorytId}&BrandIds={existBrandId}&OrderBy=FullPrice&IsDesc=true");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(2, products.Count);
        Assert.Equal("Apple iPhone 12 Pro Max", products.First().Name);
        Assert.Equal("Apple iPhone 12 Mini", products.Last().Name);
    }
}