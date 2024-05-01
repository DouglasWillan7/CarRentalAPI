using Dw.Rental.Consumer;
using DW.Rental.IoC;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppDependencies(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer(typeof(CreateRentalConsumer));
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

app.UseHttpsRedirection();

app.Run();