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
}