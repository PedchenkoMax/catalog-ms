using Catalog.Domain.Exceptions;

namespace Catalog.Domain.Entities;

public class Product
{
    public Guid ProductId { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string Image { get; private set; }
    public int StockQuantity { get; private set; }
    public bool IsActive { get; private set; }
    public Category Category { get; private set; }

    protected Product()
    {
    }

    public Product(
        Guid productId,
        Guid categoryId,
        bool isActive,
        string name,
        string description,
        decimal price,
        string image
    )
    {
        ProductId = productId;
        CategoryId = categoryId;
        IsActive = isActive;
        Name = name;
        Description = description;
        Price = price;
        Image = image;

        this.Validate();
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public bool HasStock()
    {
        return StockQuantity > 0;
    }

    public void ChangeCategory(Category category)
    {
        Category = category;
        CategoryId = category.Id;
    }

    public void ChangeName(string name)
    {
        AssertionConcern.AssertArgumentNotEmpty(name, "Product name cannot be empty");
        AssertionConcern.AssertArgumentLength(name, 60, "Product name cannot be longer than 60 characters");
        Name = name;
    }

    public void ChangeDescription(string description)
    {
        AssertionConcern.AssertArgumentNotEmpty(description, "Product description cannot be empty");
        AssertionConcern.AssertArgumentLength(description, 4000, "Product description cannot be longer than 4000 characters");
        Description = description;
    }

    public void ChangePrice(decimal price)
    {
        AssertionConcern.AssertArgumentGreaterThan(price, 0.01M, "Product price cannot be less than 0.01");
        Price = price;
    }

    public void ChangeImage(string image)
    {
        AssertionConcern.AssertArgumentNotEmpty(image, "Product image cannot be empty");
        Image = image;
    }

    private void Validate()
    {
        AssertionConcern.AssertArgumentNotEmpty(Name, "Product name cannot be empty");
        AssertionConcern.AssertArgumentLength(Name, 60, "Product name cannot be longer than 60 characters");
        AssertionConcern.AssertArgumentNotEmpty(Description, "Product description cannot be empty");
        AssertionConcern.AssertArgumentLength(Description, 4000, "Product description cannot be longer than 4000 characters");
        AssertionConcern.AssertArgumentNotEmpty(CategoryId,"Product category id cannot be empty");
        AssertionConcern.AssertArgumentGreaterThan(Price, 0.01M, "Product price cannot be less than 0.01");
        AssertionConcern.AssertArgumentNotEmpty(Image, "Product image cannot be empty");
    }
}