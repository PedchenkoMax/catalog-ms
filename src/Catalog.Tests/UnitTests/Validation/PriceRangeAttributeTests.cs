namespace Catalog.Tests.UnitTests.Validation;

public class PriceRangeAttributeTests
{
    private class TestModel
    {
        [PriceRange]
        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }
    }

    [Fact]
    public void MinPriceLessThanZero_ReturnsValidationError()
    {
        var model = new TestModel
        {
            MinPrice = -1,
            MaxPrice = 10
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
        Assert.Equal("MinPrice must not be less than zero.", results[0].ErrorMessage);
    }

    [Fact]
    public void MaxPriceLessThanZero_ReturnsValidationError()
    {
        var model = new TestModel
        {
            MinPrice = 1,
            MaxPrice = -10
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
        Assert.Equal("MaxPrice must not be less than zero.", results[0].ErrorMessage);
    }

    [Fact]
    public void MinPriceGreaterThanOrEqualToMaxPrice_ReturnsValidationError()
    {
        var model = new TestModel
        {
            MinPrice = 10,
            MaxPrice = 10
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
        Assert.Equal("MinPrice must be lower than MaxPrice.", results[0].ErrorMessage);
    }

    [Fact]
    public void MinPriceLessThanMaxPrice_ReturnsSuccess()
    {
        var model = new TestModel
        {
            MinPrice = 1,
            MaxPrice = 10
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }
}