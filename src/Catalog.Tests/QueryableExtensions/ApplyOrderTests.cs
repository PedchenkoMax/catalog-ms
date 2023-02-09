namespace Catalog.Tests.QueryableExtensions;

public class ApplyOrderTests
{
    [Fact]
    public void GivenOrderFilterWithOrderByDiscountAndIsDescFalse_ShouldOrderByDiscount()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var IsDesc = false;
        var order = new OrderFilter(OrderByEnum.Discount, IsDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderBy(p => p.Discount), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByDiscountAndIsDescTrue_ShouldOrderByDiscountDescending()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var IsDesc = true;
        var order = new OrderFilter(OrderByEnum.Discount, IsDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.Discount), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByFullPrice_ShouldOrderByFullPrice()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var IsDesc = false;
        var order = new OrderFilter(OrderByEnum.FullPrice, IsDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderBy(p => p.FullPrice), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByFullPriceAndIsDescTrue_ShouldOrderByFullPriceDescending()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var IsDesc = true;
        var order = new OrderFilter(OrderByEnum.FullPrice, IsDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderByDescending(p => p.FullPrice), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByQuantity_ShouldOrderByQuantity()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var IsDesc = false;
        var order = new OrderFilter(OrderByEnum.Quantity, IsDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderBy(p => p.Quantity), result);
    }

    [Fact]
    public void GivenOrderFilterWithOrderByQuantityAndIsDescTrue_ShouldOrderByQuantityDescending()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var IsDesc = true;
        var order = new OrderFilter(OrderByEnum.Quantity, IsDesc);

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
    public void GivenOrderFilterWithIsDescFalse_ShouldUseDefaultOrderByDiscount()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var IsDesc = false;
        var order = new OrderFilter(default, IsDesc);

        var result = products.ApplyOrder(order);

        Assert.Equal(products.OrderBy(p => p.Discount), result);
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