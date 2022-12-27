using Catalog.Domain.Objects;

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
        AssertionConcern.AssertArgumentNotEmpty(name, "Product description cannot be empty");
        Name = name;
    }

    public void ChangeDescription(string description)
    {
        AssertionConcern.AssertArgumentNotEmpty(description, "Product description cannot be empty");
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
        AssertionConcern.AssertArgumentNotEmpty(Description, "Category Description cannot be empty");
        AssertionConcern.AssertArgumentNotEmpty(Image, "Product image cannot be empty");
    }
}