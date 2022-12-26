
namespace Catalog.Domain.Entities
{
    public class Category
    {
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
        }
      
    }
}
