namespace Catalog.Tests.IntegrationTests.Aplication;

public class ProductsIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient client;
    public ProductsIntegrationTests(TestingWebAppFactory<Program> factory)
        => client = factory.CreateClient();

    [Fact]
    public async Task ProductByIdAsync_ProductIdExists_ReturnOkResult()
    {
        Guid existProductId = SeedDataConstants.Phone1;        
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/{existProductId}");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_ProductIdExists_ReturnApplicationJsonUtf8()
    {
        Guid existProductId = SeedDataConstants.Phone1;
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/{existProductId}");

        Assert.Equal("application/json; charset=utf-8; ver=1", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task ProductByIdAsync_ProductIdNotExists_ReturnNotFound()
    {
        Guid notExistProductId = Guid.NewGuid();
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/{notExistProductId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_ProductEmptyId_ReturnBadRequest()
    {
        Guid emptyProductId = Guid.Empty;
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/{emptyProductId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.IsType<List<Product>>(products);
        Assert.Equal(10, products.Count);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnFivePaginedProducts()
    {
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/?PageIndex=0&PageSize=5");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.IsType<List<Product>>(products);
        Assert.Equal(5, products.Count);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnAllProducts()
    {
        HttpResponseMessage response = await client.GetAsync($"api/v1/products/?PageIndex=0&PageSize=30");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.IsType<List<Product>>(products);
        Assert.Equal(28, products.Count);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytId_ReturnFilteredProducts()
    {
        Guid existCategorytId = SeedDataConstants.CategoryPhone;
        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}");

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
        Guid notExistCategorytId = Guid.NewGuid();
        HttpResponseMessage response = await client.GetAsync($"api/products?CategoryId={notExistCategorytId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandId_ReturnFilteredProducts()
    {
        Guid existCategorytId = SeedDataConstants.CategoryPhone;
        Guid existBrandId = SeedDataConstants.BrandApple;

        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}&BrandIds={existBrandId}");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(2, products.Count);
        Assert.Collection(products,
            item => Assert.Equal("Apple iPhone 12 Mini", item.Name),
            item => Assert.Equal("Apple iPhone 12 Pro Max", item.Name));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdAndMinPrice_ReturnFilteredProducts()
    {
        Guid existCategorytId = SeedDataConstants.CategoryPhone;
        Guid existBrandId = SeedDataConstants.BrandApple;
        double minPrice = 700.0;

        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}&BrandIds={existBrandId}&MinPrice={minPrice}");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(1, products.Count);
        Assert.Collection(products, item => Assert.Equal("Apple iPhone 12 Pro Max", item.Name));
        Assert.Collection(products, item => Assert.Equal(1099.99M, item.FullPrice));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdAndMaxPrice_ReturnFilteredProducts()
    {
        Guid existCategorytId = SeedDataConstants.CategoryPhone;
        Guid existBrandId = SeedDataConstants.BrandApple;
        double maxPrice = 1000.0;

        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}&BrandIds={existBrandId}&MaxPrice={maxPrice}");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(1, products.Count);
        Assert.Collection(products, item => Assert.Equal("Apple iPhone 12 Mini", item.Name));
        Assert.Collection(products, item => Assert.Equal(699.99M, item.FullPrice));
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdOrderByPrice_ReturnFilteredProducts()
    {
        Guid existCategorytId = SeedDataConstants.CategoryPhone;
        Guid existBrandId = SeedDataConstants.BrandApple;

        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}&BrandIds={existBrandId}&OrderBy=FullPrice&IsDesc=false");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(2, products.Count);
        Assert.Equal("Apple iPhone 12 Mini", products.First().Name);
        Assert.Equal("Apple iPhone 12 Pro Max", products.Last().Name);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithCategorytIdAndBrandIdOrderByPriceDesc_ReturnFilteredProducts()
    {
        Guid existCategorytId = SeedDataConstants.CategoryPhone;
        Guid existBrandId = SeedDataConstants.BrandApple;

        HttpResponseMessage response = await client.GetAsync($"api/v1/products?CategoryId={existCategorytId}&BrandIds={existBrandId}&OrderBy=FullPrice&IsDesc=true");
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.Equal(2, products.Count);
        Assert.Equal("Apple iPhone 12 Pro Max", products.First().Name);
        Assert.Equal("Apple iPhone 12 Mini", products.Last().Name);
    }
}