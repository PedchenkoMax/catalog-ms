using Catalog.API.Middlewares;
using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddDbContext<CatalogContext>(o => o.UseSqlServer(builder.Configuration["ConnectionString"]));
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ConfigureExceptionHandler();
    
    app.UseAuthorization();

    app.MapControllers();
}

app.Run();