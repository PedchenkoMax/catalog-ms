using Catalog.API.DTO.Filters;
using Catalog.API.QueryableExtensions;
using Xunit;

namespace Catalog.Tests.UnitTests.QueryableExtensions;

public class ApplyOrderTests
{
    [Fact]
    public void GivenOrderFilterWithOrderByDiscountAndIsDescFalse_ShouldOrderByDiscount()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = false;
        var order = new OrderFilter(OrderByEnum.Discount, isDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderBy(p => p.Discount), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByDiscountAndIsDescTrue_ShouldOrderByDiscountDescending()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = true;
        var order = new OrderFilter(OrderByEnum.Discount, isDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.Discount), result);
    }


    [Fact]
    public void GivenOrderFilterWithOrderByDiscountAndIsDescDefault_ShouldOrderByDiscountDescending() {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = true;
        var order = new OrderFilter(OrderByEnum.Discount, default);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.Discount), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByFullPrice_ShouldOrderByFullPrice()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = false;
        var order = new OrderFilter(OrderByEnum.FullPrice, isDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderBy(p => p.FullPrice), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByFullPriceAndIsDescTrue_ShouldOrderByFullPriceDescending()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = true;
        var order = new OrderFilter(OrderByEnum.FullPrice, isDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.FullPrice), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByFullPriceAndIsDescDefault_ShouldOrderByFullPriceDescending() {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = true;
        var order = new OrderFilter(OrderByEnum.FullPrice, default);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.FullPrice), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByQuantity_ShouldOrderByQuantity()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = false;
        var order = new OrderFilter(OrderByEnum.Quantity, isDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderBy(p => p.Quantity), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByQuantityAndIsDescTrue_ShouldOrderByQuantityDescending()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = true;
        var order = new OrderFilter(OrderByEnum.Quantity, isDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.Quantity), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByQuantityAndIsDescDefault_ShouldOrderByQuantityDescending() {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = true;
        var order = new OrderFilter(OrderByEnum.Quantity, default);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.Quantity), result);
    }

    [Fact]
    public void GivenOrderFilterWithoutOrderBy_ShouldUseDefaultOrderByDiscount()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var order = new OrderFilter(default, default);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.Discount), result);
    }

    [Fact]
    public void GivenOrderFilterWithIsDescFalse_ShouldUseAsserdindOrderByDiscount()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = false;
        var order = new OrderFilter(default, isDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderBy(p => p.Discount), result);
    }

    [Fact]
    public void GivenOrderFilterWithIsDescTrue_ShouldUseDefaultOrderByDiscount() {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var isDesc = true;
        var order = new OrderFilter(default, isDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.Discount), result);
    }

    [Fact]
    public void GivenOrderFilterWithNulls_ShouldOrderByDiscount()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var order = new OrderFilter(null, null);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.Discount), result);
    }    
}