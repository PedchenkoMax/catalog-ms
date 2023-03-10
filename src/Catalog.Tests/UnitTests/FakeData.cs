namespace Catalog.Tests.UnitTests;

public class FakeData
{
    public static void Seed(CatalogContext dbContext) 
    {
        dbContext.Brands.AddRange(GetFakeBrandsList());
        dbContext.Categories.AddRange(GetFakeCategoryList());
        dbContext.Products.AddRange(GetFakeProductsList());

        dbContext.SaveChanges();
    }

    public static List<BrandEntity> GetFakeBrandsList()
    {
        return new List<BrandEntity>()
        {
           new BrandEntity
            {
                Id = BrandApple,
                Name = "Apple",
                Image = "https://blob.com/BrandApple.png"
            },
            new BrandEntity
            {
                Id = BrandSamsung,
                Name = "Samsung",
                Image = "https://blob.com/BrandSamsung.png"
            },
            new BrandEntity
            {
                Id = BrandLg,
                Name = "Lg",
                Image = "https://blob.com/BrandLg.png"
            },
            new BrandEntity
            {
                Id = BrandLenovo,
                Name = "Lenovo",
                Image = "https://blob.com/BrandLenovo.png"
            }
        };
    }

    public static List<CategoryEntity> GetFakeCategoryList()
    {
        return new List<CategoryEntity>()
        {
            new CategoryEntity
            {
                Id = CategoryPhone,
                Name = "Phone",
                Image = "https://blob.com/CategoryPhone.png"
            },
            new CategoryEntity
            {
                Id = CategoryTv,
                Name = "TV",
                Image = "https://blob.com/CategoryTv.png"
            },
            new CategoryEntity
            {
                Id = CategoryNotebook,
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
                Id = Phone1,
                Name = "Apple iPhone 12 Pro Max",
                Description = "The Apple iPhone 12 Pro Max features a 6.7-inch Super Retina XDR display",                            
                FullPrice = 1099.99M,
                Discount = 0.0M,
                Quantity = 15,
                IsActive = true,
                BrandId = BrandApple,
                CategoryId = CategoryPhone
            },
            new ProductEntity
            {
                Id = Phone2,
                Name = "Apple iPhone 12 Mini",
                Description = "The Apple iPhone 12 Mini features a 5.4-inch Super Retina XDR display",                             
                FullPrice = 699.99M,
                Discount = 0.0M,
                Quantity = 20,
                IsActive = true,
                BrandId = BrandApple,
                CategoryId = CategoryPhone
            },
            new ProductEntity
            {
                Id = Phone3,
                Name = "Samsung Galaxy S21",
                Description = "The Samsung Galaxy S21 features a 6.2-inch AMOLED display",                             
                FullPrice = 899.99M,
                Discount = 0.0M,
                Quantity = 20,
                IsActive = true,
                BrandId = BrandSamsung,
                CategoryId = CategoryPhone
            },
            new ProductEntity
            {
                Id = Phone4,
                Name = "Samsung Galaxy Note 20",
                Description = "The Samsung Galaxy Note 20 features a 6.7-inch AMOLED display",
                FullPrice = 799.99M,
                Discount = 49.990M,
                Quantity = 15,
                IsActive = true,
                BrandId = BrandSamsung,
                CategoryId = CategoryPhone
            },
            new ProductEntity
            {
                Id = Phone5,
                Name = "Samsung Galaxy Z Flip",
                Description = "The Samsung Galaxy Z Flip features a 6.7-inch foldable AMOLED display",                
                FullPrice = 999.99M,
                Discount = 100.0M,
                Quantity = 0,
                IsActive = false,
                BrandId = BrandSamsung,
                CategoryId = CategoryPhone
            },
            new ProductEntity
            {
                Id = Tv1,
                Name = "Samsung QN55Q70T 55-Inch 4K UHD Smart TV",
                Description = "The Samsung QN55Q70T features a 55-inch 4K UHD display",
                FullPrice = 799.99M,
                Discount = 99.99M,
                Quantity = 10,
                IsActive = true,
                CategoryId = CategoryTv,
                BrandId = BrandSamsung
            },
            new ProductEntity
            {
                Id = Tv2,
                Name = "Samsung QN82Q70T 82-Inch 4K UHD Smart TV",
                Description = "The Samsung QN82Q70T features an 82-inch 4K UHD display, Quantum Processor 4K",             
                FullPrice = 2199.99M,
                Discount = 0.0M,
                Quantity = 5,
                IsActive = true,
                CategoryId = CategoryTv,
                BrandId = BrandSamsung
            },
            new ProductEntity
            {
                Id = Tv3,
                Name = "Samsung QN85Q900RB 85-Inch 8K UHD Smart TV",
                Description = "The Samsung QN85Q900RB features an 85-inch 8K UHD display, Quantum Processor 8K",                
                FullPrice = 3999.99M,
                Discount = 399.99M,
                Quantity = 13,
                IsActive = true,
                CategoryId = CategoryTv,
                BrandId = BrandSamsung
            },
            new ProductEntity
            {
                Id = Tv4,
                Name = "LG OLED65CXP 65-Inch 4K UHD Smart TV",
                Description = "The LG OLED65CXP features a 65-inch 4K UHD OLED display",                
                FullPrice = 1999.99M,
                Discount = 0.0M,
                Quantity = 0,
                IsActive = false,
                CategoryId = CategoryTv,
                BrandId = BrandLg
            },
            new ProductEntity
            {
                Id = Tv5,
                Name = "LG OLED77GX 77-Inch 4K UHD Smart TV",
                Description = "The LG OLED77GX features a 77-inch 4K UHD OLED display",                
                FullPrice = 4999.99M,
                Discount = 999.99M,
                Quantity = 1,
                IsActive = true,
                CategoryId = CategoryTv,
                BrandId = BrandLg
            },
            new ProductEntity
            {
                Id = Notebook1,
                Name = "Lenovo ThinkPad X1 Carbon 14-Inch Laptop",
                Description = "The Lenovo ThinkPad X1 Carbon features a 14-inch full HD display",                
                FullPrice = 999.99M,
                Discount = 0.0M,
                Quantity = 10,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandLenovo
            },
            new ProductEntity
            {
                Id = Notebook2,
                Name = "Lenovo Ideapad 330 15-Inch Laptop",
                Description = "The Lenovo Ideapad 330 features a 15-inch full HD display",                
                FullPrice = 499.99M,
                Discount = 199.99M,
                Quantity = 20,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandLenovo
            },
            new ProductEntity
            {
                Id = Notebook3,
                Name = "Lenovo Legion Y540 15-Inch Gaming Laptop",
                Description = "The Lenovo Legion Y540 features a 15-inch full HD display",                
                FullPrice = 1299.99M,
                Discount = 0.0M,
                Quantity = 15,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandLenovo
            },
            new ProductEntity
            {
                Id = Notebook4,
                Name = "Lenovo Yoga C930 2-In-1 Laptop",
                Description = "The Lenovo Yoga C930 features a 13.9-inch full HD display",               
                FullPrice = 1299.99M,
                Discount = 100.0M,
                Quantity = 10,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandLenovo
            },
            new ProductEntity
            {
                Id = Notebook5,
                Name = "Lenovo ThinkPad X1 Yoga",
                Description = "The Lenovo ThinkPad X1 Yoga features a 14-inch Full HD IPS touch screen",                
                FullPrice = 1599.99M,
                Discount = 0.0M,
                Quantity = 20,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandLenovo
            },
            new ProductEntity
            {
                Id = Notebook6,
                Name = "Lenovo Legion 5",
                Description = "The Lenovo Legion 5 features a 15.6-inch Full HD IPS display",                
                FullPrice = 999.99M,
                Discount = 0.0M,
                Quantity = 10,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandLenovo
            },
            new ProductEntity
            {
                Id = Notebook7,
                Name = "Lenovo IdeaPad 5",
                Description = "The Lenovo IdeaPad 5 features a 14-inch Full HD IPS display",                
                FullPrice = 699.99M,
                Discount = 100.0M,
                Quantity = 15,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandLenovo
            },
            new ProductEntity
            {
                Id = Notebook8,
                Name = "Lenovo Yoga 9i",
                Description = "The Lenovo Yoga 9i features a 14-inch Full HD IPS touch screen",                 
                FullPrice = 1799.99M,
                Discount = 200.0M,
                Quantity = 0,
                IsActive = false,
                CategoryId = CategoryNotebook,
                BrandId = BrandLenovo
            },
            new ProductEntity
            {
                Id = Notebook9,
                Name = "Lenovo ThinkPad P1",
                Description = "The Lenovo ThinkPad P1 features a 15.6-inch Full HD IPS display",                 
                FullPrice = 2199.99M,
                Discount = 300.0M,
                Quantity = 3,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandLenovo
            },
            new ProductEntity
            {
                Id = Notebook10,
                Name = "Apple MacBook Pro 16-inch",
                Description = "The Apple MacBook Pro 16-inch features a 16-inch Retina Display",                
                FullPrice = 2499.99M,
                Discount = 500.0M,
                Quantity = 0,
                IsActive = false,
                CategoryId = CategoryNotebook,
                BrandId = BrandApple
            },
            new ProductEntity
            {
                Id = Notebook11,
                Name = "Apple MacBook Air 13-inch",
                Description = "The Apple MacBook Air 13-inch features a 13.3-inch Retina Display",                
                FullPrice = 999.99M,
                Discount = 100.0M,
                Quantity = 10,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandApple
            },
            new ProductEntity
            {
                Id = Notebook12,
                Name = "Apple MacBook Pro 15-inch",
                Description = "The Apple MacBook Pro 15-inch features a 15.4-inch Retina Display",                
                FullPrice = 2799.99M,
                Discount = 700.0M,
                Quantity = 5,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandApple
            },
            new ProductEntity
            {
                Id = Notebook13,
                Name = "Apple MacBook 12-inch",
                Description = "The Apple MacBook 12-inch features a 12-inch Retina Display",                
                FullPrice = 799.99M,
                Discount = 50.0M,
                Quantity = 20,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandApple
            },
            new ProductEntity
            {
                Id = Notebook14,
                Name = "Apple MacBook Pro 13-inch",
                Description = "The Apple MacBook Pro 13-inch features a 13.3-inch Retina Display",                
                FullPrice = 1499.99M,
                Discount = 250.0M,
                Quantity = 8,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandApple
            },
            new ProductEntity
            {
                Id = Notebook15,
                Name = "Apple MacBook Pro 16-inch",
                Description = "The Apple MacBook Pro 16-inch features a 16-inch Retina display",                
                FullPrice = 2499.99M,
                Discount = 500.0M,
                Quantity = 2,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandApple
            },
            new ProductEntity
            {
                Id = Notebook16,
                Name = "Apple MacBook 12-inch",
                Description = "The Apple MacBook 12-inch features a 12-inch Retina display",                
                FullPrice = 799.99M,
                Discount = 100.0M,
                Quantity = 6,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandApple
            },
            new ProductEntity
            {
                Id = Notebook17,
                Name = "Apple MacBook Pro Touch Bar",
                Description = "The Apple MacBook Pro Touch Bar features a 13.3-inch Retina display",                
                FullPrice = 1299.99M,
                Discount = 150.0M,
                Quantity = 4,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandApple
            },
            new ProductEntity
            {
                Id = Notebook18,
                Name = "Apple MacBook Pro Touch Bar 15-inch",
                Description = "The Apple MacBook Pro Touch Bar 15-inch features a 15-inch Retina display",                
                FullPrice = 1799.99M,
                Discount = 250.0M,
                Quantity = 3,
                IsActive = true,
                CategoryId = CategoryNotebook,
                BrandId = BrandApple
            }
        };
    }

    public static readonly Guid BrandApple = Guid.Parse("d942706b-e4e2-48f9-bbdc-b022816471f0");
    public static readonly Guid BrandSamsung = Guid.Parse("2cd4b9a0-f70d-476d-a3cc-908da43f93c4");
    public static readonly Guid BrandLg = Guid.Parse("1F5AD630-32CD-42E9-8218-E26F1D375C0C");
    public static readonly Guid BrandLenovo = Guid.Parse("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2");

    public static readonly Guid CategoryPhone = Guid.Parse("5e7274ad-3132-4ad7-be36-38778a8f7b1c");
    public static readonly Guid CategoryTv = Guid.Parse("72a7a013-8bc4-4ae6-89cb-d9f19e0c9cf9");
    public static readonly Guid CategoryNotebook = Guid.Parse("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2");

    public static readonly Guid Phone1 = Guid.Parse("5b515247-f6f5-47e1-ad06-95f317a0599b");
    public static readonly Guid Phone2 = Guid.Parse("56d6294f-7b80-4a78-856a-92b141de2d1c");
    public static readonly Guid Phone3 = Guid.Parse("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a");
    public static readonly Guid Phone4 = Guid.Parse("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b");
    public static readonly Guid Phone5 = Guid.Parse("1e338b12-8aa6-438f-8832-8c7429805d59");

    public static readonly Guid Tv1 = Guid.Parse("d1ae1de1-1aa8-4650-937c-4ed882038ad7");
    public static readonly Guid Tv2 = Guid.Parse("FEAD0B48-9986-43CB-A5C0-14F91EB6DCDF");
    public static readonly Guid Tv3 = Guid.Parse("9D52DA22-00DA-4C84-AB62-0C4279A332AF");
    public static readonly Guid Tv4 = Guid.Parse("7BA99BE2-9284-4343-BC2F-8997A5A85B62");
    public static readonly Guid Tv5 = Guid.Parse("D9B7D7C8-61BB-4CEE-AEC7-D6AA4B8B6315");

    public static readonly Guid Notebook1 = Guid.Parse("A14A4839-A0C9-4570-8D03-AB0C58EEC6B2");
    public static readonly Guid Notebook2 = Guid.Parse("9B06D390-6B11-439B-AB6F-378890DA5F23");
    public static readonly Guid Notebook3 = Guid.Parse("CA1D16AF-EABE-4D47-ACB7-C6830C9BE9E9");
    public static readonly Guid Notebook4 = Guid.Parse("B8C13516-C11A-4715-9187-3AEF69BCCA24");
    public static readonly Guid Notebook5 = Guid.Parse("A9F9A957-E79F-4397-892D-5939F4AD4C1B");
    public static readonly Guid Notebook6 = Guid.Parse("3DB3B3E6-E061-49AA-8D12-DB3D664F9CF7");
    public static readonly Guid Notebook7 = Guid.Parse("2E0D8B6B-B1AC-4534-9B10-0F1E11D91EBF");
    public static readonly Guid Notebook8 = Guid.Parse("9FFE5287-D8F5-43E5-A01B-B8829D5550FB");
    public static readonly Guid Notebook9 = Guid.Parse("2947D95E-6C93-4C8B-8E8A-061729FCCAE5");

    public static readonly Guid Notebook10 = Guid.Parse("B1DCEB3B-3D3A-484B-AC06-BC7049C9B7E9");
    public static readonly Guid Notebook11 = Guid.Parse("6F7C6E33-6B63-4FAA-A4E1-9B27F25B8A08");
    public static readonly Guid Notebook12 = Guid.Parse("B1E14AC9-9EDA-4D8C-A3C3-738056E2D7A1");
    public static readonly Guid Notebook13 = Guid.Parse("5AA5B7E5-918D-4C4D-8D87-F43F2E8031A3");
    public static readonly Guid Notebook14 = Guid.Parse("BF44D8C7-AACD-4C0E-B1AE-14A1C6B2A9B7");
    public static readonly Guid Notebook15 = Guid.Parse("B6F2FC6B-B6AE-48D7-9AFD-8A2854B6812E");
    public static readonly Guid Notebook16 = Guid.Parse("C9A06102-CE71-49F7-A64A-E7A6C67D8A1E");
    public static readonly Guid Notebook17 = Guid.Parse("FCB2D30E-34B0-4F3F-92C9-29D7B5FC1CC8");
    public static readonly Guid Notebook18 = Guid.Parse("10E52BC0-9D9C-4C2D-84F3-1D2057D0C19F");
}