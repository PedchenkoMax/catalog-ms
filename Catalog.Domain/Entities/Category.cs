
using Catalog.Domain.Objects;

namespace Catalog.Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        
        
        public ICollection<Product> Products { get; set; }


        protected Category() { }

        public Category(string name, string description, string image)
        {
            Name = name;
            Description = description;
            Image = image;

            this.Validate();
        }        

        private void Validate()
        {
            AssertionConcern.AssertArgumentNotEmpty(Name, "Category name cannot be empty");
            AssertionConcern.AssertArgumentNotEmpty(Description, "Category Description cannot be empty");            
        }

    }
}
