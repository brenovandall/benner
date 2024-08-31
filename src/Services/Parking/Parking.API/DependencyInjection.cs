using Microsoft.EntityFrameworkCore;
using Parking.API.Data;
using Parking.gRPC;

namespace Parking.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();

        services.AddDbContext<ParkingContext>(options => 
            options.UseSqlite(configuration.GetConnectionString("DB_CONNECTION")));

        var assembly = typeof(Program).Assembly;
        var validation = typeof(ValidationBehaviors<,>);
        var logging = typeof(LoggingBehavior<,>);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(validation);
            config.AddOpenBehavior(logging);
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddGrpcClient<ParkingProtoService.ParkingProtoServiceClient>(options =>
        {
            options.Address = new Uri(configuration["GrpcSettings:ParkingUrl"]!);
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            return handler;
        });

        services.AddExceptionHandler<CustomExceptionHandler>();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(options => { });

        return app;
    }
}
