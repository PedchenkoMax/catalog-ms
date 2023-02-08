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
                    { new Guid("0229111a-9b5a-46d1-a88f-43cefe4df139"), "https://blob.com/Notebook1Image-1.png", true, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("02736b38-7c71-4e1a-bc71-62fa433cd8f4"), "https://blob.com/Notebook14Image-3.png", false, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("03d990df-9d2a-4f93-93bb-0ef891533033"), "https://blob.com/Phone3Image-3.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("045d5fbe-9ab5-4b84-ba8d-b3051d772870"), "https://blob.com/Notebook5Image-4.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("0697e071-2830-4680-b4e4-9d247d8c8cdd"), "https://blob.com/Notebook2Image-4.png", false, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("09a7dbb4-eb72-451e-92d5-5152ac623d68"), "https://blob.com/Phone4Image-3.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("0b0f418b-a8d2-4233-87e7-5cee347a7f4c"), "https://blob.com/Phone1Image-2.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("0da393d1-f554-4e76-833f-2e0d261062d1"), "https://blob.com/Notebook9Image-7.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("0e2f1b9b-49e9-4f51-9a6e-4251b5f96853"), "https://blob.com/Notebook10Image-2.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("0e6b01bb-6d70-49e9-88f5-83f4a2ebc01e"), "https://blob.com/Tv5Image-2.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("0f551034-f20e-4295-be4e-86d21d57e742"), "https://blob.com/Notebook9Image-2.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("0f8cbb58-3919-44c0-8987-8b2de2257530"), "https://blob.com/Notebook6Image-6.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("119c51a9-b432-4eba-a43c-74a7b65bdf1f"), "https://blob.com/Notebook6Image-2.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("11e2ef8a-3f0f-4b32-8a63-38a7aea99a56"), "https://blob.com/Phone3Image-11.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("127f7955-9057-4d3d-b148-21c79e7324aa"), "https://blob.com/Tv4Image-3.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("13f28541-9d04-4773-9c4a-88211a9e09a8"), "https://blob.com/Notebook2Image-1.png", true, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("17cc2c34-b472-409f-aac7-df9d9710ab44"), "https://blob.com/Notebook7Image-3.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("19475c74-8e02-4964-adf4-70b97ccfdf8b"), "https://blob.com/Notebook18Image-5.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("19b5f731-7359-481b-bee8-b6d235a3f54b"), "https://blob.com/Phone4Image-10.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("1be9f548-4967-47c3-96de-f0345d3828bb"), "https://blob.com/Phone1Image-3.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("1dd9215c-fb9a-4a8b-b903-a8d170248cae"), "https://blob.com/Notebook10Image-11.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("2099518c-4da9-4768-b0e9-9405f7f89ea3"), "https://blob.com/Tv4Image-5.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("22359c5b-7d3a-40fe-b8e9-f1b491c45d23"), "https://blob.com/Notebook9Image-8.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("225043aa-b2c3-40a1-b5e7-980c87e51d8a"), "https://blob.com/Notebook7Image-4.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("22f4cb95-0f1a-46dc-b4dd-92da8d768cfa"), "https://blob.com/Phone5Image-3.png", false, new Guid("1e338b12-8aa6-438f-8832-8c7429805d59") },
                    { new Guid("25ff91c3-e7e9-43a6-b486-f9b5d2f6f911"), "https://blob.com/Notebook7Image-1.png", true, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("272f5f81-d6e8-4e95-8f11-147c9a6c0723"), "https://blob.com/Notebook8Image-12.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("2aec7b99-b0b9-43ca-85f9-ce826c1a6875"), "https://blob.com/Notebook9Image-6.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("2c5eb837-50f1-4080-833a-563af6bcbc1b"), "https://blob.com/Notebook8Image-10.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("2d681e9b-9549-473a-b039-2adeb93ba95e"), "https://blob.com/Notebook2Image-3.png", false, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("2fd62c39-4714-410c-b662-223e5a7a22da"), "https://blob.com/Notebook1Image-2.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("30c74d93-d286-407d-8b48-16899bca8d48"), "https://blob.com/Notebook4Image-8.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("30f53949-0439-4e8e-8bad-31c36bc4dba1"), "https://blob.com/Tv4Image-2.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("32ef7750-8d46-40af-9f33-a84a5b85c7c1"), "https://blob.com/Tv1Image-2.png", false, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("33bcc01a-40e8-4783-9f0c-6c3cac1e30c6"), "https://blob.com/Notebook9Image-3.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("33cf33a7-b03d-4d45-9c3f-eba0526ad0d9"), "https://blob.com/Tv2Image-3.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("357709d5-97a7-4941-8a90-da60edb02450"), "https://blob.com/Tv1Image-4.png", false, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("35b8314c-680c-4b0e-8665-232357b8dffe"), "https://blob.com/Notebook17Image-4.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("35c249ba-0171-4fec-a5e5-93b31a658121"), "https://blob.com/Tv1Image-3.png", false, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("36563575-8fb3-49cb-bc0d-57dbb0471cec"), "https://blob.com/Notebook12Image-2.png", false, new Guid("b1e14ac9-9eda-4d8c-a3c3-738056e2d7a1") },
                    { new Guid("3a9937a4-f801-4b77-a652-7ccc2fddcecb"), "https://blob.com/Notebook1Image-5.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("3cac40df-ca6e-43be-9fe0-4b626dee17e4"), "https://blob.com/Notebook8Image-9.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("3cf79df9-5386-4c38-8a74-e0cde4f2e086"), "https://blob.com/Notebook10Image-7.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("3f0d5342-00d0-45be-8f03-3fadc1f38ee5"), "https://blob.com/Phone3Image-1.png", true, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("438e66a3-b971-4ab5-a34a-16a279a652df"), "https://blob.com/Notebook8Image-7.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("442509f5-2b61-4509-87eb-2add7f5171fc"), "https://blob.com/Phone4Image-9.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("443a94c3-bede-4c5b-8829-9b5dfbf6bd63"), "https://blob.com/Notebook4Image-11.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("45f39b16-e921-461b-96f5-4b4cb31db433"), "https://blob.com/Notebook11Image-1.png", true, new Guid("6f7c6e33-6b63-4faa-a4e1-9b27f25b8a08") },
                    { new Guid("468c15ea-ed47-412c-9714-7da85634568f"), "https://blob.com/Notebook9Image-9.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("471e9581-540c-45d5-927e-4299210e8afd"), "https://blob.com/Tv4Image-4.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("47f18ce7-0564-4f16-9edc-a6bba3279dec"), "https://blob.com/Notebook17Image-6.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("486b86f9-0291-4967-ac5b-90a175e8d35c"), "https://blob.com/Phone1Image-1.png", true, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("48a172ef-cc02-48f4-bb12-4a402e7e851e"), "https://blob.com/Notebook5Image-6.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("4ab8ee6f-2f2e-48a9-9b04-1da5291c7c2e"), "https://blob.com/Phone4Image-8.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("4da6255a-de6e-4ddd-9894-388bc9f01824"), "https://blob.com/Notebook8Image-2.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("4e2936de-8684-4add-8405-f4b94c396e6f"), "https://blob.com/Notebook13Image-5.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("4f5a88a2-215f-4e0a-8e9a-e0ea161a20a4"), "https://blob.com/Notebook17Image-1.png", true, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("504684ce-04ac-4766-bebd-5d75899a189d"), "https://blob.com/Tv5Image-8.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("504aee3f-3640-4d9c-b154-fc77dee08df5"), "https://blob.com/Notebook4Image-5.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("508a778c-ec8a-4257-9bc1-d0cb39034bfc"), "https://blob.com/Tv5Image-4.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("510d8f56-8968-4a2e-a269-041ab6ad1784"), "https://blob.com/Phone1Image-5.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("5128b2dc-8c97-4fec-a3f4-ffe7096711d6"), "https://blob.com/Notebook3Image-2.png", false, new Guid("ca1d16af-eabe-4d47-acb7-c6830c9be9e9") },
                    { new Guid("5766584e-a8fc-43e4-bcef-8f128d136e79"), "https://blob.com/Notebook4Image-1.png", true, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("57dc43bf-5d7e-4b7d-9aec-4e307901d269"), "https://blob.com/Phone4Image-6.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("59c2f5db-b72e-4110-b869-4a67530c32de"), "https://blob.com/Notebook17Image-7.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("5a313a48-0506-4b24-acd7-2c0bac998585"), "https://blob.com/Phone3Image-8.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("5a3cfac3-84f5-4427-97f3-160521631862"), "https://blob.com/Tv5Image-7.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("5bddb73a-302d-4189-ba9a-1e2b80d0e275"), "https://blob.com/Notebook10Image-15.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("5c5044fb-074b-4dd7-adf3-26d9f710720f"), "https://blob.com/Notebook10Image-1.png", true, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("5d09be73-0073-4e89-939e-1f979b4767cb"), "https://blob.com/Phone3Image-5.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("6067caae-30de-4fdc-97cc-763e7be724f3"), "https://blob.com/Notebook17Image-5.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("60d04d3f-10a5-4b20-ac54-5dd2cfd796ac"), "https://blob.com/Notebook17Image-12.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("63c1e37b-efe7-42ed-ae5e-b76b1058b660"), "https://blob.com/Notebook8Image-6.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("64e0e5e8-77e9-444d-9576-92f3ff7ff520"), "https://blob.com/Phone5Image-1.png", true, new Guid("1e338b12-8aa6-438f-8832-8c7429805d59") },
                    { new Guid("652930a1-17e1-496e-912b-ad9ce9233cd6"), "https://blob.com/Notebook17Image-9.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("653bb8a8-30b4-4cd4-bbed-544a845ace92"), "https://blob.com/Phone3Image-12.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("67537bf6-f798-40d8-9f14-dbafe4e5174b"), "https://blob.com/Notebook9Image-10.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("69d2a300-7baf-4dc4-98c7-6e1d0330adec"), "https://blob.com/Notebook9Image-12.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("6ab530a1-7765-4514-a63c-d7311f54d1d8"), "https://blob.com/Notebook4Image-12.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("6d498f2f-63e7-4fc3-8b68-83ff5bad93b9"), "https://blob.com/Notebook15Image-4.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("6eaa817b-005d-45b5-a5df-39a8a071b345"), "https://blob.com/Notebook10Image-6.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("6ed57021-09c8-4aa3-aed9-9f2efbc01809"), "https://blob.com/Tv2Image-4.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("72a9d1be-7297-4ff6-85c4-9ed9a7dea253"), "https://blob.com/Tv4Image-9.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("743d689b-a8b4-4710-b345-a4fcc4ba0754"), "https://blob.com/Notebook10Image-10.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("7495eb00-b667-41ea-8ebf-f3df12e53be2"), "https://blob.com/Notebook4Image-4.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("768026fa-71f2-44e0-9b24-36d2a93adf7f"), "https://blob.com/Notebook15Image-7.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("77a92a27-6f8f-48fd-9cc9-90d8f344dafe"), "https://blob.com/Notebook17Image-10.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("7bcf0f6e-c498-4f13-99ae-4583844d9a44"), "https://blob.com/Notebook5Image-3.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("7c162a73-26c1-4286-a374-c51012550277"), "https://blob.com/Notebook17Image-11.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("7c58ac74-ff66-4bf9-a748-ebf31bb7f247"), "https://blob.com/Notebook15Image-1.png", true, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("7d0ccb17-ba8c-4a86-89b5-d1100da5ce6f"), "https://blob.com/Phone2Image-3.png", false, new Guid("56d6294f-7b80-4a78-856a-92b141de2d1c") },
                    { new Guid("7d83a3ac-8e39-424f-88b8-515c43149fdf"), "https://blob.com/Tv4Image-6.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("810a2d34-ecbf-491e-a2d0-ee380215ff31"), "https://blob.com/Tv4Image-7.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("8237691b-6519-4d4f-8286-d3cec42f3f76"), "https://blob.com/Phone4Image-12.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("829f5dd9-b75e-4bff-ba5a-5788ab7650f8"), "https://blob.com/Phone4Image-7.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("8371319b-36a1-4abf-a696-3876d28c635f"), "https://blob.com/Notebook10Image-12.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("83f1324e-4182-4242-b3ed-a5f0f3715d49"), "https://blob.com/Phone1Image-4.png", false, new Guid("5b515247-f6f5-47e1-ad06-95f317a0599b") },
                    { new Guid("84bde116-6eea-468e-97db-815f289ba3b2"), "https://blob.com/Notebook14Image-1.png", true, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("852623ff-8950-4d5d-a898-2064716ee935"), "https://blob.com/Tv3Image-1.png", true, new Guid("9d52da22-00da-4c84-ab62-0c4279a332af") },
                    { new Guid("8611e7b5-a7f0-409c-94e2-4ea821e7db1c"), "https://blob.com/Phone4Image-2.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("86bf0d04-efff-4c7f-bdd3-d3c5dfa9bd74"), "https://blob.com/Notebook4Image-6.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("876bf0f2-2f2e-41df-884d-10b4ec6d4e32"), "https://blob.com/Notebook10Image-13.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("87ab57b5-02ca-4f60-a023-99d10a351d89"), "https://blob.com/Phone4Image-1.png", true, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("899f058d-e6db-453c-ac3d-a4cbd61e60ee"), "https://blob.com/Notebook4Image-7.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("8a7a7ccc-89b4-4c37-8006-0408aef64e32"), "https://blob.com/Notebook7Image-5.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("8cc74c62-42d5-4a47-a7c3-94bb78f37a6b"), "https://blob.com/Phone4Image-4.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("8cf1767d-00b3-4770-8a57-7c6ec378a19b"), "https://blob.com/Notebook12Image-1.png", true, new Guid("b1e14ac9-9eda-4d8c-a3c3-738056e2d7a1") },
                    { new Guid("8d0d9740-a867-412b-9475-3bdcb0b8d5a9"), "https://blob.com/Notebook6Image-3.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("8d4104db-8ca6-4166-bfda-87c94254ebb7"), "https://blob.com/Notebook4Image-2.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("8da2f5a5-0a1b-4ad6-af09-6eaa08ab96eb"), "https://blob.com/Notebook16Image-1.png", true, new Guid("c9a06102-ce71-49f7-a64a-e7a6c67d8a1e") },
                    { new Guid("8e073eee-5a1d-4dee-a3cb-4128845fb5ba"), "https://blob.com/Notebook10Image-4.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("91a45fbd-d92b-46bb-9a40-710cba45aa76"), "https://blob.com/Tv1Image-1.png", true, new Guid("d1ae1de1-1aa8-4650-937c-4ed882038ad7") },
                    { new Guid("92e136ed-cdea-4339-a62f-8f4013258523"), "https://blob.com/Notebook8Image-1.png", true, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("9430b9ce-695b-43af-a729-bd1d4fdf95ef"), "https://blob.com/Notebook2Image-2.png", false, new Guid("9b06d390-6b11-439b-ab6f-378890da5f23") },
                    { new Guid("94db66ae-2f8c-4842-8099-e335523c5da6"), "https://blob.com/Notebook6Image-7.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("95fde400-d57a-42a9-b6f6-e23df802f964"), "https://blob.com/Notebook5Image-1.png", true, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("96e8ced5-05bd-4dfd-bf67-fc5bfdef348b"), "https://blob.com/Notebook14Image-4.png", false, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("973037fc-5d21-4531-a14e-3fd9707175cf"), "https://blob.com/Notebook8Image-4.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("97ad46c3-3453-44aa-9190-567298130cba"), "https://blob.com/Notebook4Image-3.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("980e1316-7983-4510-8be3-84c607552886"), "https://blob.com/Notebook15Image-5.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("9b432cf7-d36c-4887-8e92-d8706a57c773"), "https://blob.com/Notebook18Image-4.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("9d5714de-2bbc-4c73-95a7-b94d758af2a2"), "https://blob.com/Notebook15Image-2.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("9dfc7230-4b50-47fa-a988-0473456203ff"), "https://blob.com/Notebook17Image-8.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("9e74941a-2468-4f70-9205-e2e6ef8fc115"), "https://blob.com/Phone3Image-6.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("9ea7b08a-0fac-4280-9bb0-214b97e704da"), "https://blob.com/Notebook7Image-6.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("9f71fc3c-7e53-4658-b636-2230df68f3c9"), "https://blob.com/Phone5Image-2.png", false, new Guid("1e338b12-8aa6-438f-8832-8c7429805d59") },
                    { new Guid("a24e0bd9-205e-4485-a83c-f03c4d51ceab"), "https://blob.com/Tv2Image-5.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("a28c42bb-1d01-4003-85bb-e01fc34b6911"), "https://blob.com/Tv4Image-1.png", true, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("a28dc83d-194c-44b0-ae63-c02fba651790"), "https://blob.com/Notebook13Image-4.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("a32e64fd-2ead-4592-917d-d40bc79a8210"), "https://blob.com/Tv5Image-1.png", true, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("a482eafe-8f20-4594-bfcf-d5de46870026"), "https://blob.com/Tv2Image-1.png", true, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("a7e78037-ff86-4f22-a0ac-9ac5a489668a"), "https://blob.com/Notebook1Image-4.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("a88b209f-a700-4512-add8-f792ab918652"), "https://blob.com/Notebook8Image-5.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("aa478db5-51d7-47c8-9078-2b7229486d0d"), "https://blob.com/Notebook13Image-3.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("aa558df4-428e-4297-8f80-7d9a1565a849"), "https://blob.com/Notebook6Image-5.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("abb4203c-2421-4c8a-bce3-9f715d4eef2c"), "https://blob.com/Notebook15Image-6.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("ad456988-3c2e-4595-ac71-583460ccdb3a"), "https://blob.com/Notebook8Image-11.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("adfc77a3-15d6-4b2f-824d-10af09c09ac1"), "https://blob.com/Tv5Image-5.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("af19ea01-df28-42c5-9b98-8892edbb2cd0"), "https://blob.com/Tv2Image-6.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("af80e00e-1105-4411-8e41-bc6d8ff8ff77"), "https://blob.com/Notebook8Image-3.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("b056386d-938d-4517-85b8-9c17cbd45504"), "https://blob.com/Notebook14Image-2.png", false, new Guid("bf44d8c7-aacd-4c0e-b1ae-14a1c6b2a9b7") },
                    { new Guid("b2bde7b0-8774-49b5-b497-4d9108f5435b"), "https://blob.com/Notebook17Image-2.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("b3e81a5a-45bf-4c91-9294-293a90764717"), "https://blob.com/Phone3Image-10.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("b60ad8fd-ce46-4849-be1c-3df2a085e6d3"), "https://blob.com/Phone4Image-13.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("b7dc90df-9b53-4933-81b1-87c4119568a9"), "https://blob.com/Notebook7Image-8.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("baac1c9c-da44-40fd-939a-6091546461de"), "https://blob.com/Tv5Image-6.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("baf63f0a-b0cb-435d-849b-7a644fc56577"), "https://blob.com/Notebook13Image-1.png", true, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("bc435141-a012-4c87-9b57-7a569af00bfb"), "https://blob.com/Notebook9Image-13.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("bc4c2f95-58de-4fe9-991d-fb09773ad8f0"), "https://blob.com/Notebook7Image-2.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("be3cad7e-f035-456e-9f1e-00a511b79e07"), "https://blob.com/Tv2Image-2.png", false, new Guid("fead0b48-9986-43cb-a5c0-14f91eb6dcdf") },
                    { new Guid("be89d563-43ac-4ad4-8577-40273b6c8578"), "https://blob.com/Notebook13Image-2.png", false, new Guid("5aa5b7e5-918d-4c4d-8d87-f43f2e8031a3") },
                    { new Guid("c1dc83af-1856-4d09-bd64-ba15e10aa159"), "https://blob.com/Notebook1Image-3.png", false, new Guid("a14a4839-a0c9-4570-8d03-ab0c58eec6b2") },
                    { new Guid("c32558d9-2937-48fc-b777-df216b431b53"), "https://blob.com/Notebook7Image-7.png", false, new Guid("2e0d8b6b-b1ac-4534-9b10-0f1e11d91ebf") },
                    { new Guid("c408911c-497a-4cd7-91da-3c90225753ad"), "https://blob.com/Notebook9Image-1.png", true, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("c65be84d-a999-4015-b691-14204342b9bc"), "https://blob.com/Phone3Image-7.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("c6b83980-d386-4b05-b7eb-6fea347ffb89"), "https://blob.com/Phone3Image-9.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("ca0e380f-c4c5-431d-b228-1031b5d0ab00"), "https://blob.com/Notebook6Image-4.png", false, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("cb59a8d8-40e9-4682-9299-f50e1b27e137"), "https://blob.com/Phone2Image-2.png", false, new Guid("56d6294f-7b80-4a78-856a-92b141de2d1c") },
                    { new Guid("cce5553a-cc4e-4be3-869f-893d96d1d3be"), "https://blob.com/Phone3Image-2.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("ce370bb5-b85e-43bb-8e6e-6003bb10a99e"), "https://blob.com/Notebook3Image-1.png", true, new Guid("ca1d16af-eabe-4d47-acb7-c6830c9be9e9") },
                    { new Guid("d0a8bc88-9259-4b65-a1c0-74b4310ee806"), "https://blob.com/Notebook10Image-5.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("d1c08ee1-b529-4e7d-b66c-929ec3b15bb9"), "https://blob.com/Notebook4Image-9.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") },
                    { new Guid("d23ba654-3452-4677-a3d1-67a95673532e"), "https://blob.com/Notebook9Image-5.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("d5dd2088-eea5-471f-aedf-edc11ff838ca"), "https://blob.com/Phone4Image-11.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("d5e4eb43-a9e6-4b39-abd6-7fb621e57d0a"), "https://blob.com/Phone4Image-5.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("d8817af3-b100-4648-a01d-684bd38f29ad"), "https://blob.com/Notebook18Image-6.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("daab08c3-315d-4827-b86d-4a9ac318e5f2"), "https://blob.com/Notebook18Image-2.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("dbba7597-f742-4bc2-9953-19cded88d2a6"), "https://blob.com/Notebook18Image-3.png", false, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("dbec7d22-6a4a-467a-828d-b3b525a7d45a"), "https://blob.com/Notebook17Image-3.png", false, new Guid("fcb2d30e-34b0-4f3f-92c9-29d7b5fc1cc8") },
                    { new Guid("dc883c3c-b6ab-4e48-b1e8-e88c961356ba"), "https://blob.com/Notebook8Image-8.png", false, new Guid("9ffe5287-d8f5-43e5-a01b-b8829d5550fb") },
                    { new Guid("dda593d7-2705-4bb2-8117-b227f89c8392"), "https://blob.com/Phone4Image-14.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("ddd5a27c-7741-4032-b78a-094ef46fb3ca"), "https://blob.com/Notebook9Image-11.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("ddf07f61-b43a-4884-a937-540eb05b0f6f"), "https://blob.com/Notebook6Image-1.png", true, new Guid("3db3b3e6-e061-49aa-8d12-db3d664f9cf7") },
                    { new Guid("e175775a-b961-4a17-a07c-b0db806241f3"), "https://blob.com/Notebook10Image-14.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("e2471b39-5e6f-4558-8743-7442b8e15969"), "https://blob.com/Notebook9Image-14.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("e59a46b8-d2fd-4641-99c1-f09784a4de0d"), "https://blob.com/Notebook15Image-3.png", false, new Guid("b6f2fc6b-b6ae-48d7-9afd-8a2854b6812e") },
                    { new Guid("e8b729d5-bef9-4028-872c-a3ce610295ed"), "https://blob.com/Notebook5Image-2.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("eb3ceb75-85dd-43e5-a6cc-45e9cfe2bfde"), "https://blob.com/Notebook10Image-3.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("ed951fdc-2b2d-4a3e-9726-57a9de6b2caa"), "https://blob.com/Phone4Image-15.png", false, new Guid("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b") },
                    { new Guid("efe55075-bc9f-47e2-b14b-b3fbfb67095d"), "https://blob.com/Notebook10Image-9.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("f18b17f8-1ac8-41d1-afb9-e3be47ab8aa9"), "https://blob.com/Notebook10Image-8.png", false, new Guid("b1dceb3b-3d3a-484b-ac06-bc7049c9b7e9") },
                    { new Guid("f8a93477-f2dd-49c1-a7ff-b5c3277c2cd6"), "https://blob.com/Phone3Image-4.png", false, new Guid("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a") },
                    { new Guid("f96afd91-74a1-4527-be3a-ef340f2367fd"), "https://blob.com/Notebook9Image-4.png", false, new Guid("2947d95e-6c93-4c8b-8e8a-061729fccae5") },
                    { new Guid("f970cd16-6fae-409f-86c5-03ef2ba6f9e0"), "https://blob.com/Notebook18Image-1.png", true, new Guid("10e52bc0-9d9c-4c2d-84f3-1d2057d0c19f") },
                    { new Guid("fa266b0c-4d8a-4322-a149-ffbe308f3684"), "https://blob.com/Tv4Image-8.png", false, new Guid("7ba99be2-9284-4343-bc2f-8997a5a85b62") },
                    { new Guid("fa80037d-ef68-45dd-a400-3531c287ac7e"), "https://blob.com/Tv5Image-3.png", false, new Guid("d9b7d7c8-61bb-4cee-aec7-d6aa4b8b6315") },
                    { new Guid("fb51452f-184a-4ac2-9ba5-766cbf1faa3c"), "https://blob.com/Phone2Image-1.png", true, new Guid("56d6294f-7b80-4a78-856a-92b141de2d1c") },
                    { new Guid("fb5df156-885d-48d7-ad82-5016e435c615"), "https://blob.com/Notebook5Image-5.png", false, new Guid("a9f9a957-e79f-4397-892d-5939f4ad4c1b") },
                    { new Guid("fd6b47c9-d081-4c8f-8d96-eca8b8eb81c8"), "https://blob.com/Notebook4Image-10.png", false, new Guid("b8c13516-c11a-4715-9187-3aef69bcca24") }
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
