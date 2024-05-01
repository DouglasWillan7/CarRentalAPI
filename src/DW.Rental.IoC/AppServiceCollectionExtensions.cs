using DW.Rental.Bucket.Configs;
using DW.Rental.Bucket.Repositories;
using DW.Rental.Data.DatabaseContext;
using DW.Rental.Data.Repositories;
using DW.Rental.Domain.Configs;
using DW.Rental.Domain.Handlers.Motorcycle.Commands;
using DW.Rental.Domain.Repositories;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DW.Rental.IoC;

public static class AppServiceCollectionExtensions
{
    public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        AddDatabase(services, configuration);
        AddRepositories(services);
        AddAuthentication(services, configuration);
        AddAuthorization(services);
        //AddMassTransit(services, configuration);

        services.AddMediatR(typeof(CreateMotorcycleCommandHandler).Assembly);

        services.Configure<BucketSettings>(configuration.GetSection("BucketSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
    }

    public static void AddAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("Deliveryman", policy => policy.RequireRole("Deliveryman"));
        });
    }

    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]!)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });
    }

    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["dbContextSettings:ConnectionString"];

        services.AddDbContext<LocationDbContext>(options => options.UseNpgsql(connectionString));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<IDeliverymanRepository, DeliverymanRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();
    }


    //public static void AddMassTransit(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddMassTransit(x =>
    //    {
    //        x.UsingRabbitMq((ctx, cfg) =>
    //        {
    //            cfg.Host(configuration["RabbitSettings:Endpoint"], 5672, "/", c =>
    //            {
    //                c.Username(configuration["RabbitSettings:Username"]);
    //                c.Password(configuration["RabbitSettings:Password"]);
    //            });

    //            cfg.ConfigureEndpoints(ctx);
    //        });
    //    });
    //}

}
