namespace Catalog.Tests.Controllers;

public class FakeData
{
    public static List<BrandEntity> GetFakeBrandsList()
    {
        return new List<BrandEntity>()
        {
            new()
            {
                BrandId = Guid.NewGuid(),
                Name = "Apple",
                Image = "https://blob.com/BrandApple.png"
            },
            new()
            {
                BrandId = Guid.NewGuid(),
                Name = "Dell",
                Image = "https://blob.com/BrandDell.png"
            },
            new()
            {
                BrandId = Guid.NewGuid(),
                Name = "Lenovo",
                Image = "https://blob.com/BrandLenovo.png"
            }
        };
    }

    public static List<CategoryEntity> GetFakeCategoryList()
    {
        return new List<CategoryEntity>()
        {
            new()
            {
                CategoryId =Guid.NewGuid(),
                Name = "Phone",
                Image = "https://blob.com/CategoryPhone.png"
            },
            new()
            {
                CategoryId = Guid.NewGuid(),
                Name = "TV",
                Image = "https://blob.com/CategoryTv.png"
            },
            new()
            {
                CategoryId = Guid.NewGuid(),
                Name = "Notebook",
                Image = "https://blob.com/CategoryNotebook.png"
            }
        };
    }
}



