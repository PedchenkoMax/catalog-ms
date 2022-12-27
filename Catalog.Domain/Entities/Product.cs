
using Catalog.Domain.Objects;

namespace Catalog.Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Brand { get; private set; }
        public bool Active { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Image { get; private set; }
        public int StockQuantity { get; private set; }
        public int CategoryId { get; private set; }
        public int BrandId { get; private set; }
        public Category Category { get; private set; }        

        protected Product() { }

        public Product(
            Guid productId,
            string name,
            string description,
            string brand,
            bool active,
            decimal value,            
            string image,
            int categoryId
            )
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Brand = brand;
            Active = active;
            Value = value;
            CreatedAt = DateTime.Now;
            Image = image;
            CategoryId = categoryId;

            this.Validate();
        }

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public bool HasStock(int quantity)
        {
            return StockQuantity >= quantity;
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

        public void ChangeValue(decimal value)
        {
            AssertionConcern.AssertArgumentGreaterThan(Value, 1, "Product value cannot be less than 1");
            Value = value;
        }

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        private void Validate()
        {
            AssertionConcern.AssertArgumentNotEmpty(Name, "Product name cannot be empty");
            AssertionConcern.AssertArgumentNotEmpty(Description, "Product description cannot be empty");
            AssertionConcern.AssertArgumentNotEquals(CategoryId, 0, "Product category id cannot be empty");
            AssertionConcern.AssertArgumentGreaterThan(Value, 1, "Product value cannot be less than 1");
            AssertionConcern.AssertArgumentNotEmpty(Image, "Product image cannot be empty");
        }
    }


}


