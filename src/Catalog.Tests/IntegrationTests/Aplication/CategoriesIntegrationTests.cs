using System.Net;
using System.Net.Http.Json;
using Catalog.API.DTO;
using Catalog.Tests.UnitTests;
using Xunit;

namespace Catalog.Tests.IntegrationTests.Aplication;

public class CategoriesIntegrationTests : IClassFixture<ApplicationFixture>
{
    private const string CategoryEndpoint = "api/v1/categories";
    
    private readonly HttpClient client;
    public CategoriesIntegrationTests(ApplicationFixture applicationFixture)
        => client = applicationFixture.Factory.CreateClient();

    [Fact]
    public async Task GetCategoriesAsync_WithData_ReturnOkResult()
    {
        var response = await client.GetAsync(CategoryEndpoint);
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCategoriesAsync_WithData_ReturnApplicationJsonUtf8()
    {
        var response = await client.GetAsync(CategoryEndpoint);

        Assert.Equal("application/json; charset=utf-8; ver=1", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task GetCategoriesAsync_WithData_ReturnAllBrands()
    {
        var response = await client.GetAsync(CategoryEndpoint);
        response.EnsureSuccessStatusCode();

        var categories = await response.Content.ReadFromJsonAsync<List<Category>>();

        Assert.Equal(3, categories.Count);
        Assert.Collection(categories,
            item => Assert.Equal("Phone", item.Name),
            item => Assert.Equal("TV", item.Name),
            item => Assert.Equal("Notebook", item.Name));
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WithExistCategoryId_ReturnOkResult()
    {
        var existCategoryId = FakeData.CategoryPhone;
        var response = await client.GetAsync($"{CategoryEndpoint}/{existCategoryId}");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WithNotExistCategoryId_ReturnNotFound() 
    {
        var notExistCategoryId = Guid.NewGuid();
        var response = await client.GetAsync($"{CategoryEndpoint}/{notExistCategoryId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WithEmptyCategoryId_ReturnBadRequest()
    {
        var emptyCategoryId = Guid.Empty;
        var response = await client.GetAsync($"{CategoryEndpoint}/{emptyCategoryId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public async Task GetCategoryByIdAsync_WithPhoneCategoryId_ReturnPhoneCategory() 
    {
        var phoneCategoryId = FakeData.BrandApple;
        var response = await client.GetAsync($"{CategoryEndpoint}/{phoneCategoryId}");

        var category = await response.Content.ReadFromJsonAsync<Category>();

        Assert.IsType<Category>(category);
        Assert.Equal("Phone", category.Name);
    }
}