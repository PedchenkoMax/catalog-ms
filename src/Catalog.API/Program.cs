using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Catalog.API.Middlewares;
using Catalog.Domain.Abstractions.EventBus;
using Catalog.Infrastructure.Database;
using Catalog.Infrastructure.MessageBroker;
using MassTransit;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
{
    services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

    services.AddMassTransit(configurator =>
    {
        configurator.SetKebabCaseEndpointNameFormatter();

        configurator.AddConsumers(Assembly.GetExecutingAssembly());

        configurator.UsingRabbitMq((context, busConfigurator) =>
        {
            busConfigurator.ConfigureEndpoints(context);

            var host = builder.Configuration["MessageBroker:Host"];
            var username = builder.Configuration["MessageBroker:Username"];
            var password = builder.Configuration["MessageBroker:Password"];

            busConfigurator.Host(host, h =>
            {
                h.Username(username);
                h.Password(password);
            });
        });
    });
    
    services.AddTransient<IEventBus, EventBus>();

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

    app.UseExceptionHandler("/error");
    //app.ConfigureExceptionHandler();

    app.UseAuthorization();

    app.MapControllers();
}

app.Run();

public partial class Program { }