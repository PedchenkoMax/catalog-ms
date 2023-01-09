using Catalog.Infrastructure;
using Catalog.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string connStr = @"Server=localhost,1433;User Id=sa;Password=Str0ngPa$$w0rd;TrustServerCertificate=True";
builder.Services.AddDbContext<CatalogContext>(opt => opt.UseSqlServer(connStr));

builder.Services.AddScoped<ProductRepository>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();