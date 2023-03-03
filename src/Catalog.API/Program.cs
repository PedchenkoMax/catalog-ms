using System.Reflection;
using System.Text.Json.Serialization;
using Catalog.API;
using Catalog.Domain.Abstractions.EventBus;
using Catalog.Infrastructure.Database;
using Catalog.Infrastructure.MessageBroker;
using MassTransit;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
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
    services.ConfigureOptions<ConfigureSwaggerOptions>();
    services.AddDbContext<CatalogContext>(o => o.UseSqlServer(builder.Configuration["ConnectionString"]));
    services.AddHttpLogging(l => l.LoggingFields = HttpLoggingFields.All);

    services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        options.ReportApiVersions = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new MediaTypeApiVersionReader("ver"));
    });

    services.AddVersionedApiExplorer(setup =>
    {
        setup.GroupNameFormat = "'v'VVV";
        setup.SubstituteApiVersionInUrl = true;
    });
}

var app = builder.Build();
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
        });
    }

    app.UseHttpLogging();

    app.UseExceptionHandler("/error");

    app.UseAuthorization();

    app.MapControllers();
}

app.Run();

public partial class Program { }