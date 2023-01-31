using Catalog.API.Middlewares;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddDbContext<CatalogContext>(o => o.UseSqlServer(builder.Configuration["ConnectionString"]));
    services.AddHttpLogging(l => l.LoggingFields = HttpLoggingFields.All);
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpLogging();

    app.ConfigureExceptionHandler();
    
    app.UseAuthorization();

    app.MapControllers();
}

app.Run();