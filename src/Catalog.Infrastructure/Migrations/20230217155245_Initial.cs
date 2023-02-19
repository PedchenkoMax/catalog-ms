using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("1f5ad630-32cd-42e9-8218-e26f1d375c0c"), "https://blob.com/BrandLg.png", "Lg" },
                    { new Guid("2cd4b9a0-f70d-476d-a3cc-908da43f93c4"), "https://blob.com/BrandSamsung.png", "Samsung" },
                    { new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), "https://blob.com/BrandApple.png", "Apple" },
                    { new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), "https://blob.com/BrandLenovo.png", "Lenovo" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("5e7274ad-3132-4ad7-be36-38778a8f7b1c"), "https://blob.com/CategoryPhone.png", "Phone" },
                    { new Guid("72a7a013-8bc4-4ae6-89cb-d9f19e0c9cf9"), "https://blob.com/CategoryTv.png", "TV" },
                    { new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "https://blob.com/CategoryNotebook.png", "Notebook" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "Discount", "FullPrice", "IsActive", "Name", "Quantity" },
                values: new object[,]
                {
                    { new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Apple MacBook Pro Touch Bar 15-inch features a 15-inch Retina display, Intel Core i7 processor, and 32GB of RAM.", 250.0m, 1799.99m, true, "Apple MacBook Pro Touch Bar 15-inch", 3 },
                    { new Guid("1e338b12-8aa6-438f-8832-8c7429805d59"), new Guid("2cd4b9a0-f70d-476d-a3cc-908da43f93c4"), new Guid("5e7274ad-3132-4ad7-be36-38778a8f7b1c"), "The Samsung Galaxy Z Flip features a 6.7-inch foldable AMOLED display, Snapdragon 855+ processor, and a dual camera system.", 100.0m, 999.99m, false, "Samsung Galaxy Z Flip", 0 },
                    { new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5"), new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Lenovo ThinkPad P1 features a 15.6-inch Full HD IPS display, Intel Core i7 processor, and NVIDIA Quadro P620 graphics card.", 300.0m, 2199.99m, true, "Lenovo ThinkPad P1", 3 },
                    { new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf"), new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Lenovo IdeaPad 5 features a 14-inch Full HD IPS display, Intel Core i5 processor, and 8GB of RAM.", 100.0m, 699.99m, true, "Lenovo IdeaPad 5", 15 },
                    { new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7"), new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Lenovo Legion 5 features a 15.6-inch Full HD IPS display, AMD Ryzen 5 4600H processor, and NVIDIA GeForce GTX 1650 graphics card.", 0.0m, 999.99m, true, "Lenovo Legion 5", 10 },
                    { new Guid("56d6294f-7b80-4a78-856a-92b141de2d1c"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("5e7274ad-3132-4ad7-be36-38778a8f7b1c"), "The Apple iPhone 12 Mini features a 5.4-inch Super Retina XDR display, A14 Bionic chip, and a dual-camera system.", 0.0m, 699.99m, true, "Apple iPhone 12 Mini", 20 },
                    { new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Apple MacBook 12-inch features a 12-inch Retina Display, 7th-generation dual-core Intel Core m3 processor, and 8GB of RAM.", 50.0m, 799.99m, true, "Apple MacBook 12-inch", 20 },
                    { new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("5e7274ad-3132-4ad7-be36-38778a8f7b1c"), "The Apple iPhone 12 Pro Max features a 6.7-inch Super Retina XDR display, A14 Bionic chip, and a triple-camera system.", 0.0m, 1099.99m, true, "Apple iPhone 12 Pro Max", 15 },
                    { new Guid("6f7c6e33-6b63-4faa-a4e1-9b27f25b8a08"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Apple MacBook Air 13-inch features a 13.3-inch Retina Display, 8th-generation dual-core Intel Core i5 processor, and 8GB of RAM.", 100.0m, 999.99m, true, "Apple MacBook Air 13-inch", 10 },
                    { new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62"), new Guid("1f5ad630-32cd-42e9-8218-e26f1d375c0c"), new Guid("72a7a013-8bc4-4ae6-89cb-d9f19e0c9cf9"), "The LG OLED65CXP features a 65-inch 4K UHD OLED display, α9 Gen 3 AI Processor 4K, and Dolby Vision IQ technology.", 0.0m, 1999.99m, false, "LG OLED65CXP 65-Inch 4K UHD Smart TV", 0 },
                    { new Guid("9b06d390-6b11-439b-ab6f-378890da5f23"), new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Lenovo Ideapad 330 features a 15-inch full HD display, Intel Celeron N4000 processor, and 4GB RAM.", 199.99m, 499.99m, true, "Lenovo Ideapad 330 15-Inch Laptop", 20 },
                    { new Guid("9d52da22-00da-4c84-ab62-0c4279a332af"), new Guid("2cd4b9a0-f70d-476d-a3cc-908da43f93c4"), new Guid("72a7a013-8bc4-4ae6-89cb-d9f19e0c9cf9"), "The Samsung QN85Q900RB features an 85-inch 8K UHD display, Quantum Processor 8K, and Real Game Enhancer technology.", 399.99m, 3999.99m, true, "Samsung QN85Q900RB 85-Inch 8K UHD Smart TV", 13 },
                    { new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb"), new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Lenovo Yoga 9i features a 14-inch Full HD IPS touch screen, Intel Core i7 processor, and 16GB of RAM.", 200.0m, 1799.99m, false, "Lenovo Yoga 9i", 0 },
                    { new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2"), new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Lenovo ThinkPad X1 Carbon features a 14-inch full HD display, 8th Gen Intel Core i7 processor, and 16GB RAM.", 0.0m, 999.99m, true, "Lenovo ThinkPad X1 Carbon 14-Inch Laptop", 10 },
                    { new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b"), new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Lenovo ThinkPad X1 Yoga features a 14-inch Full HD IPS touch screen, Intel Core i7 processor, and 8GB of RAM.", 0.0m, 1599.99m, true, "Lenovo ThinkPad X1 Yoga", 20 },
                    { new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Apple MacBook Pro 16-inch features a 16-inch Retina Display, 9th-generation 6-Core Intel Core i7 processor, and AMD Radeon Pro 5300M graphics card.", 500.0m, 2499.99m, false, "Apple MacBook Pro 16-inch", 0 },
                    { new Guid("b1e14ac9-9eda-4d8c-a3c3-738056e2d7a1"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Apple MacBook Pro 15-inch features a 15.4-inch Retina Display, 9th-generation 8-Core Intel Core i9 processor, and AMD Radeon Pro 555X graphics card.", 700.0m, 2799.99m, true, "Apple MacBook Pro 15-inch", 5 },
                    { new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Apple MacBook Pro 16-inch features a 16-inch Retina display, 9th generation Intel Core i9 processor, and 32GB of RAM.", 500.0m, 2499.99m, true, "Apple MacBook Pro 16-inch", 2 },
                    { new Guid("b8c13516-c11a-4715-9187-3aef69bcca24"), new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Lenovo Yoga C930 features a 13.9-inch full HD display, 8th Gen Intel Core i7 processor, and 8GB RAM.", 100.0m, 1299.99m, true, "Lenovo Yoga C930 2-In-1 Laptop", 10 },
                    { new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Apple MacBook Pro 13-inch features a 13.3-inch Retina Display, 10th-generation quad-core Intel Core i5 processor, and Intel Iris Plus Graphics.", 250.0m, 1499.99m, true, "Apple MacBook Pro 13-inch", 8 },
                    { new Guid("c9a06102-ce71-49f7-a64a-e7a6c67d8a1e"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Apple MacBook 12-inch features a 12-inch Retina display, Intel Core m3 processor, and 8GB of RAM.", 100.0m, 799.99m, true, "Apple MacBook 12-inch", 6 },
                    { new Guid("ca1d16af-eabe-4d47-acb7-c6830c9be9e9"), new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Lenovo Legion Y540 features a 15-inch full HD display, 9th Gen Intel Core i7 processor, and 16GB RAM.", 0.0m, 1299.99m, true, "Lenovo Legion Y540 15-Inch Gaming Laptop", 15 },
                    { new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7"), new Guid("2cd4b9a0-f70d-476d-a3cc-908da43f93c4"), new Guid("72a7a013-8bc4-4ae6-89cb-d9f19e0c9cf9"), "The Samsung QN55Q70T features a 55-inch 4K UHD display, Quantum Processor 4K, and Real Game Enhancer technology.", 99.99m, 799.99m, true, "Samsung QN55Q70T 55-Inch 4K UHD Smart TV", 10 },
                    { new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315"), new Guid("1f5ad630-32cd-42e9-8218-e26f1d375c0c"), new Guid("72a7a013-8bc4-4ae6-89cb-d9f19e0c9cf9"), "The LG OLED77GX features a 77-inch 4K UHD OLED display, α9 Gen 3 AI Processor 4K, and Dolby Vision IQ technology.", 999.99m, 4999.99m, true, "LG OLED77GX 77-Inch 4K UHD Smart TV", 1 },
                    { new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a"), new Guid("2cd4b9a0-f70d-476d-a3cc-908da43f93c4"), new Guid("5e7274ad-3132-4ad7-be36-38778a8f7b1c"), "The Samsung Galaxy S21 features a 6.2-inch AMOLED display, Exynos 2100 or Snapdragon 888 processor, and a triple camera system.", 0.0m, 899.99m, true, "Samsung Galaxy S21", 20 },
                    { new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8"), new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "The Apple MacBook Pro Touch Bar features a 13.3-inch Retina display, Intel Core i5 processor, and 16GB of RAM.", 150.0m, 1299.99m, true, "Apple MacBook Pro Touch Bar", 4 },
                    { new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b"), new Guid("2cd4b9a0-f70d-476d-a3cc-908da43f93c4"), new Guid("5e7274ad-3132-4ad7-be36-38778a8f7b1c"), "The Samsung Galaxy Note 20 features a 6.7-inch AMOLED display, Snapdragon 865+ processor, and a triple camera system.", 49.990m, 799.99m, true, "Samsung Galaxy Note 20", 15 },
                    { new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf"), new Guid("2cd4b9a0-f70d-476d-a3cc-908da43f93c4"), new Guid("72a7a013-8bc4-4ae6-89cb-d9f19e0c9cf9"), "The Samsung QN82Q70T features an 82-inch 4K UHD display, Quantum Processor 4K, and Real Game Enhancer technology.", 0.0m, 2199.99m, true, "Samsung QN82Q70T 82-Inch 4K UHD Smart TV", 5 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "IsMain", "ProductId" },
                values: new object[,]
                {
                    { new Guid("00222684-7694-4b21-bfb0-9aa44ecaed9d"), "https://blob.com/Notebook6Image-1.png", true, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("00de5a93-ca5a-43da-ac3a-f383fa493330"), "https://blob.com/Tv4Image-6.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("046ac7b0-b631-4d9a-bde6-7f4c46eb7036"), "https://blob.com/Tv2Image-4.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("05d607b0-f59d-45f9-9a5c-61fb39255e10"), "https://blob.com/Phone1Image-5.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("070bf487-2795-4d96-ae5a-cf1a96168846"), "https://blob.com/Notebook7Image-4.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("0777e478-c7e9-4197-aa9a-3eb08b7ae98d"), "https://blob.com/Notebook8Image-5.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("07ff7b58-9ba6-4ece-b0a5-7b652960c9bf"), "https://blob.com/Phone4Image-11.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("084ee411-25b6-43c0-8724-9e7211e0e087"), "https://blob.com/Notebook1Image-4.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("085d261a-9d0d-46eb-b253-a01ef54defa0"), "https://blob.com/Phone4Image-12.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("0965e728-f000-4581-8f45-7790ea09c315"), "https://blob.com/Tv4Image-2.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("0ae21b2c-fc20-4390-b325-9d186bb4cd4a"), "https://blob.com/Tv3Image-1.png", true, new Guid("9d52da22-00da-4c84-ab62-0c4279a332af") },
                    { new Guid("0c5e9f1c-25c8-4202-8bfc-871864696d09"), "https://blob.com/Notebook2Image-3.png", false, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("0e470a0b-52b4-40b3-8e58-4a34675f59fb"), "https://blob.com/Phone4Image-8.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("0f0d719d-7afa-4c38-aa14-b1e1b2012622"), "https://blob.com/Notebook1Image-5.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("10417238-9a38-4950-9519-a87505915613"), "https://blob.com/Phone4Image-3.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("11c8cfea-398f-45fe-a892-5b8cf92e2eff"), "https://blob.com/Notebook5Image-6.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("13994a5a-6fe1-4068-8713-caa4c58e24fb"), "https://blob.com/Phone4Image-1.png", true, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("15824281-420d-4650-aa6d-6630e0da9555"), "https://blob.com/Notebook18Image-2.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("16f4ea6f-7768-4624-9371-25b32757e428"), "https://blob.com/Phone3Image-7.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("1b2b0d2d-c624-418e-9e77-fd361afee3e0"), "https://blob.com/Notebook5Image-2.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("1b527f1e-7401-48d8-bb55-a9c27f382859"), "https://blob.com/Notebook18Image-3.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("1ee29c53-8bdb-4311-a1f3-f2d0a9000188"), "https://blob.com/Phone4Image-2.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("217427d5-77b9-4601-b98a-51e814a0f5e8"), "https://blob.com/Notebook9Image-7.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("21d291d4-c3be-477a-82fa-c0ea0e7d6c2b"), "https://blob.com/Notebook10Image-12.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("229a7e1d-b39c-4d57-9874-2d59a7ab4172"), "https://blob.com/Notebook17Image-2.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("23514395-915a-4f1a-868b-db4714390732"), "https://blob.com/Tv4Image-5.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("28ba1250-a682-438a-92b7-32787be68761"), "https://blob.com/Tv5Image-1.png", true, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("2946f55c-9e53-42cd-b302-b5db188341c0"), "https://blob.com/Notebook15Image-7.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("2ab0e75f-8a48-44b2-987d-2306012828ba"), "https://blob.com/Notebook9Image-11.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("2b541cde-25ee-4c71-a757-c9a242107063"), "https://blob.com/Notebook9Image-3.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("2b73427b-04dc-4b1a-a573-dbdf9bcb733c"), "https://blob.com/Notebook12Image-1.png", true, new Guid("b1e14ac9-9eda-4d8c-a3c3-738056e2d7a1") },
                    { new Guid("2d616ea9-4850-4b5b-8cdd-63e9285b99ee"), "https://blob.com/Notebook7Image-5.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("2f99a785-96aa-4af7-b1dd-b9f1412d42e5"), "https://blob.com/Notebook13Image-5.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("2fa743f1-90cc-4bd2-9357-3c057311a3e8"), "https://blob.com/Notebook10Image-14.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("2fff0265-abc3-4396-970c-18d9d904900f"), "https://blob.com/Notebook4Image-3.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("3948489a-52cb-458a-9243-7340a6612c4f"), "https://blob.com/Notebook17Image-7.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("3a7c5ab5-e8af-416d-b678-f6d609eff41a"), "https://blob.com/Tv5Image-4.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("3cd3268d-f1f1-4c20-8467-e9544f508647"), "https://blob.com/Notebook10Image-3.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("3e326d06-3b21-44fb-8294-16b90c1c57e5"), "https://blob.com/Notebook6Image-4.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("3e7469d1-a34f-4149-aac6-0ec46b53bd60"), "https://blob.com/Phone2Image-2.png", false, new Guid("56d6294f-7b80-4a78-856a-92b141de2d1c") },
                    { new Guid("3ee61abb-77c9-403b-bbfd-90806d775f89"), "https://blob.com/Tv5Image-3.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("3f9818f7-469e-44b4-ab4b-717566d1eec2"), "https://blob.com/Phone3Image-4.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("4122cae5-c60f-4e42-bbdb-b7f1c4f773e0"), "https://blob.com/Notebook14Image-1.png", true, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("42402305-b554-4f5c-8d51-ac4f4cf31d19"), "https://blob.com/Notebook4Image-7.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("428c3f1b-6747-4351-84bf-9e4d3884eb9c"), "https://blob.com/Notebook5Image-3.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("42bc36d2-cf7f-4fc7-ac00-5832ba26e9a5"), "https://blob.com/Tv1Image-1.png", true, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("42cac58b-4b7b-4418-92b2-112b971a365b"), "https://blob.com/Notebook7Image-6.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("43b061d9-7f2b-43f2-bd01-c0290f79d65b"), "https://blob.com/Notebook10Image-5.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("472e65f3-4061-47a5-a43c-1547b12291a9"), "https://blob.com/Phone3Image-10.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("47fb6a76-0756-49ea-8924-908cba434f39"), "https://blob.com/Notebook12Image-2.png", false, new Guid("b1e14ac9-9eda-4d8c-a3c3-738056e2d7a1") },
                    { new Guid("482befa9-d2ca-4230-ac1c-22386999536e"), "https://blob.com/Tv4Image-7.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("4860af95-b4de-46f0-ba87-dcc740f029b2"), "https://blob.com/Notebook9Image-5.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("48783352-f2bc-42ce-9a84-ceb13bbb1b01"), "https://blob.com/Notebook8Image-1.png", true, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("495ae53b-eaf8-4a64-8764-4086a22256a2"), "https://blob.com/Notebook4Image-9.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("4976ac52-588d-402b-88fb-7f3a2888d718"), "https://blob.com/Notebook17Image-6.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("4ad217a0-4f77-421e-8bef-069feb623b90"), "https://blob.com/Notebook7Image-2.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("4b4dec85-e260-41b6-b16e-61ab943e98f5"), "https://blob.com/Notebook4Image-1.png", true, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("4b847747-6132-4e02-a312-cf90bc258274"), "https://blob.com/Phone3Image-1.png", true, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("4e1b37fe-4e71-43e6-a42a-b1cfc7cde32b"), "https://blob.com/Notebook10Image-15.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("504108bd-8f31-4e7c-9379-73ee77fbabff"), "https://blob.com/Phone3Image-5.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("5261f1bb-368c-49c3-ac70-9d2f7a2a5c3a"), "https://blob.com/Notebook9Image-6.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("532e6222-4147-45da-8f89-a3847ef392f1"), "https://blob.com/Notebook14Image-2.png", false, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("533ee759-46db-44b1-a0dd-22b42f218d00"), "https://blob.com/Phone5Image-2.png", false, new Guid("1e338b12-8aa6-438f-8832-8c7429805d59") },
                    { new Guid("548a0bb1-0ae0-44bb-aeb1-e889f649e174"), "https://blob.com/Tv5Image-2.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("55f8b7a1-d3a3-4bfe-80d4-323891669f14"), "https://blob.com/Notebook15Image-6.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("56d2f62a-6b18-4a3e-a5e4-5f6663a9ce69"), "https://blob.com/Phone4Image-14.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("57186806-86d6-49dc-9a63-3de462e3fb20"), "https://blob.com/Notebook9Image-1.png", true, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("57bdffee-0c95-4b27-9fae-9f45a625c179"), "https://blob.com/Notebook4Image-5.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("58ee67f2-74d7-4ee8-8198-f0d60b6d986a"), "https://blob.com/Notebook4Image-8.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("592be906-a5af-4115-a7c1-402f0330aae8"), "https://blob.com/Tv4Image-8.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("5ab9f4c9-540e-445f-a162-682528859873"), "https://blob.com/Notebook2Image-4.png", false, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("5ae75376-8255-4567-8738-d2f9bd2a153c"), "https://blob.com/Notebook5Image-5.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("5c88d8e5-cef4-4ef7-9b7d-a17277c96de3"), "https://blob.com/Phone1Image-3.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("5d8db396-dbbf-48ef-9219-4c1337fe2e91"), "https://blob.com/Tv4Image-4.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("5e198ac9-5de6-4906-967d-5c1cc788d45c"), "https://blob.com/Notebook17Image-1.png", true, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("5e2c76b1-0aec-4981-b8c4-4f2765960bc8"), "https://blob.com/Notebook4Image-2.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("5fda070e-5d5f-4116-a55c-fae067086e51"), "https://blob.com/Notebook17Image-3.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("6061c69c-4ae2-42a7-ad10-4175f9663699"), "https://blob.com/Notebook17Image-10.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("60730b38-a54f-470d-adfe-92d8c1d06a27"), "https://blob.com/Tv5Image-7.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("632368e4-063d-47f5-b41e-5f8ed8883c93"), "https://blob.com/Notebook10Image-13.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("63a87823-2606-4dfe-8c9a-7f005661b240"), "https://blob.com/Phone1Image-2.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("63df1887-0f22-4d80-bb5b-58e403e4303f"), "https://blob.com/Tv2Image-5.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("657f3a97-dc89-4242-a156-9d60c8f03f5e"), "https://blob.com/Notebook10Image-9.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("674573ce-82cc-48f2-af41-9b0ecb752835"), "https://blob.com/Notebook2Image-2.png", false, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("67b6b3af-80fd-46e3-8111-8e2794d4588b"), "https://blob.com/Notebook9Image-10.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("686f51e1-9b0c-477e-b0c8-feef5565d491"), "https://blob.com/Tv1Image-2.png", false, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("6be8da95-c169-41f2-8349-216a9a94a143"), "https://blob.com/Notebook8Image-11.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("6ee0eb22-4637-4d28-ac88-cba308b043e5"), "https://blob.com/Notebook4Image-11.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("6fca4753-a079-41b8-a93b-296a5c2553c2"), "https://blob.com/Phone3Image-9.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("7108ffd4-a101-473e-8a26-745adb970cfe"), "https://blob.com/Notebook17Image-11.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("7495f0d1-4677-4635-9bb8-3abe5a320612"), "https://blob.com/Notebook8Image-6.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("7522ffd5-61fd-43c8-90cc-d7d62202a32d"), "https://blob.com/Notebook18Image-1.png", true, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("765d3bea-6dba-4626-8b0f-4b59700c08b0"), "https://blob.com/Tv5Image-8.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("778e2e9c-b774-4af9-b770-8d326eef40cd"), "https://blob.com/Notebook13Image-3.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("790823e1-22d4-4e03-a11d-bf297fa37885"), "https://blob.com/Phone3Image-3.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("7d658408-eb42-4021-8cf7-98d24da0a414"), "https://blob.com/Notebook8Image-8.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("7e9b5816-9cc7-4667-95ed-b0722c3feffb"), "https://blob.com/Phone3Image-8.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("7f686b27-cdf5-40c6-aaf4-1210f106462b"), "https://blob.com/Notebook18Image-6.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("7fc44e81-abd2-4b52-acd8-d3edca886e86"), "https://blob.com/Notebook17Image-9.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("8083967b-7506-467a-9bd1-2cdc70c360fa"), "https://blob.com/Notebook9Image-9.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("832dff97-c68a-4c21-82b1-c189b83bbb5d"), "https://blob.com/Phone5Image-1.png", true, new Guid("1e338b12-8aa6-438f-8832-8c7429805d59") },
                    { new Guid("84c00050-518d-45ff-8a3e-f9a7516f3bb4"), "https://blob.com/Notebook1Image-2.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("85ae6f5a-c605-4eef-96dc-878ae70e688c"), "https://blob.com/Notebook10Image-6.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("8717085b-e508-4f0e-8368-4ec29dd83d8f"), "https://blob.com/Notebook13Image-2.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("885ea514-5137-4b3f-88ca-8cc9fd9422b2"), "https://blob.com/Phone2Image-1.png", true, new Guid("56d6294f-7b80-4a78-856a-92b141de2d1c") },
                    { new Guid("8af5ea2a-c57c-4944-ad4d-462aa095195d"), "https://blob.com/Notebook15Image-1.png", true, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("8d7e32f1-414d-4855-b0fb-41bd7c3a6dd3"), "https://blob.com/Notebook3Image-2.png", false, new Guid("ca1d16af-eabe-4d47-acb7-c6830c9be9e9") },
                    { new Guid("8da21553-b05f-4819-a229-4ce53b515cb1"), "https://blob.com/Notebook17Image-4.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("8e2a3d74-84db-41d2-b526-1cac9d6e15b8"), "https://blob.com/Tv2Image-3.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("8eb65a40-bf15-4d01-af41-e42a9b6da2df"), "https://blob.com/Notebook10Image-2.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("90b07a02-b8ec-41b8-8158-11e60bb2e9d4"), "https://blob.com/Phone4Image-15.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("91c55121-152e-483a-922e-08a436da5b3c"), "https://blob.com/Notebook8Image-4.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("92323c0a-9854-488a-8281-3126ea71b040"), "https://blob.com/Phone4Image-5.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("9365abdd-c0df-49c3-897a-fe60ffcb7828"), "https://blob.com/Notebook6Image-2.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("95c71d8d-d2f2-42ac-a942-37510d624cd4"), "https://blob.com/Notebook18Image-5.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("98bf68c3-e4ed-48a6-b34b-f46cc0ba436b"), "https://blob.com/Tv4Image-3.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("99564373-61c6-4de3-af24-777746fd6b55"), "https://blob.com/Phone4Image-4.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("995be10a-1b6f-48e9-99a1-80d8d9a99525"), "https://blob.com/Phone3Image-11.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("99dbc44f-ac39-48ac-8143-7ad83f7e359e"), "https://blob.com/Notebook14Image-3.png", false, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("9b20a969-fff8-4acd-8389-50f1aa065ead"), "https://blob.com/Phone4Image-9.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("9b862059-2bf0-4a85-9367-56f5e81d9731"), "https://blob.com/Notebook5Image-1.png", true, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("9e5c6061-204b-4ecf-a365-e04b4022aecf"), "https://blob.com/Notebook15Image-4.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("9e7baec1-d7c6-4cdc-a38f-6c171b84d090"), "https://blob.com/Notebook16Image-1.png", true, new Guid("c9a06102-ce71-49f7-a64a-e7a6c67d8a1e") },
                    { new Guid("9ee5c809-1c00-484f-a2ec-547186c528ca"), "https://blob.com/Notebook7Image-7.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("9f86e0c5-a992-4da3-83b4-75f8253d0db9"), "https://blob.com/Phone1Image-4.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("a4d1df73-9a2c-462b-b385-bcac8ed66f9e"), "https://blob.com/Tv1Image-4.png", false, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("a5289601-41e4-44bd-81cf-2a7012bb2294"), "https://blob.com/Notebook14Image-4.png", false, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("a575917b-dfd3-4df3-ba32-454a36c097cd"), "https://blob.com/Notebook7Image-3.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("a5a8b073-428b-4dac-b5b7-b4d765e37593"), "https://blob.com/Notebook3Image-1.png", true, new Guid("ca1d16af-eabe-4d47-acb7-c6830c9be9e9") },
                    { new Guid("a6e72d41-f9a4-46f8-b19b-c9399474521b"), "https://blob.com/Notebook4Image-12.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("a74aa5e5-7b65-48c0-a42f-af8e21946ace"), "https://blob.com/Notebook8Image-12.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("aa46f21d-69e1-46f3-80a2-dded68d48d69"), "https://blob.com/Tv5Image-6.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("b071e47f-4f06-4a9f-b301-2d17ff5692e4"), "https://blob.com/Notebook7Image-8.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("b25afcfb-6666-446a-b979-0e924525d9f0"), "https://blob.com/Notebook4Image-4.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("b540f3ef-a5f3-45f6-9970-a7859d9b7cbb"), "https://blob.com/Phone4Image-6.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("b574a40c-8404-4178-ad6b-d13636403916"), "https://blob.com/Notebook9Image-8.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("b57eb1b5-2ed4-4a05-865f-e8f178d996a6"), "https://blob.com/Notebook2Image-1.png", true, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("b702d185-909a-4d58-bbc2-a710ae14db97"), "https://blob.com/Phone1Image-1.png", true, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("b933f3ce-aa7b-42ec-865e-c9fe4a8bba61"), "https://blob.com/Notebook15Image-5.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("b94341a8-9304-4b53-ba9c-66c997d98d0c"), "https://blob.com/Notebook6Image-7.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("ba335933-59a9-4cd3-8270-5d0b2fe0f5dc"), "https://blob.com/Phone4Image-7.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("bc5eaf98-0456-4f5f-9958-7e969cfd7223"), "https://blob.com/Phone3Image-6.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("bd137bdb-8e64-4f02-8cba-63a8c7f5974e"), "https://blob.com/Notebook4Image-10.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("c09a55fe-715e-4ba1-b12e-e7ba9438b241"), "https://blob.com/Notebook1Image-1.png", true, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("c0b0559e-1e2a-44e2-8c02-455db889df18"), "https://blob.com/Notebook10Image-7.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("c272e56a-0e16-4e45-8f83-0b09a0662008"), "https://blob.com/Notebook10Image-11.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("c4487299-5478-417c-9170-e8f76a7317c2"), "https://blob.com/Notebook13Image-1.png", true, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("c6888aab-4499-4528-a521-fb70cbf92a47"), "https://blob.com/Notebook11Image-1.png", true, new Guid("6f7c6e33-6b63-4faa-a4e1-9b27f25b8a08") },
                    { new Guid("c7e8053d-daa3-4e2e-818a-47137076c76f"), "https://blob.com/Notebook4Image-6.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("c999add8-e57b-485a-a3cd-cb1250794f08"), "https://blob.com/Tv1Image-3.png", false, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("ca3fb685-76d9-4e2f-b7e9-09535c37075c"), "https://blob.com/Phone3Image-12.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("cb17b250-ae10-4cd3-86b4-6959d1dd529d"), "https://blob.com/Notebook1Image-3.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("cb40aa7d-86de-42cb-94ad-b22f01ab3640"), "https://blob.com/Notebook9Image-4.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("cb693704-c544-418c-8c07-3b3429f0d53a"), "https://blob.com/Notebook9Image-13.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("cc4a2392-2294-467c-b28e-ae562649e856"), "https://blob.com/Notebook15Image-3.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("cdc110a2-33a1-478f-83cd-f5f256e4c456"), "https://blob.com/Tv5Image-5.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("cdc6cc20-fc65-43d0-94d6-b804707ae1e3"), "https://blob.com/Notebook6Image-6.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("ce4a0a30-8c05-44fe-92c3-7b41abd918d5"), "https://blob.com/Tv2Image-1.png", true, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("ce93543f-055f-46f4-8dad-151975316434"), "https://blob.com/Notebook17Image-8.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("d139ee3f-913a-41aa-8968-ba2152222dd0"), "https://blob.com/Notebook5Image-4.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("d2033544-38f1-4890-85b8-6690c3b61f1b"), "https://blob.com/Notebook13Image-4.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("d42240c6-b58e-41fd-accf-3867423269d8"), "https://blob.com/Notebook7Image-1.png", true, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("d737398f-0389-4d50-a9ce-d255e5a543ce"), "https://blob.com/Notebook9Image-2.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("d8211e3e-09e6-46c4-8fdd-c63c4f912b32"), "https://blob.com/Tv2Image-6.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("d848ee7b-458f-4ec7-9c05-750770eba112"), "https://blob.com/Notebook17Image-12.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("d8cde4e2-05d9-46fe-bd9b-edaceedda9e5"), "https://blob.com/Notebook8Image-9.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("d97e26b8-d059-4a1d-b5f8-872dbc132bfb"), "https://blob.com/Phone5Image-3.png", false, new Guid("1e338b12-8aa6-438f-8832-8c7429805d59") },
                    { new Guid("d999ee44-e2a1-48fd-a6d1-671b5327b432"), "https://blob.com/Notebook10Image-4.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("dd361f4a-30af-48e4-8614-d939eea5d5d6"), "https://blob.com/Notebook8Image-2.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("de1f9034-b898-4132-ae3f-b3fb98b53c01"), "https://blob.com/Notebook8Image-7.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("de85ba4b-0344-4330-9dbc-8dc3fb8228a2"), "https://blob.com/Notebook6Image-5.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("df5bb9b4-7ada-48ed-b7a4-a3916e3cc05c"), "https://blob.com/Tv4Image-9.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("e0885726-1ba1-4dcb-8bce-bdb7bd5044c0"), "https://blob.com/Notebook9Image-14.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("e0c39d2e-9ad9-4c9c-bd92-addc3ac7ec27"), "https://blob.com/Notebook10Image-10.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("e1238991-3eda-4e17-88ce-697edb9d2baa"), "https://blob.com/Notebook15Image-2.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("e17823ce-3b81-4895-8911-00a1b5612d76"), "https://blob.com/Notebook10Image-8.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("e2afffb7-fe55-40c8-8664-02503fe1417c"), "https://blob.com/Notebook10Image-1.png", true, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("e503acf1-54da-4a80-ae22-5708a768fc97"), "https://blob.com/Phone4Image-10.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("e5668e42-8d1c-40b5-b7bd-6998c0de58d4"), "https://blob.com/Notebook6Image-3.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("e6db38c0-2ba1-4f09-af72-065d2aaa96b9"), "https://blob.com/Notebook18Image-4.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("e991a20b-f5b1-4d43-93e4-6fff838a86bc"), "https://blob.com/Tv4Image-1.png", true, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("ea28383f-129d-4d12-81b7-083fd532b57e"), "https://blob.com/Notebook8Image-10.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("ed57dd49-0885-442e-adf9-cf94cd4f5772"), "https://blob.com/Notebook9Image-12.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("efa186fc-3b92-4077-967a-35334908ea4f"), "https://blob.com/Phone2Image-3.png", false, new Guid("56d6294f-7b80-4a78-856a-92b141de2d1c") },
                    { new Guid("efdff28d-5967-4619-a99d-d8af2d702632"), "https://blob.com/Notebook8Image-3.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("f6503df9-3e85-4870-b295-68cc7f54d59b"), "https://blob.com/Phone4Image-13.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("fa2c2e8e-a516-443e-9504-5c922b40f47c"), "https://blob.com/Phone3Image-2.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("fc240852-9b07-424d-84cd-7440f55240e6"), "https://blob.com/Tv2Image-2.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("ff69e2d3-77e0-40b9-b351-ef264c9f593f"), "https://blob.com/Notebook17Image-5.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
