using DW.Rental.Api.Endpoints;
using DW.Rental.IoC;
using Microsoft.OpenApi.Models;
using MassTransit;
using DW.Rental.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DW.API.Rental", Description = "Api responsavel pelo contexto de locação de Motos para entregadores", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});

builder.Services.ConfigureAppDependencies(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitSettings:Endpoint"], 5672, "/", c =>
        {
            c.Username(builder.Configuration["RabbitSettings:Username"]);
            c.Password(builder.Configuration["RabbitSettings:Password"]);
        });

        cfg.ConfigureEndpoints(ctx);
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIRental v1");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapMotoAppEndpoints();
app.MapDeliveryAppEndpoints();
app.MapLoginAppEndpoints();
app.MapRentalAppEndpoints();

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocationDbContext>();
    db.Database.Migrate();
}

app.Run();

public partial class Program { }
