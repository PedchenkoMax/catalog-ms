using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("1f5ad630-32cd-42e9-8218-e26f1d375c0c"), "https://blob.com/BrandLg.png", "Lg" },
                    { new Guid("2cd4b9a0-f70d-476d-a3cc-908da43f93c4"), "https://blob.com/BrandSamsung.png", "Samsung" },
                    { new Guid("d942706b-e4e2-48f9-bbdc-b022816471f0"), "https://blob.com/BrandApple.png", "Apple" },
                    { new Guid("fd4fb70d-efdd-4d25-8a02-1c0438cb4eb2"), "https://blob.com/BrandLenovo.png", "Lenovo" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("5e7274ad-3132-4ad7-be36-38778a8f7b1c"), "https://blob.com/CategoryPhone.png", "Phone" },
                    { new Guid("72a7a013-8bc4-4ae6-89cb-d9f19e0c9cf9"), "https://blob.com/CategoryTv.png", "TV" },
                    { new Guid("ee6fe0c6-fc9c-46cf-93e9-f2e0d26676c2"), "https://blob.com/CategoryNotebook.png", "Notebook" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BrandId", "CategoryId", "Description", "Discount", "FullPrice", "IsActive", "Name", "Quantity" },
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
                columns: new[] { "ProductImageId", "ImageUrl", "IsMain", "ProductId" },
                values: new object[,]
                {
                    { new Guid("00111df3-416c-4d65-8107-c462ef520ce9"), "https://blob.com/Tv2Image-3.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("027681b1-6e3e-4889-98f5-26e382acbba3"), "https://blob.com/Notebook18Image-1.png", true, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("029db6ee-d8bd-43e0-953c-cc369f61c33b"), "https://blob.com/Notebook8Image-1.png", true, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("05198a38-c7f5-4b08-a03f-973528792bb2"), "https://blob.com/Notebook18Image-5.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("0645f6f8-1940-4598-b533-cf8d2aba8f98"), "https://blob.com/Phone3Image-2.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("0689efb3-7509-4736-9e47-54b522e2e162"), "https://blob.com/Notebook10Image-10.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("06cbad32-71a5-49f9-961a-1bbd8aebacd1"), "https://blob.com/Notebook14Image-4.png", false, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("0b8a1f78-ee9c-4a3a-b278-bed38a96735f"), "https://blob.com/Tv5Image-3.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("0bd46944-f55b-4613-be80-7a5188ea735c"), "https://blob.com/Notebook16Image-1.png", true, new Guid("c9a06102-ce71-49f7-a64a-e7a6c67d8a1e") },
                    { new Guid("0d8dc41a-6f68-4a2e-a515-ff1a8b0210c0"), "https://blob.com/Tv4Image-5.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("0d9f1fdd-57c8-4981-a024-b641ae5fbce5"), "https://blob.com/Tv1Image-4.png", false, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("100fc105-ea0c-4043-9a7a-e335c3d76250"), "https://blob.com/Phone5Image-3.png", false, new Guid("1e338b12-8aa6-438f-8832-8c7429805d59") },
                    { new Guid("108e24ce-4e87-484e-9a49-87a87df8860d"), "https://blob.com/Notebook1Image-2.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("10c01693-f655-40d3-aa2f-8172113e8057"), "https://blob.com/Tv2Image-1.png", true, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("12e3e26a-2503-47d7-9840-8e90453f27fd"), "https://blob.com/Phone4Image-14.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("14c687cd-3a2f-4247-9a83-8ad7e676a6a4"), "https://blob.com/Notebook8Image-10.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("1576076a-debc-4c76-a3d0-617888486a9f"), "https://blob.com/Notebook15Image-7.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("15f3fbe9-50c6-460b-92e7-9d41a34506e1"), "https://blob.com/Notebook4Image-3.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("160f8b0f-45a6-4310-b67c-e76efb9d3efe"), "https://blob.com/Notebook17Image-8.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("1a4537ea-c003-4ee9-bc16-32b2094ece69"), "https://blob.com/Notebook13Image-5.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("1e0bc8b2-6a6c-4c0e-93fa-cf5cb2404c14"), "https://blob.com/Tv4Image-4.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("1ef95192-5651-45fa-b6cd-c378625a33e2"), "https://blob.com/Notebook15Image-2.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("216121ef-4cb4-481a-b810-bd073d1b5811"), "https://blob.com/Notebook10Image-13.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("2547bfaf-9cb4-4c53-8189-194bc2553f8f"), "https://blob.com/Notebook6Image-1.png", true, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("2682489e-a9ae-4807-a850-f22aa53ff895"), "https://blob.com/Notebook5Image-3.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("26d9d6f7-fda4-450b-91de-30d0732d3a42"), "https://blob.com/Phone2Image-3.png", false, new Guid("56d6294f-7b80-4a78-856a-92b141de2d1c") },
                    { new Guid("2946fe4f-d222-4a47-9e43-664679f5e454"), "https://blob.com/Tv5Image-5.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("2a10aab4-66de-41c0-8edb-38f69226239d"), "https://blob.com/Notebook10Image-11.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("2b76e482-23b1-48de-9c1a-25bb1dabb207"), "https://blob.com/Tv5Image-2.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("2ba326de-eb92-4a4e-af02-35bd0c0e0856"), "https://blob.com/Notebook9Image-7.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("2c1a4198-6809-4822-88d9-edd5ac7c11d9"), "https://blob.com/Notebook14Image-3.png", false, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("2e17f276-a649-4db5-a076-7e3569865279"), "https://blob.com/Notebook5Image-5.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("2f0da35a-8983-4acc-9ebd-78a2d3b03a4b"), "https://blob.com/Notebook1Image-5.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("2ff720d8-d96e-4d4c-8e18-cd5f58b653d2"), "https://blob.com/Notebook2Image-2.png", false, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("314ca60e-7bbb-45a8-90b5-397b730c436a"), "https://blob.com/Tv3Image-1.png", true, new Guid("9d52da22-00da-4c84-ab62-0c4279a332af") },
                    { new Guid("33d71ee9-ce40-49dd-83b2-12b9e6bde95c"), "https://blob.com/Phone4Image-5.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("373625f6-d62b-4433-81d0-79599d28765c"), "https://blob.com/Notebook4Image-6.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("39840fe0-639b-4f69-88c0-58c191850e3a"), "https://blob.com/Notebook13Image-4.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("3a59c190-55d2-47c5-8f7c-afbb693b57a7"), "https://blob.com/Notebook9Image-12.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("3e868d72-0e23-4069-8009-35f3fdad04c8"), "https://blob.com/Notebook18Image-4.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("407efa56-b8a6-4eaf-accb-e5def413cfb0"), "https://blob.com/Phone3Image-8.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("409dcbf9-b2d9-43ae-8be6-8cda3fe8e447"), "https://blob.com/Notebook5Image-2.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("41184023-31c6-45c0-befc-6c00afcf7986"), "https://blob.com/Notebook13Image-2.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("428222a8-6b03-4df0-b2fc-4d338193d041"), "https://blob.com/Phone5Image-2.png", false, new Guid("1e338b12-8aa6-438f-8832-8c7429805d59") },
                    { new Guid("42c94b65-e4f7-4261-8835-eef6d53bb77e"), "https://blob.com/Notebook13Image-1.png", true, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("430e98ea-022e-4a97-a679-82194b5ebc83"), "https://blob.com/Tv4Image-9.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("46ae3bbc-116f-46e6-9071-66fcb8730003"), "https://blob.com/Notebook1Image-4.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("483fa116-b715-406a-9048-46eba05f9bef"), "https://blob.com/Tv2Image-4.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("4a1f4bb5-fa90-4558-98d7-d6442cbb0380"), "https://blob.com/Notebook6Image-3.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("4ad4fc29-c454-4603-9bd0-85831485f6a6"), "https://blob.com/Phone4Image-9.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("4c0e09fb-40f6-4cea-b6b8-8aeef7452ff2"), "https://blob.com/Phone1Image-1.png", true, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("4ca5bbcd-b9c9-4fea-903a-143156c56eff"), "https://blob.com/Notebook2Image-4.png", false, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("4d10da91-83ce-4070-bdfe-40104d6cc24c"), "https://blob.com/Notebook1Image-1.png", true, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("5099b62d-0b6b-429a-bc04-5ff70448f080"), "https://blob.com/Tv4Image-8.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("541ce01d-da5d-416b-98bf-5d22bdfa3eb2"), "https://blob.com/Notebook9Image-13.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("55b63011-aa85-4e0a-a177-ef9c175b01ff"), "https://blob.com/Notebook6Image-6.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("560b1adc-0b3a-4931-add3-c2d4ac3858e6"), "https://blob.com/Notebook14Image-2.png", false, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("57038238-5059-41ad-9216-3efc5243ef97"), "https://blob.com/Tv4Image-1.png", true, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("584f1734-afdb-4ac4-b450-3dc43556303a"), "https://blob.com/Notebook17Image-7.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("59d5486d-d8e2-4255-ba06-a93cbb426f32"), "https://blob.com/Phone5Image-1.png", true, new Guid("1e338b12-8aa6-438f-8832-8c7429805d59") },
                    { new Guid("5a72322a-0450-4a07-baf5-20a3de4e6e49"), "https://blob.com/Notebook11Image-1.png", true, new Guid("6f7c6e33-6b63-4faa-a4e1-9b27f25b8a08") },
                    { new Guid("5c507e4f-79dd-4125-a232-033d085d9a88"), "https://blob.com/Phone3Image-6.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("5ce0ff68-efa0-466f-8857-9eb28e597c1a"), "https://blob.com/Notebook5Image-4.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("5e2ceb5b-2bb2-4512-a113-6d9b4b85a20d"), "https://blob.com/Notebook9Image-4.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("5f2c0c10-b02b-4c0e-aa61-a304db7c49ef"), "https://blob.com/Notebook10Image-7.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("611c6dda-153c-477f-9dca-a9cafc94d443"), "https://blob.com/Notebook18Image-3.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("6227ccec-6d86-4458-92a0-5b7cd8e33e33"), "https://blob.com/Notebook6Image-7.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("67ee666b-187c-42ea-aa5f-27c6a15a498d"), "https://blob.com/Tv1Image-1.png", true, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("68d9457c-6723-4bbf-8e49-8dd777d2c1c2"), "https://blob.com/Phone4Image-6.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("69c70675-043d-4da4-a333-8f94a69c1d50"), "https://blob.com/Notebook17Image-4.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("69da90cd-af69-4532-a168-1d380cf061dc"), "https://blob.com/Tv2Image-6.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("6a8ad94c-64d5-4b9b-a248-ec9f7154b2fe"), "https://blob.com/Notebook4Image-4.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("6be09ef4-eb11-4e08-aa24-3569db287e9e"), "https://blob.com/Phone4Image-7.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("6f334700-1f17-4d1f-a442-124b5562ddc8"), "https://blob.com/Tv1Image-2.png", false, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("72b15767-0be8-47a8-8096-21f4adc0a583"), "https://blob.com/Phone4Image-15.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("73339b06-6dea-4074-8985-41bf4cdcf1fb"), "https://blob.com/Notebook10Image-6.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("74cf4dbd-9d85-4e7e-abba-d29841a94334"), "https://blob.com/Notebook2Image-3.png", false, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("76d2ee7c-7d57-46cd-8e48-36ff9ec06f10"), "https://blob.com/Notebook3Image-1.png", true, new Guid("ca1d16af-eabe-4d47-acb7-c6830c9be9e9") },
                    { new Guid("788b1d6a-7e12-4b1f-9dc1-facd0e1d63b2"), "https://blob.com/Tv4Image-2.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("7ba343f0-6b6b-46a9-9c5f-5748f51b2465"), "https://blob.com/Notebook7Image-2.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("7cc3d852-f385-4890-b620-5ca106c5d490"), "https://blob.com/Notebook9Image-9.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("7de25952-06d8-484e-ac35-e1c3771e27b6"), "https://blob.com/Notebook9Image-6.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("7ed963d2-9260-48cf-bc1b-fd5eb3ee1f0d"), "https://blob.com/Notebook15Image-3.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("7f54920d-391d-4cc0-84d3-ef15675d4c1b"), "https://blob.com/Phone1Image-2.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("7fd13cf8-6cdf-4b41-9437-6bce5d9eac15"), "https://blob.com/Notebook17Image-2.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("800b2089-1fc1-466a-985c-f832e7989eb3"), "https://blob.com/Notebook10Image-1.png", true, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("8020a2f7-c2a9-4623-ae32-94fd5465088f"), "https://blob.com/Notebook10Image-12.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("83218b0c-947b-4efe-8b29-52588e262726"), "https://blob.com/Notebook18Image-2.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("892bcf2c-5059-466c-ad65-0d6073ecfd52"), "https://blob.com/Notebook15Image-6.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("8ab0ed37-ef47-4304-9d00-9bbd521d5c2d"), "https://blob.com/Phone3Image-11.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("8c4d05ce-8d60-4daf-bc94-124c611a849c"), "https://blob.com/Notebook10Image-3.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("8e4d5333-ae4f-47f5-8046-866b027025a6"), "https://blob.com/Notebook10Image-2.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("8eeaff3c-79e4-461a-9c50-6450e53d1b4a"), "https://blob.com/Notebook9Image-5.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("91019024-8242-4177-a16a-fe00184261d5"), "https://blob.com/Notebook4Image-7.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("92540d3c-eb08-40ea-a414-f9a1f344dde5"), "https://blob.com/Notebook8Image-7.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("926d143c-ebd0-4331-aef6-33a509431b97"), "https://blob.com/Notebook8Image-8.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("93833727-f515-41c3-93b6-ec5dfa6ed915"), "https://blob.com/Notebook5Image-6.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("9496e829-2781-474d-81c6-f437f9f68a3e"), "https://blob.com/Notebook10Image-4.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("9544e8d4-fb07-4b05-9d8f-1052170352b9"), "https://blob.com/Tv5Image-8.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("9566e200-25a4-4d9d-b299-b539c1f26105"), "https://blob.com/Phone3Image-1.png", true, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("97744b9e-a64f-454e-bfba-a9ebb6b57b3f"), "https://blob.com/Notebook4Image-10.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("98443ff1-d745-44bd-8cf7-ef9f91594745"), "https://blob.com/Tv5Image-6.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("99de7a00-a4d2-4ac6-be74-7117a70502ad"), "https://blob.com/Notebook8Image-9.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("99e822cb-5ff5-4a3c-a6b4-b04e3f02e8b3"), "https://blob.com/Notebook7Image-3.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("9a8e49a5-dfa4-40ab-af78-73e86ce1ade1"), "https://blob.com/Notebook8Image-2.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("9f8a890a-ea65-44e9-af49-2c1cc3c5d0e5"), "https://blob.com/Phone4Image-1.png", true, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("9fab965a-f957-4274-bc2e-429cd8d5dc3c"), "https://blob.com/Notebook6Image-2.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("a002bf5b-1d0d-4167-84b8-f7cbe835b901"), "https://blob.com/Tv4Image-6.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("a0fa2d7b-4343-4757-8689-ba2b47c0b7a5"), "https://blob.com/Notebook18Image-6.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("a1089ab4-2526-44fc-a447-2a7fd198af69"), "https://blob.com/Notebook17Image-1.png", true, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("a1f1b3bf-bcd6-44ae-a01e-88b75d508b27"), "https://blob.com/Notebook5Image-1.png", true, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("a3c8f454-5f09-4481-bdf6-678f256c974a"), "https://blob.com/Notebook17Image-12.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("a4a37efc-7b7f-4847-bb0a-8069460a8a69"), "https://blob.com/Notebook10Image-9.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("a4b59495-751f-48db-85cb-ecaf5a16fc70"), "https://blob.com/Tv4Image-7.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("aca8c1c7-5a6f-4f19-947d-f8f12655f354"), "https://blob.com/Phone4Image-12.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("ad85b9d4-f93c-4663-ab41-84f6fec3218e"), "https://blob.com/Tv5Image-4.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("add12709-f570-4ec2-bb92-f5e61fa77534"), "https://blob.com/Notebook8Image-5.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("ae314e6a-e262-47e0-9166-ff71de586133"), "https://blob.com/Notebook17Image-9.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("aed35b4c-d63b-46f2-a7d8-0588bb074a7a"), "https://blob.com/Phone3Image-3.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("b016cb4b-b97f-4dc0-9026-55c5effa09b4"), "https://blob.com/Phone4Image-13.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("b156bb22-059d-4e2a-b8c3-7f3f06d538b7"), "https://blob.com/Notebook9Image-1.png", true, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("b1916bf7-81b4-43fa-a235-0cf68c44ad7f"), "https://blob.com/Phone4Image-8.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("b3d6d92d-3774-496c-a787-c9316a4dbe59"), "https://blob.com/Phone1Image-5.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("b3da7a41-31f8-4b88-b775-16b4009f57cb"), "https://blob.com/Notebook9Image-8.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("b5269867-19c6-4f9f-a481-36b6d3f8295d"), "https://blob.com/Phone2Image-1.png", true, new Guid("56d6294f-7b80-4a78-856a-92b141de2d1c") },
                    { new Guid("b61e3dae-fd12-45e2-a518-9de50664f12b"), "https://blob.com/Notebook17Image-3.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("b7a9e1d5-06d3-49cc-bc70-3a7a126287ae"), "https://blob.com/Notebook4Image-9.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("b7bbff57-5579-41c0-9fc1-6661b8185728"), "https://blob.com/Notebook10Image-14.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("b938ca6b-9153-4942-805b-8d8ccc47127b"), "https://blob.com/Notebook3Image-2.png", false, new Guid("ca1d16af-eabe-4d47-acb7-c6830c9be9e9") },
                    { new Guid("bb877cbf-ff39-4142-9887-eb21d858f3bb"), "https://blob.com/Tv2Image-5.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("bb87d810-514e-458e-9a67-719d146edcd4"), "https://blob.com/Notebook12Image-2.png", false, new Guid("b1e14ac9-9eda-4d8c-a3c3-738056e2d7a1") },
                    { new Guid("bcd3c25d-9d56-42e8-bedb-2095a4fff56e"), "https://blob.com/Phone3Image-12.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("bedfe18f-48fc-44f1-9839-7d58af1f6fa6"), "https://blob.com/Phone3Image-9.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("c05a7e19-7093-4ba9-8f00-7a4d0d7656ee"), "https://blob.com/Notebook7Image-1.png", true, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("c0b2173e-be10-4d3b-aa53-1bdf04b65ea0"), "https://blob.com/Notebook13Image-3.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("c38f48a1-c633-429d-9938-1b1ec6591d7c"), "https://blob.com/Notebook7Image-7.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("c396985c-94ed-4c97-969f-d46d7e78756a"), "https://blob.com/Notebook9Image-10.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("c3e41c4a-613c-482d-b119-bdad09aab8c3"), "https://blob.com/Notebook7Image-8.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("c4ba6d5b-d7c3-46f4-84a9-07ed642d786e"), "https://blob.com/Tv1Image-3.png", false, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("c536a9d8-b167-412d-9922-1d5e2181bd1b"), "https://blob.com/Tv5Image-1.png", true, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("c7c62b22-7d65-429d-87d5-73954bcfa64b"), "https://blob.com/Notebook2Image-1.png", true, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("c9c90fa9-36e8-48c3-904c-eaeab885b7db"), "https://blob.com/Phone1Image-4.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("cab85db6-8e45-4ac4-953a-3bcc724f3a30"), "https://blob.com/Notebook15Image-4.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("cbbab2d2-9b41-4407-91cc-4ef86c9eae29"), "https://blob.com/Phone4Image-2.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("ccc37342-9b3d-4b06-96e2-5ae7dfcb3a48"), "https://blob.com/Phone3Image-5.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("cd197da6-9ae9-4076-8d1b-424e1fcaae64"), "https://blob.com/Tv4Image-3.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("cdb3d937-946b-47ea-8750-e4a21c5764d0"), "https://blob.com/Notebook9Image-2.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("cee41e50-c381-408f-8513-ba86d33e5216"), "https://blob.com/Notebook4Image-8.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("d14aa347-3c1e-4152-9380-fae533ca2380"), "https://blob.com/Notebook15Image-5.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("d2b418a3-3e6e-48a5-a498-131f183501f1"), "https://blob.com/Phone1Image-3.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("d58d1282-526d-480d-b3df-23ce0bfbf090"), "https://blob.com/Phone4Image-4.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("d63acde8-2aec-47db-8ebc-b83adbb2ebd4"), "https://blob.com/Notebook12Image-1.png", true, new Guid("b1e14ac9-9eda-4d8c-a3c3-738056e2d7a1") },
                    { new Guid("d7ab5d9d-2cb9-4e99-86bb-0b71d657ee86"), "https://blob.com/Notebook17Image-5.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("d83a929a-284d-4d4c-bde4-d661a24d67d8"), "https://blob.com/Phone4Image-11.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("db458927-934a-4de3-a171-2e7df716c6cb"), "https://blob.com/Notebook8Image-12.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("dbb12303-2019-4de6-b300-57ca4cce65c6"), "https://blob.com/Notebook10Image-8.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("dc17f100-09e8-48ff-824c-890fce06ff5c"), "https://blob.com/Phone3Image-4.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("dd0dbc9f-b081-4183-9987-f5800fe80bb8"), "https://blob.com/Tv2Image-2.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("e1aa4554-b2b3-48fe-91e0-8bf2637d6139"), "https://blob.com/Notebook9Image-14.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("e42e829f-5fd3-4eff-b3a8-e8e5171cab1b"), "https://blob.com/Notebook15Image-1.png", true, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("e53dd825-c7cd-413c-824c-52e84a344681"), "https://blob.com/Notebook7Image-6.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("e7afe7ee-7b57-4931-8245-785f206b54a6"), "https://blob.com/Notebook4Image-1.png", true, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("e92b900f-fe26-49ed-bd70-f2c58f8ca497"), "https://blob.com/Notebook14Image-1.png", true, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("e94280e6-6a05-4b30-a042-4fae2fa98b1e"), "https://blob.com/Notebook17Image-11.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("e9d779f1-0349-4bb1-8bde-ee3c5cae1eae"), "https://blob.com/Notebook10Image-5.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("ec93d683-1a12-4e6a-bbaf-14136719078f"), "https://blob.com/Notebook4Image-5.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("edad0956-e51c-414c-81c8-cab80c3152c4"), "https://blob.com/Phone3Image-10.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("edcc65c0-9354-4dc0-a448-574bea15a144"), "https://blob.com/Phone3Image-7.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("ee6397cd-faab-42a8-b20a-abe71b2deea1"), "https://blob.com/Notebook4Image-2.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("f059e1ee-9039-44da-bc27-c160764e6b55"), "https://blob.com/Notebook9Image-11.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("f1181a68-e602-42e4-bf69-faf41f479c83"), "https://blob.com/Phone2Image-2.png", false, new Guid("56d6294f-7b80-4a78-856a-92b141de2d1c") },
                    { new Guid("f11d7228-3212-4e48-ab3e-2a692f8691ce"), "https://blob.com/Notebook1Image-3.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("f1330446-be54-449d-b752-ef49fc7ef863"), "https://blob.com/Phone4Image-10.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("f1fe637f-dfec-4eda-a688-fa876976250e"), "https://blob.com/Tv5Image-7.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("f212f1c5-7a44-4696-9037-b5a7a6fc72e1"), "https://blob.com/Notebook8Image-3.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("f21d906d-3d77-4f78-b858-3503ba6ca26b"), "https://blob.com/Notebook7Image-4.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("f2ec0a3e-9bff-4842-91c8-41d88aa69f95"), "https://blob.com/Notebook8Image-4.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("f304c929-acd2-481c-b6c4-0e4a0d618c26"), "https://blob.com/Notebook4Image-11.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("f4013afc-51ad-4f1c-9c07-2b1bc11b83ba"), "https://blob.com/Notebook17Image-10.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("f5e82eef-9eb0-4da6-8d5f-a4e598dfb2b6"), "https://blob.com/Notebook8Image-6.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("f6cc91d7-68cf-46b8-90c7-7db719cec38c"), "https://blob.com/Notebook17Image-6.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("f93af791-6064-48ad-9c11-d6b739344937"), "https://blob.com/Notebook6Image-5.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("f995373b-6762-4b8c-bcd5-7b057ad482a3"), "https://blob.com/Notebook4Image-12.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("fb18ad98-f915-43ce-8d6d-538e26f83004"), "https://blob.com/Notebook10Image-15.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("fbc326b2-e438-4f6a-b228-a2b3a20b5583"), "https://blob.com/Notebook6Image-4.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("fd75e2f8-97a2-4804-9b3f-54d345cf4eec"), "https://blob.com/Notebook8Image-11.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("fdc439be-4669-416c-ab8c-0c9a06485ba1"), "https://blob.com/Phone4Image-3.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("fee25673-ad3f-4361-ae3f-3a8cb827215d"), "https://blob.com/Notebook9Image-3.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("fee41bf3-53a6-4de5-817c-f4740126f70f"), "https://blob.com/Notebook7Image-5.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") }
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
