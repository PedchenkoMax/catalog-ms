using System.Net;
using System.Net.Http.Json;
using Catalog.API.DTO;
using Catalog.Tests.Seed;
using Xunit;

namespace Catalog.Tests.IntegrationTests.Aplication;

public class ProductsIntegrationTests : IClassFixture<IntegrationTestsFixture> 
{
    private readonly HttpClient client;
    public ProductsIntegrationTests(IntegrationTestsFixture fixture)
        => client = fixture.ApiClient;

    [Fact]
    public async Task ProductByIdAsync_WithExistsProductId_ReturnOkResult()
    {
        Guid existProductId = FakeData.Phone1;        
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/{existProductId}");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WithExistsProuctId_ReturnApplicationJsonUtf8()
    {
        Guid existProductId = FakeData.Phone1;
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/{existProductId}");

        Assert.Equal("application/json; charset=utf-8; ver=1", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task ProductByIdAsync_WithNotExistsProductId_ReturnNotFound()
    {
        Guid notExistProductId = Guid.NewGuid();
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/{notExistProductId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WithEmptyProductId_ReturnBadRequest()
    {
        Guid emptyProductId = Guid.Empty;
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/{emptyProductId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WithIphoneProductId_ReturnIphoneProduct() {
        Guid iphoneProductId = FakeData.Phone1;
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/{iphoneProductId}");

        var product = await response.Content.ReadFromJsonAsync<Product>();

        Assert.IsType<Product>(product);
        Assert.Equal("Apple iPhone 12 Pro Max", product.Name);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnOkResult()
    {        
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8; ver=1", response.Content.Headers.ContentType.ToString());       
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnDefaultPaginedTenProducts()
    {
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<PaginatedList<Product>>();

        Assert.IsType<PaginatedList<Product>>(products);
        Assert.Equal(10, products.Items.Count());
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnFivePaginedProducts()
    {
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/?PageIndex=0&PageSize=5");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<PaginatedList<Product>>();

        Assert.IsType<PaginatedList<Product>>(products);
        Assert.Equal(5, products.Items.Count());
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnAllProducts()
    {
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/?PageIndex=0&PageSize=30");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<PaginatedList<Product>>();

        Assert.IsType<PaginatedList<Product>>(products);
        Assert.Equal(28, products.Items.Count());
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytId_ReturnFilteredProducts()
    {
        Guid existCategorytId = FakeData.CategoryPhone;
        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}");

        var products = await response.Content.ReadFromJsonAsync<PaginatedList<Product>>();

        Assert.Equal(5, products.Items.Count());
        Assert.Collection(products.Items,
            item => Assert.Equal("Samsung Galaxy Z Flip", item.Name),
            item => Assert.Equal("Samsung Galaxy Note 20", item.Name),
            item => Assert.Equal("Samsung Galaxy S21", item.Name),
            item => Assert.Equal("Apple iPhone 12 Mini", item.Name),
            item => Assert.Equal("Apple iPhone 12 Pro Max", item.Name));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithNotExistCategorytId_ReturnNotFound()
    {
        Guid notExistCategorytId = Guid.NewGuid();
        HttpResponseMessage response = await client.GetAsync($"api/products?CategoryId={notExistCategorytId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandId_ReturnFilteredProducts()
    {
        Guid existCategorytId = FakeData.CategoryPhone;
        Guid existBrandId = FakeData.BrandApple;

        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}&BrandIds={existBrandId}");
        var products = await response.Content.ReadFromJsonAsync<PaginatedList<Product>>();

        Assert.Equal(2, products.Items.Count());
        Assert.Collection(products.Items,
            item => Assert.Equal("Apple iPhone 12 Mini", item.Name),
            item => Assert.Equal("Apple iPhone 12 Pro Max", item.Name));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdAndMinPrice_ReturnFilteredProducts()
    {
        Guid existCategorytId = FakeData.CategoryPhone;
        Guid existBrandId = FakeData.BrandApple;
        double minPrice = 700.0;

        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}&BrandIds={existBrandId}&MinPrice={minPrice}");
        var products = await response.Content.ReadFromJsonAsync<PaginatedList<Product>>();

        Assert.Equal(1, products.Items.Count());
        Assert.Collection(products.Items, item => Assert.Equal("Apple iPhone 12 Pro Max", item.Name));
        Assert.Collection(products.Items, item => Assert.Equal(1099.99M, item.FullPrice));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdAndMaxPrice_ReturnFilteredProducts()
    {
        Guid existCategorytId = FakeData.CategoryPhone;
        Guid existBrandId = FakeData.BrandApple;
        double maxPrice = 1000.0;

        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}&BrandIds={existBrandId}&MaxPrice={maxPrice}");
        var products = await response.Content.ReadFromJsonAsync<PaginatedList<Product>>();

        Assert.Equal(1, products.Items.Count());
        Assert.Collection(products.Items, item => Assert.Equal("Apple iPhone 12 Mini", item.Name));
        Assert.Collection(products.Items, item => Assert.Equal(699.99M, item.FullPrice));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdOrderByPrice_ReturnFilteredProducts()
    {
        Guid existCategorytId = FakeData.CategoryPhone;
        Guid existBrandId = FakeData.BrandApple;

        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}&BrandIds={existBrandId}&OrderBy=FullPrice&IsDesc=false");
        var products = await response.Content.ReadFromJsonAsync<PaginatedList<Product>>();

        Assert.Equal(2, products.Items.Count());
        Assert.Equal("Apple iPhone 12 Mini", products.Items.First().Name);
        Assert.Equal("Apple iPhone 12 Pro Max", products.Items.Last().Name);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdOrderByPriceDesc_ReturnFilteredProducts()
    {
        Guid existCategorytId = FakeData.CategoryPhone;
        Guid existBrandId = FakeData.BrandApple;

        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}&BrandIds={existBrandId}&OrderBy=FullPrice&IsDesc=true");
        var products = await response.Content.ReadFromJsonAsync<PaginatedList<Product>>();

        Assert.Equal(2, products.Items.Count());
        Assert.Equal("Apple iPhone 12 Pro Max", products.Items.First().Name);
        Assert.Equal("Apple iPhone 12 Mini", products.Items.Last().Name);
    }
}