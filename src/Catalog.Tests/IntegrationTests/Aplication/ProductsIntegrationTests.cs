﻿namespace Catalog.Tests.IntegrationTests.Aplication;

public class ProductsIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient client;
    public ProductsIntegrationTests(TestingWebAppFactory<Program> factory)
        => client = factory.CreateClient();

    [Fact]
    public async Task ProductByIdAsync_ProductIdExists_ReturnOkResult()
    {
        Guid existProductId = SeedDataConstants.Phone1;        
        HttpResponseMessage response = await client.GetAsync($"api/products/{existProductId}");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_ProductIdExists_ReturnApplicationJsonUtf8()
    {
        Guid existProductId = SeedDataConstants.Phone1;
        HttpResponseMessage response = await client.GetAsync($"api/products/{existProductId}");

        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task ProductByIdAsync_ProductIdNotExists_ReturnNotFound()
    {
        Guid notExistProductId = Guid.NewGuid();
        HttpResponseMessage response = await client.GetAsync($"api/products/{notExistProductId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_ProductEmptyId_ReturnBadRequest()
    {
        Guid emptyProductId = Guid.Empty;
        HttpResponseMessage response = await client.GetAsync($"api/products/{emptyProductId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnOkResult()
    {        
        HttpResponseMessage response = await client.GetAsync($"api/products/");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());       
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnDefaultPaginedTenProducts()
    {
        HttpResponseMessage response = await client.GetAsync($"api/products/");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.IsType<List<Product>>(products);
        Assert.Equal(10, products.Count);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WithoutParametersWithData_ReturnFivePaginedProducts()
    {
        HttpResponseMessage response = await client.GetAsync($"api/products/?PageIndex=0&PageSize=5");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();

        Assert.IsType<List<Product>>(products);
        Assert.Equal(5, products.Count);
    }    
}