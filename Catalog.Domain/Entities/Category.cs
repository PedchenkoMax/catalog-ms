using Catalog.Domain.Exceptions;

namespace Catalog.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Image { get; private set; }
    public ICollection<Product> Products { get; set; }

    protected Category()
    {
    }

    public Category(string name, string description, string image)
    {
        Name = name;
        Description = description;
        Image = image;

        this.Validate();
    }

        
    public void ChangeName(string name)
    {
        AssertionConcern.AssertArgumentNotEmpty(name, "Category name cannot be empty");
        AssertionConcern.AssertArgumentLength(name, 30, "Category name cannot be longer than 30 characters");
        Name = name;
    }

    public void ChangeDescription(string description)
    {
        AssertionConcern.AssertArgumentNotEmpty(description, "Category description cannot be empty");
        AssertionConcern.AssertArgumentLength(description, 2000, "Category name cannot be longer than 2000 characters");
        Description = description;
    }

    public void ChangeImage(string image)
    {
        AssertionConcern.AssertArgumentNotEmpty(image, "Product image cannot be empty");
        Image = image;
    }
        
    private void Validate()
    {
        AssertionConcern.AssertArgumentNotEmpty(Name, "Category name cannot be empty");
        AssertionConcern.AssertArgumentLength(Name, 30, "Category name cannot be longer than 30 characters");
        AssertionConcern.AssertArgumentNotEmpty(Description, "Category Description cannot be empty");
        AssertionConcern.AssertArgumentLength(Description, 2000, "Category name cannot be longer than 2000 characters");
        AssertionConcern.AssertArgumentNotEmpty(Image, "Product image cannot be empty");
    }
}