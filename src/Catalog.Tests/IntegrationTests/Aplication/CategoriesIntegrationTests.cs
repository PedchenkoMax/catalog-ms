using System.Net;
using System.Net.Http.Json;
using Catalog.API.DTO;
using Catalog.Tests.UnitTests;
using Xunit;

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
    public async Task GetCategoriesAsync_WithData_ReturnAllBrands()
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

    [Fact]
    public async Task GetCategoryByIdAsync_WithExistCategoryId_ReturnOkResult()
    {
        Guid existCategoryId = FakeData.CategoryPhone;
        var response = await client.GetAsync($"/api/v1/categories/{existCategoryId}");
        response.EnsureSuccessStatusCode();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WithNotExistCategoryId_ReturnNotFound() 
    {
        Guid notExistCategoryId = Guid.NewGuid();
        var response = await client.GetAsync($"/api/v1/categories/{notExistCategoryId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WithEmptyCategoryId_ReturnBadRequest()
    {
        Guid emptyCategoryId = Guid.Empty;
        var response = await client.GetAsync($"/api/v1/categories/{emptyCategoryId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public async Task GetCategoryByIdAsync_WithPhoneCategoryId_ReturnPhoneCategory() 
    {
        Guid phoneCategoryId = FakeData.BrandApple;
        var response = await client.GetAsync($"/api/v1/categories/{phoneCategoryId}");

        var category = await response.Content.ReadFromJsonAsync<Category>();

        Assert.IsType<Category>(category);
        Assert.Equal("Phone", category.Name);
    }
}