namespace Catalog.Tests.Controllers;

public class FakeData
{
    public static List<BrandEntity> GetFakeBrandsList()
    {
        return new List<BrandEntity>()
        {
            new()
            {
                BrandId = SeedDataConstants.BrandApple,
                Name = "Apple",
                Image = "https://blob.com/BrandApple.png"
            },
            new()
            {
                BrandId = SeedDataConstants.BrandSamsung,
                Name = "Samsung",
                Image = "https://blob.com/BrandSamsung.png"
            },
            new()
            {
                BrandId = SeedDataConstants.BrandLg,
                Name = "Lg",
                Image = "https://blob.com/BrandLg.png"
            },
            new()
            {
                BrandId = SeedDataConstants.BrandLenovo,
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
                CategoryId = SeedDataConstants.CategoryPhone,
                Name = "Phone",
                Image = "https://blob.com/CategoryPhone.png"
            },
            new()
            {
                CategoryId = SeedDataConstants.CategoryTv,
                Name = "TV",
                Image = "https://blob.com/CategoryTv.png"
            },
            new()
            {
                CategoryId = SeedDataConstants.CategoryNotebook,
                Name = "Notebook",
                Image = "https://blob.com/CategoryNotebook.png"
            }
        };
    }

    public static List<ProductEntity> GetFakeProductsList()
    {
        return new List<ProductEntity>()
        {
            new ProductEntity
            {
                ProductId = SeedDataConstants.Phone1,
                Name = "Apple iPhone 12 Pro Max",                
                FullPrice = 1099.99M,
                Discount = 0.0M,
                Quantity = 15,             
                BrandId = SeedDataConstants.BrandApple,
                CategoryId = SeedDataConstants.CategoryPhone
            },
            new ProductEntity
            {
                ProductId = SeedDataConstants.Phone2,
                Name = "Apple iPhone 11",                
                FullPrice = 699.99M,
                Discount = 0.0M,
                Quantity = 20,               
                BrandId = SeedDataConstants.BrandApple,
                CategoryId = SeedDataConstants.CategoryPhone
            },
            new ProductEntity
            {
                ProductId = SeedDataConstants.Phone3,
                Name = "Samsung Galaxy S21",               
                FullPrice = 899.99M,
                Discount = 0.0M,
                Quantity = 20,               
                BrandId = SeedDataConstants.BrandSamsung,
                CategoryId = SeedDataConstants.CategoryPhone
            },           
            new ProductEntity
            {
                ProductId = SeedDataConstants.Tv1,
                Name = "Samsung QN55Q70T 55-Inch 4K UHD Smart TV",                
                FullPrice = 799.99M,
                Discount = 99.99M,
                Quantity = 10,               
                CategoryId = SeedDataConstants.CategoryTv,
                BrandId = SeedDataConstants.BrandSamsung
            },
            new ProductEntity
            {
                ProductId = SeedDataConstants.Tv2,
                Name = "Samsung QN82Q70T 82-Inch 4K UHD Smart TV",                
                FullPrice = 2199.99M,
                Discount = 0.0M,
                Quantity = 5,                
                CategoryId = SeedDataConstants.CategoryTv,
                BrandId = SeedDataConstants.BrandSamsung
            },            
            new ProductEntity
            {
                ProductId = SeedDataConstants.Tv4,
                Name = "LG OLED65CXP 65-Inch 4K UHD Smart TV",               
                FullPrice = 1999.99M,
                Discount = 0.0M,
                Quantity = 0,                
                CategoryId = SeedDataConstants.CategoryTv,
                BrandId = SeedDataConstants.BrandLg
            },           
            new ProductEntity
            {
                ProductId = SeedDataConstants.Notebook1,
                Name = "Lenovo ThinkPad X1 Carbon 14-Inch Laptop",                
                FullPrice = 999.99M,
                Discount = 0.0M,
                Quantity = 10,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },
            new ProductEntity
            {
                ProductId = SeedDataConstants.Notebook2,
                Name = "Lenovo Ideapad 330 15-Inch Laptop",                
                FullPrice = 499.99M,
                Discount = 199.99M,
                Quantity = 20,              
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },            
            new ProductEntity
            {
                ProductId = SeedDataConstants.Notebook10,
                Name = "Apple MacBook Pro 16-inch",                
                FullPrice = 2499.99M,
                Discount = 500.0M,
                Quantity = 0,               
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                ProductId = SeedDataConstants.Notebook11,
                Name = "Apple MacBook Air 13-inch",                
                FullPrice = 999.99M,
                Discount = 100.0M,
                Quantity = 10,               
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                ProductId = SeedDataConstants.Notebook12,
                Name = "Apple MacBook Pro 15-inch",                
                FullPrice = 2799.99M,
                Discount = 700.0M,
                Quantity = 5,             
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                ProductId = SeedDataConstants.Notebook13,
                Name = "Apple MacBook 12-inch",                
                FullPrice = 799.99M,
                Discount = 50.0M,
                Quantity = 20,               
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            }            
        };
    }
}



