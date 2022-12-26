using System;

namespace Catalog.Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Image { get; private set; }
        public int StockQuantity { get; private set; }        

        protected Product() { }

        public Product(
            string name,
            string description,
            bool active,
            decimal value,
            Guid productId,
            string image
            )
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Active = active;
            Value = value;
            CreatedAt = DateTime.Now;
            Image = image;           
        }
    }

}
